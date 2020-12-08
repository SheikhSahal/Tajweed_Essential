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


        public ActionResult Edit(int id)
        {

            AP_Menu menu = new AP_Menu();
            var Menulist = db.user_rights(Convert.ToInt32(Session["User_id"]));
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Student> Student_list = db.Get_edit_student_list(id);
            ViewBag.student = Student_list;

            List_Header getheader= db.list_get_Headerdata(id);
            ViewBag.getHeader = getheader;

            List<List_Details> getdtl = db.List_get_detaildata(id);
            ViewBag.getdtllist = getdtl;

            ViewBag.id = id;
            return View(menudisplay);
        }

        [HttpPost]
        public ActionResult Edit(List_Header lh)
        {
            bool status = false;
            db.update_listName(lh.List_name, lh.List_id);

            foreach (var listdt in lh.List_Details)
            {
                listdt.List_id = lh.List_id;
                db.InsertList_Detail(listdt);
            }
            status = true;
            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult Dle_list(int id)
        {
            bool status = false;
            db.delteList_dtl(id);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Delete_bulk(int id)
        {
            db.listMstdelete(id);
            db.listDtlmstdelete(id);


            return RedirectToAction("Index", "List_Header_list");
        }

    }
}