using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001FF RID: 511
	public interface IRadPageableItemContainer
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600123C RID: 4668
		// (remove) Token: 0x0600123D RID: 4669
		event EventHandler<RadDataPagerPageEventArgs> TotalRowCountAvailable;

		// Token: 0x0600123E RID: 4670
		void SetPageProperties(int startRowIndex, int maximumRows, bool databind);

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600123F RID: 4671
		int MaximumRows { get; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001240 RID: 4672
		int StartRowIndex { get; }
	}
}
