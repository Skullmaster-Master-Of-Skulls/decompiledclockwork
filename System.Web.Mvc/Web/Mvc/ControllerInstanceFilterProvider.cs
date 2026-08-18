using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020000BE RID: 190
	public class ControllerInstanceFilterProvider : IFilterProvider
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			if (controllerContext.Controller != null)
			{
				yield return new Filter(controllerContext.Controller, FilterScope.First, new int?(int.MinValue));
			}
			yield break;
		}
	}
}
