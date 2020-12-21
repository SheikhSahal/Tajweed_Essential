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
    public class Helper_ParamController : Controller
    {
        Database db = new Database();
        // GET: Helper_Param
        public ActionResult Index(string bh_id, string hlp_id, string Courses_text)
        {

            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {


                if (bh_id == "0")
                {
                    bh_id = "%";
                }

                if (hlp_id == "0")
                {
                    hlp_id = "%";
                }

                if (Courses_text == "-Courses-")
                {
                    ViewBag.Courses_text = "ALL";
                }
                else
                {
                    ViewBag.Courses_text = Courses_text;
                }
                
                List<Helper_dtl> hlpdtl = db.Report_Helper_details(hlp_id, bh_id);
                ViewBag.bh_id = bh_id;
                ViewBag.hlp_id = hlp_id;
                ViewBag.helperdtl = hlpdtl;
            }

            return View();
        }

        //string bh_id, string hlp_id, string Courses_text
        public void ExportToExcel(string bh_id, string hlp_id, string Courses_text)
        {
            List<Helper_dtl> hlpdtl = db.Report_Helper_details(hlp_id, bh_id);


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");


            ws.Cells["A1"].Value = "Tajweed Essential";

            ws.Cells["A2"].Value = "Helper Report";

            ws.Cells["A3"].Value = "Course Name:";
            ws.Cells["B3"].Value = Courses_text;

            ws.Cells["A6"].Value = "S/No.";
            ws.Cells["B6"].Value = "Student Name";
            ws.Cells["C6"].Value = "Student Contact";
            ws.Cells["D6"].Value = "Helper Name";
            ws.Cells["E6"].Value = "Helper Contact";

            int rowStart = 7;
            int sno = 1;
            foreach (var item in hlpdtl)
            {

                ws.Cells[string.Format("A{0}", rowStart)].Value = sno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.stud_name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.stud_contact;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Hlper_name;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.hlper_contact;
                rowStart++;
                sno++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Helper_Report.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}