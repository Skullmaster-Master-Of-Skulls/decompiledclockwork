using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B5 RID: 1205
	[ToolboxItem(false)]
	public class RepeaterItem : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x06003C4D RID: 15437 RVA: 0x000C38E9 File Offset: 0x000C1AE9
		public RepeaterItem(int itemIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.itemType = itemType;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x000C38FF File Offset: 0x000C1AFF
		// (set) Token: 0x06003C4F RID: 15439 RVA: 0x000C3907 File Offset: 0x000C1B07
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

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000C3910 File Offset: 0x000C1B10
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000C3918 File Offset: 0x000C1B18
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000C3920 File Offset: 0x000C1B20
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				RepeaterCommandEventArgs args = new RepeaterCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000C394E File Offset: 0x000C1B4E
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x000C394E File Offset: 0x000C1B4E
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002375 RID: 9077
		private int itemIndex;

		// Token: 0x04002376 RID: 9078
		private ListItemType itemType;

		// Token: 0x04002377 RID: 9079
		private object dataItem;
	}
}
