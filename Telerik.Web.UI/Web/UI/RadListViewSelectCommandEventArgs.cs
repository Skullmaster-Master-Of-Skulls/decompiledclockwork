using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200194D RID: 6477
	public class RadListViewSelectCommandEventArgs : RadListViewCommandEventArgs
	{
		// Token: 0x0600FA99 RID: 64153 RVA: 0x00386B06 File Offset: 0x00384D06
		public RadListViewSelectCommandEventArgs(RadListViewDataItem item, object commandSource, object argument) : base(item, commandSource, "Select", argument)
		{
			this.ListViewDataItem = item;
		}

		// Token: 0x17004BB8 RID: 19384
		// (get) Token: 0x0600FA9A RID: 64154 RVA: 0x00386B1D File Offset: 0x00384D1D
		// (set) Token: 0x0600FA9B RID: 64155 RVA: 0x00386B25 File Offset: 0x00384D25
		public RadListViewDataItem ListViewDataItem { get; set; }

		// Token: 0x17004BB9 RID: 19385
		// (get) Token: 0x0600FA9C RID: 64156 RVA: 0x00386B2E File Offset: 0x00384D2E
		// (set) Token: 0x0600FA9D RID: 64157 RVA: 0x00386B36 File Offset: 0x00384D36
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

		// Token: 0x0600FA9E RID: 64158 RVA: 0x00386B4C File Offset: 0x00384D4C
		public override void ExecuteCommand(object source)
		{
			this.ListViewDataItem.Selected = true;
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
