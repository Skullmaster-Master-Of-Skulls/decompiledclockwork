using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.WebHost;
using System.Web.Http.WebHost.Routing;
using System.Web.Routing;

namespace System.Web.Http
{
	// Token: 0x0200001D RID: 29
	public static class GlobalConfiguration
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000479A File Offset: 0x0000299A
		public static HttpConfiguration Configuration
		{
			get
			{
				return GlobalConfiguration._configuration.Value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000047A6 File Offset: 0x000029A6
		public static HttpMessageHandler DefaultHandler
		{
			get
			{
				return GlobalConfiguration._defaultHandler.Value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000047B2 File Offset: 0x000029B2
		public static HttpServer DefaultServer
		{
			get
			{
				return GlobalConfiguration._defaultServer.Value;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047BE File Offset: 0x000029BE
		public static void Configure(Action<HttpConfiguration> configurationCallback)
		{
			if (configurationCallback == null)
			{
				throw new ArgumentNullException("configurationCallback");
			}
			configurationCallback(GlobalConfiguration.Configuration);
			GlobalConfiguration.Configuration.EnsureInitialized();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047E3 File Offset: 0x000029E3
		internal static void Reset()
		{
			GlobalConfiguration._configuration = GlobalConfiguration.CreateConfiguration();
			GlobalConfiguration._defaultHandler = GlobalConfiguration.CreateDefaultHandler();
			GlobalConfiguration._defaultServer = GlobalConfiguration.CreateDefaultServer();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004883 File Offset: 0x00002A83
		private static Lazy<HttpConfiguration> CreateConfiguration()
		{
			return new Lazy<HttpConfiguration>(delegate()
			{
				HttpConfiguration httpConfiguration = new HttpConfiguration(new HostedHttpRouteCollection(RouteTable.Routes));
				ServicesContainer services = httpConfiguration.Services;
				services.Replace(typeof(IAssembliesResolver), new WebHostAssembliesResolver());
				services.Replace(typeof(IHttpControllerTypeResolver), new WebHostHttpControllerTypeResolver());
				services.Replace(typeof(IHostBufferPolicySelector), new WebHostBufferPolicySelector());
				services.Replace(typeof(IExceptionHandler), new WebHostExceptionHandler(services.GetExceptionHandler()));
				return httpConfiguration;
			});
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000048B8 File Offset: 0x00002AB8
		private static Lazy<HttpMessageHandler> CreateDefaultHandler()
		{
			return new Lazy<HttpMessageHandler>(() => new HttpRoutingDispatcher(GlobalConfiguration._configuration.Value));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000048F7 File Offset: 0x00002AF7
		private static Lazy<HttpServer> CreateDefaultServer()
		{
			return new Lazy<HttpServer>(() => new HttpServer(GlobalConfiguration._configuration.Value, GlobalConfiguration._defaultHandler.Value));
		}

		// Token: 0x04000030 RID: 48
		private static Lazy<HttpConfiguration> _configuration = GlobalConfiguration.CreateConfiguration();

		// Token: 0x04000031 RID: 49
		private static Lazy<HttpMessageHandler> _defaultHandler = GlobalConfiguration.CreateDefaultHandler();

		// Token: 0x04000032 RID: 50
		private static Lazy<HttpServer> _defaultServer = GlobalConfiguration.CreateDefaultServer();
	}
}
