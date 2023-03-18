using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Level_up.Models;

namespace Level_up.Controllers
{
    public class CategoryController : ApiController
    {
        database db = new database();
        [HttpGet]
        public IHttpActionResult cat(string type)
        {
            var c = db.Categories.Where(n=>n.type==type).ToList();
            return Ok(c);
        }
    }
}
