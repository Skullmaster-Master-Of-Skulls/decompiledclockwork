using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200126A RID: 4714
	public class TreeListItemDragDropEventArgs : EventArgs
	{
		// Token: 0x0600C40E RID: 50190 RVA: 0x002BEB04 File Offset: 0x002BCD04
		public TreeListItemDragDropEventArgs(TreeListDataItemCollection draggedItems, TreeListDataItem destinationItem, TreeListHeaderItem destinationHeaderItem, string htmlElement, IDictionary updatedParentKeyValues)
		{
			this.DraggedItems = draggedItems;
			this.DestinationDataItem = destinationItem;
			this.DestinationHeaderItem = destinationHeaderItem;
			this.HtmlElement = htmlElement;
			this.UpdatedParentKeyValues = updatedParentKeyValues;
			this.ExpandTargetItem = true;
		}

		// Token: 0x17003F24 RID: 16164
		// (get) Token: 0x0600C40F RID: 50191 RVA: 0x002BEB38 File Offset: 0x002BCD38
		// (set) Token: 0x0600C410 RID: 50192 RVA: 0x002BEB40 File Offset: 0x002BCD40
		public TreeListDataItemCollection DraggedItems { get; private set; }

		// Token: 0x17003F25 RID: 16165
		// (get) Token: 0x0600C411 RID: 50193 RVA: 0x002BEB49 File Offset: 0x002BCD49
		// (set) Token: 0x0600C412 RID: 50194 RVA: 0x002BEB51 File Offset: 0x002BCD51
		public TreeListDataItem DestinationDataItem { get; private set; }

		// Token: 0x17003F26 RID: 16166
		// (get) Token: 0x0600C413 RID: 50195 RVA: 0x002BEB5A File Offset: 0x002BCD5A
		// (set) Token: 0x0600C414 RID: 50196 RVA: 0x002BEB62 File Offset: 0x002BCD62
		public TreeListHeaderItem DestinationHeaderItem { get; private set; }

		// Token: 0x17003F27 RID: 16167
		// (get) Token: 0x0600C415 RID: 50197 RVA: 0x002BEB6B File Offset: 0x002BCD6B
		// (set) Token: 0x0600C416 RID: 50198 RVA: 0x002BEB73 File Offset: 0x002BCD73
		public string HtmlElement { get; private set; }

		// Token: 0x17003F28 RID: 16168
		// (get) Token: 0x0600C417 RID: 50199 RVA: 0x002BEB7C File Offset: 0x002BCD7C
		// (set) Token: 0x0600C418 RID: 50200 RVA: 0x002BEB84 File Offset: 0x002BCD84
		public IDictionary UpdatedParentKeyValues { get; private set; }

		// Token: 0x17003F29 RID: 16169
		// (get) Token: 0x0600C419 RID: 50201 RVA: 0x002BEB8D File Offset: 0x002BCD8D
		// (set) Token: 0x0600C41A RID: 50202 RVA: 0x002BEB95 File Offset: 0x002BCD95
		public bool Canceled { get; set; }

		// Token: 0x17003F2A RID: 16170
		// (get) Token: 0x0600C41B RID: 50203 RVA: 0x002BEB9E File Offset: 0x002BCD9E
		// (set) Token: 0x0600C41C RID: 50204 RVA: 0x002BEBA6 File Offset: 0x002BCDA6
		public bool ExpandTargetItem { get; set; }
	}
}
