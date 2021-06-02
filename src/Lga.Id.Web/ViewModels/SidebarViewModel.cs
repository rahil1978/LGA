using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ViewModels
{

    public class SidebarViewModel
    {
        public string CurrentController { get; set; }
        public string CurrentAction { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDevelopment { get; set; }
        public bool IsOrderPlacer { get; set; }

    }
}
