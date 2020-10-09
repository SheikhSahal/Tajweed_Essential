using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AP_Menu
    {
        public AP_Menu()
        {
            List = new List<AP_Menu>();
        }
        public int Menu_id { get; set; }
        public string Menu_name { get; set; }
        public string Menu_URL { get; set; }
        public string Menu_icon { get; set; }
        public int? Menu_parent_id { get; set; }
        public List<AP_Menu> List { get; set; }
        public List<AP_Menu> Menutree(List<AP_Menu> menulist, int? parentid)
        {
            return menulist.Where(x => x.Menu_parent_id == parentid).Select(
                x => new AP_Menu
                {
                    Menu_id = x.Menu_id,
                    Menu_URL = x.Menu_URL,
                    Menu_name = x.Menu_name,
                    Menu_icon = x.Menu_icon,
                    Menu_parent_id = x.Menu_parent_id,
                    List = Menutree(menulist, x.Menu_id)
                }).ToList();
        }
    }
}