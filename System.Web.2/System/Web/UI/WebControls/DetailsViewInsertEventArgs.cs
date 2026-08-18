using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003DF RID: 991
	public class DetailsViewInsertEventArgs : CancelEventArgs
	{
		// Token: 0x06003047 RID: 12359 RVA: 0x0009E548 File Offset: 0x0009C748
		public DetailsViewInsertEventArgs(object commandArgument) : base(false)
		{
			this._commandArgument = commandArgument;
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x0009E558 File Offset: 0x0009C758
		public object CommandArgument
		{
			get
			{
				return this._commandArgument;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x0009E560 File Offset: 0x0009C760
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

		// Token: 0x0400207D RID: 8317
		private object _commandArgument;

		// Token: 0x0400207E RID: 8318
		private OrderedDictionary _values;
	}
}
