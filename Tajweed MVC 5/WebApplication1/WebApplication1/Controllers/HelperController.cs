﻿using System;
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

            var Menulist = db.user_rights(13);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View(menudisplay);
        }

        [HttpPost]
        public ActionResult Index(Helper_mst hm)
        {
            bool status = false;
            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            var hp_id = db.AutoGenerate_Helper_id();
            hm.Hpl_id = Convert.ToInt32(hp_id.Hpl_id);

            db.InsertHelperHeader(hm);

            foreach (var hmdtl in hm.Helper_dtl)
            {
                hmdtl.Hpl_id = Convert.ToInt32(hp_id.Hpl_id);
                db.InsertHelperDetails(hmdtl);
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}