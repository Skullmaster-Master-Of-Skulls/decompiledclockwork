using System;

namespace a.b
{
	// Token: 0x0200028E RID: 654
	internal class et
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x00069534 File Offset: 0x00068534
		public et(byte[] A_0, int A_1)
		{
			this.b = p.k(A_0, A_1);
			int num = A_1 + 2;
			this.c = A_0[num];
			num++;
			this.d = A_0[num];
			num++;
			this.e = p.i(A_0, num);
			num += 4;
			this.f = p.g(A_0, num);
			num += 8;
		}

		// Token: 0x0400113F RID: 4415
		public const int a = 16;

		// Token: 0x04001140 RID: 4416
		private short b;

		// Token: 0x04001141 RID: 4417
		private byte c;

		// Token: 0x04001142 RID: 4418
		private byte d;

		// Token: 0x04001143 RID: 4419
		private int e;

		// Token: 0x04001144 RID: 4420
		private long f;
	}
}
