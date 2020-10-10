using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Attendance_listController : Controller
    {
        Database db = new Database();
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(15);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> attlist = db.Attfetchdetail();
            ViewBag.attlist = attlist;

            return View(menudisplay);
        }
    }
}