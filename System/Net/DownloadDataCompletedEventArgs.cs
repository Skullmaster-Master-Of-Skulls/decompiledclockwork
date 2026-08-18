using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000491 RID: 1169
	public class DownloadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x0008D289 File Offset: 0x0008C289
		internal DownloadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x0008D29C File Offset: 0x0008C29C
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04002468 RID: 9320
		private byte[] m_Result;
	}
}
