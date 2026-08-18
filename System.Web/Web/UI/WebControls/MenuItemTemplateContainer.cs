using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E3 RID: 1507
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemTemplateContainer : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x06004A7D RID: 19069 RVA: 0x00130E35 File Offset: 0x0012FE35
		public MenuItemTemplateContainer(int itemIndex, MenuItem dataItem)
		{
			this._itemIndex = itemIndex;
			this._dataItem = dataItem;
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x00130E4B File Offset: 0x0012FE4B
		// (set) Token: 0x06004A7F RID: 19071 RVA: 0x00130E53 File Offset: 0x0012FE53
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004A80 RID: 19072 RVA: 0x00130E5C File Offset: 0x0012FE5C
		public int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00130E64 File Offset: 0x0012FE64
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				if (commandEventArgs is MenuEventArgs)
				{
					base.RaiseBubbleEvent(this, commandEventArgs);
				}
				else
				{
					MenuEventArgs args = new MenuEventArgs((MenuItem)this._dataItem, source, commandEventArgs);
					base.RaiseBubbleEvent(this, args);
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x00130EAB File Offset: 0x0012FEAB
		object IDataItemContainer.DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x00130EB3 File Offset: 0x0012FEB3
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x00130EBB File Offset: 0x0012FEBB
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002B7C RID: 11132
		private int _itemIndex;

		// Token: 0x04002B7D RID: 11133
		private object _dataItem;
	}
}
