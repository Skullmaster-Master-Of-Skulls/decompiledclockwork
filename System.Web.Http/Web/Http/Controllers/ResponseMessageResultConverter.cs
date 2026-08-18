using System;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000AE RID: 174
	public class ResponseMessageResultConverter : IActionResultConverter
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			HttpResponseMessage httpResponseMessage = (HttpResponseMessage)actionResult;
			if (httpResponseMessage == null)
			{
				throw Error.InvalidOperation(SRResources.ResponseMessageResultConverter_NullHttpResponseMessage, new object[0]);
			}
			httpResponseMessage.EnsureResponseHasRequest(controllerContext.Request);
			return httpResponseMessage;
		}
	}
}
