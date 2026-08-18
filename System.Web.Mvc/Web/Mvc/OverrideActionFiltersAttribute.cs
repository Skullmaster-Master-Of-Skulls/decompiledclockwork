using System;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x02000090 RID: 144
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideActionFiltersAttribute : FilterAttribute, IOverrideFilter
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000C249 File Offset: 0x0000A449
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IActionFilter);
			}
		}
	}
}
