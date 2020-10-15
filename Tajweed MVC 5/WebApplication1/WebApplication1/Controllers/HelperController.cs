using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class HelperController : Controller
    {
        Database db = new Database();
        // GET: Helper
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

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

        [HttpPost]
        public ActionResult Index(Helper_mst hm)
        {
            string status = null;
            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            var hp_id = db.AutoGenerate_Helper_id();
            hm.Hpl_id = Convert.ToInt32(hp_id.Hpl_id);

            Batch_header get_date = db.get_batch_Master_data(hm.bh_id);
            var bhdate = get_date.course_end_date.Date;

            DateTime date = DateTime.Now;
            var current_date = date.Date;

            if (bhdate < current_date)
            {
                status = "ederror";
            }
            else
            {
            db.InsertHelperHeader(hm);
            foreach (var hmdtl in hm.Helper_dtl)
            {
                hmdtl.Hpl_id = Convert.ToInt32(hp_id.Hpl_id);
                db.InsertHelperDetails(hmdtl);
            }
            status = "done";
            }


            return new JsonResult { Data = new { status = status } };
        }
    }
}