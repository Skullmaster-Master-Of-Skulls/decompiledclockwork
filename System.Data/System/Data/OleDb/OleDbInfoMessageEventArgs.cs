using System;

namespace System.Data.OleDb
{
	// Token: 0x0200022B RID: 555
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001FA0 RID: 8096 RVA: 0x0027B968 File Offset: 0x0027AD68
		internal OleDbInfoMessageEventArgs(OleDbException exception)
		{
			this.exception = exception;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0027B988 File Offset: 0x0027AD88
		public int ErrorCode
		{
			get
			{
				return this.exception.ErrorCode;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0027B9A8 File Offset: 0x0027ADA8
		public OleDbErrorCollection Errors
		{
			get
			{
				return this.exception.Errors;
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0027B9C8 File Offset: 0x0027ADC8
		internal bool ShouldSerializeErrors()
		{
			return this.exception.ShouldSerializeErrors();
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0027B9E8 File Offset: 0x0027ADE8
		public string Message
		{
			get
			{
				return this.exception.Message;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0027BA08 File Offset: 0x0027AE08
		public string Source
		{
			get
			{
				return this.exception.Source;
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0027BA28 File Offset: 0x0027AE28
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001411 RID: 5137
		private readonly OleDbException exception;
	}
}
