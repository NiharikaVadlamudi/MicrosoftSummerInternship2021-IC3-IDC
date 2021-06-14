using System;
using System.Linq;
using System.Net;
using UserStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using UserStore.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using UserTenant.Models;


namespace UserStore.Controllers
{
    public class UsersController : ODataController
    {
        private UserStoreContext _db;

        public const int pageSizeFix = 5;


        public UsersController(UserStoreContext context)
        {
            _db = context;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            _db.Database.EnsureCreated();
            _db.SaveChanges();
        }

        [EnableQuery(PageSize = pageSizeFix)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Users.Include(b=>b.assignedPlans).Include(b=>b.onlineDialinConferencingPolicy).ToList());
        }


        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult Get(string key,string version)
        {
            User reqUser = _db.Users.FirstOrDefault(c => c.objectId == key);
            try
            {
                return Ok(reqUser);
            }

            catch (Exception e)
            {
                
                return NotFound(e.Message);
            }

        }

        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] User userObj)
        {
           try
           {
               _db.Users.Add(userObj);
               _db.SaveChanges();
               return Ok(Created(userObj));

           }
           catch (Exception e)
           {
               return BadRequest(e.Message);
           }

        }


        [EnableQuery]
        [HttpDelete]
        public IActionResult Delete(int key)
        {
            User b = _db.Users.FirstOrDefault(c => c.tenantId == key);

            try
            {
                _db.Users.Remove(b);
                _db.SaveChanges();
                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [EnableQuery]
        [HttpPut("{id}")]
        public IActionResult Put(int key,[FromBody] User userPatch)
        {
         
            if (userPatch == null)
            {
                return BadRequest();
            }

            User reqUser = _db.Users.FirstOrDefault(x => x.tenantId == key);

            if (reqUser == null)
            {
                return NotFound();
            }
           
            _db.Entry(reqUser).CurrentValues.SetValues(userPatch);
            _db.SaveChanges();
            return Ok();

        }

    }
}
