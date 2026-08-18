using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200049D RID: 1181
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x0008D357 File Offset: 0x0008C357
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive) : base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
			this.m_BytesSent = bytesSent;
			this.m_TotalBytesToSend = totalBytesToSend;
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x0008D380 File Offset: 0x0008C380
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0008D388 File Offset: 0x0008C388
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x0008D390 File Offset: 0x0008C390
		public long BytesSent
		{
			get
			{
				return this.m_BytesSent;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0008D398 File Offset: 0x0008C398
		public long TotalBytesToSend
		{
			get
			{
				return this.m_TotalBytesToSend;
			}
		}

		// Token: 0x0400246F RID: 9327
		private long m_BytesReceived;

		// Token: 0x04002470 RID: 9328
		private long m_TotalBytesToReceive;

		// Token: 0x04002471 RID: 9329
		private long m_BytesSent;

		// Token: 0x04002472 RID: 9330
		private long m_TotalBytesToSend;
	}
}
