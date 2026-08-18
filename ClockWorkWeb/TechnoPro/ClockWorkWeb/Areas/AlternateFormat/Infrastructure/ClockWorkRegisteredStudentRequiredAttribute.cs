using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoPro.ClockWorkWeb.Infrastructure;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure
{
	// Token: 0x02000183 RID: 387
	public class ClockWorkRegisteredStudentRequiredAttribute : AuthorizeAttribute
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x000498F0 File Offset: 0x00047AF0
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return LogonPerson.Instance.GetLogonStudentPersonId(httpContext.Session) > 0;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00049918 File Offset: 0x00047B18
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			bool flag = !filterContext.HttpContext.User.Identity.IsAuthenticated;
			if (flag)
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
			else
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
				{
					controller = "MessageHandler",
					action = "NotRegisteredClockWorkStudent"
				}));
			}
		}
	}
}
