using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000AF RID: 175
	public class ListViewDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00022300 File Offset: 0x00020500
		public ListViewDeleteEventArgs(int itemIndex) : base(false)
		{
			this._itemIndex = itemIndex;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00022310 File Offset: 0x00020510
		public int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00022318 File Offset: 0x00020518
		public IOrderedDictionary Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new OrderedDictionary();
				}
				return this._keys;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00022333 File Offset: 0x00020533
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

		// Token: 0x040002E0 RID: 736
		private int _itemIndex;

		// Token: 0x040002E1 RID: 737
		private OrderedDictionary _values;

		// Token: 0x040002E2 RID: 738
		private OrderedDictionary _keys;
	}
}
