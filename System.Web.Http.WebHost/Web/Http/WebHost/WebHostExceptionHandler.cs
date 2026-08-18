using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Web.Http.WebHost.Properties;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000017 RID: 23
	internal class WebHostExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003E2C File Offset: 0x0000202C
		public WebHostExceptionHandler(IExceptionHandler innerHandler)
		{
			this._innerHandler = innerHandler;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003E3B File Offset: 0x0000203B
		public IExceptionHandler InnerHandler
		{
			get
			{
				return this._innerHandler;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E44 File Offset: 0x00002044
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			if (exceptionContext.CatchBlock == WebHostExceptionCatchBlocks.HttpControllerHandlerBufferContent)
			{
				WebHostExceptionHandler.HandleWebHostBufferedContentException(context);
				return TaskHelpers.Completed();
			}
			return this._innerHandler.HandleAsync(context, cancellationToken);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003E8C File Offset: 0x0000208C
		private static void HandleWebHostBufferedContentException(ExceptionHandlerContext context)
		{
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
			HttpResponseMessage response = exceptionContext.Response;
			if (response == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ExceptionContext).Name,
					"Response"
				}), "context");
			}
			HttpContent content = response.Content;
			if (content == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpResponseMessage).Name,
					"Content"
				}), "context");
			}
			HttpResponseMessage httpResponseMessage;
			try
			{
				MediaTypeHeaderValue contentType = content.Headers.ContentType;
				string message = (contentType != null) ? Error.Format(SRResources.Serialize_Response_Failed_MediaType, new object[]
				{
					content.GetType().Name,
					contentType
				}) : Error.Format(SRResources.Serialize_Response_Failed, new object[]
				{
					content.GetType().Name
				});
				httpResponseMessage = request.CreateErrorResponse(HttpStatusCode.InternalServerError, new InvalidOperationException(message, exception));
				httpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
			}
			catch
			{
				httpResponseMessage = request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			context.Result = new ResponseMessageResult(httpResponseMessage);
		}

		// Token: 0x04000025 RID: 37
		private readonly IExceptionHandler _innerHandler;
	}
}
