using System;
using System.Linq;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000EF RID: 239
	public interface IHttpActionSelector
	{
		// Token: 0x060005FC RID: 1532
		HttpActionDescriptor SelectAction(HttpControllerContext controllerContext);

		// Token: 0x060005FD RID: 1533
		ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor);
	}
}
