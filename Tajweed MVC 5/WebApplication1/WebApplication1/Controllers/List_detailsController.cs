using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class List_detailsController : Controller
    {
        Database db = new Database();
        // GET: List_details
        public ActionResult Index(int id)
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

                List<List_Details> listdetails = db.List_Details_data(id);
                ViewBag.listdtl = listdetails;
                return View(menudisplay);
            }
        }

        
        public ActionResult Delstdlist(int id, int list_id)
        {
            db.listdtldelete(id, list_id);
            return RedirectToAction("Index","list_details", new { id=list_id });
        }
    }
}