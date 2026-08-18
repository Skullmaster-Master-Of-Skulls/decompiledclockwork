using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using ClockWorkLogger;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.Controllers
{
	// Token: 0x02000154 RID: 340
	[NoCache]
	public class AccountController : Controller
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x00048584 File Offset: 0x00046784
		[AllowAnonymous]
		public ActionResult Login(string returnUrl = "")
		{
			CWLogger.Logger.Debug(string.Format("AccountController::Login: {0}", returnUrl ?? "NULL"));
			bool flag = !string.IsNullOrEmpty(returnUrl);
			if (flag)
			{
				base.HttpContext.Session["gotourl"] = returnUrl;
			}
			return this.Redirect("~/user/test/login.aspx");
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000485E4 File Offset: 0x000467E4
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout(false);
			base.HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
			FormsAuthentication.SignOut();
			return base.RedirectToAction("Index", "Home");
		}
	}
}
