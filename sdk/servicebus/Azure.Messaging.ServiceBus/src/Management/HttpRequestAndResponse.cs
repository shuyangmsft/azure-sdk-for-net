﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using Azure.Core;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Authorization;
using Azure.Messaging.ServiceBus.Primitives;
using Azure.Core.Pipeline;
using System.Collections.Generic;
using System.Globalization;

namespace Azure.Messaging.ServiceBus.Management
{
    internal class HttpRequestAndResponse
    {
        private readonly HttpPipeline _pipeline;
        private readonly string _fullyQualifiedNamespace;
        private readonly TokenCredential _tokenCredential;
        private readonly int _port;
        private readonly ClientDiagnostics _diagnostics;

        /// <summary>
        /// Initializes a new <see cref="HttpRequestAndResponse"/> which can be used to send http request and response.
        /// </summary>
        public HttpRequestAndResponse(
            HttpPipeline pipeline,
            ClientDiagnostics diagnostics,
            TokenCredential tokenCredential,
            string fullyQualifiedNamespace)
        {
            _pipeline = pipeline;
            _diagnostics = diagnostics;
            _tokenCredential = tokenCredential;
            _fullyQualifiedNamespace = fullyQualifiedNamespace;
            _port = GetPort(_fullyQualifiedNamespace);
        }


        internal async Task ThrowIfRequestFailedAsync(Request request, Response response)
        {
            if ((response.Status >= 200) && (response.Status < 400))
            {
                return;
            }
            RequestFailedException ex = await _diagnostics.CreateRequestFailedExceptionAsync(response).ConfigureAwait(false);
            if (response.Status == (int)HttpStatusCode.Unauthorized)
            {
                throw new ServiceBusException(
                    ex.Message,
                    ServiceBusException.FailureReason.Unauthorized,
                    innerException: ex);
            }

            if (response.Status == (int)HttpStatusCode.NotFound)
            {
                throw new ServiceBusException(
                    ex.Message,
                    ServiceBusException.FailureReason.MessagingEntityNotFound,
                    innerException: ex);
            }

            if (response.Status == (int)HttpStatusCode.Conflict)
            {
                if (request.Method.Equals(RequestMethod.Delete))
                {
                    throw new ServiceBusException(true, ex.Message, innerException: ex);
                }

                if (request.Method.Equals(RequestMethod.Put) && request.Headers.TryGetValue("If-Match", out _))
                {
                    // response.RequestMessage.Headers.IfMatch.Count > 0 is true for UpdateEntity scenario
                    throw new ServiceBusException(
                        true,
                        ex.Message,
                        innerException: ex);
                }

                throw new ServiceBusException(
                    ex.Message,
                    ServiceBusException.FailureReason.MessagingEntityAlreadyExists,
                    innerException: ex);
            }

            if (response.Status == (int)HttpStatusCode.Forbidden)
            {
                if (ex.Message.Contains(ManagementClientConstants.ForbiddenInvalidOperationSubCode))
                {
                    throw new InvalidOperationException(ex.Message, ex);
                }

                throw new ServiceBusException(
                    ex.Message,
                    ServiceBusException.FailureReason.QuotaExceeded,
                    innerException: ex);
            }

            if (response.Status == (int)HttpStatusCode.BadRequest)
            {
                throw new ArgumentException(ex.Message, ex);
            }

            if (response.Status == (int)HttpStatusCode.ServiceUnavailable)
            {
                throw new ServiceBusException(
                    ex.Message,
                    ServiceBusException.FailureReason.ServiceBusy,
                    innerException: ex);
            }

            throw new ServiceBusException(
                true,
                $"{ex.Message}; response status code: {response.Status}",
                innerException: ex);
        }

        private Task<string> GetToken(Uri requestUri) =>
            GetTokenAsync(requestUri.GetLeftPart(UriPartial.Path));

        public async Task<string> GetTokenAsync(string requestUri)
        {
            var scope = requestUri;
            var credential = (ServiceBusTokenCredential)_tokenCredential;
            if (!credential.IsSharedAccessSignatureCredential)
            {
                scope = Constants.DefaultScope;
            }
            AccessToken token = await _tokenCredential.GetTokenAsync(new TokenRequestContext(new[] { scope }), CancellationToken.None).ConfigureAwait(false);
            return token.Token;
        }

        public async Task<Page<T>> GetEntitiesPageAsync<T>(
            string path,
            string nextSkip,
            Func<string, IReadOnlyList<T>> parseFunction,
            CancellationToken cancellationToken)
        {
            int skip = 0;
            int maxCount = 100;
            if (nextSkip != null)
            {
                skip = int.Parse(nextSkip, CultureInfo.InvariantCulture);
            }
            Response response = await GetEntityAsync(path, $"$skip={skip}&$top={maxCount}", false, cancellationToken).ConfigureAwait(false);
            string result = await ReadAsString(response).ConfigureAwait(false);

            IReadOnlyList<T> description = parseFunction.Invoke(result);
            skip += maxCount;
            nextSkip = skip.ToString(CultureInfo.InvariantCulture);

            if (description.Count < maxCount || description.Count == 0)
            {
                nextSkip = null;
            }
            return Page<T>.FromValues(description, nextSkip, response);
        }

