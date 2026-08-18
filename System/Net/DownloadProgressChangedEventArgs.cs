using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200049B RID: 1179
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x0008D32E File Offset: 0x0008C32E
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive) : base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x0008D347 File Offset: 0x0008C347
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0008D34F File Offset: 0x0008C34F
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x0400246D RID: 9325
		private long m_BytesReceived;

		// Token: 0x0400246E RID: 9326
		private long m_TotalBytesToReceive;
	}
}
