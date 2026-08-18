using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200027D RID: 637
	internal class dk
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x000680BD File Offset: 0x000670BD
		public virtual string b()
		{
			return this.a;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000680C5 File Offset: 0x000670C5
		public virtual dk.a c()
		{
			return this.b.b;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000680D2 File Offset: 0x000670D2
		public virtual int a()
		{
			return this.b.c;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000680DF File Offset: 0x000670DF
		public virtual int f()
		{
			return this.b.d;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000680EC File Offset: 0x000670EC
		public virtual int d()
		{
			return this.b.e;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000680F9 File Offset: 0x000670F9
		public virtual dk.a e()
		{
			return this.b.g;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00068106 File Offset: 0x00067106
		public virtual dk.a g()
		{
			return this.b.f;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00068114 File Offset: 0x00067114
		internal dk(byte[] A_0)
		{
			this.b = null;
			this.a = "";
			try
			{
				int num = (int)ii.b(A_0, 2, 4);
				int num2 = 2 * (int)ii.b(A_0, 6, 8);
				string @string = Encoding.GetEncoding("UTF-16LE").GetString(A_0, 0, A_0.Length);
				this.a = new string(@string.ToCharArray(), 8, num2);
				int num3 = 8 + num2;
				int num4 = (int)ii.b(A_0, num3, num3 + 2);
				num3 = 4 + num;
				for (int i = 0; i < num4; i++)
				{
					if (((int)ii.b(A_0, num3 + 4, num3 + 6) & 2) != 0)
					{
						this.b = new dk.b(this, A_0, num3 + 6);
						break;
					}
					num3 += 66;
				}
			}
			catch (Exception)
			{
				this.b = null;
				this.a = "";
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000681EC File Offset: 0x000671EC
		internal dk(string A_0, byte[] A_1)
		{
			this.a = A_0;
			this.b = null;
			try
			{
				this.b = new dk.b(this, new dk.a(this), A_1, 0);
			}
			catch (Exception)
			{
				this.b = null;
				A_0 = "";
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00068244 File Offset: 0x00067244
		public virtual bool a(dk A_0)
		{
			return this.a.ToUpper().Equals(A_0.a.ToUpper()) && this.b.a(A_0.b);
		}

		// Token: 0x040010E1 RID: 4321
		private string a;

		// Token: 0x040010E2 RID: 4322
		private dk.b b;

		// Token: 0x0200027E RID: 638
		public class a
		{
			// Token: 0x060016C1 RID: 5825 RVA: 0x00068279 File Offset: 0x00067279
			private void a(dk A_0)
			{
				this.a = A_0;
			}

			// Token: 0x060016C2 RID: 5826 RVA: 0x00068282 File Offset: 0x00067282
			public dk a()
			{
				return this.a;
			}

			// Token: 0x060016C3 RID: 5827 RVA: 0x0006828C File Offset: 0x0006728C
			internal a(dk A_0)
			{
				this.a(A_0);
				this.b = 0;
				this.c = 0;
				this.d = 0;
				this.e = 0;
				this.f = 0;
				this.g = 0;
				this.h = 0;
				this.i = 0;
			}

			// Token: 0x060016C4 RID: 5828 RVA: 0x000682E0 File Offset: 0x000672E0
			internal a(dk A_0, byte[] A_1, int A_2)
			{
				this.a(A_0);
				this.b = (short)(ii.b(A_1, A_2, A_2 + 2) & 32767L);
				this.c = (short)(ii.b(A_1, A_2 + 2, A_2 + 4) & 32767L);
				this.d = (short)(ii.b(A_1, A_2 + 4, A_2 + 6) & 32767L);
				this.e = (short)(ii.b(A_1, A_2 + 6, A_2 + 8) & 32767L);
				this.f = (short)(ii.b(A_1, A_2 + 8, A_2 + 10) & 32767L);
				this.g = (short)(ii.b(A_1, A_2 + 10, A_2 + 12) & 32767L);
				this.h = (short)(ii.b(A_1, A_2 + 12, A_2 + 14) & 32767L);
				this.i = (short)(ii.b(A_1, A_2 + 14, A_2 + 16) & 32767L);
			}

			// Token: 0x060016C5 RID: 5829 RVA: 0x000683D0 File Offset: 0x000673D0
			internal virtual bool a(dk.a A_0)
			{
				return this.b == A_0.b && this.c == A_0.c && this.d == A_0.d && this.e == A_0.e && this.f == A_0.f && this.g == A_0.g && this.h == A_0.h && this.i == A_0.i;
			}

			// Token: 0x040010E3 RID: 4323
			private dk a;

			// Token: 0x040010E4 RID: 4324
			public short b;

			// Token: 0x040010E5 RID: 4325
			public short c;

			// Token: 0x040010E6 RID: 4326
			public short d;

			// Token: 0x040010E7 RID: 4327
			public short e;

			// Token: 0x040010E8 RID: 4328
			public short f;

			// Token: 0x040010E9 RID: 4329
			public short g;

			// Token: 0x040010EA RID: 4330
			public short h;

			// Token: 0x040010EB RID: 4331
			public short i;
		}

		// Token: 0x0200027F RID: 639
		private class b
		{
			// Token: 0x060016C6 RID: 5830 RVA: 0x0006844F File Offset: 0x0006744F
			private void a(dk A_0)
			{
				this.a = A_0;
			}

			// Token: 0x060016C7 RID: 5831 RVA: 0x00068458 File Offset: 0x00067458
			public dk a()
			{
				return this.a;
			}

			// Token: 0x060016C8 RID: 5832 RVA: 0x00068460 File Offset: 0x00067460
			internal b(dk A_0, dk.a A_1, byte[] A_2, int A_3)
			{
				this.a(A_0);
				this.b = A_1;
				this.a(A_2, A_3);
				ii.b(A_2, A_3 + 12, A_3 + 14);
				this.f = new dk.a(A_0, A_2, A_3 + 14);
				ii.b(A_2, A_3 + 30, A_3 + 32);
				this.g = new dk.a(A_0, A_2, A_3 + 32);
			}

			// Token: 0x060016C9 RID: 5833 RVA: 0x000684D0 File Offset: 0x000674D0
			internal b(dk A_0, byte[] A_1, int A_2)
			{
				this.a(A_0);
				this.b = new dk.a(A_0, A_1, A_2);
				this.a(A_1, A_2 + 16);
				this.f = new dk.a(A_0, A_1, A_2 + 28);
				this.g = new dk.a(A_0, A_1, A_2 + 44);
			}

			// Token: 0x060016CA RID: 5834 RVA: 0x00068525 File Offset: 0x00067525
			private void a(byte[] A_0, int A_1)
			{
				this.c = (int)ii.b(A_0, A_1, A_1 + 4);
				this.d = (int)ii.b(A_0, A_1 + 4, A_1 + 8);
				this.e = (int)ii.b(A_0, A_1 + 8, A_1 + 12);
			}

			// Token: 0x060016CB RID: 5835 RVA: 0x00068560 File Offset: 0x00067560
			internal virtual bool a(dk.b A_0)
			{
				return this.b.a(A_0.b) && this.c == A_0.c && this.d == A_0.d && this.e == A_0.e && this.f.a(A_0.f) && this.g.a(A_0.g);
			}

			// Token: 0x040010EC RID: 4332
			private dk a;

			// Token: 0x040010ED RID: 4333
			internal dk.a b;

			// Token: 0x040010EE RID: 4334
			internal int c;

			// Token: 0x040010EF RID: 4335
			internal int d;

			// Token: 0x040010F0 RID: 4336
			internal int e;

			// Token: 0x040010F1 RID: 4337
			internal dk.a f;

			// Token: 0x040010F2 RID: 4338
			internal dk.a g;
		}
	}
}
