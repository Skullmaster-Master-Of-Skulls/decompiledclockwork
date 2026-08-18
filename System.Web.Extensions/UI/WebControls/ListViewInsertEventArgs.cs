using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B2 RID: 178
	public class ListViewInsertEventArgs : CancelEventArgs
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x000223E0 File Offset: 0x000205E0
		public ListViewInsertEventArgs(ListViewItem item) : base(false)
		{
			this._item = item;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000223F0 File Offset: 0x000205F0
		public ListViewItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000223F8 File Offset: 0x000205F8
		public IOrderedDictionary Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new OrderedDictionary();
				}
				return this._values;
			}
		}

		// Token: 0x040002E9 RID: 745
		private ListViewItem _item;

		// Token: 0x040002EA RID: 746
		private OrderedDictionary _values;
	}
}
