using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F9 RID: 249
	public class ConfigurationFilterProvider : IFilterProvider
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x000143CA File Offset: 0x000125CA
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			return configuration.Filters;
		}
	}
}
