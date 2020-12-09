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

        public ActionResult Index(int id , string bh_name)
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

                ViewBag.Batch_name = bh_name;


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



        //public ActionResult Approved(int id, int bh_id, string bh_name)
        //{
        //    db.Approveduser(id);
        //    return RedirectToAction("Index","User_status",new { id = bh_id , bh_name = bh_name });
        //}

        public ActionResult DeleteUser(int id, int bh_id, string bh_name)
        {
            db.DeleteUser(id);

            return RedirectToAction("Index", "User_status", new { id = bh_id, bh_name = bh_name });
        }

        public ActionResult Interview(int Userid,string Interview)
        {
            bool status = false;
            db.User_interview(Userid, Interview);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Books(int Userid, string Books)
        {
            bool status = false;
            db.Usr_stat_pur_books(Userid, Books);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Group(int Userid, string Group)
        {
            bool status = false;
            db.Usr_stat_Group(Userid, Group);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}