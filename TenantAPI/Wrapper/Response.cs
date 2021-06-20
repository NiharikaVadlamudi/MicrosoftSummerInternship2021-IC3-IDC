using System;
using System.Collections.Generic;

namespace UserTenant.Wrapper
{
    public class Response<T>
    {
       
        public Response(T data)
        {   
            Users.Add(data);
        }
        //public T Users{ get; set; }
        public List<T> Users = new List<T>();

    }
}


