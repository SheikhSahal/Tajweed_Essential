using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class OldstudentAddController : Controller
    {
        // GET: OldstudentAdd
        Database db = new Database();
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();
            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> Course_dropdown = db.get_Course_dropdown();
            ViewBag.course = Course_dropdown;

            return View(menudisplay);
        }

        [HttpPost]
        public ActionResult Index(Registor r)
        {
            bool status = false;
            db.oldstudentRegistration(r);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}