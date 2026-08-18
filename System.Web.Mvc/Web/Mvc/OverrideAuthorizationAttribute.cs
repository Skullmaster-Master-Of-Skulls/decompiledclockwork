using System;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x02000092 RID: 146
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthorizationAttribute : FilterAttribute, IOverrideFilter
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000C271 File Offset: 0x0000A471
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthorizationFilter);
			}
		}
	}
}
