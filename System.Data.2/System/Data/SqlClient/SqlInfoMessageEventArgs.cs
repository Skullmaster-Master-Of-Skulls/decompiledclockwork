using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001D0 RID: 464
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x000CED8C File Offset: 0x000CE18C
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this.exception = exception;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x000CEDA8 File Offset: 0x000CE1A8
		public SqlErrorCollection Errors
		{
			get
			{
				return this.exception.Errors;
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000CEDC0 File Offset: 0x000CE1C0
		private bool ShouldSerializeErrors()
		{
			return this.exception != null && 0 < this.exception.Errors.Count;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000CEDEC File Offset: 0x000CE1EC
		public string Message
		{
			get
			{
				return this.exception.Message;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000CEE04 File Offset: 0x000CE204
		public string Source
		{
			get
			{
				return this.exception.Source;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000CEE1C File Offset: 0x000CE21C
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x040010BB RID: 4283
		private SqlException exception;
	}
}
