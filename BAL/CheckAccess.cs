using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bus_Ticket_Booking_Management_System.BAL
{
    #region CheckAccess
    public class CheckAccess : ActionFilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("Username") == null )
                filterContext.Result=new RedirectResult("~/Login/LoginPage");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revaliadte";
            context.HttpContext.Response.Headers["Expires"] = "1";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(context);
        }
    }
    #endregion
}
