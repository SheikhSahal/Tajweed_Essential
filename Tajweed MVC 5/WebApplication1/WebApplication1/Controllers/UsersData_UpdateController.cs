using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class UsersData_UpdateController : Controller
    {
        Database db = new Database();
        // GET: UsersData_Update
        public ActionResult Index(int id)
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

                TempData["Registor_bh_id"] = id;
                Registor userdata = db.Registor_get_stud_data(id);
                ViewBag.Userdata = userdata;
                return View(menudisplay);
            }
        }

        [HttpPost]
        public ActionResult Index(Registor r)
        {
            bool status = false;
            db.RegistorUpdate(r);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
    }
}