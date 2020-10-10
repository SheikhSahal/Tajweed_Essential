using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Helper_list
    {
        public int hlp_id { get; set; }
        public int bh_id { get; set; }
        public string batch_name { get; set; }
        public string Helper { get; set; }
        public int stud_enroll { get; set; }
        public DateTime bh_end_date { get; set; }
    }
}