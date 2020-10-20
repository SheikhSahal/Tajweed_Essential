﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_AttendanceController : Controller
    {
        Database db = new Database();
        // GET: New_Attendance
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            var Menulist = db.user_rights(1030);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            return View(menudisplay);
        }
    }
}