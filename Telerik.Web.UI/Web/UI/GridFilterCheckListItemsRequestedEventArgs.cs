using System;

namespace Telerik.Web.UI
{
	// Token: 0x020004C9 RID: 1225
	public class GridFilterCheckListItemsRequestedEventArgs : EventArgs
	{
		// Token: 0x06002C71 RID: 11377 RVA: 0x00091D5F File Offset: 0x0008FF5F
		public GridFilterCheckListItemsRequestedEventArgs(RadListBox listBox, GridColumn column)
		{
			this.Column = column;
			this.ListBox = listBox;
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x00091D75 File Offset: 0x0008FF75
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x00091D7D File Offset: 0x0008FF7D
		public GridColumn Column { get; private set; }

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00091D86 File Offset: 0x0008FF86
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x00091D8E File Offset: 0x0008FF8E
		public RadListBox ListBox { get; private set; }
	}
}
