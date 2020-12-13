using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Report_ParamController : Controller
    {
        Database db = new Database();
        // GET: Report_Param
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();
                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                List<Batch_header> bh = db.Course_DropDown();
                ViewBag.cordropdown = bh;

                List<Student> std = db.Approved_student_list();
                ViewBag.stddropdown = std;


                return View(menudisplay);
            }
        }


    }
}