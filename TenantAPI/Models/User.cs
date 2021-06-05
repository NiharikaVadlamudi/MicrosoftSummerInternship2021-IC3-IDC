using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class User
    {
        [Key]
        [Newtonsoft.Json.JsonProperty("tenantId")]
        //[Required(ErrorMessage = "Invalid Input : TenantID")]
        public int tenantId { get; set; }

        [Newtonsoft.Json.JsonProperty("displayName")]
        public string displayName { get; set; }

        [Required(ErrorMessage = "Invalid Input : ObjectID (PartitionKey)")]
        public string objectId { get; set; }


        [Newtonsoft.Json.JsonProperty("givenName")]
        public string givenName { get; set; }

        [Newtonsoft.Json.JsonProperty("enterpriseVoiceEnabled")]
        public bool enterpriseVoiceEnabled { get; set; }

        [Newtonsoft.Json.JsonProperty("interpretedUserType")]
        public string interpretedUserType { get; set; }

        [Newtonsoft.Json.JsonProperty("surname")]
        public string surname { get; set; }

        [Newtonsoft.Json.JsonProperty("usageLocation")]
        public string usageLocation { get; set; }

        [Newtonsoft.Json.JsonProperty("userPrincipalName")]
        public string userPrincipalName { get; set; }


        //Addition of other transaction classes.

        public DataProviderErrors dataProviderErrors { get; set; }
        public OnlineDialinConferencingPolicy onlineDialinConferencingPolicy { get; set; }
        //public List<AssignedPlans> assignedPlans { get; set; }




    }

}
