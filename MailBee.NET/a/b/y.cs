using System;

namespace a.b
{
	// Token: 0x020002A9 RID: 681
	internal class y
	{
		// Token: 0x060017CF RID: 6095 RVA: 0x0006D05A File Offset: 0x0006C05A
		internal y(int A_0, short A_1)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0006D070 File Offset: 0x0006C070
		public int f()
		{
			return this.a;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0006D078 File Offset: 0x0006C078
		public short d()
		{
			return this.b;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0006D080 File Offset: 0x0006C080
		public int c()
		{
			return this.a / 128;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006D08E File Offset: 0x0006C08E
		public int e()
		{
			return this.a / 4;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0006D098 File Offset: 0x0006C098
		public int b()
		{
			return this.e() - 1;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0006D0A2 File Offset: 0x0006C0A2
		public int a()
		{
			return this.b() * 4;
		}

		// Token: 0x040011DF RID: 4575
		private int a;

		// Token: 0x040011E0 RID: 4576
		private short b;
	}
}
