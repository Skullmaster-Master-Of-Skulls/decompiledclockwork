using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideActionFiltersAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000A050 File Offset: 0x00008250
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000A053 File Offset: 0x00008253
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IActionFilter);
			}
		}
	}
}
