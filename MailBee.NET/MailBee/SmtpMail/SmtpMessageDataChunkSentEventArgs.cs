using System;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200014A RID: 330
	public class SmtpMessageDataChunkSentEventArgs : CommonEventArgs
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x00031168 File Offset: 0x00030168
		internal SmtpMessageDataChunkSentEventArgs(MailMessage A_0, int A_1, int A_2, int A_3, bc A_4) : base(A_4)
		{
			this.d = A_0;
			this.a = A_1;
			this.b = A_2;
			this.c = A_3;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0003118F File Offset: 0x0003018F
		public int BytesJustSent
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00031197 File Offset: 0x00030197
		public int TotalBytesSent
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0003119F File Offset: 0x0003019F
		public int DataTotalLength
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x000311A7 File Offset: 0x000301A7
		public MailMessage MailMessage
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x04000850 RID: 2128
		private int a;

		// Token: 0x04000851 RID: 2129
		private int b;

		// Token: 0x04000852 RID: 2130
		private int c;

		// Token: 0x04000853 RID: 2131
		private MailMessage d;
	}
}
