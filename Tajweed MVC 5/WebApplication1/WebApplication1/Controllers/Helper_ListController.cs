﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Helper_ListController : Controller
    {
        Database db = new Database();
        // GET: Batch_List
        public ActionResult Index()
        {
            List<Helper_list> hllist = db.Helperfetchdetail();
            ViewBag.helperlist = hllist;

            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

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

        public ActionResult DeleteRecord(int id)
        {
            db.Delete_Helper(id);
            return RedirectToAction("index", "Helper_List");
        }


        [HttpGet]
        public ActionResult updateHelper(int id)
        {
           var enddate_with_id = db.get_helper_Course_end_date(id);

            DateTime date = DateTime.Now;
            var current_date = date.Date;
            if (enddate_with_id.created_date < current_date)
            {
                return RedirectToAction("Index", "Helper_list");
            }
            else
            {

            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(15);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            TempData["mydata"] = id;
            Helper_mst hmdata = db.get_helper_Master_data(id);
            ViewBag.hmdata = hmdata;

            List<Helper_dtl> hddata = db.Get_helper_detail_data(id);
            ViewBag.hddata = hddata;

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            return View(menudisplay);
            }
        }


        

        [HttpPost]
        public ActionResult updateHelper(Helper_mst hm)
        {
            bool status = false;

            var BH_id = TempData["mydata"];
            hm.Hpl_id = Convert.ToInt32(BH_id);

            List<Student> sdp = db.Student_DropDown();
            ViewBag.stddropdown = sdp;

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            db.Update_Helper_master(hm);

            if (hm.Helper_dtl != null)
            {
                foreach (var hmdtl in hm.Helper_dtl)
                {
                    hmdtl.Hpl_id = Convert.ToInt32(BH_id);
                    db.InsertHelperDetails(hmdtl);
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
            db.hlpDelete(id, Convert.ToInt32(BH_id));
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}