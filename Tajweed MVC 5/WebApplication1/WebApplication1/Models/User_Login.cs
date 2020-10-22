using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User_Login
    {
        public string User_name { get; set; }
        public string Password { get; set; }
        public string User_email { get; set; }
        public int Role_id { get; set; }
    }
}
