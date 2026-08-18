using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F3E RID: 3902
	public class RibbonBarMenuItemClickEventArgs : EventArgs
	{
		// Token: 0x17002F17 RID: 12055
		// (get) Token: 0x060094CA RID: 38090 RVA: 0x00214BFA File Offset: 0x00212DFA
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002F18 RID: 12056
		// (get) Token: 0x060094CB RID: 38091 RVA: 0x00214C02 File Offset: 0x00212E02
		public RibbonBarMenuItem ParentItem
		{
			get
			{
				return this._parentItem;
			}
		}

		// Token: 0x17002F19 RID: 12057
		// (get) Token: 0x060094CC RID: 38092 RVA: 0x00214C0A File Offset: 0x00212E0A
		public RibbonBarMenu Menu
		{
			get
			{
				return this._menu;
			}
		}

		// Token: 0x17002F1A RID: 12058
		// (get) Token: 0x060094CD RID: 38093 RVA: 0x00214C12 File Offset: 0x00212E12
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002F1B RID: 12059
		// (get) Token: 0x060094CE RID: 38094 RVA: 0x00214C1A File Offset: 0x00212E1A
		public RibbonBarMenuItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x060094CF RID: 38095 RVA: 0x00214C24 File Offset: 0x00212E24
		public RibbonBarMenuItemClickEventArgs(RibbonBarMenuItem item, RibbonBarMenu menu, RibbonBarGroup group)
		{
			this._group = group;
			this._menu = menu;
			this._parentItem = item.ParentItem;
			this._item = item;
			this._index = ((this._parentItem == null) ? this._menu.Items.IndexOf(this._item) : this._parentItem.Items.IndexOf(this._item));
		}

		// Token: 0x04002A97 RID: 10903
		private RibbonBarGroup _group;

		// Token: 0x04002A98 RID: 10904
		private RibbonBarMenu _menu;

		// Token: 0x04002A99 RID: 10905
		private RibbonBarMenuItem _parentItem;

		// Token: 0x04002A9A RID: 10906
		private RibbonBarMenuItem _item;

		// Token: 0x04002A9B RID: 10907
		private int _index;
	}
}
