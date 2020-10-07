using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tajweed.DB;
using Tajweed.Models;


namespace Tajweed.Controllers
{
    public class Batch : Controller
    {
        Database db = new Database();
        public IActionResult Index()
        {
            
            List<Teacher>  tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;


            return View();
        }

        [HttpPost]
        public IActionResult Index(Batch_header bh)
        {

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



            return View();
        }
    }
}
