using System;
using MailBee;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x020003F1 RID: 1009
	internal class i : MailBeeException
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x0009657B File Offset: 0x0009557B
		public i(int A_0, short A_1, byte[] A_2, DnsReplyCode A_3) : base(A_0)
		{
			this.a = A_1;
			this.b = A_2;
			this.c = A_3;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0009659A File Offset: 0x0009559A
		public short c()
		{
			return this.a;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000965A2 File Offset: 0x000955A2
		public byte[] b()
		{
			return this.b;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000965AA File Offset: 0x000955AA
		public DnsReplyCode a()
		{
			return this.c;
		}

		// Token: 0x040017A4 RID: 6052
		private short a;

		// Token: 0x040017A5 RID: 6053
		private byte[] b;

		// Token: 0x040017A6 RID: 6054
		private DnsReplyCode c;
	}
}
