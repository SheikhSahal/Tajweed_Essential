using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tajweed.Models;
using Tajweed.DB;

namespace Tajweed.Controllers
{
    public class Batch_ListController : Controller
    {
        Database db = new Database();
        public IActionResult Index()
        {
            List<Batch_list> bhlist = db.Batchfetchdetail();
            ViewBag.batchlist = bhlist;
            return View();
        }


        public ActionResult DeleteRecord(int id)
        {

            db.DeleteBatch(id);

            return RedirectToAction("index", "Batch_List");
        }

        [HttpGet]
        public ActionResult updatebatch(int id)
        {
            TempData["mydata"] = id; 
            Batch_header bhdata = db.get_batch_Master_data(id);
            ViewBag.bhdata = bhdata;

            List<Batch_details> bddata= db.Get_Batch_detail_data(id);
            ViewBag.bddata = bddata;

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;


            return View();
        }


        [HttpPost]
        public ActionResult updatebatch(Batch_header bh)
        {
            var BH_id = TempData["mydata"];
            bh.Bh_id = Convert.ToInt32(BH_id);

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            db.Update_batch_Master(bh);

            foreach (var bhdtl in bh.Batch_details)
            {
                bhdtl.Bh_id = Convert.ToInt32(BH_id);
                db.InsertBatchDetails(bhdtl);
            }

            return View();
        }
    }
}
