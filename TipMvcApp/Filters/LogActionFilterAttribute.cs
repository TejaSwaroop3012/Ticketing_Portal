using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace TipMvcApp.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            RouteData rData = context.RouteData;
            var controller = rData.Values["Controller"];
            var action = rData.Values["action"];
            Debug.WriteLine($"The {action} Action in {controller} Controller is executing at Time{DateTime.Now}..");
        }
    }
}
