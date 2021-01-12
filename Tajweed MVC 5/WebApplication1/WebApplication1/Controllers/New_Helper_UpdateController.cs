using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_Helper_UpdateController : Controller
    {
        Database db = new Database();
        // GET: New_Helper_Update
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

                Helper_mst hm = db.Hlp_mst_data(id);
                ViewBag.hlphd = hm;


                List<Helper_dtl> hd = db.Helpers_Details_list(id);
                ViewBag.hlpdt = hd;


                List<Helper_dtl> stud_list = db.List_dropdown(hm.list_id);
                ViewBag.hlpdt_list = stud_list;


                List<Batch_header> Course_dropdown = db.get_Course_dropdown();
                ViewBag.course = Course_dropdown;

                List<List_Header> List_dropdown = db.get_list_dropdown();
                ViewBag.list = List_dropdown;

                return View(menudisplay);
            }
        }


        [HttpPost]
        public ActionResult Index(Helper_mst hmst)
        {
            Helper_mst hm = db.Hlp_mst_data(hmst.Hpl_id);
            ViewBag.hlphd = hm;


            List<Helper_dtl> hd = db.Helpers_Details_list(hmst.Hpl_id);
            ViewBag.hlpdt = hd;


            List<Helper_dtl> stud_list = db.List_dropdown(hm.list_id);
            ViewBag.hlpdt_list = stud_list;


            List<Batch_header> Course_dropdown = db.get_Course_dropdown();
            ViewBag.course = Course_dropdown;

            List<List_Header> List_dropdown = db.get_list_dropdown();
            ViewBag.list = List_dropdown;

            bool status = false;


            db.Update_hlp_header(hmst.Helper_name, hmst.Hpl_id);

            foreach (var s in hmst.Helper_dtl)
            {
                s.Hpl_id = hmst.Hpl_id;
                db.Update_hlp_dtl(s);
            }

            status = true;
            return new JsonResult { Data = new { status = status } };
        }
    }
}