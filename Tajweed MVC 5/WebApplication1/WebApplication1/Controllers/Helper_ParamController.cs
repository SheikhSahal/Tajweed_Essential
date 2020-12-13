using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class Helper_ParamController : Controller
    {
        Database db = new Database();
        // GET: Helper_Param
        public ActionResult Index(string bh_id, string hlp_id, string Courses_text)
        {

            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {


                if (bh_id == "0")
                {
                    bh_id = "%";
                }

                if (hlp_id == "0")
                {
                    hlp_id = "%";
                }

                if (Courses_text == "-Courses-")
                {
                    ViewBag.Courses_text = "ALL";
                }
                else
                {
                    ViewBag.Courses_text = Courses_text;
                }

                List<Helper_dtl> hlpdtl = db.Report_Helper_details(hlp_id, bh_id);
                ViewBag.helperdtl = hlpdtl;
            }

            return View();
        }
    }
}