using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200017A RID: 378
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x00049AB8 File Offset: 0x00047CB8
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive) : base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00049AD1 File Offset: 0x00047CD1
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00049AD9 File Offset: 0x00047CD9
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x04001226 RID: 4646
		private long m_BytesReceived;

		// Token: 0x04001227 RID: 4647
		private long m_TotalBytesToReceive;
	}
}
