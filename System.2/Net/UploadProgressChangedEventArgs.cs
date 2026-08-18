using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200017C RID: 380
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00049AE1 File Offset: 0x00047CE1
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive) : base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
			this.m_BytesSent = bytesSent;
			this.m_TotalBytesToSend = totalBytesToSend;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00049B0A File Offset: 0x00047D0A
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00049B12 File Offset: 0x00047D12
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00049B1A File Offset: 0x00047D1A
		public long BytesSent
		{
			get
			{
				return this.m_BytesSent;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00049B22 File Offset: 0x00047D22
		public long TotalBytesToSend
		{
			get
			{
				return this.m_TotalBytesToSend;
			}
		}

		// Token: 0x04001228 RID: 4648
		private long m_BytesReceived;

		// Token: 0x04001229 RID: 4649
		private long m_TotalBytesToReceive;

		// Token: 0x0400122A RID: 4650
		private long m_BytesSent;

		// Token: 0x0400122B RID: 4651
		private long m_TotalBytesToSend;
	}
}
