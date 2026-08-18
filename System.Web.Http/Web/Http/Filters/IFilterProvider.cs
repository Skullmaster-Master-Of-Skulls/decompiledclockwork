using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F4 RID: 244
	public interface IFilterProvider
	{
		// Token: 0x0600060D RID: 1549
		IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor);
	}
}
