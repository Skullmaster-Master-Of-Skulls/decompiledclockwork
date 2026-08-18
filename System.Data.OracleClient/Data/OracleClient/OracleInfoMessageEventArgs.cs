using System;

namespace System.Data.OracleClient
{
	// Token: 0x02000067 RID: 103
	public sealed class OracleInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x000696B4 File Offset: 0x00068AB4
		internal OracleInfoMessageEventArgs(OracleException exception)
		{
			this.exception = exception;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000696D4 File Offset: 0x00068AD4
		public int Code
		{
			get
			{
				return this.exception.Code;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000696F4 File Offset: 0x00068AF4
		public string Message
		{
			get
			{
				return this.exception.Message;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00069714 File Offset: 0x00068B14
		public string Source
		{
			get
			{
				return this.exception.Source;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00069734 File Offset: 0x00068B34
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x0400042D RID: 1069
		private OracleException exception;
	}
}
