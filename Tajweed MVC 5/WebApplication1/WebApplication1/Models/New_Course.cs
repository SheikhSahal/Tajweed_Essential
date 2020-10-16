using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class New_Course
    {
        public int bh_id { get; set; }
        public string BATCH_NAME { get; set; }
        public int Teacher_1 { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string Delete_flag { get; set; }
        public DateTime bh_end_date { get; set; }
        public DateTime bh_start_date { get; set; }
        public int Teacher_2 { get; set; }
        public int Teacher_3 { get; set; }
        public int Teacher_4 { get; set; }
        public int Teacher_5 { get; set; }
        public string Course_desc { get; set; }
        public string Teach_name { get; set; }
    }
}