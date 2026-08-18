using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000E2 RID: 226
	public class ApiControllerActionInvoker : IHttpActionInvoker
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00011C63 File Offset: 0x0000FE63
		public virtual Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return ApiControllerActionInvoker.InvokeActionAsyncCore(actionContext, cancellationToken);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00011F84 File Offset: 0x00010184
		private static async Task<HttpResponseMessage> InvokeActionAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			HttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor;
			HttpControllerContext controllerContext = actionContext.ControllerContext;
			HttpResponseMessage result2;
			try
			{
				object result = await actionDescriptor.ExecuteAsync(controllerContext, actionContext.ActionArguments, cancellationToken);
				bool isDeclaredTypeActionResult = typeof(IHttpActionResult).IsAssignableFrom(actionDescriptor.ReturnType);
				if (result == null && isDeclaredTypeActionResult)
				{
					throw Error.InvalidOperation(SRResources.ApiControllerActionInvoker_NullHttpActionResult, new object[0]);
				}
				if (isDeclaredTypeActionResult || actionDescriptor.ReturnType == typeof(object))
				{
					IHttpActionResult actionResult = result as IHttpActionResult;
					if (actionResult == null && isDeclaredTypeActionResult)
					{
						throw Error.InvalidOperation(SRResources.ApiControllerActionInvoker_InvalidHttpActionResult, new object[]
						{
							result.GetType()
						});
					}
					if (actionResult != null)
					{
						HttpResponseMessage response = await actionResult.ExecuteAsync(cancellationToken);
						if (response == null)
						{
							throw Error.InvalidOperation(SRResources.ResponseMessageResultConverter_NullHttpResponseMessage, new object[0]);
						}
						response.EnsureResponseHasRequest(actionContext.Request);
						return response;
					}
				}
				result2 = actionDescriptor.ResultConverter.Convert(controllerContext, result);
			}
			catch (HttpResponseException ex)
			{
				HttpResponseMessage response2 = ex.Response;
				response2.EnsureResponseHasRequest(actionContext.Request);
				result2 = response2;
			}
			return result2;
		}
	}
}
