using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B3 RID: 179
	[ToolboxItem(false)]
	public class ListViewItem : Control, INamingContainer, IDataItemContainer
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x00022413 File Offset: 0x00020613
		public ListViewItem(ListViewItemType itemType)
		{
			this._itemType = itemType;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00022422 File Offset: 0x00020622
		public ListViewItemType ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0002242A File Offset: 0x0002062A
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00022432 File Offset: 0x00020632
		public virtual object DataItem { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002243B File Offset: 0x0002063B
		public virtual int DataItemIndex
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002243B File Offset: 0x0002063B
		public virtual int DisplayIndex
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00022440 File Offset: 0x00020640
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				ListViewCommandEventArgs args = new ListViewCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x040002EB RID: 747
		private ListViewItemType _itemType;
	}
}
