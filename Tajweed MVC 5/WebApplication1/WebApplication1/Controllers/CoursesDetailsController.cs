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
            List<New_Course> course_cart = db.Courses_Cart();
            ViewBag.courselist = course_cart;

            return View();
        }
    }
}