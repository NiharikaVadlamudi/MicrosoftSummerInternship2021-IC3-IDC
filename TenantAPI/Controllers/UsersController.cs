using System;
using System.Linq;
using System.Net;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class BooksController : ODataController
    {
        private UserStoreContext _db;

        public BooksController(UserStoreContext context)
        {
            _db = context;
            _db.Database.EnsureCreated();
            _db.SaveChanges();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

        }

        [EnableQuery(PageSize = 1)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get(int key, string version)
        {
            User reqBook = _db.Books.FirstOrDefault(c => c.tenantId == key);
            try
            {
                return Ok(_db.Books.FirstOrDefault(c => c.tenantId == key));
            }

            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] User book)
        {
           try
           {
               _db.Books.Add(book);
               _db.SaveChanges();
               return Ok(Created(book));

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
            User b = _db.Books.FirstOrDefault(c => c.tenantId == key);

            try
            {
                _db.Books.Remove(b);
                _db.SaveChanges();
                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put(int key,[FromBody] User userPatch)
        {
            //Method 1 
            //User foundUser =_db.Books.FirstOrDefault(c => c.tenantId == key);
            //_db.Entry(foundUser).CurrentValues.SetValues(userPatch);
            //_db.SaveChanges();
            //return Ok();

            //Method 2
            //_db.Entry(userPatch).State = EntityState.Modified;
            //_db.SaveChanges();
            //return NoContent();


            if (userPatch == null)
            {
                return BadRequest();
            }

            User reqUser = _db.Books.FirstOrDefault(x => x.tenantId == key);

            if (reqUser == null)
            {
                return NotFound();
            }

            try
            {
                _db.Entry(reqUser).CurrentValues.SetValues(userPatch);
                _db.SaveChanges();
                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
