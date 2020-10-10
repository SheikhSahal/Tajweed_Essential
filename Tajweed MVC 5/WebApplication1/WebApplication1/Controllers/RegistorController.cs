using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegistorController : Controller
    {
        Database db = new Database();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Registor r)
        {
            bool status = false;
            db.Registration(r);
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
    }
}