using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class List_Header_listController : Controller
    {
        Database db = new Database();
        // GET: List_Header_list
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

                List<List_Header> AllList = db.List_Header_list();
                ViewBag.alllist = AllList;

                return View(menudisplay);
            }
        }
    }
}