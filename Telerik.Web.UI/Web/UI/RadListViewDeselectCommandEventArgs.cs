using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200194E RID: 6478
	public class RadListViewDeselectCommandEventArgs : RadListViewCommandEventArgs
	{
		// Token: 0x0600FA9F RID: 64159 RVA: 0x00386BC0 File Offset: 0x00384DC0
		public RadListViewDeselectCommandEventArgs(RadListViewDataItem item, object commandSource, object argument) : base(item, commandSource, "Deselect", argument)
		{
			this.ListViewDataItem = item;
		}

		// Token: 0x17004BBA RID: 19386
		// (get) Token: 0x0600FAA0 RID: 64160 RVA: 0x00386BD7 File Offset: 0x00384DD7
		// (set) Token: 0x0600FAA1 RID: 64161 RVA: 0x00386BDF File Offset: 0x00384DDF
		public RadListViewDataItem ListViewDataItem { get; set; }

		// Token: 0x17004BBB RID: 19387
		// (get) Token: 0x0600FAA2 RID: 64162 RVA: 0x00386BE8 File Offset: 0x00384DE8
		// (set) Token: 0x0600FAA3 RID: 64163 RVA: 0x00386BF0 File Offset: 0x00384DF0
		public override RadListViewItem ListViewItem
		{
			get
			{
				return this.ListViewDataItem;
			}
			set
			{
				this.ListViewDataItem = (RadListViewDataItem)value;
				base.ListViewItem = value;
			}
		}

		// Token: 0x0600FAA4 RID: 64164 RVA: 0x00386C08 File Offset: 0x00384E08
		public override void ExecuteCommand(object source)
		{
			this.ListViewDataItem.Selected = false;
			this.ListViewItem.OwnerListView.FireOnSelectedIndexChanged(EventArgs.Empty);
			if (!this.ListViewItem.OwnerListView.EnableViewState)
			{
				this.ListViewItem.OwnerListView.DataSource = null;
			}
			RadListViewRebindReason rebindReason = RadListViewRebindReason.PostBackEvent;
			this.ListViewItem.OwnerListView.ObtainDataSource(rebindReason);
			this.ListViewItem.OwnerListView.DataBind();
		}
	}
}
