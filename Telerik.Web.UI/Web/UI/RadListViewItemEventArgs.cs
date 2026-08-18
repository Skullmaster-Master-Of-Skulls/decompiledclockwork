using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019BD RID: 6589
	public class RadListViewItemEventArgs : EventArgs
	{
		// Token: 0x0600FE9B RID: 65179 RVA: 0x00392A21 File Offset: 0x00390C21
		public RadListViewItemEventArgs(RadListViewItem item)
		{
			this.Item = item;
		}

		// Token: 0x17004CDD RID: 19677
		// (get) Token: 0x0600FE9C RID: 65180 RVA: 0x00392A30 File Offset: 0x00390C30
		// (set) Token: 0x0600FE9D RID: 65181 RVA: 0x00392A38 File Offset: 0x00390C38
		public RadListViewItem Item { get; private set; }
	}
}
