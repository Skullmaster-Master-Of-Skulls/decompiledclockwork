using System;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000B0 RID: 176
	public class VoidResultConverter : IActionResultConverter
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000CAAD File Offset: 0x0000ACAD
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			return controllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
		}
	}
}
