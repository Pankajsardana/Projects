using Microsoft.AspNetCore.Mvc.Filters;

namespace ePizzaHub.UI.Helper
{
    public class ActivityLogFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User;
            string conrollername = context.Controller.ToString();
            string actionName = context.ActionDescriptor.DisplayName;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
            var user = context.HttpContext.User;
            string conrollername = context.Controller.ToString();
            string actionName = context.ActionDescriptor.DisplayName;
        }
    }
}
