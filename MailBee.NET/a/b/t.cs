using System;

namespace a.b
{
	// Token: 0x02000285 RID: 645
	internal class t
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x00068860 File Offset: 0x00067860
		public t(byte[] A_0, int A_1)
		{
			int num = p.i(A_0, A_1);
			if (num == 0)
			{
				this.a = new byte[0];
				return;
			}
			this.a = p.b(A_0, A_1 + 4, num);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0006889B File Offset: 0x0006789B
		public int a()
		{
			return 4 + this.a.Length;
		}

		// Token: 0x040010FB RID: 4347
		private byte[] a;
	}
}
