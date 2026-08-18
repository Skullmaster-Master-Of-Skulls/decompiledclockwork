using System;

namespace a.b
{
	// Token: 0x020002A6 RID: 678
	internal class fq
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x0006CC6A File Offset: 0x0006BC6A
		public fq(byte[] A_0, int A_1)
		{
			this.a = new ai(A_0, A_1);
			this.b = new @if(A_0, A_1 + 16);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0006CC8F File Offset: 0x0006BC8F
		public int a()
		{
			return 16 + this.b.a();
		}

		// Token: 0x040011B2 RID: 4530
		private ai a;

		// Token: 0x040011B3 RID: 4531
		private @if b;
	}
}
