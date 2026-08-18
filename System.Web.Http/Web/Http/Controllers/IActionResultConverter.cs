using System;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000032 RID: 50
	public interface IActionResultConverter
	{
		// Token: 0x06000133 RID: 307
		HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult);
	}
}
