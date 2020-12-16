using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class MonthReport_ParamController : Controller
    {
        Database db = new Database();
        // GET: MonthReport_Param
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();
                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                List<Batch_header> batch = db.Report_Courses_list();
                ViewBag.bthdropdown = batch;

                return View(menudisplay);
            }
        }
    }
}