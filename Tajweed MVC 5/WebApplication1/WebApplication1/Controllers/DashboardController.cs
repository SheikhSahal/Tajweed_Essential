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

            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();
                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                Attendance_data count_user = db.Get_all_users();
                ViewBag.allusers = count_user.att_id;

                Attendance_data courses = db.Get_Valid_Course();
                ViewBag.allcourses = courses.att_id;

                Attendance_data helpers = db.Get_Expired_Course();
                ViewBag.allhelper = helpers.att_id;

                Attendance_data attendance = db.Get_all_attendance();
                ViewBag.attendance = attendance.att_id;

                return View(menudisplay);
            }


        }

        public ActionResult AttInsert(int id, string pass)
        {
            var att = db.get_att_pass(id, pass);
            string status = null;

            var get_att_dup = db.stop_duplicate_att(id, Convert.ToInt32(Session["User_id"]));
            if (get_att_dup.att_id == 1)
            {
                status = "dup";
            }
            else
            {
                if (Convert.ToInt32(att.att_id) == 1)
                {
                    status = "done";
                    db.Insertattdetails(id, Convert.ToInt32(Session["User_id"]));
                }
                else
                {
                    status = "err";
                }
            }



            return new JsonResult { Data = new { status = status } };
        }
    }
}
