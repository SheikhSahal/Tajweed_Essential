using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Attendance_mst
    {
        public int att_id { get; set; }
        public int bh_id { get; set; }
        public string att_pass { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        
    }
}