using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000403 RID: 1027
	public class FormViewDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x0600320C RID: 12812 RVA: 0x000A37B0 File Offset: 0x000A19B0
		public FormViewDeleteEventArgs(int rowIndex) : base(false)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000A37C0 File Offset: 0x000A19C0
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000A37C8 File Offset: 0x000A19C8
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

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000A37E3 File Offset: 0x000A19E3
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

		// Token: 0x040020F7 RID: 8439
		private int _rowIndex;

		// Token: 0x040020F8 RID: 8440
		private OrderedDictionary _keys;

		// Token: 0x040020F9 RID: 8441
		private OrderedDictionary _values;
	}
}
