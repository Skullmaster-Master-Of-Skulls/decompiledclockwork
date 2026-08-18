using System;
using System.IO;

namespace a.b
{
	// Token: 0x02000293 RID: 659
	internal class gv
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00069A9D File Offset: 0x00068A9D
		public gv(byte[] A_0, int A_1)
		{
			this.c = p.i(A_0, A_1);
			this.b = p.i(A_0, A_1 + 4);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00069AC1 File Offset: 0x00068AC1
		public gv(int A_0, int A_1)
		{
			this.c = A_0;
			this.b = A_1;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00069AD7 File Offset: 0x00068AD7
		public long a()
		{
			return (long)this.b;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00069AE0 File Offset: 0x00068AE0
		public long b()
		{
			return (long)this.c;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00069AE9 File Offset: 0x00068AE9
		public byte[] c()
		{
			byte[] array = new byte[8];
			p.c(array, 0, this.c);
			p.c(array, 4, this.b);
			return array;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00069B0B File Offset: 0x00068B0B
		public int a(Stream A_0)
		{
			p.b(this.c, A_0);
			p.b(this.b, A_0);
			return 8;
		}

		// Token: 0x0400114A RID: 4426
		public const int a = 8;

		// Token: 0x0400114B RID: 4427
		private int b;

		// Token: 0x0400114C RID: 4428
		private int c;
	}
}
