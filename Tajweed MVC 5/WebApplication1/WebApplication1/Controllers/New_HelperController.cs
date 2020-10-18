using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class New_HelperController : Controller
    {
        Database db = new Database();
        // GET: New_Helper
        public ActionResult Index()
        {
            AP_Menu menu = new AP_Menu();

            var Menulist = db.user_rights(1030);
            List<AP_Menu> menudisplay = menu.Menutree(Menulist, null);

            List<Batch_header> Course_dropdown = db.get_Course_dropdown();
            ViewBag.course = Course_dropdown;

            List<List_Header> List_dropdown = db.get_list_dropdown();
            ViewBag.list = List_dropdown;


            return View(menudisplay);
        }

        //public ActionResult Cas_stud_id(int id)
        //{
        //    List<Student> casc_std_list = db.get_Cascade_list_student(id);
        //    return Json(new SelectList(casc_std_list, "Stud_id", "Stud_name"));
        //}

        public ActionResult Cascade_stud(int id, string flag)
        {
            List<Student> Cas_stu = new List<Student>();
            if(flag == "A")
            {
                List<Student> casc_std = db.get_Cascade_student(id);
                Cas_stu = casc_std;
            }
            else
            {
                List<Student> casc_std_list = db.get_Cascade_list_student(id);
                Cas_stu = casc_std_list;
            }

           
            return Json( new SelectList(Cas_stu, "Stud_id", "Stud_name"));
        }

     
    }
}