using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class AttandanceController : Controller
    {
        Database db = new Database();
        // GET: Attandance
        public ActionResult Index()
        {

            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View(menudisplay);
        }


        [HttpPost]
        public ActionResult Index(Attendance_mst am)
        {
            string status = null;
            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            var at_id = db.AutoGenerate_attendance();

            am.att_id = Convert.ToInt32(at_id.att_id);

            Batch_header get_date = db.get_batch_Master_data(am.bh_id);
            var bhdate = get_date.course_end_date.Date;

            DateTime date = DateTime.Now;
            var current_date = date.Date;


            if (bhdate < current_date)
            {
                status = "ederror";
            }
            else
            {
               var dprcd= db.get_Duplicate_data(am.bh_id, current_date);

                if (dprcd.att_id == 0)
                {
                    db.InsertAttHeader(am);
                    status = "done";
                }
                else
                {
                    status = "dupcte";
                }
            }
            return new JsonResult { Data = new { status = status } };
        }



        public ActionResult DelAtt(int id)
        {
            db.Deleteatt(id);
            return RedirectToAction("index", "Attendance_list");
        }
    }
}