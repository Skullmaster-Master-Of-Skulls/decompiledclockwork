using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000407 RID: 1031
	public class FormViewInsertEventArgs : CancelEventArgs
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x000A3878 File Offset: 0x000A1A78
		public FormViewInsertEventArgs(object commandArgument) : base(false)
		{
			this._commandArgument = commandArgument;
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000A3888 File Offset: 0x000A1A88
		public object CommandArgument
		{
			get
			{
				return this._commandArgument;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000A3890 File Offset: 0x000A1A90
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

		// Token: 0x040020FF RID: 8447
		private object _commandArgument;

		// Token: 0x04002100 RID: 8448
		private OrderedDictionary _values;
	}
}
