using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobOffersMVC.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("loggedUserId").HasValue)
            {
                context.HttpContext.Response.Redirect("/Users/List");
                context.Result = new EmptyResult();
            }
        }
    }
}
