using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.ModelBinding;
using Level_up.Models;


namespace Level_up.Controllers
{
    public class bookController : ApiController
    {
        database db = new database();
        [HttpGet]
        
        public IHttpActionResult Res(string catname,string levname)
        {
            

            var book = db.has_R.Where(n => n.Category.Name == catname & n.Level.name == levname).ToList();
            

            return Ok(book);
        }
    }
}
