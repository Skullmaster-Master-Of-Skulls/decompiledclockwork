using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001213 RID: 4627
	public class TreeListItemCreatedEventArgs : EventArgs
	{
		// Token: 0x0600BF2B RID: 48939 RVA: 0x002A571A File Offset: 0x002A391A
		public TreeListItemCreatedEventArgs(TreeListItem item)
		{
			this.Item = item;
		}

		// Token: 0x17003DAE RID: 15790
		// (get) Token: 0x0600BF2C RID: 48940 RVA: 0x002A5729 File Offset: 0x002A3929
		// (set) Token: 0x0600BF2D RID: 48941 RVA: 0x002A5731 File Offset: 0x002A3931
		public TreeListItem Item { get; internal set; }
	}
}
