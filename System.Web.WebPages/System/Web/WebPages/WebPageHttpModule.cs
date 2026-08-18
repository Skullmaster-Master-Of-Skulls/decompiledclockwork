using System;

namespace System.Web.WebPages
{
	// Token: 0x02000082 RID: 130
	internal class WebPageHttpModule : IHttpModule
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000CA22 File Offset: 0x0000AC22
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000CA29 File Offset: 0x0000AC29
		internal static bool AppStartExecuteCompleted { get; set; }

		// Token: 0x060003D6 RID: 982 RVA: 0x0000CA31 File Offset: 0x0000AC31
		public void Dispose()
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000CA33 File Offset: 0x0000AC33
		public void Init(HttpApplication application)
		{
			if (application.Context.Items[WebPageHttpModule._hasBeenRegisteredKey] != null)
			{
				return;
			}
			application.Context.Items[WebPageHttpModule._hasBeenRegisteredKey] = true;
			WebPageHttpModule.InitApplication(application);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000CA6E File Offset: 0x0000AC6E
		internal static void InitApplication(HttpApplication application)
		{
			WebPageHttpModule.StartApplication(application);
			WebPageHttpModule.InitializeApplication(application);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		internal static void InitializeApplication(HttpApplication application)
		{
			WebPageHttpModule.InitializeApplication(application, new EventHandler(WebPageHttpModule.OnApplicationPostResolveRequestCache), WebPageHttpModule.Initialize);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CA98 File Offset: 0x0000AC98
		internal static void InitializeApplication(HttpApplication application, EventHandler onApplicationPostResolveRequestCache, EventHandler initialize)
		{
			if (initialize != null)
			{
				initialize(application, EventArgs.Empty);
			}
			application.PostResolveRequestCache += onApplicationPostResolveRequestCache;
			if (ApplicationStartPage.Exception != null || WebPageHttpModule.BeginRequest != null)
			{
				application.BeginRequest += WebPageHttpModule.OnBeginRequest;
			}
			application.EndRequest += WebPageHttpModule.OnEndRequest;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000CAED File Offset: 0x0000ACED
		internal static void StartApplication(HttpApplication application)
		{
			WebPageHttpModule.StartApplication(application, new Action<HttpApplication>(ApplicationStartPage.ExecuteStartPage), WebPageHttpModule.ApplicationStart);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000CB08 File Offset: 0x0000AD08
		internal static void StartApplication(HttpApplication application, Action<HttpApplication> executeStartPage, EventHandler applicationStart)
		{
			lock (WebPageHttpModule._appStartExecutedLock)
			{
				if (!WebPageHttpModule._appStartExecuted)
				{
					WebPageHttpModule._appStartExecuted = true;
					executeStartPage(application);
					WebPageHttpModule.AppStartExecuteCompleted = true;
					if (applicationStart != null)
					{
						applicationStart(application, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		internal static void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
		{
			HttpContextBase context = new HttpContextWrapper(((HttpApplication)sender).Context);
			new WebPageRoute().DoPostResolveRequestCache(context);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000CB95 File Offset: 0x0000AD95
		internal static void OnBeginRequest(object sender, EventArgs e)
		{
			if (ApplicationStartPage.Exception != null)
			{
				throw new HttpException(null, ApplicationStartPage.Exception);
			}
			if (WebPageHttpModule.BeginRequest != null)
			{
				WebPageHttpModule.BeginRequest(sender, e);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		internal static void OnEndRequest(object sender, EventArgs e)
		{
			if (WebPageHttpModule.EndRequest != null)
			{
				WebPageHttpModule.EndRequest(sender, e);
			}
			HttpApplication httpApplication = (HttpApplication)sender;
			RequestResourceTracker.DisposeResources(new HttpContextWrapper(httpApplication.Context));
		}

		// Token: 0x0400011B RID: 283
		internal static EventHandler Initialize;

		// Token: 0x0400011C RID: 284
		internal static EventHandler ApplicationStart;

		// Token: 0x0400011D RID: 285
		internal static EventHandler BeginRequest;

		// Token: 0x0400011E RID: 286
		internal static EventHandler EndRequest;

		// Token: 0x0400011F RID: 287
		private static bool _appStartExecuted = false;

		// Token: 0x04000120 RID: 288
		private static readonly object _appStartExecutedLock = new object();

		// Token: 0x04000121 RID: 289
		private static readonly object _hasBeenRegisteredKey = new object();
	}
}
