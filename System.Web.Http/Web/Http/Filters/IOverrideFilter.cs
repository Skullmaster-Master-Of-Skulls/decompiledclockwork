using System;

namespace System.Web.Http.Filters
{
	// Token: 0x02000074 RID: 116
	public interface IOverrideFilter : IFilter
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600031B RID: 795
		Type FiltersToOverride { get; }
	}
}
