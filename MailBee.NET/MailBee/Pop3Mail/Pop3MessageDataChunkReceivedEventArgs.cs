using System;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x0200057F RID: 1407
	public class Pop3MessageDataChunkReceivedEventArgs : CommonEventArgs
	{
		// Token: 0x06002F42 RID: 12098 RVA: 0x000DFEA5 File Offset: 0x000DEEA5
		internal Pop3MessageDataChunkReceivedEventArgs(int A_0, int A_1, int A_2, int A_3, bc A_4) : base(A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000DFECC File Offset: 0x000DEECC
		public int MessageNumber
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000DFED4 File Offset: 0x000DEED4
		public int BytesJustReceived
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000DFEDC File Offset: 0x000DEEDC
		public int TotalBytesReceived
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000DFEE4 File Offset: 0x000DEEE4
		public int EstimatedDataLength
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x04001FFD RID: 8189
		private int a;

		// Token: 0x04001FFE RID: 8190
		private int b;

		// Token: 0x04001FFF RID: 8191
		private int c;

		// Token: 0x04002000 RID: 8192
		private int d;
	}
}
