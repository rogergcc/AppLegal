using AppLegal.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class MenuItem : BaseModel
    {
        public MenuItem()
        {
        }
        public string Icon { get; set; }
        public bool IsSeparatorVisible { get; set; }

        public MenuType MenuType { get; set; }
    }
}
