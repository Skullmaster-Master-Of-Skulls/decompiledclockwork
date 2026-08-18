using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003DB RID: 987
	public class DetailsViewDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x06003032 RID: 12338 RVA: 0x0009E480 File Offset: 0x0009C680
		public DetailsViewDeleteEventArgs(int rowIndex) : base(false)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x0009E490 File Offset: 0x0009C690
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x0009E498 File Offset: 0x0009C698
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

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x0009E4B3 File Offset: 0x0009C6B3
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

		// Token: 0x04002075 RID: 8309
		private int _rowIndex;

		// Token: 0x04002076 RID: 8310
		private OrderedDictionary _keys;

		// Token: 0x04002077 RID: 8311
		private OrderedDictionary _values;
	}
}
