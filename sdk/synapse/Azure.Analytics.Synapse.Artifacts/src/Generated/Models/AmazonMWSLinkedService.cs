// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace Azure.Analytics.Synapse.Artifacts.Models
{
    /// <summary> Amazon Marketplace Web Service linked service. </summary>
    public partial class AmazonMWSLinkedService : LinkedService
    {
        /// <summary> Initializes a new instance of AmazonMWSLinkedService. </summary>
        /// <param name="endpoint"> The endpoint of the Amazon MWS server, (i.e. mws.amazonservices.com). </param>
        /// <param name="marketplaceID"> The Amazon Marketplace ID you want to retrieve data from. To retrieve data from multiple Marketplace IDs, separate them with a comma (,). (i.e. A2EUQ1WTGCTBG2). </param>
        /// <param name="sellerID"> The Amazon seller ID. </param>
        /// <param name="accessKeyId"> The access key id used to access data. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="endpoint"/>, <paramref name="marketplaceID"/>, <paramref name="sellerID"/>, or <paramref name="accessKeyId"/> is null. </exception>
        public AmazonMWSLinkedService(object endpoint, object marketplaceID, object sellerID, object accessKeyId)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (marketplaceID == null)
            {
                throw new ArgumentNullException(nameof(marketplaceID));
            }
            if (sellerID == null)
            {
                throw new ArgumentNullException(nameof(sellerID));
            }
            if (accessKeyId == null)
            {
                throw new ArgumentNullException(nameof(accessKeyId));
            }

            Endpoint = endpoint;
            MarketplaceID = marketplaceID;
            SellerID = sellerID;
            AccessKeyId = accessKeyId;
            Type = "AmazonMWS";
        }

        /// <summary> Initializes a new instance of AmazonMWSLinkedService. </summary>
        /// <param name="type"> Type of linked service. </param>
        /// <param name="connectVia"> The integration runtime reference. </param>
        /// <param name="description"> Linked service description. </param>
        /// <param name="parameters"> Parameters for linked service. </param>
        /// <param name="annotations"> List of tags that can be used for describing the linked service. </param>
        /// <param name="additionalProperties"> . </param>
        /// <param name="endpoint"> The endpoint of the Amazon MWS server, (i.e. mws.amazonservices.com). </param>
        /// <param name="marketplaceID"> The Amazon Marketplace ID you want to retrieve data from. To retrieve data from multiple Marketplace IDs, separate them with a comma (,). (i.e. A2EUQ1WTGCTBG2). </param>
        /// <param name="sellerID"> The Amazon seller ID. </param>
        /// <param name="mwsAuthToken"> The Amazon MWS authentication token. </param>
        /// <param name="accessKeyId"> The access key id used to access data. </param>
        /// <param name="secretKey"> The secret key used to access data. </param>
        /// <param name="useEncryptedEndpoints"> Specifies whether the data source endpoints are encrypted using HTTPS. The default value is true. </param>
        /// <param name="useHostVerification"> Specifies whether to require the host name in the server&apos;s certificate to match the host name of the server when connecting over SSL. The default value is true. </param>
        /// <param name="usePeerVerification"> Specifies whether to verify the identity of the server when connecting over SSL. The default value is true. </param>
        /// <param name="encryptedCredential"> The encrypted credential used for authentication. Credentials are encrypted using the integration runtime credential manager. Type: string (or Expression with resultType string). </param>
        internal AmazonMWSLinkedService(string type, IntegrationRuntimeReference connectVia, string description, IDictionary<string, ParameterSpecification> parameters, IList<object> annotations, IDictionary<string, object> additionalProperties, object endpoint, object marketplaceID, object sellerID, SecretBase mwsAuthToken, object accessKeyId, SecretBase secretKey, object useEncryptedEndpoints, object useHostVerification, object usePeerVerification, object encryptedCredential) : base(type, connectVia, description, parameters, annotations, additionalProperties)
        {
            Endpoint = endpoint;
            MarketplaceID = marketplaceID;
            SellerID = sellerID;
            MwsAuthToken = mwsAuthToken;
            AccessKeyId = accessKeyId;
            SecretKey = secretKey;
            UseEncryptedEndpoints = useEncryptedEndpoints;
            UseHostVerification = useHostVerification;
            UsePeerVerification = usePeerVerification;
            EncryptedCredential = encryptedCredential;
            Type = type ?? "AmazonMWS";
        }

        /// <summary> The endpoint of the Amazon MWS server, (i.e. mws.amazonservices.com). </summary>
        public object Endpoint { get; set; }
        /// <summary> The Amazon Marketplace ID you want to retrieve data from. To retrieve data from multiple Marketplace IDs, separate them with a comma (,). (i.e. A2EUQ1WTGCTBG2). </summary>
        public object MarketplaceID { get; set; }
        /// <summary> The Amazon seller ID. </summary>
        public object SellerID { get; set; }
        /// <summary> The Amazon MWS authentication token. </summary>
        public SecretBase MwsAuthToken { get; set; }
        /// <summary> The access key id used to access data. </summary>
        public object AccessKeyId { get; set; }
        /// <summary> The secret key used to access data. </summary>
        public SecretBase SecretKey { get; set; }
        /// <summary> Specifies whether the data source endpoints are encrypted using HTTPS. The default value is true. </summary>
        public object UseEncryptedEndpoints { get; set; }
        /// <summary> Specifies whether to require the host name in the server&apos;s certificate to match the host name of the server when connecting over SSL. The default value is true. </summary>
        public object UseHostVerification { get; set; }
        /// <summary> Specifies whether to verify the identity of the server when connecting over SSL. The default value is true. </summary>
        public object UsePeerVerification { get; set; }
        /// <summary> The encrypted credential used for authentication. Credentials are encrypted using the integration runtime credential manager. Type: string (or Expression with resultType string). </summary>
        public object EncryptedCredential { get; set; }
    }
}