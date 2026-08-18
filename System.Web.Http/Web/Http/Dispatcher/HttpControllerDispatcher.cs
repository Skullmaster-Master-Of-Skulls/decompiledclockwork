using System;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x0200011C RID: 284
	public class HttpControllerDispatcher : HttpMessageHandler
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00016A89 File Offset: 0x00014C89
		public HttpControllerDispatcher(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00016AA6 File Offset: 0x00014CA6
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00016AAE File Offset: 0x00014CAE
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00016ACF File Offset: 0x00014CCF
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

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00016AD8 File Offset: 0x00014CD8
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00016AF9 File Offset: 0x00014CF9
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00016B02 File Offset: 0x00014D02
		private IHttpControllerSelector ControllerSelector
		{
			get
			{
				if (this._controllerSelector == null)
				{
					this._controllerSelector = this._configuration.Services.GetHttpControllerSelector();
				}
				return this._controllerSelector;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00016EF8 File Offset: 0x000150F8
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpControllerContext controllerContext = null;
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				HttpControllerDescriptor controllerDescriptor = this.ControllerSelector.SelectController(request);
				if (controllerDescriptor == null)
				{
					return request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
					{
						request.RequestUri
					}), SRResources.NoControllerSelected);
				}
				IHttpController controller = controllerDescriptor.CreateController(request);
				if (controller == null)
				{
					return request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
					{
						request.RequestUri
					}), SRResources.NoControllerCreated);
				}
				controllerContext = HttpControllerDispatcher.CreateControllerContext(request, controllerDescriptor, controller);
				return await controller.ExecuteAsync(controllerContext, cancellationToken);
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
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpControllerDispatcher, request)
			{
				ControllerContext = controllerContext
			};
			await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage response = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			if (response == null)
			{
				exceptionInfo.Throw();
			}
			return response;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00016F50 File Offset: 0x00015150
		private static HttpControllerContext CreateControllerContext(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, IHttpController controller)
		{
			HttpConfiguration configuration = controllerDescriptor.Configuration;
			HttpConfiguration configuration2 = request.GetConfiguration();
			if (configuration2 == null)
			{
				request.SetConfiguration(configuration);
			}
			else if (configuration2 != configuration)
			{
				request.SetConfiguration(configuration);
			}
			HttpRequestContext httpRequestContext = request.GetRequestContext();
			if (httpRequestContext == null)
			{
				httpRequestContext = new RequestBackedHttpRequestContext(request)
				{
					Configuration = configuration
				};
				request.SetRequestContext(httpRequestContext);
			}
			return new HttpControllerContext(httpRequestContext, request, controllerDescriptor, controller);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00016FAC File Offset: 0x000151AC
		private static HttpConfiguration EnsureNonNull(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			return configuration;
		}

		// Token: 0x040001F5 RID: 501
		private readonly HttpConfiguration _configuration;

		// Token: 0x040001F6 RID: 502
		private IExceptionLogger _exceptionLogger;

		// Token: 0x040001F7 RID: 503
		private IExceptionHandler _exceptionHandler;

		// Token: 0x040001F8 RID: 504
		private IHttpControllerSelector _controllerSelector;
	}
}
