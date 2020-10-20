using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_AttendanceController : Controller
    {
        Database db = new Database();
        // GET: New_Attendance
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            List<Batch_header> Course_dropdown = db.get_Course_dropdown();
            ViewBag.course = Course_dropdown;

            var Menulist = db.user_rights(1030);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            return View(menudisplay);
        }

        [HttpPost]
        public ActionResult Index(Attendance_data ad)
        {
            return View();
        }

        
        public ActionResult get_att_data(int batch_name)
        {
            List<Student> casc_std_list = db.get_att_students(batch_name);
            return Json(new SelectList(casc_std_list, "Stud_id", "Stud_name"));
        }
    }
}