using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoPro.ClockWorkWeb.Infrastructure;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.UI.ClientManager.Web.Core.ConfidentialityAgreement;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.ConfidentialityAgreement;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure
{
	// Token: 0x02000185 RID: 389
	public class AlternateFormatConfidentialityAgreementRequiredAttribute : AuthorizeAttribute
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00049A18 File Offset: 0x00047C18
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			int logonStudentPersonId = LogonPerson.Instance.GetLogonStudentPersonId(httpContext.Session);
			IStudentConfidentialityAgreementWebClientManager studentConfidentialityAgreementWebClientManager = new StudentConfidentialityAgreementWebClientManager(eClockWorkModules.Alternate_Format);
			return !studentConfidentialityAgreementWebClientManager.IsConfidentialityAgreementSigningRequired(logonStudentPersonId);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00049A4C File Offset: 0x00047C4C
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
					controller = "StudentConfidentialityAgreement",
					action = "Index",
					returnUrl = filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl)
				}));
			}
		}
	}
}
