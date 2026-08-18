using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E30 RID: 3632
	public class RibbonBarDropDownSelectedIndexChangedEventArgs : EventArgs
	{
		// Token: 0x17002B82 RID: 11138
		// (get) Token: 0x0600897E RID: 35198 RVA: 0x001F5CBA File Offset: 0x001F3EBA
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002B83 RID: 11139
		// (get) Token: 0x0600897F RID: 35199 RVA: 0x001F5CC2 File Offset: 0x001F3EC2
		public RibbonBarDropDown DropDown
		{
			get
			{
				return this._dropDown;
			}
		}

		// Token: 0x17002B84 RID: 11140
		// (get) Token: 0x06008980 RID: 35200 RVA: 0x001F5CCA File Offset: 0x001F3ECA
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002B85 RID: 11141
		// (get) Token: 0x06008981 RID: 35201 RVA: 0x001F5CD2 File Offset: 0x001F3ED2
		public RibbonBarListItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x001F5CDA File Offset: 0x001F3EDA
		public RibbonBarDropDownSelectedIndexChangedEventArgs(RibbonBarListItem item, RibbonBarDropDown dropDown, RibbonBarGroup group)
		{
			this._group = group;
			this._dropDown = dropDown;
			this._item = item;
			this._index = this._dropDown.Items.IndexOf(this._item);
		}

		// Token: 0x04002676 RID: 9846
		private RibbonBarGroup _group;

		// Token: 0x04002677 RID: 9847
		private RibbonBarDropDown _dropDown;

		// Token: 0x04002678 RID: 9848
		private RibbonBarListItem _item;

		// Token: 0x04002679 RID: 9849
		private int _index;
	}
}
