using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000489 RID: 1161
	public class ObjectDataSourceFilteringEventArgs : CancelEventArgs
	{
		// Token: 0x060039A6 RID: 14758 RVA: 0x000BAE37 File Offset: 0x000B9037
		public ObjectDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000BAE46 File Offset: 0x000B9046
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x040022BC RID: 8892
		private IOrderedDictionary _parameterValues;
	}
}
