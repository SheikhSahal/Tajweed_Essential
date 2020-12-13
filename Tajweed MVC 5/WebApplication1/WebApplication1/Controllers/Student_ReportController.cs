using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Student_ReportController : Controller
    {
        Database db = new Database();
        // GET: Student_Report
        public ActionResult Index(string Courses, string Student, string Coursesname)
        {

            List<Registor> reg = new List<Registor>();

            if (Student == "0" && Courses == "0")
            {
                reg = db.Student_Report("%", "%", "%", "%");
            }

            if (Courses == "0")
            {
                Courses = "%";
            }

            if (Student == "A")
            {
                reg = db.Student_Report("Y", "Y", "Y", Courses);
            }

            if (Student == "W")
            {
                reg = db.Student_Report("N", "N", "N", Courses);
            }

            if (Student == "I")
            {
                reg = db.Student_Report("Y", "%", "%", Courses);
            }

            if (Student == "B")
            {
                reg = db.Student_Report("%", "Y", "%", Courses);
            }

            if (Student == "G")
            {
                reg = db.Student_Report("%", "%", "Y", Courses);
            }


            if (Coursesname == "-Students-")
            {
                ViewBag.course = "ALL";
            }
            else
            {
                ViewBag.course = Coursesname;
            }


            ViewBag.reg = reg;


            return View();
        }
    }
}