using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using UserStore.Models;
using System.Collections.Generic;

namespace UserTenant.Models
{
    public class DataSource
    {
        public string json { get; set; }
        public IList<User> deserializedUserList { get; set; }

        public static IList<User> GetInitialUsers()
        {
            var json = System.IO.File.ReadAllText("randomData.json");
            IList<User> deserializedUserList = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<User>>(json);
            return (deserializedUserList);
        }
    }
}
