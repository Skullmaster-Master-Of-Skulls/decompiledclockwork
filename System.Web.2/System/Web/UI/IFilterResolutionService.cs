using System;

namespace System.Web.UI
{
	// Token: 0x020002A5 RID: 677
	public interface IFilterResolutionService
	{
		// Token: 0x06001F96 RID: 8086
		bool EvaluateFilter(string filterName);

		// Token: 0x06001F97 RID: 8087
		int CompareFilters(string filter1, string filter2);
	}
}
