using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F5 RID: 245
	public class ActionDescriptorFilterProvider : IFilterProvider
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x000142AC File Offset: 0x000124AC
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			IEnumerable<FilterInfo> first = from instance in actionDescriptor.ControllerDescriptor.GetFilters()
			select new FilterInfo(instance, FilterScope.Controller);
			IEnumerable<FilterInfo> second = from instance in actionDescriptor.GetFilters()
			select new FilterInfo(instance, FilterScope.Action);
			return first.Concat(second);
		}
	}
}
