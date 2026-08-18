using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200017A RID: 378
	[ToolboxItem(false)]
	public class ReorderListItem : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001B6BE File Offset: 0x000198BE
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001B6ED File Offset: 0x000198ED
		public ListItemType ItemType
		{
			get
			{
				if (this._baseItem != null)
				{
					return this._baseItem.ItemType;
				}
				if (this._isAddItem)
				{
					throw new InvalidOperationException("Item type isn't valid for Add items.");
				}
				return this._itemType;
			}
			set
			{
				this._itemType = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001B6F6 File Offset: 0x000198F6
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0001B712 File Offset: 0x00019912
		public object DataItem
		{
			get
			{
				if (this._baseItem != null)
				{
					return this._baseItem.DataItem;
				}
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0001B71B File Offset: 0x0001991B
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0001B737 File Offset: 0x00019937
		public int ItemIndex
		{
			get
			{
				if (this._baseItem != null)
				{
					return this._baseItem.ItemIndex;
				}
				return this._itemIndex;
			}
			set
			{
				this._itemIndex = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0001B740 File Offset: 0x00019940
		public bool IsAddItem
		{
			get
			{
				if (this._baseItem != null)
				{
					return this._baseItem.IsAddItem;
				}
				return this._isAddItem;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001B75C File Offset: 0x0001995C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001B764 File Offset: 0x00019964
		internal ReorderListItem(ReorderListItem baseItem, HtmlTextWriterTag tag)
		{
			this._baseItem = baseItem;
			this._tag = tag;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001B782 File Offset: 0x00019982
		public ReorderListItem(int index) : this(index, false)
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001B78C File Offset: 0x0001998C
		public ReorderListItem(int index, bool isAddItem)
		{
			this._itemIndex = index;
			if (!isAddItem)
			{
				this.ID = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					"_rli",
					index
				});
			}
			else
			{
				this.ID = string.Format(CultureInfo.InvariantCulture, "{0}Insert", new object[]
				{
					"_rli"
				});
			}
			base.Style["vertical-align"] = "middle";
			this._isAddItem = isAddItem;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001B822 File Offset: 0x00019A22
		public ReorderListItem(object dataItem, int index, ListItemType itemType) : this(index)
		{
			this._dataItem = dataItem;
			this._itemType = itemType;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001B83C File Offset: 0x00019A3C
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				ReorderListCommandEventArgs args2 = new ReorderListCommandEventArgs(commandEventArgs, source, this);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return true;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001B867 File Offset: 0x00019A67
		public int DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0001B86F File Offset: 0x00019A6F
		public int DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x040003FE RID: 1022
		internal const string ItemBaseName = "_rli";

		// Token: 0x040003FF RID: 1023
		private object _dataItem;

		// Token: 0x04000400 RID: 1024
		private ReorderListItem _baseItem;

		// Token: 0x04000401 RID: 1025
		private HtmlTextWriterTag _tag = HtmlTextWriterTag.Li;

		// Token: 0x04000402 RID: 1026
		private int _itemIndex;

		// Token: 0x04000403 RID: 1027
		private ListItemType _itemType;

		// Token: 0x04000404 RID: 1028
		private bool _isAddItem;
	}
}
