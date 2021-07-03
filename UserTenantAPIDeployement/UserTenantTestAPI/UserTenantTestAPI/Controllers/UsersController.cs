using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

//File Imports
using UserTenantTestAPI.Models;
using UserTenantTestAPI.Controllers;


namespace UserTenantTestAPI.Controllers
{
    public class UsersController : ODataController
    {
        private UserStoreContext _db;
        private readonly ILogger<UsersController> _logger;
        public const int pageSizeFix =10;

        public UsersController(UserStoreContext context, ILogger<UsersController> logger)
        {
            _logger = logger;
            _db = context;
            _db.Database.EnsureCreated();
            _db.SaveChanges();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            Console.WriteLine("Starting the Application...Database contains : {0} instances", _db.Users.Count());
        }

        [EnableQuery(PageSize = pageSizeFix)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (_db.Users.Count() != 0)
                {
                    _logger.LogDebug("Operation GET Successful!");
                    _logger.LogInformation("GettingAllUsersFromDatabase");
                    
                    return Ok(_db.Users.Include(b => b.assignedPlans).Include(b => b.onlineDialinConferencingPolicy).AsQueryable());
                }
                else
                {
                    _logger.LogDebug("Empty Database");
                    return NoContent();
                }

            }
            catch (Exception e)
            {
                _logger.LogError("ExecutionFailed:GETAllUsers", e, e.Message);
                return NotFound();

            }

        }


        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult Get(string key, string version)
        {
            User reqUser = _db.Users.FirstOrDefault(c => c.tenantNo == key);
            if (reqUser == null)
            {
                _logger.LogInformation("GETbyTenantID: Invalid Object");
                return NoContent();
            }
            try
            {
                _logger.LogInformation("GETbyTenantID {}", reqUser.tenantNo);
                return Ok(reqUser);
            }

            catch (Exception e)
            {
                _logger.LogError("ExecutionFailed:GETbyTenantID,{}", e.Message);
                return NotFound(e.Message);
            }

        }

        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] User userObj)
        {
            try
            {
                _logger.LogInformation("PostingUser : Successful , ID : ", userObj.tenantNo);
                _db.Users.Add(userObj);
                _db.SaveChanges();
                return Ok(Created(userObj));

            }
            catch (Exception e)
            {
                _logger.LogInformation("ExecutionFailed:POST Operation,ErrorUnderstanding : {}", e.Message);
                return BadRequest(e.Message);
            }

        }


        [EnableQuery]
        [HttpDelete]
        public IActionResult Delete(string key)
        {
            User userObj = _db.Users.FirstOrDefault(c => c.tenantNo == key);
            if (userObj == null)
            {
                _logger.LogInformation("DELETEbyObjectID: Invalid Object");
                return NoContent();
            }
            try
            {
                _logger.LogInformation("DELETE Operation Successful");
                _db.Users.Remove(userObj);
                _db.SaveChanges();
                return Ok();
            }

            catch (Exception e)
            {
                _logger.LogInformation("DELETE Exception Case : {}", e.Message);
                return BadRequest(e.Message);
            }

        }

        [EnableQuery]
        [HttpPut("{id}")]
        public IActionResult Put(string key, [FromBody] User userPatch)
        {

            if (userPatch == null)
            {
                _logger.LogInformation("ExecutionFailed:PUT,BadRequest!");
                return BadRequest();
            }

            User reqUser = _db.Users.FirstOrDefault(c => c.tenantNo == key);

            if (reqUser == null)
            {
                _logger.LogInformation("NonExistingObject:ExecutionFailed");
                return NotFound();
            }
            _logger.LogInformation("PUT Operation Sucessful!");
            _db.Entry(reqUser).CurrentValues.SetValues(userPatch);
            _db.SaveChanges();
            return Ok();

        }

    }
}
