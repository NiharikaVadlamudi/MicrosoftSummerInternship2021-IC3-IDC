using System;
namespace Microsoft.Azure.Cosmos.Samples.Bulk
{
    public class AssignedPlans
    {
        public string servicePlanId { get; set; }
        public string serviceInstance { get; set; }
        public string subscribedPlanId { get; set; }
        public DateTime assignedTimestamp { get; set; }
        public string capabilityStatus { get; set; }
        public string capability { get; set; }

    }
}
