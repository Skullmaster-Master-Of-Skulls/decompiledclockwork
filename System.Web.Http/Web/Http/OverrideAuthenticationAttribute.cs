using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthenticationAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000A067 File Offset: 0x00008267
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A06A File Offset: 0x0000826A
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthenticationFilter);
			}
		}
	}
}
