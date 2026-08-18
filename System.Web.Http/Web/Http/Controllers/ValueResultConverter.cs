using System;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000AF RID: 175
	public class ValueResultConverter<T> : IActionResultConverter
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			HttpResponseMessage httpResponseMessage = actionResult as HttpResponseMessage;
			if (httpResponseMessage != null)
			{
				httpResponseMessage.EnsureResponseHasRequest(controllerContext.Request);
				return httpResponseMessage;
			}
			T value = (T)((object)actionResult);
			return controllerContext.Request.CreateResponse(HttpStatusCode.OK, value, controllerContext.Configuration);
		}
	}
}
