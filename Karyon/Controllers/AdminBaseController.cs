using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Karyon.Controllers
{
    public class AdminBaseController : Controller
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {
            // code involving this.Session // edited to simplify

            // If the browser session or authentication session has expired...
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
               
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "AdminLogin", Controller = "Home" }));
                
            }
               
            else
            {
                if (HttpContext.Session.GetString("AdminId") == "0")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "AdminLogin", Controller = "Home" }));
                }
            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
