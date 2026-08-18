using System;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x02000091 RID: 145
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthenticationAttribute : FilterAttribute, IOverrideFilter
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000C25D File Offset: 0x0000A45D
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthenticationFilter);
			}
		}
	}
}
