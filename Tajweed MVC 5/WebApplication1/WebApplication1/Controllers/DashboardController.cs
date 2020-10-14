using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        Database db = new Database();
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();
            var Menulist = db.user_rights(1024);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Attendance_data> att_data= db.Attendancefetchdetail(Convert.ToInt32(Session["User_id"]));
            ViewBag.attendnce = att_data;

            return View(menudisplay);
        }

        public ActionResult AttInsert(int id)
        {
            return View();
        }
    }
}
