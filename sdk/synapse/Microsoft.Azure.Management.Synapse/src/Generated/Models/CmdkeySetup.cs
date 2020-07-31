// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Synapse.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The custom setup of running cmdkey commands.
    /// </summary>
    [Newtonsoft.Json.JsonObject("CmdkeySetup")]
    [Rest.Serialization.JsonTransformation]
    public partial class CmdkeySetup : CustomSetupBase
    {
        /// <summary>
        /// Initializes a new instance of the CmdkeySetup class.
        /// </summary>
        public CmdkeySetup()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CmdkeySetup class.
        /// </summary>
        /// <param name="targetName">The server name of data source
        /// access.</param>
        /// <param name="userName">The user name of data source access.</param>
        /// <param name="password">The password of data source access.</param>
        public CmdkeySetup(object targetName, object userName, SecretBase password)
        {
            TargetName = targetName;
            UserName = userName;
            Password = password;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the server name of data source access.
        /// </summary>
        [JsonProperty(PropertyName = "typeProperties.targetName")]
        public object TargetName { get; set; }

        /// <summary>
        /// Gets or sets the user name of data source access.
        /// </summary>
        [JsonProperty(PropertyName = "typeProperties.userName")]
        public object UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of data source access.
        /// </summary>
        [JsonProperty(PropertyName = "typeProperties.password")]
        public SecretBase Password { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (TargetName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TargetName");
            }
            if (UserName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "UserName");
            }
            if (Password == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Password");
            }
        }
    }
}