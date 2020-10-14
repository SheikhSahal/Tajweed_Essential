using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        //public ActionResult Index(DateTime From_Date, DateTime To_Date, int Batch_Name)
        public ActionResult Index()
        {
            return View();
        }
    }
}