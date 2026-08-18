using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000050E4 File Offset: 0x000032E4
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			CWLogger.Logger.Debug("CustomAuthorizeAttribute::HandleUnauthorizedRequest: checking if user is authenticated ...");
			CWLogger logger = CWLogger.Logger;
			string str = "CustomAuthorizeAttribute::HandleUnauthorizedRequest: User.Identity.IsAuthenticated = ";
			string text;
			if (filterContext == null)
			{
				text = null;
			}
			else
			{
				HttpContextBase httpContext = filterContext.HttpContext;
				if (httpContext == null)
				{
					text = null;
				}
				else
				{
					IPrincipal user = httpContext.User;
					if (user == null)
					{
						text = null;
					}
					else
					{
						IIdentity identity = user.Identity;
						text = ((identity != null) ? identity.IsAuthenticated.ToString() : null);
					}
				}
			}
			logger.Debug(str + (text ?? "NULL"));
			bool flag = !filterContext.HttpContext.User.Identity.IsAuthenticated;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string overrideLoginUrl = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl) ?? "";
				bool flag2 = new string[]
				{
					"",
					"login.aspx"
				}.Any((string g) => overrideLoginUrl.Equals(g, StringComparison.OrdinalIgnoreCase));
				bool flag3 = !flag2;
				if (flag3)
				{
					filterContext.Result = new RedirectResult(overrideLoginUrl, false);
				}
				else
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
					{
						{
							"action",
							"Login"
						},
						{
							"controller",
							"Account"
						},
						{
							"area",
							""
						},
						{
							"returnUrl",
							filterContext.HttpContext.Request.Url
						}
					});
				}
			}
			else
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005258 File Offset: 0x00003458
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			CWLogger.Logger.Debug("CustomAuthorizeAttribute::AuthorizeCore: checking if user is authenticated ...");
			CWLogger logger = CWLogger.Logger;
			string str = "CustomAuthorizeAttribute::AuthorizeCore: User.Identity.IsAuthenticated = ";
			string text;
			if (httpContext == null)
			{
				text = null;
			}
			else
			{
				IPrincipal user = httpContext.User;
				if (user == null)
				{
					text = null;
				}
				else
				{
					IIdentity identity = user.Identity;
					text = ((identity != null) ? identity.IsAuthenticated.ToString() : null);
				}
			}
			logger.Debug(str + (text ?? "NULL"));
			bool flag = !httpContext.User.Identity.IsAuthenticated;
			if (flag)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(null);
				CWLogger.Logger.Debug("CustomAuthorizeAttribute::AuthorizeCore: Is ClockWork Identity in session authenticated = " + (((currentClockWorkIdentity != null) ? currentClockWorkIdentity.IsAuthenticated.ToString() : null) ?? "NULL"));
				bool flag2 = currentClockWorkIdentity != null && currentClockWorkIdentity.IsAuthenticated;
				if (flag2)
				{
					CWLogger.Logger.Debug("CustomAuthorizeAttribute::AuthorizeCore: User.IdentityIsAuthenticated=false but clockwork session identity is set, so user is authenticated");
					return true;
				}
			}
			bool isAuthenticated = httpContext.User.Identity.IsAuthenticated;
			if (isAuthenticated)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager2 = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity2 = webAuthenticationAuthorizationWebClientManager2.GetCurrentClockWorkIdentity(null);
				bool flag3 = currentClockWorkIdentity2 == null || !currentClockWorkIdentity2.IsAuthenticated;
				if (flag3)
				{
					IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager3 = new WebAuthenticationAuthorizationWebClientManager();
					webAuthenticationAuthorizationWebClientManager3.LogoutFromClockWork();
				}
			}
			bool flag4 = !httpContext.User.Identity.IsAuthenticated;
			if (flag4)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_LoginFirstWithoutCredenntials);
				bool flag5 = settingValue;
				if (flag5)
				{
					IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager4 = new WebAuthenticationAuthorizationWebClientManager();
					webAuthenticationAuthorizationWebClientManager4.TryToAuthenticateUser("", "");
				}
			}
			return base.AuthorizeCore(httpContext);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000053F5 File Offset: 0x000035F5
		private void SetLastReturnUrl(HttpSessionStateBase session, Uri uri)
		{
			session.Remove("gotourl");
			session.Remove("gotoUri");
			session.Add("gotoUri", uri);
			CWLogger.Logger.Debug(string.Format("CustomAuthorizationAttribute:: gotoUri={0}", uri));
		}
	}
}