        public async Task<Response> GetEntityAsync(
            string entityPath,
            string query,
            bool enrich,
            CancellationToken cancellationToken)
        {
            var queryString = $"{ManagementClientConstants.apiVersionQuery}&enrich={enrich}";
            if (query != null)
            {
                queryString = queryString + "&" + query;
            }
            Uri uri = new UriBuilder(_fullyQualifiedNamespace)
            {
                Path = entityPath,
                Scheme = Uri.UriSchemeHttps,
                Port = _port,
                Query = queryString
            }.Uri;

            RequestUriBuilder requestUriBuilder = new RequestUriBuilder();
            requestUriBuilder.Reset(uri);

            Request request = _pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri = requestUriBuilder;
            Response response = await SendHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }

        public async Task<Response> PutEntityAsync(
            string entityPath,
            string requestBody,
            bool isUpdate,
            string forwardTo,
            string fwdDeadLetterTo,
            CancellationToken cancellationToken)
        {
            Uri uri = new UriBuilder(_fullyQualifiedNamespace)
            {
                Path = entityPath,
                Port = _port,
                Scheme = Uri.UriSchemeHttps,
                Query = $"{ManagementClientConstants.apiVersionQuery}"
            }.Uri;
            var requestUriBuilder = new RequestUriBuilder();
            requestUriBuilder.Reset(uri);

            Request request = _pipeline.CreateRequest();
            request.Method = RequestMethod.Put;
            request.Uri = requestUriBuilder;
            request.Content = RequestContent.Create(Encoding.UTF8.GetBytes(requestBody));
            request.Headers.Add("Content-Type", ManagementClientConstants.AtomContentType);

            if (isUpdate)
            {
                request.Headers.Add("If-Match", "*");
            }

            var credential = (ServiceBusTokenCredential)_tokenCredential;
            if (!string.IsNullOrWhiteSpace(forwardTo))
            {
                var token = await GetTokenAsync(forwardTo).ConfigureAwait(false);
                request.Headers.Add(
                    ManagementClientConstants.ServiceBusSupplementartyAuthorizationHeaderName,
                    credential.IsSharedAccessSignatureCredential == true ? token : $"Bearer { token }");
            }

            if (!string.IsNullOrWhiteSpace(fwdDeadLetterTo))
            {
                var token = await GetTokenAsync(fwdDeadLetterTo).ConfigureAwait(false);
                request.Headers.Add(
                    ManagementClientConstants.ServiceBusDlqSupplementaryAuthorizationHeaderName,
                    credential.IsSharedAccessSignatureCredential == true ? token : $"Bearer { token }");
            }

            Response response = await SendHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }

        public async Task<Response> DeleteEntityAsync(
            string entityPath,
            CancellationToken cancellationToken)
        {
            Uri uri = new UriBuilder(_fullyQualifiedNamespace)
            {
                Path = entityPath,
                Scheme = Uri.UriSchemeHttps,
                Port = _port,
                Query = ManagementClientConstants.apiVersionQuery
            }.Uri;
            var requestUriBuilder = new RequestUriBuilder();
            requestUriBuilder.Reset(uri);

            Request request = _pipeline.CreateRequest();
            request.Uri = requestUriBuilder;
            request.Method = RequestMethod.Delete;

            Response response = await SendHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }

        private async Task<Response> SendHttpRequestAsync(
            Request request,
            CancellationToken cancellationToken)
        {
            var credential = (ServiceBusTokenCredential)_tokenCredential;
            if (credential.IsSharedAccessSignatureCredential)
            {
                var token = await GetToken(request.Uri.ToUri()).ConfigureAwait(false);
                request.Headers.Add("Authorization", token);
            }
            request.Headers.Add("UserAgent", $"SERVICEBUS/{ManagementClientConstants.ApiVersion}(api-origin={ClientInfo.Framework};os={ClientInfo.Platform};version={ClientInfo.Version};product={ClientInfo.Product})");

            Response response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);

            await ThrowIfRequestFailedAsync(request, response).ConfigureAwait(false);
            return response;
        }

        private static async Task<string> ReadAsString(Response response)
        {
            string exceptionMessage;
            using StreamReader reader = new StreamReader(response.ContentStream);
            exceptionMessage = await reader.ReadToEndAsync().ConfigureAwait(false);
            return exceptionMessage;
        }

        private static int GetPort(string endpoint)
        {
            // used for internal testing
            if (endpoint.EndsWith("onebox.windows-int.net", StringComparison.InvariantCultureIgnoreCase))
            {
                return 4446;
            }

            return -1;
        }
    }
}