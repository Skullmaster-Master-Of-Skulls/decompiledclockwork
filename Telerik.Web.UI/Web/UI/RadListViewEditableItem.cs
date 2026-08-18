using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001998 RID: 6552
	public class RadListViewEditableItem : RadListViewDataItem
	{
		// Token: 0x0600FDA2 RID: 64930 RVA: 0x0038FB18 File Offset: 0x0038DD18
		public RadListViewEditableItem(RadListView ownerListView, int displayIndex) : this(ownerListView, displayIndex, RadListViewItemType.EditItem)
		{
			base.DisplayIndex = displayIndex;
		}

		// Token: 0x0600FDA3 RID: 64931 RVA: 0x0038FB2A File Offset: 0x0038DD2A
		internal RadListViewEditableItem(RadListView ownerListView, int displayIndex, RadListViewItemType itemType) : base(ownerListView, displayIndex, itemType)
		{
			base.DisplayIndex = displayIndex;
		}

		// Token: 0x17004C90 RID: 19600
		// (get) Token: 0x0600FDA4 RID: 64932 RVA: 0x0038FB3C File Offset: 0x0038DD3C
		public override bool IsInEditMode
		{
			get
			{
				return true;
			}
		}
	}
}
