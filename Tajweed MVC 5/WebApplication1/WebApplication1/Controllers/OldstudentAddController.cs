using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class OldstudentAddController : Controller
    {
        // GET: OldstudentAdd
        Database db = new Database();
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else if (Session["Role_id"].ToString() == "2")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                AP_Menu menu = new AP_Menu();
                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                //List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                //ViewBag.course = Course_dropdown;

                return View(menudisplay);
            }
        }

        [HttpPost]
        public ActionResult Index(int bh_id, string Full_Name, string IDCardNo, string M_W_no)
        {
            bool status = false;
            db.oldstudentRegistration(bh_id, Full_Name, IDCardNo, M_W_no);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}