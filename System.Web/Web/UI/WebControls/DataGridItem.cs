using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200053C RID: 1340
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridItem : TableRow, IDataItemContainer, INamingContainer
	{
		// Token: 0x06004218 RID: 16920 RVA: 0x0011214A File Offset: 0x0011114A
		public DataGridItem(int itemIndex, int dataSetIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.dataSetIndex = dataSetIndex;
			this.itemType = itemType;
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x00112167 File Offset: 0x00111167
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x0011216F File Offset: 0x0011116F
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x00112178 File Offset: 0x00111178
		public virtual int DataSetIndex
		{
			get
			{
				return this.dataSetIndex;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x00112180 File Offset: 0x00111180
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x00112188 File Offset: 0x00111188
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00112190 File Offset: 0x00111190
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DataGridCommandEventArgs args = new DataGridCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x001121BE File Offset: 0x001111BE
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.itemType = itemType;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x001121C7 File Offset: 0x001111C7
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x001121CF File Offset: 0x001111CF
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataSetIndex;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x001121D7 File Offset: 0x001111D7
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x040028F0 RID: 10480
		private int itemIndex;

		// Token: 0x040028F1 RID: 10481
		private int dataSetIndex;

		// Token: 0x040028F2 RID: 10482
		private ListItemType itemType;

		// Token: 0x040028F3 RID: 10483
		private object dataItem;
	}
}
