using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class User_CreateController : Controller
    {
        Database db = new Database();
        // GET: User_Create
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            List<Role> tdp = db.Role_Dropdown();
            ViewBag.roledropdown = tdp;

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            return View(menudisplay);
        }

        [HttpPost]
        public ActionResult Index(User_Login ul)
        {
            bool status = false;

            db.Insert_User_login(ul);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}