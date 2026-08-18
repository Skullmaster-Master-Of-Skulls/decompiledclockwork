using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001214 RID: 4628
	public class TreeListItemDataBoundEventArgs : EventArgs
	{
		// Token: 0x0600BF2E RID: 48942 RVA: 0x002A573A File Offset: 0x002A393A
		public TreeListItemDataBoundEventArgs(TreeListItem item)
		{
			this.Item = item;
		}

		// Token: 0x17003DAF RID: 15791
		// (get) Token: 0x0600BF2F RID: 48943 RVA: 0x002A5749 File Offset: 0x002A3949
		// (set) Token: 0x0600BF30 RID: 48944 RVA: 0x002A5751 File Offset: 0x002A3951
		public TreeListItem Item { get; internal set; }
	}
}
