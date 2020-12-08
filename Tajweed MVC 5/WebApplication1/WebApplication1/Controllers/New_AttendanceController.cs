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
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();

                List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                ViewBag.course = Course_dropdown;

                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                return View(menudisplay);
            }
        }

        [HttpPost]
        public ActionResult Index(Attendance_data ad)
        {
            bool status = false;

            var att_valid = db.attendance_valid(ad.BH_id);
            if (att_valid.att_id == 1)
            {
                foreach(var i in ad.Attendance_dtl)
                {
                    db.attuserupdate(ad.BH_id,i.Att_status,i.stud_id);
                }
                status = true;
            }
            else
            {
                List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                ViewBag.course = Course_dropdown;

                var att_id = db.AutoGenerate_attendance_id();
                ad.att_id = att_id.att_id;

                db.Insert_attendance(ad);

                foreach (var s in ad.Attendance_dtl)
                {
                    s.bh_id = ad.BH_id;
                    s.att_id = att_id.att_id;
                    db.Insert_attendance_details(s);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }


     

        public ActionResult get_att_data(int batch_name)
        {
            List<Student> casc_std_list = new List<Student>();
            var att_valid = db.attendance_valid(batch_name);
            if (att_valid.att_id == 1)
            {
                casc_std_list = db.get_att_students(batch_name);
                
            }
            else
            {
                casc_std_list = db.all_get_att_students(batch_name);
            }

            return Json(new SelectList(casc_std_list, "Stud_id", "Stud_name"));
        }
    }
}