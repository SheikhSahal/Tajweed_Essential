using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class User_Create_ListController : Controller
    {
        Database db = new Database();
        // GET: User_Create_List
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                AP_Menu menu = new AP_Menu();

                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                List<Registor> all_users = db.All_users();
                ViewBag.all_users = all_users;

                return View(menudisplay);
            }
        }

        [HttpPost]
        public ActionResult Index(int usr_id, string user_active)
        {
            bool status = false;

            db.Userupdate(usr_id,user_active);
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
    }
}