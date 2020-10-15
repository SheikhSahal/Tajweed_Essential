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
            ViewBag.Batch = Batch_Name;
            List<Att_Report> Att_report = db.report_att_present(Batch_Name,From_Date,To_Date);
            ViewBag.att_report = Att_report;

            List<Att_Report> Att_report_ab = db.report_att_abcent(Batch_Name);
            ViewBag.att_report_ab = Att_report_ab;


            return View();
        }
    }
}