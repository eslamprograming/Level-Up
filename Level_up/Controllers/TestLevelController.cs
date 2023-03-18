using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.WebPages;
using Level_up.Models;
using Microsoft.IdentityModel.Tokens;

namespace Level_up.Controllers
{
    public class TestLevelController : ApiController
    {
        database db = new database();
        [HttpGet]
        
        public IHttpActionResult test(string catname, string levname)
        {
            int l_id = db.Levels.Where(n => n.name == levname).Select(n => n.L_ID).SingleOrDefault();
            int c_id = db.Categories.Where(n => n.Name == catname).Select(n => n.ID).FirstOrDefault();

            //int l_id = 0, c_id = 0;
            //if (catname == "frontend") c_id = 1;
            //if (catname == "backend") c_id = 2;
            //if (catname == "IELTS") c_id = 4;
            //if (catname == "TOEFL") c_id = 5;

            //if (levname == "level1") l_id = 1;
            //if (levname == "level2") l_id = 2;
            //if (levname == "level3") l_id = 3;
            //if (levname == "level4") l_id = 4;




            var date = ((ClaimsIdentity)User.Identity).Claims.ToList();
            var email = date[0].Value;
            var user = db.Users.FirstOrDefault(n => n.email == email);
            int id = user.u_ID;
            if (user == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            //int id = 1;
            

            var clu = db.Cat_Level_User.Where(n => n.u_ID == id && n.ID == c_id && n.L_ID == l_id).FirstOrDefault();
            if (clu != null)
            {
                var clq = db.Cat_Level_Quction.Where(n => n.ID == c_id && n.L_ID == l_id).ToList();
                return Ok(clq);
            }
            else
            {
                var ee = db.Cat_Level_User.Where(n => n.u_ID == id && n.ID == c_id).Select(n=>n.L_ID).ToList();
                if (!ee.IsNullOrEmpty())
                {
                    int levid = 0;
                    foreach (var item in ee)
                    {
                        levid =levid+ 1;
                    }
               
                    
                    var levelname = db.Levels.Where(n => n.L_ID == levid).Select(n=>n.name).FirstOrDefault();
                    
                    return Ok("your level is: "+ levelname);

                }
                else
                {
                    Cat_Level_User u2 = new Cat_Level_User();
                    u2.u_ID = id;
                    u2.ID=c_id;
                    u2.L_ID = l_id;
                    db.Cat_Level_User.Add(u2);
                    db.SaveChanges();
                    var level1 = db.Cat_Level_Quction.Where(n => n.ID == c_id && n.L_ID == 1).ToList();
                    return Ok(level1);
                }
            }

           
        }
        [HttpPost]
        public IHttpActionResult anserExam(string name, string lev, string[] ans)
        {
            Category cat = db.Categories.Where(n => n.Name == name).SingleOrDefault();
            int c_id = cat.ID;
            Level le = db.Levels.Where(n => n.name == lev).SingleOrDefault();
            int l_id = le.L_ID;
            var E = db.Cat_Level_Quction.Where(n => n.ID == cat.ID & n.L_ID == l_id).Select(n => n.Quction.anseur).ToList();
            var date = ((ClaimsIdentity)User.Identity).Claims.ToList();
            var email = date[0].Value;
            var user = db.Users.FirstOrDefault(n => n.email == email);
            int id = user.u_ID;
            
            int degree = 0;
            for (int i = 0; i < ans.Length; i++)
            {
                if (ans[i] == E[i])
                {
                    degree+=4;
                }

            }
            Degree d = new Degree();
            d.Mark = degree;
            db.Degrees.Add(d);
            db.SaveChanges();

            user_degree cul = new user_degree();
           
            cul.u_ID = id;
            cul.ID = c_id;
            cul.D_ID = d.D_ID;
           
            


            
            if (l_id == 4&&degree>=10)
            {
                return Ok("finished"+" degree is:"+degree);
            }
            if (degree >= 10)
            {
                //level +
                cul.L_ID = l_id + 1;
                db.user_degree.Add(cul);
                db.SaveChanges();
                return Ok("success degre is:" + degree);
            }

            return Ok("faild your degree is:"+degree);
        }
    }
}
