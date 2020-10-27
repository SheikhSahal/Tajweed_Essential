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

        public ActionResult Index(int id)
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

                List<Registor> Reg = db.fatch_students_list(id);
                ViewBag.userdata = Reg;



                return View(menudisplay);
            }
            //string status = null;
            //if (Session["User_id"] == null)
            //{
            //    status = "usernull";
            //}
            //else
            //{
            //    if (Convert.ToInt32(Session["Role_id"]) == 1)
            //    {
            //        status = "done";
            //    }
            //    else
            //    {
            //        status = "usernotrole";
            //    }
            //}

            //if (status == "usernull")
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else if (status == "usernotrole")
            //{
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    return View(menudisplay);
            //}
        }



        public ActionResult Approved(int id, int bh_id)
        {
            db.Approveduser(id);
            return RedirectToAction("Index","User_status",new { id = bh_id });
        }

        public ActionResult DeleteUser(int id, int bh_id)
        {
            db.DeleteUser(id, bh_id);

            return RedirectToAction("Index", "User_status", new { id = bh_id });
        }
    }
}