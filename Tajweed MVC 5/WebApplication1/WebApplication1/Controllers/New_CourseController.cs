using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_CourseController : Controller
    {
        Database db = new Database();
        // GET: New_Course
        public ActionResult Index()
        {
            if(Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();

                List<Teacher> tdp = db.Teacher_DropDown();
                ViewBag.Teachdropdown = tdp;

                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                return View(menudisplay);
            }
           
        }
        [HttpPost]
        public ActionResult Index(New_Course nc)
        {

            bool status = false;
            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            db.InsertCourse(nc);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
    }
}