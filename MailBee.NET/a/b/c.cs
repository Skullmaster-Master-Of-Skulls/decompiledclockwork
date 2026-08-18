using System;
using System.IO;

namespace a.b
{
	// Token: 0x02000286 RID: 646
	internal class c
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x000688A8 File Offset: 0x000678A8
		public c(byte[] A_0, int A_1)
		{
			int num = p.i(A_0, A_1);
			if (num < 4)
			{
				this.a = 0;
				this.b = new byte[0];
				return;
			}
			this.a = p.i(A_0, A_1 + 4);
			this.b = p.b(A_0, A_1 + 8, num - 4);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000688FC File Offset: 0x000678FC
		public int a()
		{
			return 8 + this.b.Length;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00068908 File Offset: 0x00067908
		public byte[] b()
		{
			return this.b;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00068910 File Offset: 0x00067910
		public byte[] c()
		{
			byte[] array = new byte[this.a()];
			p.c(array, 0, 4 + this.b.Length);
			p.c(array, 4, this.a);
			Array.Copy(this.b, 0, array, 8, this.b.Length);
			return array;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0006895E File Offset: 0x0006795E
		public int a(Stream A_0)
		{
			p.b(4 + this.b.Length, A_0);
			p.b(this.a, A_0);
			A_0.Write(this.b, 0, this.b.Length);
			return 8 + this.b.Length;
		}

		// Token: 0x040010FC RID: 4348
		private int a;

		// Token: 0x040010FD RID: 4349
		private byte[] b;
	}
}
