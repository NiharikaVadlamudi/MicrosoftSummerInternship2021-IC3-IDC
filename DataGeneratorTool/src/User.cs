using System;
using System.Collections.Generic;

namespace Microsoft.Azure.Cosmos.Samples.Bulk
{
    public class User
    {
        public User()
        {
            DataProviderErrors dataProviderErrors = new DataProviderErrors();
        }

        public string pk { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public string objectId { get; set; }
        public string givenName { get; set; }
        public bool enterpriseVoiceEnabled { get; set; }
        public string surname { get; set; }
        public string usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string interpretedUserType { get; set; }

        public DataProviderErrors dataProviderErrors { get; set; }
        public OnlineDialinConferencingPolicy onlineDialinConferencingPolicy { get; set; }
        public ICollection<AssignedPlans> assignedPlans { get; set; }


    }
}
