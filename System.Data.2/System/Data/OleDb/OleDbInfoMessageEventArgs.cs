using System;

namespace System.Data.OleDb
{
	// Token: 0x02000253 RID: 595
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x00100D4C File Offset: 0x0010014C
		internal OleDbInfoMessageEventArgs(OleDbException exception)
		{
			this.exception = exception;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00100D68 File Offset: 0x00100168
		public int ErrorCode
		{
			get
			{
				return this.exception.ErrorCode;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x00100D80 File Offset: 0x00100180
		public OleDbErrorCollection Errors
		{
			get
			{
				return this.exception.Errors;
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00100D98 File Offset: 0x00100198
		internal bool ShouldSerializeErrors()
		{
			return this.exception.ShouldSerializeErrors();
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x00100DB0 File Offset: 0x001001B0
		public string Message
		{
			get
			{
				return this.exception.Message;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00100DC8 File Offset: 0x001001C8
		public string Source
		{
			get
			{
				return this.exception.Source;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00100DE0 File Offset: 0x001001E0
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001726 RID: 5926
		private readonly OleDbException exception;
	}
}
