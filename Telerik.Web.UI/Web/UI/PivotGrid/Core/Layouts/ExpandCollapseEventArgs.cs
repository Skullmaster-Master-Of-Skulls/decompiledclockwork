using System;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CEB RID: 3307
	internal class ExpandCollapseEventArgs : EventArgs
	{
		// Token: 0x06007B91 RID: 31633 RVA: 0x001C63B8 File Offset: 0x001C45B8
		internal ExpandCollapseEventArgs(object item, int layoutSlot, int slotsCount)
		{
			this.Item = item;
			this.LayoutSlot = layoutSlot;
			this.SlotsCount = slotsCount;
		}

		// Token: 0x1700277C RID: 10108
		// (get) Token: 0x06007B92 RID: 31634 RVA: 0x001C63D5 File Offset: 0x001C45D5
		// (set) Token: 0x06007B93 RID: 31635 RVA: 0x001C63DD File Offset: 0x001C45DD
		public object Item { get; private set; }

		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x06007B94 RID: 31636 RVA: 0x001C63E6 File Offset: 0x001C45E6
		// (set) Token: 0x06007B95 RID: 31637 RVA: 0x001C63EE File Offset: 0x001C45EE
		public int LayoutSlot { get; private set; }

		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x06007B96 RID: 31638 RVA: 0x001C63F7 File Offset: 0x001C45F7
		// (set) Token: 0x06007B97 RID: 31639 RVA: 0x001C63FF File Offset: 0x001C45FF
		public int SlotsCount { get; private set; }
	}
}
