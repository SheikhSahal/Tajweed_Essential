using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Tajweed;

namespace Tajweed.Controllers
{
    public class Login : Controller
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
