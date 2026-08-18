using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Owin.Properties;
using System.Web.Http.Results;

namespace System.Web.Http.Owin.ExceptionHandling
{
	// Token: 0x0200000B RID: 11
	internal class DefaultExceptionHandler : IExceptionHandler
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002F4C File Offset: 0x0000114C
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			DefaultExceptionHandler.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F5C File Offset: 0x0000115C
		private static void Handle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			HttpRequestMessage request = exceptionContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ExceptionContext).Name,
					"Request"
				}), "context");
			}
			context.Result = new ResponseMessageResult(request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionContext.Exception));
		}
	}
}
