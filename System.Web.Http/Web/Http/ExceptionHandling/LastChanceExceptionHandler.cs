using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000047 RID: 71
	internal class LastChanceExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00007A52 File Offset: 0x00005C52
		public LastChanceExceptionHandler(IExceptionHandler innerHandler)
		{
			if (innerHandler == null)
			{
				throw new ArgumentNullException("innerHandler");
			}
			this._innerHandler = innerHandler;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007A6F File Offset: 0x00005C6F
		public IExceptionHandler InnerHandler
		{
			get
			{
				return this._innerHandler;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007A78 File Offset: 0x00005C78
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			if (context != null)
			{
				ExceptionContext exceptionContext = context.ExceptionContext;
				ExceptionContextCatchBlock catchBlock = exceptionContext.CatchBlock;
				if (catchBlock != null && catchBlock.IsTopLevel)
				{
					context.Result = LastChanceExceptionHandler.CreateDefaultLastChanceResult(exceptionContext);
				}
			}
			return this._innerHandler.HandleAsync(context, cancellationToken);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007ABC File Offset: 0x00005CBC
		private static IHttpActionResult CreateDefaultLastChanceResult(ExceptionContext context)
		{
			Exception exception = context.Exception;
			if (exception == null)
			{
				return null;
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				return null;
			}
			HttpRequestContext requestContext = context.RequestContext;
			if (requestContext == null)
			{
				return null;
			}
			HttpConfiguration configuration = requestContext.Configuration;
			if (configuration == null)
			{
				return null;
			}
			ServicesContainer services = configuration.Services;
			IContentNegotiator contentNegotiator = services.GetContentNegotiator();
			if (contentNegotiator == null)
			{
				return null;
			}
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			return new ExceptionResult(exception, requestContext.IncludeErrorDetail, contentNegotiator, request, formatters);
		}

		// Token: 0x04000093 RID: 147
		private readonly IExceptionHandler _innerHandler;
	}
}
