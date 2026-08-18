using System;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000182 RID: 386
	public class ImapEnvelopeDownloadedEventArgs : CommonEventArgs
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00035812 File Offset: 0x00034812
		internal ImapEnvelopeDownloadedEventArgs(int A_0, int A_1, Envelope A_2, bc A_3) : base(A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00035831 File Offset: 0x00034831
		public int MessageNumber
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00035839 File Offset: 0x00034839
		public int DataLength
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00035841 File Offset: 0x00034841
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00035849 File Offset: 0x00034849
		public Envelope DownloadedEnvelope
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x04000931 RID: 2353
		private int a;

		// Token: 0x04000932 RID: 2354
		private int b;

		// Token: 0x04000933 RID: 2355
		private Envelope c;
	}
}
