using System;
using System.IO;

namespace a.b
{
	// Token: 0x0200028C RID: 652
	internal class fu : em
	{
		// Token: 0x06001708 RID: 5896 RVA: 0x000694A5 File Offset: 0x000684A5
		public fu()
		{
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000694AD File Offset: 0x000684AD
		public fu(em A_0)
		{
			this.a(A_0.e());
			this.b(A_0.d());
			this.b(A_0.c());
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000694DC File Offset: 0x000684DC
		public new int a(Stream A_0, int A_1)
		{
			int num = 0;
			long num2 = this.d();
			if (A_1 == 1200 && num2 == 30L)
			{
				num2 = 31L;
			}
			return num + h7.a(A_0, (uint)num2) + e3.a(A_0, num2, this.c(), A_1);
		}
	}
}
