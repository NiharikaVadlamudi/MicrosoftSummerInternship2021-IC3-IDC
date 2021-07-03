using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace UserTenantTestAPI.Models
{
    [Owned]
    public class OnlineDialinConferencingPolicy
    {
        [Newtonsoft.Json.JsonProperty("Authority")]
        public string authority { get; set; }

        [Newtonsoft.Json.JsonProperty("Name")]
        public string name { get; set; }
    }
}
