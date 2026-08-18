using System;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000184 RID: 388
	public class ImapEnvelopeDataChunkReceivedEventArgs : CommonEventArgs
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x00035852 File Offset: 0x00034852
		internal ImapEnvelopeDataChunkReceivedEventArgs(int A_0, int A_1, int A_2, bc A_3) : base(A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00035871 File Offset: 0x00034871
		public int MessageNumber
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00035879 File Offset: 0x00034879
		public int BytesJustReceived
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00035881 File Offset: 0x00034881
		public int TotalBytesReceived
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x04000934 RID: 2356
		private int a;

		// Token: 0x04000935 RID: 2357
		private int b;

		// Token: 0x04000936 RID: 2358
		private int c;
	}
}
