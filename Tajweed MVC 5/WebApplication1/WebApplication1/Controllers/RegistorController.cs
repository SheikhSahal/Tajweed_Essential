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

        public ActionResult Index(int id, DateTime end_date)
        {
            var current_date = DateTime.Now.Date;

            if (end_date.Date <= current_date)
            {
                return RedirectToAction("Index","CoursesDetails");
            }
            else
            {
                TempData["Registor_bh_id"] = id;
                return View();
            }

         
        }

        [HttpPost]
        public ActionResult Index(Registor r)
        {
            string status = null;

            //var get_email = db.get_email(r.email);

            //if (Convert.ToInt32(get_email.email) == 1)
            //{
            //    status = "dupl";
            //}
            //else
            //{

            r.bh_id = Convert.ToInt32(TempData["Registor_bh_id"]);
            db.Registration(r);
            status = "done";
            //}




            return new JsonResult { Data = new { status = status } };
        }
    }
}