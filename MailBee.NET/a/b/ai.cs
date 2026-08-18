using System;

namespace a.b
{
	// Token: 0x02000294 RID: 660
	internal class ai
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00069B28 File Offset: 0x00068B28
		public ai(byte[] A_0, int A_1)
		{
			this.b = p.i(A_0, A_1);
			this.c = p.k(A_0, A_1 + 4);
			this.d = p.k(A_0, A_1 + 6);
			this.e = p.g(A_0, A_1 + 8);
		}

		// Token: 0x0400114D RID: 4429
		public const int a = 16;

		// Token: 0x0400114E RID: 4430
		private int b;

		// Token: 0x0400114F RID: 4431
		private short c;

		// Token: 0x04001150 RID: 4432
		private short d;

		// Token: 0x04001151 RID: 4433
		private long e;
	}
}
