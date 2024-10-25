using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Karyon.Controllers
{
    public class AssociateBaseController : Controller
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {
            // code involving this.Session // edited to simplify

            // If the browser session or authentication session has expired...
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerId")))
            {
               
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Login", Controller = "Home" }));
                
            }
               
            else
            {
                if (HttpContext.Session.GetString("CustomerId") == "0")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Login", Controller = "Home" }));
                }
            }
            base.OnActionExecuting(filterContext); // re-added in edit
        }

    }
}
