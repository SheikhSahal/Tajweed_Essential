﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class CoursesDetailsController : Controller
    {
        Database db = new Database();
        // GET: CoursesDetails
        public ActionResult Index()
        {
           
                List<New_Course> course_cart = db.Courses_Cart();
                ViewBag.courselist = course_cart;

                List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                ViewBag.course = Course_dropdown;

                return View();
            
        }


        public ActionResult check_user_status(int bh_id,string IDCardno)
        {
            string status = null;
            var user_status = db.user_check_status(bh_id, IDCardno);

            if(user_status.User_status == "W")
            {
                status = "W";
            }
            else if (user_status.Usr_stat_intview == "Y" && user_status.Usr_stat_Group == "Y" && user_status.Usr_stat_pur_books == "Y")
            {
                    status = "A";
            }
            else
            {
                status = "N";
            }


            return new JsonResult { Data = new { status = status } };
        }
    }
}