using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200042C RID: 1068
	public class GridViewUpdateEventArgs : CancelEventArgs
	{
		// Token: 0x060033D5 RID: 13269 RVA: 0x000A92AE File Offset: 0x000A74AE
		public GridViewUpdateEventArgs(int rowIndex) : base(false)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x000A92BE File Offset: 0x000A74BE
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

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000A92D9 File Offset: 0x000A74D9
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

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000A92F4 File Offset: 0x000A74F4
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

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000A930F File Offset: 0x000A750F
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x0400217F RID: 8575
		private int _rowIndex;

		// Token: 0x04002180 RID: 8576
		private OrderedDictionary _values;

		// Token: 0x04002181 RID: 8577
		private OrderedDictionary _keys;

		// Token: 0x04002182 RID: 8578
		private OrderedDictionary _oldValues;
	}
}
