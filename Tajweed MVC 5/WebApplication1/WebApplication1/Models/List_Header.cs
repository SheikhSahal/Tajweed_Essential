﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class List_Header
    {
        public int id { get; set; }
        public int List_id { get; set; }
        public string List_name { get; set; }
        public string stud_count { get; set; }

        public virtual ICollection<List_Details> List_Details { get; set; }

    }
}