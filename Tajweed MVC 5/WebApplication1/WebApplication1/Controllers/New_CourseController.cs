using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;
using System.IO;

namespace WebApplication1.Controllers
{
    public class New_CourseController : Controller
    {
        Database db = new Database();
        // GET: New_Course
        public ActionResult Index()
        {
            if(Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();

                List<Teacher> tdp = db.Teacher_DropDown();
                ViewBag.Teachdropdown = tdp;

                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                return View(menudisplay);
            }
           
        }

        [HttpPost]
        public ActionResult Index(New_Course nc)
        {

            string status = "";
            List<Teacher> tdp = db.Teacher_DropDown();
            ViewBag.Teachdropdown = tdp;

            var path = "";
            var files = nc.file;

            if (nc.file != null)
            {
                if (files.ContentLength > 0)
                {
                    if (Path.GetExtension(files.FileName).ToLower() == ".jpg")
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Images"), files.FileName);
                        files.SaveAs(path);
                        nc.img = "~/Content/Images/" + files.FileName;
                        status = "Done";
                    }
                    else
                    {
                        status = "Format";
                    }
                }
            }
            else
            {
                nc.img = "N";
            }

            if (status == "Done")
            {
                db.InsertCourse(nc);
            }


            return new JsonResult { Data = new { status = status } };
        }
    }
}