using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class User_StatusController : Controller
    {
        Database db = new Database();

        public ActionResult Index()
        {
            List<Batch_list> bhlist = db.Batchfetchdetail();
            ViewBag.batchlist = bhlist;

            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> bh = db.Course_DropDown();
            ViewBag.cordropdown = bh;

            List<Registor> Reg = db.Userfetchdetail();
            ViewBag.userdata = Reg;

            string status = null;
            if (Session["User_id"] == null)
            {
                status = "usernull";
            }
            else
            {
                if (Convert.ToInt32(Session["Role_id"]) == 1)
                {
                    status = "done";
                }
                else
                {
                    status = "usernotrole";
                }
            }

            if (status == "usernull")
            {
                return RedirectToAction("Index", "Login");
            }
            else if (status == "usernotrole")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View(menudisplay);
            }
        }



        public ActionResult Approved(int id)
        {
            db.Approveduser(id);

            return RedirectToAction("Index","User_status");
        }

        public ActionResult DeleteUser(int id)
        {
            db.DeleteUser(id);

            return RedirectToAction("Index", "User_status");
        }
    }
}