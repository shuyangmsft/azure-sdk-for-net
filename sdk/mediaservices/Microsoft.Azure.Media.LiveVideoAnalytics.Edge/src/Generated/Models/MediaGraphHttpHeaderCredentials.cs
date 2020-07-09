// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Media.LiveVideoAnalytics.Edge.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Http header service credentials.
    /// </summary>
    [Newtonsoft.Json.JsonObject("#Microsoft.Media.MediaGraphHttpHeaderCredentials")]
    public partial class MediaGraphHttpHeaderCredentials : MediaGraphCredentials
    {
        /// <summary>
        /// Initializes a new instance of the MediaGraphHttpHeaderCredentials
        /// class.
        /// </summary>
        public MediaGraphHttpHeaderCredentials()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MediaGraphHttpHeaderCredentials
        /// class.
        /// </summary>
        /// <param name="headerName">HTTP header name.</param>
        /// <param name="headerValue">HTTP header value.</param>
        public MediaGraphHttpHeaderCredentials(string headerName, string headerValue)
        {
            HeaderName = headerName;
            HeaderValue = headerValue;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets HTTP header name.
        /// </summary>
        [JsonProperty(PropertyName = "headerName")]
        public string HeaderName { get; set; }

        /// <summary>
        /// Gets or sets HTTP header value.
        /// </summary>
        [JsonProperty(PropertyName = "headerValue")]
        public string HeaderValue { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (HeaderName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "HeaderName");
            }
            if (HeaderValue == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "HeaderValue");
            }
        }
    }
}