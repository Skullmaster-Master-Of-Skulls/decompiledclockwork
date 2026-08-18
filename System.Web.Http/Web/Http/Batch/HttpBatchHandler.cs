using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;

namespace System.Web.Http.Batch
{
	// Token: 0x02000024 RID: 36
	public abstract class HttpBatchHandler : HttpMessageHandler
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004CD1 File Offset: 0x00002ED1
		protected HttpBatchHandler(HttpServer httpServer)
		{
			if (httpServer == null)
			{
				throw Error.ArgumentNull("httpServer");
			}
			this._server = httpServer;
			this.Invoker = new HttpMessageInvoker(httpServer);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004CFA File Offset: 0x00002EFA
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004D02 File Offset: 0x00002F02
		public HttpMessageInvoker Invoker { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004D0B File Offset: 0x00002F0B
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004D18 File Offset: 0x00002F18
		internal IExceptionLogger ExceptionLogger
		{
			get
			{
				return this._server.ExceptionLogger;
			}
			set
			{
				this._server.ExceptionLogger = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004D26 File Offset: 0x00002F26
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004D33 File Offset: 0x00002F33
		internal IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._server.ExceptionHandler;
			}
			set
			{
				this._server.ExceptionHandler = value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000502C File Offset: 0x0000322C
		protected sealed override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.IsBatchRequest] = true;
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				return await this.ProcessBatchAsync(request, cancellationToken);
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
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpBatchHandler, request);
			await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage response = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			if (response == null)
			{
				exceptionInfo.Throw();
			}
			return response;
		}

		// Token: 0x060000F0 RID: 240
		public abstract Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x04000045 RID: 69
		private readonly HttpServer _server;
	}
}
