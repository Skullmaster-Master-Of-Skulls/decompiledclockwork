using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Services;
using System.Web.Http.Tracing.Tracers;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000155 RID: 341
	internal class TraceManager : ITraceManager
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x0001BE28 File Offset: 0x0001A028
		public void Initialize(HttpConfiguration configuration)
		{
			ITraceWriter traceWriter = configuration.Services.GetTraceWriter();
			if (traceWriter != null)
			{
				TraceManager.CreateAllTracers(configuration, traceWriter);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001BE4C File Offset: 0x0001A04C
		private static void CreateAllTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			TraceManager.CreateActionInvokerTracer(configuration, traceWriter);
			TraceManager.CreateActionSelectorTracer(configuration, traceWriter);
			TraceManager.CreateActionValueBinderTracer(configuration, traceWriter);
			TraceManager.CreateContentNegotiatorTracer(configuration, traceWriter);
			TraceManager.CreateControllerActivatorTracer(configuration, traceWriter);
			TraceManager.CreateControllerSelectorTracer(configuration, traceWriter);
			TraceManager.CreateHttpControllerTypeResolverTracer(configuration, traceWriter);
			TraceManager.CreateMessageHandlerTracers(configuration, traceWriter);
			TraceManager.CreateMediaTypeFormatterTracers(configuration, traceWriter);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BE98 File Offset: 0x0001A098
		private static TService GetService<TService>(ServicesContainer services)
		{
			return (TService)((object)services.GetService(typeof(TService)));
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		private static void CreateActionInvokerTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpActionInvoker service = TraceManager.GetService<IHttpActionInvoker>(configuration.Services);
			if (service != null && !(service is HttpActionInvokerTracer))
			{
				HttpActionInvokerTracer service2 = new HttpActionInvokerTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpActionInvoker), service2);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BEF4 File Offset: 0x0001A0F4
		private static void CreateActionSelectorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpActionSelector service = TraceManager.GetService<IHttpActionSelector>(configuration.Services);
			if (service != null && !(service is HttpActionSelectorTracer))
			{
				HttpActionSelectorTracer service2 = new HttpActionSelectorTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpActionSelector), service2);
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001BF38 File Offset: 0x0001A138
		private static void CreateActionValueBinderTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IActionValueBinder service = TraceManager.GetService<IActionValueBinder>(configuration.Services);
			if (service != null && !(service is ActionValueBinderTracer))
			{
				ActionValueBinderTracer service2 = new ActionValueBinderTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IActionValueBinder), service2);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001BF7C File Offset: 0x0001A17C
		private static void CreateContentNegotiatorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
			if (contentNegotiator != null && !(contentNegotiator is ContentNegotiatorTracer))
			{
				ContentNegotiatorTracer service = new ContentNegotiatorTracer(contentNegotiator, traceWriter);
				configuration.Services.Replace(typeof(IContentNegotiator), service);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		private static void CreateControllerActivatorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpControllerActivator service = TraceManager.GetService<IHttpControllerActivator>(configuration.Services);
			if (service != null && !(service is HttpControllerActivatorTracer))
			{
				HttpControllerActivatorTracer service2 = new HttpControllerActivatorTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerActivator), service2);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C004 File Offset: 0x0001A204
		private static void CreateControllerSelectorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpControllerSelector httpControllerSelector = configuration.Services.GetHttpControllerSelector();
			if (httpControllerSelector != null && !(httpControllerSelector is HttpControllerSelectorTracer))
			{
				HttpControllerSelectorTracer service = new HttpControllerSelectorTracer(httpControllerSelector, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerSelector), service);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C048 File Offset: 0x0001A248
		private static void CreateHttpControllerTypeResolverTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			DefaultHttpControllerTypeResolver defaultHttpControllerTypeResolver = configuration.Services.GetHttpControllerTypeResolver() as DefaultHttpControllerTypeResolver;
			if (defaultHttpControllerTypeResolver != null)
			{
				IHttpControllerTypeResolver service = new DefaultHttpControllerTypeResolverTracer(defaultHttpControllerTypeResolver, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerTypeResolver), service);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001C088 File Offset: 0x0001A288
		private static void CreateMediaTypeFormatterTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			for (int i = 0; i < configuration.Formatters.Count; i++)
			{
				MediaTypeFormatter mediaTypeFormatter = configuration.Formatters[i];
				if (!(mediaTypeFormatter is IFormatterTracer))
				{
					configuration.Formatters[i] = MediaTypeFormatterTracer.CreateTracer(configuration.Formatters[i], traceWriter, null);
				}
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		private static void CreateMessageHandlerTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			int count = configuration.MessageHandlers.Count;
			if (count > 0 && configuration.MessageHandlers[0].InnerHandler != null)
			{
				return;
			}
			if (!TraceManager.AreMessageHandlerTracersRegistered(configuration.MessageHandlers))
			{
				for (int i = count - 1; i >= 0; i--)
				{
					if (configuration.MessageHandlers[i] is RequestMessageHandlerTracer || configuration.MessageHandlers[i] is MessageHandlerTracer)
					{
						configuration.MessageHandlers.RemoveAt(i);
					}
				}
				count = configuration.MessageHandlers.Count;
				for (int j = 0; j < count * 2; j += 2)
				{
					DelegatingHandler innerHandler = configuration.MessageHandlers[j];
					DelegatingHandler item = new MessageHandlerTracer(innerHandler, traceWriter);
					configuration.MessageHandlers.Insert(j, item);
				}
				configuration.MessageHandlers.Insert(0, new RequestMessageHandlerTracer(traceWriter));
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
		private static bool AreMessageHandlerTracersRegistered(Collection<DelegatingHandler> messageHandlers)
		{
			int count = messageHandlers.Count;
			if (count == 0)
			{
				return false;
			}
			if (!(messageHandlers[0] is RequestMessageHandlerTracer))
			{
				return false;
			}
			if (count % 2 != 1)
			{
				return false;
			}
			for (int i = 2; i < count; i += 2)
			{
				DelegatingHandler delegatingHandler = messageHandlers[i - 1];
				DelegatingHandler delegatingHandler2 = messageHandlers[i];
				if (!(delegatingHandler is MessageHandlerTracer))
				{
					return false;
				}
				DelegatingHandler inner = Decorator.GetInner<DelegatingHandler>(delegatingHandler);
				if (inner != delegatingHandler2)
				{
					return false;
				}
			}
			return true;
		}
	}
}
