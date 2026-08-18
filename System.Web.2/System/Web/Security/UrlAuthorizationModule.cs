using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Management;

namespace System.Web.Security
{
	// Token: 0x020005F9 RID: 1529
	public sealed class UrlAuthorizationModule : IHttpModule
	{
		// Token: 0x06004D58 RID: 19800 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public UrlAuthorizationModule()
		{
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0010CC24 File Offset: 0x0010AE24
		public void Init(HttpApplication app)
		{
			app.AuthorizeRequest += this.OnEnter;
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0010CC38 File Offset: 0x0010AE38
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static bool CheckUrlAccessForPrincipal(string virtualPath, IPrincipal user, string verb)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			verb = verb.Trim();
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			if (!virtualPath2.IsWithinAppRoot)
			{
				throw new ArgumentException(SR.GetString("Virtual_path_outside_application_not_supported"), "virtualPath");
			}
			if (!UrlAuthorizationModule.s_EnabledDetermined)
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					HttpModulesSection httpModules = RuntimeConfig.GetConfig().HttpModules;
					int count = httpModules.Modules.Count;
					for (int i = 0; i < count; i++)
					{
						HttpModuleAction httpModuleAction = httpModules.Modules[i];
						if (Type.GetType(httpModuleAction.Type, false) == typeof(UrlAuthorizationModule))
						{
							UrlAuthorizationModule.s_Enabled = true;
							break;
						}
					}
				}
				else
				{
					List<ModuleConfigurationInfo> integratedModuleList = HttpApplication.IntegratedModuleList;
					foreach (ModuleConfigurationInfo moduleConfigurationInfo in integratedModuleList)
					{
						if (Type.GetType(moduleConfigurationInfo.Type, false) == typeof(UrlAuthorizationModule))
						{
							UrlAuthorizationModule.s_Enabled = true;
							break;
						}
					}
				}
				UrlAuthorizationModule.s_EnabledDetermined = true;
			}
			if (!UrlAuthorizationModule.s_Enabled)
			{
				return true;
			}
			AuthorizationSection authorization = RuntimeConfig.GetConfig(virtualPath2).Authorization;
			return authorization.EveryoneAllowed || authorization.IsUserAllowed(user, verb);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0010CDA4 File Offset: 0x0010AFA4
		internal static void ReportUrlAuthorizationFailure(HttpContext context, object webEventSource)
		{
			context.Response.StatusCode = 401;
			UrlAuthorizationModule.WriteErrorMessage(context);
			if (context.User != null && context.User.Identity.IsAuthenticated)
			{
				WebBaseEvent.RaiseSystemEvent(webEventSource, 4007);
			}
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0010CDF8 File Offset: 0x0010AFF8
		private void OnEnter(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.SkipAuthorization)
			{
				if (context.User == null || !context.User.Identity.IsAuthenticated)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.ANONYMOUS_REQUESTS);
				}
				return;
			}
			AuthorizationSection authorization = RuntimeConfig.GetConfig(context).Authorization;
			if (!authorization.EveryoneAllowed && !authorization.IsUserAllowed(context.User, context.Request.RequestType))
			{
				UrlAuthorizationModule.ReportUrlAuthorizationFailure(context, this);
				return;
			}
			if (context.User == null || !context.User.Identity.IsAuthenticated)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.ANONYMOUS_REQUESTS);
			}
			WebBaseEvent.RaiseSystemEvent(this, 4003);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0010CE9E File Offset: 0x0010B09E
		private static void WriteErrorMessage(HttpContext context)
		{
			context.Response.Write(UrlAuthFailedErrorFormatter.GetErrorText());
			context.Response.GenerateResponseHeadersForHandler();
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0010CEBC File Offset: 0x0010B0BC
		internal static bool RequestRequiresAuthorization(HttpContext context)
		{
			if (context.SkipAuthorization)
			{
				return false;
			}
			AuthorizationSection authorization = RuntimeConfig.GetConfig(context).Authorization;
			if (UrlAuthorizationModule._AnonUser == null)
			{
				UrlAuthorizationModule._AnonUser = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]);
			}
			return !authorization.IsUserAllowed(UrlAuthorizationModule._AnonUser, context.Request.RequestType);
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x0010CF20 File Offset: 0x0010B120
		internal static bool IsUserAllowedToPath(HttpContext context, VirtualPath virtualPath)
		{
			AuthorizationSection authorization = RuntimeConfig.GetConfig(context, virtualPath).Authorization;
			return authorization.EveryoneAllowed || authorization.IsUserAllowed(context.User, context.Request.RequestType);
		}

		// Token: 0x0400294D RID: 10573
		private static bool s_EnabledDetermined;

		// Token: 0x0400294E RID: 10574
		private static bool s_Enabled;

		// Token: 0x0400294F RID: 10575
		private static GenericPrincipal _AnonUser;
	}
}
