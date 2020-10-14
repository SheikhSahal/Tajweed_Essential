using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        Database db = new Database();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            string status = null;
            var userdata=  db.user_login(email, password);
            if(userdata.email == email && userdata.pass == password)
            {
                status = "done";
                Session["Username"] = userdata.Full_Name;
                Session["User_id"] = userdata.User_id;
                Session["Role_id"] = userdata.Role_id;
                db.Logoutupdate(userdata.User_id, "Y");
            }
            else
            {
                status = "err";
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Logout()
        {
            var sess = Convert.ToInt16(Session["User_id"]);
            db.Logoutupdate(sess,"N");

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Session["User_id"] = null;

            FormsAuthentication.SignOut();


            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return RedirectToAction("index", "Login");

        }

     

    }
}