using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideAuthorizationAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000A07E File Offset: 0x0000827E
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A081 File Offset: 0x00008281
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IAuthorizationFilter);
			}
		}
	}
}
