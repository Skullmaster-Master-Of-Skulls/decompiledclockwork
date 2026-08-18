using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020000BD RID: 189
	public interface IFilterProvider
	{
		// Token: 0x060004FD RID: 1277
		IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
	}
}
