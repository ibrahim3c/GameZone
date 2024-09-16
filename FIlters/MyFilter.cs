using Microsoft.AspNetCore.Mvc.Filters;

namespace GameZone.FIlters
{
    public class MyFilter : Attribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.ToString();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.ToString();

        }
    }
}
