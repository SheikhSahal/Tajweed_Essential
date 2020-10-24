using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class List_HeaderController : Controller
    {
        Database db = new Database();
        // GET: List_Header
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

                List<Student> Student_list = db.Get_student_list();
                ViewBag.student = Student_list;

                return View(menudisplay);
            }
        }

        [HttpPost]
        public ActionResult Index(List_Header lh)
        {
            bool status = false;
            var list = db.AutoGenerate_List_Header();

            List<Student> Student_list = db.Get_student_list();
            ViewBag.student = Student_list;

            lh.List_id = list.List_id;
            db.InsertList_header(lh);

            foreach(var listdt in lh.List_Details)
            {
                listdt.List_id = list.List_id;
                db.InsertList_Detail(listdt);
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}