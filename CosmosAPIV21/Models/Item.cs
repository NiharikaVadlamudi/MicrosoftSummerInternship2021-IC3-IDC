using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CosmosAPIV21.Models
{
    public class Item
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [Newtonsoft.Json.JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [Newtonsoft.Json.JsonProperty("enterpriseVoiceEnabled")]
        public bool EnterpriseVoiceEnabled { get; set; }

        [Newtonsoft.Json.JsonProperty("givenName")]
        public string GivenName { get; set; }

        [Newtonsoft.Json.JsonProperty("interpretedUserType")]
        public string InterpretedUserType { get; set; }

        [Newtonsoft.Json.JsonProperty("objectId")]
        public string ObjectId { get; set; }


        [Newtonsoft.Json.JsonProperty("surname")]
        public string Surname { get; set; }

        [Newtonsoft.Json.JsonProperty("usageLocation")]
        public string UsageLocation { get; set; }

        [Newtonsoft.Json.JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [Newtonsoft.Json.JsonProperty("department")]
        public string Department {get; set; }
    }
}
