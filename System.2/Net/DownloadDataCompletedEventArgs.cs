using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000170 RID: 368
	public class DownloadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DEB RID: 3563 RVA: 0x00049A13 File Offset: 0x00047C13
		internal DownloadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00049A26 File Offset: 0x00047C26
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001221 RID: 4641
		private byte[] m_Result;
	}
}
