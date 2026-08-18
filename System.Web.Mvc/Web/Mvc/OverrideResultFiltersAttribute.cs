using System;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x02000094 RID: 148
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideResultFiltersAttribute : FilterAttribute, IOverrideFilter
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000C299 File Offset: 0x0000A499
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IResultFilter);
			}
		}
	}
}
