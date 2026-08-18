using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019BB RID: 6587
	public class RadListViewItemDragDropEventArgs : EventArgs
	{
		// Token: 0x0600FE8F RID: 65167 RVA: 0x003927D3 File Offset: 0x003909D3
		public RadListViewItemDragDropEventArgs(RadListViewDataItem draggedItem, string destinationHtmlElement)
		{
			this.DraggedItem = draggedItem;
			this.DestinationHtmlElement = destinationHtmlElement;
		}

		// Token: 0x17004CD9 RID: 19673
		// (get) Token: 0x0600FE90 RID: 65168 RVA: 0x003927E9 File Offset: 0x003909E9
		// (set) Token: 0x0600FE91 RID: 65169 RVA: 0x003927F1 File Offset: 0x003909F1
		public RadListViewDataItem DraggedItem { get; private set; }

		// Token: 0x17004CDA RID: 19674
		// (get) Token: 0x0600FE92 RID: 65170 RVA: 0x003927FA File Offset: 0x003909FA
		// (set) Token: 0x0600FE93 RID: 65171 RVA: 0x00392802 File Offset: 0x00390A02
		public string DestinationHtmlElement { get; private set; }
	}
}
