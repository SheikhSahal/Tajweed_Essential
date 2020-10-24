using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Helper_mst
    {
        public int Hpl_id { get; set; }
        public int bh_id { get; set; }
        public int stu_id { get; set; }
        public int list_id { get; set; }
        public string created_by{ get; set; }
        public DateTime created_date{ get; set; }
        public string Helper_name { get; set; }

        public virtual ICollection<Helper_dtl> Helper_dtl { get; set; }
    }
}