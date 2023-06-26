using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebPhimBTL.Models.Authentication
{
	public class Authentication : ActionFilterAttribute
	{
		private readonly string allowedRoles;

		public Authentication(string roles)
		{
			allowedRoles = roles;
		}
		public override void OnActionExecuting(ActionExecutingContext context)
		{


			if (context.HttpContext.Session.GetString(allowedRoles) == null)
			{
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary {
					{"controller","LogIn"},
					{"action","login"},
				});
			}


			base.OnActionExecuting(context);

		}
	}
}
