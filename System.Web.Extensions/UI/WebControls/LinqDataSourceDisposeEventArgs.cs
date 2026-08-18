using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009F RID: 159
	public class LinqDataSourceDisposeEventArgs : CancelEventArgs
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x0001CE56 File Offset: 0x0001B056
		public LinqDataSourceDisposeEventArgs(object instance)
		{
			this._objectInstance = instance;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001CE65 File Offset: 0x0001B065
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		// Token: 0x0400025F RID: 607
		private object _objectInstance;
	}
}
