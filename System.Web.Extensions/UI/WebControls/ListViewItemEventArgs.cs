using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B4 RID: 180
	public class ListViewItemEventArgs : EventArgs
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0002246E File Offset: 0x0002066E
		public ListViewItemEventArgs(ListViewItem item)
		{
			this._item = item;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002247D File Offset: 0x0002067D
		public ListViewItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x040002ED RID: 749
		private ListViewItem _item;
	}
}
