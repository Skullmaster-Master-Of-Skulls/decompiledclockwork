using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000412 RID: 1042
	public class FormViewUpdateEventArgs : CancelEventArgs
	{
		// Token: 0x06003251 RID: 12881 RVA: 0x000A3AAF File Offset: 0x000A1CAF
		public FormViewUpdateEventArgs(object commandArgument) : base(false)
		{
			this._commandArgument = commandArgument;
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000A3ABF File Offset: 0x000A1CBF
		public object CommandArgument
		{
			get
			{
				return this._commandArgument;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000A3AC7 File Offset: 0x000A1CC7
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

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000A3AE2 File Offset: 0x000A1CE2
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

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000A3AFD File Offset: 0x000A1CFD
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

		// Token: 0x04002113 RID: 8467
		private object _commandArgument;

		// Token: 0x04002114 RID: 8468
		private OrderedDictionary _values;

		// Token: 0x04002115 RID: 8469
		private OrderedDictionary _keys;

		// Token: 0x04002116 RID: 8470
		private OrderedDictionary _oldValues;
	}
}
