using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DB;
using OfficeOpenXml;

namespace WebApplication1.Controllers
{
    public class MonthReport_ReportController : Controller
    {
        Database db = new Database();
        // GET: MonthReport_Report
        public ActionResult Index(string bh_id, DateTime? from_date, DateTime? to_date, string Courses_Nm)
        {
            string fromdate = from_date.ToString();
            string todate = to_date.ToString();
            if (from_date == null)
            {
                fromdate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }

            if (to_date == null)
            {
                todate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }

            //if (bh_id == null)
            //{
            //    bh_id ="1";
            //}

            ViewBag.From_date = fromdate;
            ViewBag.to_date = todate;
            ViewBag.courses_nm = Courses_Nm;
            ViewBag.bh_id = bh_id;

            List<Registor> Month = db.Month_wise_report(bh_id, fromdate, todate);
            ViewBag.Month = Month;




            return View();
        }

        public void ExportToExcel(string From_Date, string To_Date, string Batch_id, string batch)
        {
            List<Registor> Att_report = db.Month_wise_report(Batch_id, From_Date, To_Date);


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");


            ws.Cells["A1"].Value = "Tajweed Essential";

            ws.Cells["A2"].Value = "From Date :";
            ws.Cells["B2"].Value = From_Date;

            ws.Cells["C2"].Value = "To Date:";
            ws.Cells["D2"].Value = To_Date;

            ws.Cells["A3"].Value = "Course:";
            ws.Cells["B3"].Value = batch;

            ws.Cells["A6"].Value = "S/No.";
            ws.Cells["B6"].Value = "Student ID";
            ws.Cells["C6"].Value = "Student Name";
            ws.Cells["D6"].Value = "Days";
            ws.Cells["E6"].Value = "Present";
            ws.Cells["F6"].Value = "Abcent";
            ws.Cells["G6"].Value = "Leave";

            int rowStart = 7;
            int sno = 1;
            foreach (var item in Att_report)
            {

                ws.Cells[string.Format("A{0}", rowStart)].Value = sno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.User_id;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Full_Name;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Days;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Present;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Abcent;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Leave;
                rowStart++;
                sno++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Attendance_Report.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}