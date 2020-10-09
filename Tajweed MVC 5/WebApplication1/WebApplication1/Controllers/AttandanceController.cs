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

            var Menulist = db.user_rights(13);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View(menudisplay);
        }


        [HttpPost]
        public ActionResult Index(Attendance_mst am)
        {
            bool status = false;
            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            var at_id = db.AutoGenerate_attendance();

            am.att_id = Convert.ToInt32(at_id.att_id);

            db.InsertAttHeader(am);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }



        public ActionResult DelAtt(int id)
        {
            db.Deleteatt(id);
            return RedirectToAction("index", "Attendance_list");
        }
    }
}