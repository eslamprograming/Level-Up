using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Level_up.Models;

namespace Level_up.Controllers
{
    public class UsersController : ApiController
    {
        private database db = new database();

       
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser()
        {

            var date = ((ClaimsIdentity)User.Identity).Claims.ToList();
            int id = Convert.ToInt32(date[1].Value);
            User user = db.Users.Where(n => n.u_ID == id).SingleOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        
      

        // POST: api/AddUser
        [HttpPost]
        [Route("api/AddUser")]
        public IHttpActionResult PostUser(User user)
        {
            var u = db.Users.Where(n => n.email ==user.email).SingleOrDefault();
           if (u!=null)  return Ok("this E-mail is Exit"); 
            db.Users.Add(user);
            db.SaveChanges();
            

            return Created(" ",user);
        }


        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.u_ID == id) > 0;
        }
    }
}