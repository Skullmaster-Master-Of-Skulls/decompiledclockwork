using System;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class OverrideExceptionFiltersAttribute : Attribute, IOverrideFilter, IFilter
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A095 File Offset: 0x00008295
		public bool AllowMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A098 File Offset: 0x00008298
		public Type FiltersToOverride
		{
			get
			{
				return typeof(IExceptionFilter);
			}
		}
	}
}
