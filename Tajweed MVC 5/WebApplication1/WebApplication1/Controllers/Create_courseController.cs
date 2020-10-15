using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Create_courseController : Controller
    {
        // GET: Batch_Form
        Database db = new Database();

        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            return View(menudisplay);
        }


        [HttpPost]
        public ActionResult Index(Batch_header bh)
        {
            bool status = false;
            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            var bh_id = db.AutoGenerate_batch_id();

            bh.Bh_id = Convert.ToInt32(bh_id.Bh_id);

            db.InsertBatchHeader(bh);

            foreach (var bhdtl in bh.Batch_details)
            {
                bhdtl.Bh_id = Convert.ToInt32(bh_id.Bh_id);
                db.InsertBatchDetails(bhdtl);
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}