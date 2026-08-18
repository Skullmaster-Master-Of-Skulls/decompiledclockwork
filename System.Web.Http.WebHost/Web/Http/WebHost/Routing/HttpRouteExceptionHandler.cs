using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200000B RID: 11
	internal class HttpRouteExceptionHandler : HttpTaskAsyncHandler
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002D3C File Offset: 0x00000F3C
		public HttpRouteExceptionHandler(ExceptionDispatchInfo exceptionInfo) : this(exceptionInfo, ExceptionServices.GetLogger(GlobalConfiguration.Configuration), ExceptionServices.GetHandler(GlobalConfiguration.Configuration))
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D59 File Offset: 0x00000F59
		internal HttpRouteExceptionHandler(ExceptionDispatchInfo exceptionInfo, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler)
		{
			this._exceptionInfo = exceptionInfo;
			this._exceptionLogger = exceptionLogger;
			this._exceptionHandler = exceptionHandler;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D76 File Offset: 0x00000F76
		internal ExceptionDispatchInfo ExceptionInfo
		{
			get
			{
				return this._exceptionInfo;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D7E File Offset: 0x00000F7E
		internal IExceptionLogger ExceptionLogger
		{
			get
			{
				return this._exceptionLogger;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D86 File Offset: 0x00000F86
		internal IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._exceptionHandler;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D8E File Offset: 0x00000F8E
		public override Task ProcessRequestAsync(HttpContext context)
		{
			return this.ProcessRequestAsync(new HttpContextWrapper(context));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000030B8 File Offset: 0x000012B8
		internal async Task ProcessRequestAsync(HttpContextBase context)
		{
			Exception exception = this._exceptionInfo.SourceException;
			OperationCanceledException canceledException = exception as OperationCanceledException;
			if (canceledException != null)
			{
				context.Request.Abort();
			}
			else
			{
				HttpRequestMessage request = context.GetOrCreateHttpRequestMessage();
				HttpResponseMessage response = null;
				CancellationToken cancellationToken = context.Response.GetClientDisconnectedTokenWhenFixed();
				HttpResponseException responseException = exception as HttpResponseException;
				try
				{
					if (responseException != null)
					{
						response = responseException.Response;
						await HttpControllerHandler.CopyResponseAsync(context, request, response, this._exceptionLogger, this._exceptionHandler, cancellationToken);
					}
					else if (!(await HttpControllerHandler.CopyErrorResponseAsync(WebHostExceptionCatchBlocks.HttpWebRoute, context, request, null, this._exceptionInfo.SourceException, this._exceptionLogger, this._exceptionHandler, cancellationToken)))
					{
						this._exceptionInfo.Throw();
					}
				}
				catch (OperationCanceledException)
				{
					context.Request.Abort();
				}
				finally
				{
					request.DisposeRequestResources();
					request.Dispose();
					if (response != null)
					{
						response.Dispose();
					}
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private readonly ExceptionDispatchInfo _exceptionInfo;

		// Token: 0x0400000B RID: 11
		private readonly IExceptionLogger _exceptionLogger;

		// Token: 0x0400000C RID: 12
		private readonly IExceptionHandler _exceptionHandler;
	}
}
