using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        Database db = new Database();
        public ActionResult Index(DateTime From_Date, DateTime To_Date, int Batch_Name)
        {
            ViewBag.From_date = From_Date;
            ViewBag.to_date = To_Date;

            Attendance_data Att_header = db.Get_report_header(Batch_Name);
            ViewBag.att_report_batch_name = Att_header.Batch_name;
            ViewBag.att_report_ed_date = Att_header.Bh_end_date;

            List<Att_Report> Att_report = db.report_att_present(Batch_Name,From_Date,To_Date);
            ViewBag.att_report = Att_report;



            return View();
        }
    }
}