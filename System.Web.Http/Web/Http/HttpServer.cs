using System;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x020000EB RID: 235
	public class HttpServer : DelegatingHandler
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x000132E5 File Offset: 0x000114E5
		public HttpServer() : this(new HttpConfiguration())
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000132F2 File Offset: 0x000114F2
		public HttpServer(HttpConfiguration configuration) : this(configuration, new HttpRoutingDispatcher(configuration))
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00013301 File Offset: 0x00011501
		public HttpServer(HttpMessageHandler dispatcher) : this(new HttpConfiguration(), dispatcher)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00013310 File Offset: 0x00011510
		public HttpServer(HttpConfiguration configuration, HttpMessageHandler dispatcher)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (dispatcher == null)
			{
				throw Error.ArgumentNull("dispatcher");
			}
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			this._dispatcher = dispatcher;
			this._configuration = configuration;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001335E File Offset: 0x0001155E
		public HttpMessageHandler Dispatcher
		{
			get
			{
				return this._dispatcher;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00013366 File Offset: 0x00011566
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001336E File Offset: 0x0001156E
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001338F File Offset: 0x0001158F
		internal IExceptionLogger ExceptionLogger
		{
			get
			{
				if (this._exceptionLogger == null)
				{
					this._exceptionLogger = ExceptionServices.GetLogger(this._configuration);
				}
				return this._exceptionLogger;
			}
			set
			{
				this._exceptionLogger = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00013398 File Offset: 0x00011598
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000133B9 File Offset: 0x000115B9
		internal IExceptionHandler ExceptionHandler
		{
			get
			{
				if (this._exceptionHandler == null)
				{
					this._exceptionHandler = ExceptionServices.GetHandler(this._configuration);
				}
				return this._exceptionHandler;
			}
			set
			{
				this._exceptionHandler = value;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000133C2 File Offset: 0x000115C2
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					this._configuration.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000137BC File Offset: 0x000119BC
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpResponseMessage result;
			if (this._disposed)
			{
				result = request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, SRResources.HttpServerDisposed);
			}
			else
			{
				this.EnsureInitialized();
				SynchronizationContext context = SynchronizationContext.Current;
				if (context != null)
				{
					request.SetSynchronizationContext(context);
				}
				request.SetConfiguration(this._configuration);
				IPrincipal originalPrincipal = Thread.CurrentPrincipal;
				if (originalPrincipal == null)
				{
					Thread.CurrentPrincipal = HttpServer._anonymousPrincipal;
				}
				HttpRequestContext requestContext = request.GetRequestContext();
				if (requestContext == null)
				{
					requestContext = new RequestBackedHttpRequestContext(request);
					request.SetRequestContext(requestContext);
				}
				try
				{
					ExceptionDispatchInfo exceptionInfo;
					try
					{
						return await base.SendAsync(request, cancellationToken);
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (HttpResponseException ex)
					{
						return ex.Response;
					}
					catch (Exception source)
					{
						exceptionInfo = ExceptionDispatchInfo.Capture(source);
					}
					ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpServer, request);
					await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
					HttpResponseMessage response = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
					if (response == null)
					{
						exceptionInfo.Throw();
					}
					result = response;
				}
				finally
				{
					Thread.CurrentPrincipal = originalPrincipal;
				}
			}
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001381B File Offset: 0x00011A1B
		private void EnsureInitialized()
		{
			LazyInitializer.EnsureInitialized<object>(ref this._initializationTarget, ref this._initialized, ref this._initializationLock, delegate()
			{
				this.Initialize();
				return null;
			});
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00013841 File Offset: 0x00011A41
		protected virtual void Initialize()
		{
			this._configuration.EnsureInitialized();
			base.InnerHandler = HttpClientFactory.CreatePipeline(this._dispatcher, this._configuration.MessageHandlers);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001386A File Offset: 0x00011A6A
		private static HttpConfiguration EnsureNonNull(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			return configuration;
		}

		// Token: 0x040001A3 RID: 419
		private static readonly IPrincipal _anonymousPrincipal = new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]);

		// Token: 0x040001A4 RID: 420
		private readonly HttpConfiguration _configuration;

		// Token: 0x040001A5 RID: 421
		private readonly HttpMessageHandler _dispatcher;

		// Token: 0x040001A6 RID: 422
		private IExceptionLogger _exceptionLogger;

		// Token: 0x040001A7 RID: 423
		private IExceptionHandler _exceptionHandler;

		// Token: 0x040001A8 RID: 424
		private bool _disposed;

		// Token: 0x040001A9 RID: 425
		private bool _initialized;

		// Token: 0x040001AA RID: 426
		private object _initializationLock = new object();

		// Token: 0x040001AB RID: 427
		private object _initializationTarget;
	}
}
