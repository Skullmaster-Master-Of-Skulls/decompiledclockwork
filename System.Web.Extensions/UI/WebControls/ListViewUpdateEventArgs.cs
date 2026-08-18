using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BC RID: 188
	public class ListViewUpdateEventArgs : CancelEventArgs
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x000227DF File Offset: 0x000209DF
		public ListViewUpdateEventArgs(int itemIndex)
		{
			this._itemIndex = itemIndex;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000227EE File Offset: 0x000209EE
		public int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x000227F6 File Offset: 0x000209F6
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00022811 File Offset: 0x00020A11
		public IOrderedDictionary NewValues
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0002282C File Offset: 0x00020A2C
		public IOrderedDictionary OldValues
		{
			get
			{
				if (this._oldValues == null)
				{
					this._oldValues = new OrderedDictionary();
				}
				return this._oldValues;
			}
		}

		// Token: 0x04000301 RID: 769
		private int _itemIndex;

		// Token: 0x04000302 RID: 770
		private OrderedDictionary _values;

		// Token: 0x04000303 RID: 771
		private OrderedDictionary _keys;

		// Token: 0x04000304 RID: 772
		private OrderedDictionary _oldValues;
	}
}
