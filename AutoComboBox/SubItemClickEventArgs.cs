using System;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000100 RID: 256
	public class SubItemClickEventArgs : EventArgs
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x0004E24E File Offset: 0x0004D24E
		public SubItemClickEventArgs(ListViewItem item, int subItem)
		{
			this._subItemIndex = subItem;
			this._item = item;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0004E278 File Offset: 0x0004D278
		public int SubItem
		{
			get
			{
				return this._subItemIndex;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0004E290 File Offset: 0x0004D290
		public ListViewItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04000769 RID: 1897
		private int _subItemIndex = -1;

		// Token: 0x0400076A RID: 1898
		private ListViewItem _item = null;
	}
}
