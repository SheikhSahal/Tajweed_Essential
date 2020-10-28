using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class UsersData_ListController : Controller
    {
        Database db = new Database();
        // GET: Users_update
        public ActionResult Index()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            else
            {
                List<Batch_list> bhlist = db.Batchfetchdetail();
                ViewBag.batchlist = bhlist;
                AP_Menu menu = new AP_Menu();

                var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
                List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

                List<Registor> Reg = db.Fetch_all_students_details();
                ViewBag.userdata = Reg;

                return View(menudisplay);
            }
        }
    }
}