using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_Helper_ListController : Controller
    {
        Database db = new Database();
        // GET: New_Helper_List
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                List<Helper_mst> hm = db.Helpers_list();
                ViewBag.hlp_list = hm;

                AP_Menu menu = new AP_Menu();
                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                return View(menudisplay);
            }
        }
    }
}