using System;
namespace CosmosAPIV21.Models
{
    public class Item
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        // More Additions !
        //[Newtonsoft.Json.JsonProperty(PropertyName = "PhoneNumber")]
        //public int PhoneNumber { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "Location")]
        //public string Location { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "ProductType")]
        //public string ProductType { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "AccountType")]
        //public string AccountType { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "AssignedLicense")]
        //public int AssignedLicense { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "CallingType")]
        //public string CallingType { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "AudioConferencing")]
        //public string AudioConferencing { get; set; }
    }
}
