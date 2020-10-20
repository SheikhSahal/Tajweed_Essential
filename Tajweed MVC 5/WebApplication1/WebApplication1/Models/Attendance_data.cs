using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Attendance_data
    {
        public int att_id { get; set; }
        public int BH_id { get; set; }
        public string Batch_name { get; set; }
        public string Teach_name { get; set; }
        public DateTime Bh_end_date { get; set; }
        public DateTime att_created { get; set; }
        public string Att_status { get; set; }
        public string stud_id { get; set; }
        public string Att_pass { get; set; }

        public virtual ICollection<Attendance_dtl> Attendance_dtl { get; set; }

    }
}