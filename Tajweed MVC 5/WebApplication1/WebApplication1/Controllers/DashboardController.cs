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

            List<Attendance_data> att_data = db.Attendancefetchdetail(Convert.ToInt32(Session["User_id"]));
            ViewBag.attendnce = att_data;

            return View(menudisplay);
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
