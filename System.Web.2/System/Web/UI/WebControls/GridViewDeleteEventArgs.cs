using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200041C RID: 1052
	public class GridViewDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x06003382 RID: 13186 RVA: 0x000A8FC0 File Offset: 0x000A71C0
		public GridViewDeleteEventArgs(int rowIndex) : base(false)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000A8FD0 File Offset: 0x000A71D0
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000A8FD8 File Offset: 0x000A71D8
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

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000A8FF3 File Offset: 0x000A71F3
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

		// Token: 0x04002169 RID: 8553
		private int _rowIndex;

		// Token: 0x0400216A RID: 8554
		private OrderedDictionary _keys;

		// Token: 0x0400216B RID: 8555
		private OrderedDictionary _values;
	}
}
