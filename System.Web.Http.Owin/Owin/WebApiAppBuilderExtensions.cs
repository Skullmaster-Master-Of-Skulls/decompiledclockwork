using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin;

namespace Owin
{
	// Token: 0x0200001A RID: 26
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WebApiAppBuilderExtensions
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00005260 File Offset: 0x00003460
		public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpConfiguration configuration)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			HttpServer httpServer = new HttpServer(configuration);
			IAppBuilder result;
			try
			{
				HttpMessageHandlerOptions options = WebApiAppBuilderExtensions.CreateOptions(builder, httpServer, configuration);
				result = builder.UseMessageHandler(options);
			}
			catch
			{
				httpServer.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000052C0 File Offset: 0x000034C0
		public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpServer httpServer)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (httpServer == null)
			{
				throw new ArgumentNullException("httpServer");
			}
			HttpConfiguration configuration = httpServer.Configuration;
			HttpMessageHandlerOptions options = WebApiAppBuilderExtensions.CreateOptions(builder, httpServer, configuration);
			return builder.UseMessageHandler(options);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005300 File Offset: 0x00003500
		private static IAppBuilder UseMessageHandler(this IAppBuilder builder, HttpMessageHandlerOptions options)
		{
			return builder.Use(typeof(HttpMessageHandlerAdapter), new object[]
			{
				options
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000532C File Offset: 0x0000352C
		private static HttpMessageHandlerOptions CreateOptions(IAppBuilder builder, HttpServer server, HttpConfiguration configuration)
		{
			ServicesContainer services = configuration.Services;
			IHostBufferPolicySelector bufferPolicySelector = services.GetHostBufferPolicySelector() ?? WebApiAppBuilderExtensions._defaultBufferPolicySelector;
			IExceptionLogger logger = ExceptionServices.GetLogger(services);
			IExceptionHandler handler = ExceptionServices.GetHandler(services);
			return new HttpMessageHandlerOptions
			{
				MessageHandler = server,
				BufferPolicySelector = bufferPolicySelector,
				ExceptionLogger = logger,
				ExceptionHandler = handler,
				AppDisposing = builder.GetOnAppDisposingProperty()
			};
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005394 File Offset: 0x00003594
		internal static CancellationToken GetOnAppDisposingProperty(this IAppBuilder builder)
		{
			IDictionary<string, object> properties = builder.Properties;
			if (properties == null)
			{
				return CancellationToken.None;
			}
			object obj;
			if (!properties.TryGetValue("host.OnAppDisposing", out obj))
			{
				return CancellationToken.None;
			}
			CancellationToken? cancellationToken = obj as CancellationToken?;
			if (cancellationToken == null)
			{
				return CancellationToken.None;
			}
			return cancellationToken.Value;
		}

		// Token: 0x04000031 RID: 49
		private static readonly IHostBufferPolicySelector _defaultBufferPolicySelector = new OwinBufferPolicySelector();
	}
}
