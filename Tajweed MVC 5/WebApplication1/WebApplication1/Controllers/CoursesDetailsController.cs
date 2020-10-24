using System;
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
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                List<New_Course> course_cart = db.Courses_Cart();
                ViewBag.courselist = course_cart;

                List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                ViewBag.course = Course_dropdown;

                return View();
            }
        }


        public ActionResult check_user_status(int bh_id,string IDCardno)
        {
            string status = null;
            var user_status = db.user_check_status(bh_id, IDCardno);

            if(user_status.User_status == "W")
            {
                status = "W";
            }
            else if (user_status.User_status == "A")
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