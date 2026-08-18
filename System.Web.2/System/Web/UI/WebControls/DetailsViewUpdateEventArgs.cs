using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003EB RID: 1003
	public class DetailsViewUpdateEventArgs : CancelEventArgs
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x0009E767 File Offset: 0x0009C967
		public DetailsViewUpdateEventArgs(object commandArgument) : base(false)
		{
			this._commandArgument = commandArgument;
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0009E777 File Offset: 0x0009C977
		public object CommandArgument
		{
			get
			{
				return this._commandArgument;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x0009E77F File Offset: 0x0009C97F
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

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0009E79A File Offset: 0x0009C99A
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

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x0009E7B5 File Offset: 0x0009C9B5
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

		// Token: 0x04002091 RID: 8337
		private object _commandArgument;

		// Token: 0x04002092 RID: 8338
		private OrderedDictionary _values;

		// Token: 0x04002093 RID: 8339
		private OrderedDictionary _keys;

		// Token: 0x04002094 RID: 8340
		private OrderedDictionary _oldValues;
	}
}
