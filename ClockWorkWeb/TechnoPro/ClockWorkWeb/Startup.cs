using System;
using System.IdentityModel.Claims;
using System.Web.Helpers;
using ClockWorkLogger;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000013 RID: 19
	public class Startup
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003B04 File Offset: 0x00001D04
		public void ConfigureAuth(IAppBuilder app)
		{
			app.CreatePerOwinContext(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
			app.CreatePerOwinContext(new Func<IdentityFactoryOptions<ApplicationSignInManager>, IOwinContext, ApplicationSignInManager>(ApplicationSignInManager.Create));
			CookieAuthenticationOptions cookieAuthenticationOptions = new CookieAuthenticationOptions();
			cookieAuthenticationOptions.AuthenticationType = "ApplicationCookie";
			cookieAuthenticationOptions.CookieName = "CLOCKWORK5_WEB";
			cookieAuthenticationOptions.LoginPath = new PathString("/Account/Login");
			CookieAuthenticationProvider cookieAuthenticationProvider = new CookieAuthenticationProvider();
			cookieAuthenticationProvider.OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ClockWorkApplicationUser>(TimeSpan.FromMinutes(30.0), (ApplicationUserManager manager, ClockWorkApplicationUser user) => user.GenerateUserIdentityAsync(manager));
			cookieAuthenticationOptions.Provider = cookieAuthenticationProvider;
			app.UseCookieAuthentication(cookieAuthenticationOptions);
			AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public void Configuration(IAppBuilder app)
		{
			CWLogger.Logger.Info("Startup::Configuration::IoC ...");
			ObjectFactory.Configure(new string[]
			{
				"Common.Public.dll",
				"Common.Core.dll",
				"Common.ClientManager.ClientCaching.dll",
				"Common.ClientManager.Core.dll",
				"Common.DAO.Impl.dll"
			});
			this.ConfigureAuth(app);
		}
	}
}
