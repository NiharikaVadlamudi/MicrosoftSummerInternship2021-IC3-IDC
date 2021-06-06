using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserStore.Models
{
    [Owned]
    public class AssignedPlans
    {
        [Key]
        [Newtonsoft.Json.JsonProperty("servicePlanID")]
        [Required(ErrorMessage = "Invalid Input : ServicePlanID field")]
        public string servicePlanId { get; set; }

        [Newtonsoft.Json.JsonProperty("serviceInstance")]
        [Required(ErrorMessage = "Invalid Input : ServiceInstance field")]
        public string serviceInstance { get; set; }


        [Newtonsoft.Json.JsonProperty("subscribedPlanId")]
        [Required(ErrorMessage = "Invalid Input : SubscribedPlanId field")]
        public string subscribedPlanId { get; set; }

        [Newtonsoft.Json.JsonProperty("assignedTimestamp")]
        [Required(ErrorMessage = "Invalid Input : AssignedTimestamp field")]
        public DateTime assignedTimestamp { get; set; }

        [Newtonsoft.Json.JsonProperty("capabilityStatus")]
        [Required(ErrorMessage = "Invalid Input : CapabilityStatus field")]
        public string capabilityStatus { get; set; }

        [Newtonsoft.Json.JsonProperty("capability")]
        [Required(ErrorMessage = "Invalid Input : Capability field")]
        public string capability { get; set; }

        

    }
}

