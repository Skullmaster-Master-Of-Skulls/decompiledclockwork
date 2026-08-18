using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200199A RID: 6554
	public class RadListViewInsertItem : RadListViewEditableItem, IRadListViewInsertItem
	{
		// Token: 0x0600FDA5 RID: 64933 RVA: 0x0038FB3F File Offset: 0x0038DD3F
		public RadListViewInsertItem(RadListView ownerListView, int displayIndex) : this(ownerListView, displayIndex, RadListViewItemType.InsertItem)
		{
		}

		// Token: 0x0600FDA6 RID: 64934 RVA: 0x0038FB4A File Offset: 0x0038DD4A
		internal RadListViewInsertItem(RadListView ownerListView, int displayIndex, RadListViewItemType itemType) : base(ownerListView, displayIndex, itemType)
		{
		}
	}
}
