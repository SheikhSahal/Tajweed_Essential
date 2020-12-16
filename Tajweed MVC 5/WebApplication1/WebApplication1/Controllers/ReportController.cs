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
    public class ReportController : Controller
    {
        // GET: Report
        Database db = new Database();
        public ActionResult Index(DateTime? From_Date, DateTime? To_Date, string Batch_Name, string stud_id)
        {

            if (Batch_Name == "0")
            {
                ViewBag.att_report_batch_name = "ALL";
                ViewBag.att_report_ed_date = "ALL";
            }
            else
            {
                Attendance_data Att_header = db.Get_report_header(Batch_Name);
                ViewBag.att_report_batch_name = Att_header.Batch_name;
                //ViewBag.att_report_ed_date = Att_header.Bh_end_date;
            }

            if (From_Date == null)
            {
                From_Date = DateTime.Now.Date;
            }

            if (To_Date == null)
            {
                To_Date = DateTime.Now.Date;
            }

            if (Batch_Name == "0")
            {
                Batch_Name = "%";
            }

            if (stud_id == "0")
            {
                stud_id = "%";
            }
            ViewBag.From_date = From_Date;
            ViewBag.to_date = To_Date;
            ViewBag.Batch_Name = Batch_Name;
            ViewBag.stud_id = stud_id;

            List<Att_Report> Att_report = db.report_att_present(Batch_Name, From_Date, To_Date, stud_id);
            ViewBag.att_report = Att_report;
            return View();
        }


        public void ExportToExcel(DateTime? From_Date, DateTime? To_Date, string Batch_Name, string stud_id, string batch)
        {
            List<Att_Report> Att_report = db.report_att_present(Batch_Name, From_Date, To_Date, stud_id);


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
            ws.Cells["C6"].Value = "Attendance Date";
            ws.Cells["D6"].Value = "Student Name";
            ws.Cells["E6"].Value = "Attendance Status";

            int rowStart = 7;
            int sno = 1;
            foreach (var item in Att_report)
            {

                ws.Cells[string.Format("A{0}", rowStart)].Value = sno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.stud_id;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.created_date;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Stud_name;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.att_status;
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