using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004CA RID: 1226
	[ToolboxItem(false)]
	public class SiteMapNodeItem : WebControl, INamingContainer, IDataItemContainer
	{
		// Token: 0x06003CEA RID: 15594 RVA: 0x000C4FC1 File Offset: 0x000C31C1
		public SiteMapNodeItem(int itemIndex, SiteMapNodeItemType itemType)
		{
			this._itemIndex = itemIndex;
			this._itemType = itemType;
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000C4FD7 File Offset: 0x000C31D7
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x000C4FDF File Offset: 0x000C31DF
		public virtual SiteMapNode SiteMapNode
		{
			get
			{
				return this._siteMapNode;
			}
			set
			{
				this._siteMapNode = value;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000C4FE8 File Offset: 0x000C31E8
		public virtual int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000C4FF0 File Offset: 0x000C31F0
		public virtual SiteMapNodeItemType ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000C4FF8 File Offset: 0x000C31F8
		protected internal virtual void SetItemType(SiteMapNodeItemType itemType)
		{
			this._itemType = itemType;
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000C5001 File Offset: 0x000C3201
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.SiteMapNode;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000C5009 File Offset: 0x000C3209
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000C5009 File Offset: 0x000C3209
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x0400239D RID: 9117
		private int _itemIndex;

		// Token: 0x0400239E RID: 9118
		private SiteMapNodeItemType _itemType;

		// Token: 0x0400239F RID: 9119
		private SiteMapNode _siteMapNode;
	}
}
