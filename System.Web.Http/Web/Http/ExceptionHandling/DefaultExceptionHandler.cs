using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Results;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003E RID: 62
	internal class DefaultExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000074A1 File Offset: 0x000056A1
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			DefaultExceptionHandler.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000074B0 File Offset: 0x000056B0
		private static void Handle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			Exception exception = exceptionContext.Exception;
			HttpRequestMessage request = exceptionContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ExceptionContext).Name,
					"Request"
				}), "context");
			}
			if (exceptionContext.CatchBlock == ExceptionCatchBlocks.IExceptionFilter)
			{
				return;
			}
			context.Result = new ResponseMessageResult(request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception));
		}
	}
}
