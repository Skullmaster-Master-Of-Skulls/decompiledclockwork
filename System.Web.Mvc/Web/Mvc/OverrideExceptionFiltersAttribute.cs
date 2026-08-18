using System;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x02000093 RID: 147
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideExceptionFiltersAttribute : FilterAttribute, IOverrideFilter
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000C285 File Offset: 0x0000A485
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IExceptionFilter);
			}
		}
	}
}
