using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Tajweed;

namespace Tajweed.Controllers
{
    public class Registration : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        //const string SessionLogin = "_Status";
        //const string StatusLogin = "_login";
        string connectString = Database_connecting.connectString;

        public IActionResult Index()
        {
            return View();
        }
    }
}
