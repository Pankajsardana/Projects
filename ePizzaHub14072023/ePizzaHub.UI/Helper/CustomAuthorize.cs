using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ePizzaHub.UI.Helper
{
    public class CustomAuthorize :Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Check Authentication
           if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                //to do Get userroles from DB


                //Check Authorization
                if (!context.HttpContext.User.IsInRole(Roles)) 
                {
                    context.Result = new RedirectToActionResult("unauthorize", "Account", new { area = "" });
                }

            }
            else 
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}
