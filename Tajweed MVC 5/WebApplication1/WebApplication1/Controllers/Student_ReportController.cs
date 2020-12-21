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
    public class Student_ReportController : Controller
    {
        Database db = new Database();
        // GET: Student_Report
        public ActionResult Index(string Courses, string Student, string Coursesname)
        {

            List<Registor> reg = new List<Registor>();

            ViewBag.courses = Courses;
            ViewBag.Student = Student;
            ViewBag.Coursesname = Coursesname;

            if (Student == "0" && Courses == "0")
            {
                reg = db.Student_Report("%", "%", "%", "%");
            }

            if (Courses == "0")
            {
                Courses = "%";
            }

            if (Student == "A")
            {
                reg = db.Student_Report("Y", "Y", "Y", Courses);
            }

            if (Student == "W")
            {
                reg = db.Student_Report("N", "N", "N", Courses);
            }

            if (Student == "I")
            {
                reg = db.Student_Report("Y", "%", "%", Courses);
            }

            if (Student == "B")
            {
                reg = db.Student_Report("%", "Y", "%", Courses);
            }

            if (Student == "G")
            {
                reg = db.Student_Report("%", "%", "Y", Courses);
            }


            if (Coursesname == "-Students-")
            {
                ViewBag.course = "ALL";
            }
            else
            {
                ViewBag.course = Coursesname;
            }

            
           

            ViewBag.reg = reg;


            return View();
        }

        public void ExportToExcel(string Courses, string Student, string Coursesname)
        {

            List<Registor> reg = new List<Registor>();

            ViewBag.courses = Courses;
            ViewBag.Student = Student;
            ViewBag.Coursesname = Coursesname;

            if (Student == "0" && Courses == "0")
            {
                reg = db.Student_Report("%", "%", "%", "%");
            }

            if (Courses == "0")
            {
                Courses = "%";
            }

            if (Student == "A")
            {
                reg = db.Student_Report("Y", "Y", "Y", Courses);
            }

            if (Student == "W")
            {
                reg = db.Student_Report("N", "N", "N", Courses);
            }

            if (Student == "I")
            {
                reg = db.Student_Report("Y", "%", "%", Courses);
            }

            if (Student == "B")
            {
                reg = db.Student_Report("%", "Y", "%", Courses);
            }

            if (Student == "G")
            {
                reg = db.Student_Report("%", "%", "Y", Courses);
            }


            if (Coursesname == "-Students-")
            {
                Coursesname = "ALL";
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");


            ws.Cells["A1"].Value = "Tajweed Essential";

            ws.Cells["A2"].Value = "Student Report";
            //ws.Cells["B2"].Value = From_Date;

            //ws.Cells["C2"].Value = "To Date:";
            //ws.Cells["D2"].Value = To_Date;

            ws.Cells["A3"].Value = "Course Name:";
            ws.Cells["B3"].Value = Coursesname;

            ws.Cells["A6"].Value = "S/No.";
            ws.Cells["B6"].Value = "Student Name";
            ws.Cells["C6"].Value = "E-mail";
            ws.Cells["D6"].Value = "Contact";
            ws.Cells["E6"].Value = "Date of Birth";
            ws.Cells["F6"].Value = "ID Card";
            ws.Cells["G6"].Value = "Recommended";
            ws.Cells["H6"].Value = "Country";


            int rowStart = 7;
            int sno = 1;
            foreach (var item in reg)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = sno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Full_Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.email;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.contact;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.DOB;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.IDCardNo;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.recommended;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Country;
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