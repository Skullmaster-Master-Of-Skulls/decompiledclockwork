using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200046F RID: 1135
	public sealed class MenuItemTemplateContainer : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x060037DA RID: 14298 RVA: 0x000B6521 File Offset: 0x000B4721
		public MenuItemTemplateContainer(int itemIndex, MenuItem dataItem)
		{
			this._itemIndex = itemIndex;
			this._dataItem = dataItem;
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x000B6537 File Offset: 0x000B4737
		// (set) Token: 0x060037DC RID: 14300 RVA: 0x000B653F File Offset: 0x000B473F
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

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060037DD RID: 14301 RVA: 0x000B6548 File Offset: 0x000B4748
		public int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000B6550 File Offset: 0x000B4750
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

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000B6537 File Offset: 0x000B4737
		object IDataItemContainer.DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000B6597 File Offset: 0x000B4797
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x000B6597 File Offset: 0x000B4797
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x0400226F RID: 8815
		private int _itemIndex;

		// Token: 0x04002270 RID: 8816
		private object _dataItem;
	}
}
