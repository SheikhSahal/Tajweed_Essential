using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Batch_header
    {
        public int Bh_id { get; set; }
        public string Batch_Name { get; set; }
        public int Teacher { get; set; }
        public int Volunteer { get; set; }
        public string Zoom { get; set; }
        public DateTime course_end_date { get; set; }

        public DateTime Att_date { get; set; }
        public int att_id { get; set; }

        public virtual ICollection<Batch_details> Batch_details { get; set; }
    }
}