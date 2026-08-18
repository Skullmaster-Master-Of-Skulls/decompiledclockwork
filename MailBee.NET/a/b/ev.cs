using System;

namespace a.b
{
	// Token: 0x020002A5 RID: 677
	internal class ev
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x0006CB83 File Offset: 0x0006BB83
		public ev(byte[] A_0, int A_1, short A_2)
		{
			this.a = A_2;
			this.a(A_0, A_1);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0006CB9B File Offset: 0x0006BB9B
		public ev(short A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0006CBAC File Offset: 0x0006BBAC
		public int a(byte[] A_0, int A_1)
		{
			long num = p.h(A_0, A_1);
			int num2 = A_1 + 4;
			if (num > 2147483647L)
			{
				throw new InvalidOperationException("Vector is too long -- " + num);
			}
			int num3 = (int)num;
			this.b = new ds[num3];
			if (this.a == 12)
			{
				for (int i = 0; i < num3; i++)
				{
					ds ds = new ds();
					num2 += ds.c(A_0, num2);
					this.b[i] = ds;
				}
			}
			else
			{
				for (int j = 0; j < num3; j++)
				{
					ds ds2 = new ds((int)this.a, null);
					num2 += ds2.b(A_0, num2);
					this.b[j] = ds2;
				}
			}
			return num2 - A_1;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0006CC62 File Offset: 0x0006BC62
		public ds[] a()
		{
			return this.b;
		}

		// Token: 0x040011B0 RID: 4528
		private short a;

		// Token: 0x040011B1 RID: 4529
		private ds[] b;
	}
}
