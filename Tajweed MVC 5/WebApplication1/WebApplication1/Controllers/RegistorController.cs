using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegistorController : Controller
    {
        Database db = new Database();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Registor r)
        {
            string status = null;

            var get_email = db.get_email(r.email);

            if (Convert.ToInt32(get_email.email) == 1)
            {
                status = "dupl";
            }
            else
            {
                db.Registration(r);
                status = "done";
            }

            
            

            return new JsonResult { Data = new { status = status } };
        }
    }
}