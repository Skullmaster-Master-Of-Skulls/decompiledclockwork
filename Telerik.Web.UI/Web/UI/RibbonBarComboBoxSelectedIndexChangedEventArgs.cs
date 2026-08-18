using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E33 RID: 3635
	public class RibbonBarComboBoxSelectedIndexChangedEventArgs : EventArgs
	{
		// Token: 0x17002B86 RID: 11142
		// (get) Token: 0x0600898B RID: 35211 RVA: 0x001F5D13 File Offset: 0x001F3F13
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002B87 RID: 11143
		// (get) Token: 0x0600898C RID: 35212 RVA: 0x001F5D1B File Offset: 0x001F3F1B
		public RibbonBarComboBox ComboBox
		{
			get
			{
				return this._comboBox;
			}
		}

		// Token: 0x17002B88 RID: 11144
		// (get) Token: 0x0600898D RID: 35213 RVA: 0x001F5D23 File Offset: 0x001F3F23
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002B89 RID: 11145
		// (get) Token: 0x0600898E RID: 35214 RVA: 0x001F5D2B File Offset: 0x001F3F2B
		public RibbonBarListItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0600898F RID: 35215 RVA: 0x001F5D33 File Offset: 0x001F3F33
		public RibbonBarComboBoxSelectedIndexChangedEventArgs(RibbonBarListItem item, RibbonBarComboBox comboBox, RibbonBarGroup group)
		{
			this._group = group;
			this._comboBox = comboBox;
			this._item = item;
			this._index = this._comboBox.Items.IndexOf(this._item);
		}

		// Token: 0x0400267A RID: 9850
		private RibbonBarGroup _group;

		// Token: 0x0400267B RID: 9851
		private RibbonBarComboBox _comboBox;

		// Token: 0x0400267C RID: 9852
		private RibbonBarListItem _item;

		// Token: 0x0400267D RID: 9853
		private int _index;
	}
}
