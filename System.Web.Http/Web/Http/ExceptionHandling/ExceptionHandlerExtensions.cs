using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003B RID: 59
	public static class ExceptionHandlerExtensions
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00007170 File Offset: 0x00005370
		public static Task<HttpResponseMessage> HandleAsync(this IExceptionHandler handler, ExceptionContext context, CancellationToken cancellationToken)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionHandlerContext context2 = new ExceptionHandlerContext(context);
			return ExceptionHandlerExtensions.HandleAsyncCore(handler, context2, cancellationToken);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007380 File Offset: 0x00005580
		private static async Task<HttpResponseMessage> HandleAsyncCore(IExceptionHandler handler, ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			await handler.HandleAsync(context, cancellationToken);
			IHttpActionResult result = context.Result;
			HttpResponseMessage result2;
			if (result == null)
			{
				result2 = null;
			}
			else
			{
				HttpResponseMessage response = await result.ExecuteAsync(cancellationToken);
				if (response == null)
				{
					throw new InvalidOperationException(Error.Format(SRResources.TypeMethodMustNotReturnNull, new object[]
					{
						typeof(IHttpActionResult).Name,
						"ExecuteAsync"
					}));
				}
				result2 = response;
			}
			return result2;
		}
	}
}
