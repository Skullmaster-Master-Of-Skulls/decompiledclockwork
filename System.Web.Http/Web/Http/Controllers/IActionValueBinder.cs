using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000ED RID: 237
	public interface IActionValueBinder
	{
		// Token: 0x060005F3 RID: 1523
		HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor);
	}
}
