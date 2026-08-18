using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009A RID: 154
	public interface IPageableItemContainer
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006B3 RID: 1715
		int StartRowIndex { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006B4 RID: 1716
		int MaximumRows { get; }

		// Token: 0x060006B5 RID: 1717
		void SetPageProperties(int startRowIndex, int maximumRows, bool databind);

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060006B6 RID: 1718
		// (remove) Token: 0x060006B7 RID: 1719
		event EventHandler<PageEventArgs> TotalRowCountAvailable;
	}
}
