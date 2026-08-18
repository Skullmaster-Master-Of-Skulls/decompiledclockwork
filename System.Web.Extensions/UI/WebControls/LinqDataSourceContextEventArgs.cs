using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009D RID: 157
	public class LinqDataSourceContextEventArgs : EventArgs
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
		public LinqDataSourceContextEventArgs()
		{
			this._operation = DataSourceOperation.Select;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001CDEF File Offset: 0x0001AFEF
		public LinqDataSourceContextEventArgs(DataSourceOperation operation)
		{
			this._operation = operation;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001CDFE File Offset: 0x0001AFFE
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001CE06 File Offset: 0x0001B006
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
			set
			{
				this._objectInstance = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001CE0F File Offset: 0x0001B00F
		public DataSourceOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x0400025A RID: 602
		private object _objectInstance;

		// Token: 0x0400025B RID: 603
		private DataSourceOperation _operation;
	}
}
