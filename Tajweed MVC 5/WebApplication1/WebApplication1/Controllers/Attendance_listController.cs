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

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> attlist = db.Attfetchdetail();
            ViewBag.attlist = attlist;

            string status = null;
            if (Session["User_id"] == null)
            {
                status = "usernull";
            }
            else
            {
                if (Convert.ToInt32(Session["Role_id"]) == 1)
                {
                    status = "done";
                }
                else
                {
                    status = "usernotrole";
                }
            }

            if (status == "usernull")
            {
                return RedirectToAction("Index", "Login");
            }
            else if (status == "usernotrole")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View(menudisplay);
            }
        }
    }
}