using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000641 RID: 1601
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapNodeItem : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x06004EF1 RID: 20209 RVA: 0x0013E99A File Offset: 0x0013D99A
		public SiteMapNodeItem(int itemIndex, SiteMapNodeItemType itemType)
		{
			this._itemIndex = itemIndex;
			this._itemType = itemType;
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x0013E9B0 File Offset: 0x0013D9B0
		// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x0013E9B8 File Offset: 0x0013D9B8
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

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0013E9C1 File Offset: 0x0013D9C1
		public virtual int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x0013E9C9 File Offset: 0x0013D9C9
		public virtual SiteMapNodeItemType ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0013E9D1 File Offset: 0x0013D9D1
		protected internal virtual void SetItemType(SiteMapNodeItemType itemType)
		{
			this._itemType = itemType;
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0013E9DA File Offset: 0x0013D9DA
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.SiteMapNode;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0013E9E2 File Offset: 0x0013D9E2
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x0013E9EA File Offset: 0x0013D9EA
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002CB9 RID: 11449
		private int _itemIndex;

		// Token: 0x04002CBA RID: 11450
		private SiteMapNodeItemType _itemType;

		// Token: 0x04002CBB RID: 11451
		private SiteMapNode _siteMapNode;
	}
}
