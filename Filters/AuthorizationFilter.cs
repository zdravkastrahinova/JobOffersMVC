using JobOffersMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobOffersMVC.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (AuthenticationService.LoggedUser != null)
            {
                context.HttpContext.Response.Redirect("/Users/List");
                context.Result = new EmptyResult();
            }
        }
    }
}
