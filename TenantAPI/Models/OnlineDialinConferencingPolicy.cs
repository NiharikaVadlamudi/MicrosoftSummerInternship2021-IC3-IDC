using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;



namespace BookStore.Models
{
  
    public class OnlineDialinConferencingPolicy
    {
        [Key]
        [Newtonsoft.Json.JsonProperty("authority")]
        public string authority { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string name { get; set; }
    }
}
