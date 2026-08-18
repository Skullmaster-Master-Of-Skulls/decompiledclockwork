using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002F5 RID: 757
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06002738 RID: 10040 RVA: 0x002AA538 File Offset: 0x002A9938
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this.exception = exception;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x002AA558 File Offset: 0x002A9958
		public SqlErrorCollection Errors
		{
			get
			{
				return this.exception.Errors;
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x002AA578 File Offset: 0x002A9978
		private bool ShouldSerializeErrors()
		{
			return this.exception != null && 0 < this.exception.Errors.Count;
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x002AA5A8 File Offset: 0x002A99A8
		public string Message
		{
			get
			{
				return this.exception.Message;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x002AA5C8 File Offset: 0x002A99C8
		public string Source
		{
			get
			{
				return this.exception.Source;
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x002AA5E8 File Offset: 0x002A99E8
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x040018F4 RID: 6388
		private SqlException exception;
	}
}
