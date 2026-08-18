using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000282 RID: 642
	internal class bt
	{
		// Token: 0x060016D5 RID: 5845 RVA: 0x00068687 File Offset: 0x00067687
		public bt()
		{
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0006868F File Offset: 0x0006768F
		public bt(byte[] A_0, int A_1)
		{
			this.a(A_0, A_1);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000686A0 File Offset: 0x000676A0
		public int a(byte[] A_0, int A_1)
		{
			this.a = new bt.a(A_0, A_1);
			int num = A_1 + this.a.a();
			long num2 = this.a.c();
			if (num2 > 2147483647L)
			{
				throw new InvalidOperationException("Sorry, but POI can't store array of properties with size of " + num2 + " in memory");
			}
			int num3 = (int)num2;
			this.b = new ds[num3];
			int num4 = this.a.b;
			if (num4 == 12)
			{
				for (int i = 0; i < num3; i++)
				{
					ds ds = new ds();
					num += ds.c(A_0, num);
				}
			}
			else
			{
				for (int j = 0; j < num3; j++)
				{
					ds ds2 = new ds(num4, null);
					num += ds2.a(A_0, num);
				}
			}
			return num - A_1;
		}

		// Token: 0x040010F4 RID: 4340
		private bt.a a;

		// Token: 0x040010F5 RID: 4341
		private ds[] b;

		// Token: 0x02000283 RID: 643
		internal class b
		{
			// Token: 0x060016D8 RID: 5848 RVA: 0x00068767 File Offset: 0x00067767
			public b(byte[] A_0, int A_1)
			{
				this.c = p.h(A_0, A_1);
				this.b = p.i(A_0, A_1 + 4);
			}

			// Token: 0x040010F6 RID: 4342
			public const int a = 8;

			// Token: 0x040010F7 RID: 4343
			private int b;

			// Token: 0x040010F8 RID: 4344
			internal long c;
		}

		// Token: 0x02000284 RID: 644
		internal class a
		{
			// Token: 0x060016D9 RID: 5849 RVA: 0x0006878C File Offset: 0x0006778C
			public a(byte[] A_0, int A_1)
			{
				this.b = p.i(A_0, A_1);
				int num = A_1 + 4;
				long num2 = p.h(A_0, num);
				num += 4;
				if (1L > num2 || num2 > 31L)
				{
					throw new IllegalPropertySetDataException("Array dimension number " + num2 + " is not in [1; 31] range");
				}
				int num3 = (int)num2;
				this.a = new bt.b[num3];
				for (int i = 0; i < num3; i++)
				{
					this.a[i] = new bt.b(A_0, num);
					num += 8;
				}
			}

			// Token: 0x060016DA RID: 5850 RVA: 0x00068814 File Offset: 0x00067814
			public long c()
			{
				long num = 1L;
				foreach (bt.b b in this.a)
				{
					num *= b.c;
				}
				return num;
			}

			// Token: 0x060016DB RID: 5851 RVA: 0x00068847 File Offset: 0x00067847
			public int a()
			{
				return 8 + this.a.Length * 8;
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x00068855 File Offset: 0x00067855
			public int b()
			{
				return this.b;
			}

			// Token: 0x040010F9 RID: 4345
			private bt.b[] a;

			// Token: 0x040010FA RID: 4346
			internal int b;
		}
	}
}
