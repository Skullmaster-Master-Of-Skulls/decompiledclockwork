using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ClockWorkLogger;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Startup;
using TechnoPro.Common.UI.ClientManager.Web.Core.Startup;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000018 RID: 24
	public class MvcApplication : HttpApplication
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003D38 File Offset: 0x00001F38
		protected void Application_Start()
		{
			this.RegisterRoutes(RouteTable.Routes);
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(new Action<HttpConfiguration>(WebApiConfig.Register));
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			this.RegisterCustomModelBinders();
			CWLogger.Logger.Info("Application_Start::Configuring IoC ...");
			ObjectFactory.Configure(new string[]
			{
				"Common.Public.dll",
				"Common.Core.dll",
				"Common.ClientManager.ClientCaching.dll",
				"Common.ClientManager.Core.dll",
				"Common.DAO.Impl.dll"
			});
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			if (clockWork != null)
			{
				clockWork.ExecuteNonQuery("SET ARITHABORT ON");
			}
			this.InitializeClockWorkWeb();
			base.Application.Add("Initialized", true);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E0A File Offset: 0x0000200A
		private void RegisterRoutes(RouteCollection routes)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003E10 File Offset: 0x00002010
		private void InitializeClockWorkSettings()
		{
			ClientCache.CurrentInstance.AuthenticationMode = eAuthenticationMode.PerSession;
			ClientCache.CurrentInstance.ApplicationContext = new ApplicationContext
			{
				ExecutingPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin")
			};
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("instancename");
			ClientCache.CurrentInstance.InstanceName = (string.IsNullOrEmpty(appSettingsByNameUsingProtection) ? "ClockWork" : appSettingsByNameUsingProtection);
			base.Application.Add("Initialized", true);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003E90 File Offset: 0x00002090
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			bool flag = base.Application["Initialized"] == null;
			if (flag)
			{
				this.InitializeClockWorkSettings();
			}
			bool flag2 = base.Application["Initialized"] == null;
			if (!flag2)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_AllowClockWorkInFrame);
				bool flag3 = !settingValue;
				if (flag3)
				{
					HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
				}
				bool flag4 = HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false);
				if (flag4)
				{
					base.Response.Redirect("https://" + base.Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003F88 File Offset: 0x00002188
		protected void Application_EndRequest(object sender, EventArgs e)
		{
			bool flag = base.Response.Cookies.Count > 0;
			if (flag)
			{
				foreach (string text in base.Response.Cookies.AllKeys)
				{
					bool flag2 = text == FormsAuthentication.FormsCookieName || text.ToUpper() == "CLOCKWORK5_WEB";
					if (flag2)
					{
						base.Response.Cookies[text].Secure = true;
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004018 File Offset: 0x00002218
		private void Session_Start(object sender, EventArgs e)
		{
			bool isSecureConnection = base.Request.IsSecureConnection;
			if (isSecureConnection)
			{
				base.Response.Cookies["CLOCKWORK5_WEB"].Secure = true;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004054 File Offset: 0x00002254
		protected void Application_Error(object sender, EventArgs e)
		{
			try
			{
				Exception baseException = base.Server.GetLastError().GetBaseException();
				try
				{
					CWLogger.Logger.ErrorException("Global", baseException);
				}
				catch
				{
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000040B4 File Offset: 0x000022B4
		private void RegisterCustomModelBinders()
		{
			ModelBinders.Binders.Add(typeof(PersonBaseDTO), new LogonStudentModelBinder());
			ModelBinders.Binders.Add(typeof(PendingRequestsCart), new AlternateFormatPendingRequestsModelBinder());
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000040EC File Offset: 0x000022EC
		private void InitializeClockWorkWeb()
		{
			CWLogger.Logger.Debug("ClockWorkWeb started ...");
			IStartupWebClientManager startupWebClientManager = new StartupWebClientManager();
			startupWebClientManager.Startup();
		}
	}
}
