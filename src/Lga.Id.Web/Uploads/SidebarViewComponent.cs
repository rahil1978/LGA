using Lga.Id.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;


namespace CCR.Web.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IPrincipal _principal;
        public SidebarViewComponent(IPrincipal principal)
        {
            _principal = principal;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var sideBarViewModel = new SidebarViewModel
            {
                CurrentController = ViewContext.RouteData.Values["Controller"]?.ToString(),
                CurrentAction = ViewContext.RouteData.Values["Action"]?.ToString(),
                IsAdmin = IsAdmin(_principal),
                
              
            };

            return View(sideBarViewModel);
        }

        public static bool IsAdmin(IPrincipal user)
        {
            return true; 
        }
    }
}
