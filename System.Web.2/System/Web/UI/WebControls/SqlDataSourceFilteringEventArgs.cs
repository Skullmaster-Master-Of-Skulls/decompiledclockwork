using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D4 RID: 1236
	public class SqlDataSourceFilteringEventArgs : CancelEventArgs
	{
		// Token: 0x06003D91 RID: 15761 RVA: 0x000C6543 File Offset: 0x000C4743
		public SqlDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000C6552 File Offset: 0x000C4752
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x040023C5 RID: 9157
		private IOrderedDictionary _parameterValues;
	}
}
