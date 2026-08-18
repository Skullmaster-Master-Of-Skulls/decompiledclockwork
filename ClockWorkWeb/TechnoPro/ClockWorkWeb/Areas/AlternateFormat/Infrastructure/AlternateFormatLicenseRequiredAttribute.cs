using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure
{
	// Token: 0x02000182 RID: 386
	public class AlternateFormatLicenseRequiredAttribute : AuthorizeAttribute
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x000498B4 File Offset: 0x00047AB4
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return true;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000498C7 File Offset: 0x00047AC7
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
			{
				controller = "MessageHandler",
				action = "NotLicenseModule",
				group = Group.ALTERNATEFORMAT
			}));
		}
	}
}
