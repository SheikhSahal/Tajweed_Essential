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
            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View();
        }

        [HttpPost]
        public ActionResult Index(Helper_mst hm)
        {
            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View();
        }
    }
}