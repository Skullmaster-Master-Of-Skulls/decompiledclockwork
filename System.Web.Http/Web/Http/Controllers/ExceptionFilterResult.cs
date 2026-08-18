using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002C RID: 44
	internal class ExceptionFilterResult : IHttpActionResult
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000065E0 File Offset: 0x000047E0
		public ExceptionFilterResult(HttpActionContext context, IExceptionFilter[] filters, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, IHttpActionResult innerResult)
		{
			this._context = context;
			this._filters = filters;
			this._exceptionLogger = exceptionLogger;
			this._exceptionHandler = exceptionHandler;
			this._innerResult = innerResult;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006A00 File Offset: 0x00004C00
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				return await this._innerResult.ExecuteAsync(cancellationToken);
			}
			catch (Exception source)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(source);
			}
			Exception exception = exceptionInfo.SourceException;
			bool isCancellationException = exception is OperationCanceledException;
			ExceptionContext exceptionContext = new ExceptionContext(exception, ExceptionCatchBlocks.IExceptionFilter, this._context);
			if (!isCancellationException)
			{
				await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			}
			HttpActionExecutedContext executedContext = new HttpActionExecutedContext(this._context, exception);
			for (int i = this._filters.Length - 1; i >= 0; i--)
			{
				IExceptionFilter exceptionFilter = this._filters[i];
				await exceptionFilter.ExecuteExceptionFilterAsync(executedContext, cancellationToken);
			}
			if (executedContext.Response == null && !isCancellationException)
			{
				executedContext.Response = await this._exceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			}
			if (executedContext.Response == null)
			{
				if (exception == executedContext.Exception)
				{
					exceptionInfo.Throw();
				}
				throw executedContext.Exception;
			}
			return executedContext.Response;
		}

		// Token: 0x0400005C RID: 92
		private readonly HttpActionContext _context;

		// Token: 0x0400005D RID: 93
		private readonly IExceptionFilter[] _filters;

		// Token: 0x0400005E RID: 94
		private readonly IExceptionLogger _exceptionLogger;

		// Token: 0x0400005F RID: 95
		private readonly IExceptionHandler _exceptionHandler;

		// Token: 0x04000060 RID: 96
		private readonly IHttpActionResult _innerResult;
	}
}
