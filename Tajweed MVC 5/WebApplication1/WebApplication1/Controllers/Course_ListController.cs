using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Course_ListController : Controller
    {
        Database db = new Database();

        public ActionResult Index()
        {
            List<Batch_list> bhlist = db.Batchfetchdetail();
            ViewBag.batchlist = bhlist;

            return View();
        }


        public ActionResult DeleteRecord(int id)
        {
            db.DeleteBatch(id);
            return RedirectToAction("index", "Course_List");
        }

        [HttpGet]
        public ActionResult updateCourse(int id)
        {
            TempData["mydata"] = id;
            Batch_header bhdata = db.get_batch_Master_data(id);
            ViewBag.bhdata = bhdata;

            List<Batch_details> bddata = db.Get_Batch_detail_data(id);
            ViewBag.bddata = bddata;

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            return View();
        }

        [HttpPost]
        public ActionResult updateCourse(Batch_header bh)
        {
            bool status = false;

            var BH_id = TempData["mydata"];
            bh.Bh_id = Convert.ToInt32(BH_id);

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            db.Update_batch_Master(bh);

            if(bh.Batch_details != null)
            {
                foreach (var bhdtl in bh.Batch_details)
                {
                    bhdtl.Bh_id = Convert.ToInt32(BH_id);
                    db.InsertBatchDetails(bhdtl);
                }
            }
            status = true;



            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public ActionResult DelSin(int id)
        {
            bool status = false;
            var BH_id = TempData["mydata"];
            db.dtlDelete(id, Convert.ToInt32(BH_id));
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}