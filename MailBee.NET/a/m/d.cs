using System;
using System.Collections;
using MailBee.AntiSpam;

namespace a.m
{
	// Token: 0x02000207 RID: 519
	internal class d
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x000476F5 File Offset: 0x000466F5
		public void b(double A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000476FE File Offset: 0x000466FE
		public void c(double A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00047707 File Offset: 0x00046707
		public void a(int A_0)
		{
			this.f = A_0;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00047710 File Offset: 0x00046710
		public void a(BayesAlgorithm A_0)
		{
			this.g = A_0;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00047719 File Offset: 0x00046719
		public void a(bool A_0)
		{
			this.h = A_0;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00047724 File Offset: 0x00046724
		public d(BayesFilter A_0, c A_1)
		{
			this.j = A_0;
			this.i = A_1;
			this.c = 1.0;
			this.d = 0.0178;
			this.e = 0.52;
			this.f = 20;
			this.g = BayesAlgorithm.ChiSquareAlgorithm;
			this.h = true;
			int num = this.i.c();
			int num2 = this.i.b();
			if (num < 100)
			{
				this.a = 0.01;
			}
			else if (num < 1000)
			{
				this.a = 0.001;
			}
			else if (num < 10000)
			{
				this.a = 0.0001;
			}
			else
			{
				this.a = 1E-05;
			}
			if (num2 < 100)
			{
				this.b = 0.99;
				return;
			}
			if (num2 < 1000)
			{
				this.b = 0.999;
				return;
			}
			if (num2 < 10000)
			{
				this.b = 0.9999;
				return;
			}
			this.b = 0.99999;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004784C File Offset: 0x0004684C
		private double a(int A_0, int A_1)
		{
			double num = 0.5;
			double num2 = (double)A_1 * this.c;
			double num3 = (double)A_0;
			if (num2 + num3 > 5.0)
			{
				num2 /= Math.Max(1.0, (double)this.i.c());
				num3 /= Math.Max(1.0, (double)this.i.b());
				num = Math.Min(1.0, num3 / (num2 + num3));
				num = Math.Max(this.a, Math.Min(this.b, num));
			}
			if (this.g == BayesAlgorithm.ChiSquareAlgorithm)
			{
				num = (this.d * this.e + ((double)A_1 + (double)A_0) * num) / (this.d + ((double)A_1 + (double)A_0));
			}
			return num;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00047914 File Offset: 0x00046914
		private void b(ArrayList A_0)
		{
			for (int i = 0; i < A_0.Count; i++)
			{
				a a = (a)A_0[i];
				int a_;
				int a_2;
				if (this.i.a(a.a, out a_, out a_2))
				{
					a.e = this.a(a_2, a_);
				}
				else
				{
					a.e = 0.4;
				}
				A_0[i] = a;
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00047980 File Offset: 0x00046980
		public void b(ref ArrayList A_0)
		{
			this.b(A_0);
			b b = new b();
			b.a(A_0);
			A_0 = b.a();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000479AC File Offset: 0x000469AC
		private int a(ArrayList A_0)
		{
			double num = 1.0;
			double num2 = 1.0;
			if (this.g == BayesAlgorithm.GrahamAlgorithm)
			{
				for (int i = 0; i < A_0.Count; i++)
				{
					a a = (a)A_0[i];
					num *= a.e;
					num2 *= 1.0 - a.e;
				}
				if (num > 0.0 || num2 > 0.0)
				{
					return (int)(num / (num + num2) * 100.0 + 0.5);
				}
			}
			else
			{
				double num3 = 0.0;
				double num4 = 0.0;
				for (int j = 0; j < A_0.Count; j++)
				{
					a a2 = (a)A_0[j];
					num *= a2.e;
					num2 *= 1.0 - a2.e;
					if (num2 < 1E-200)
					{
						double num5;
						num2 = global::a.m.d.a(num2, out num5);
						num4 += num5;
					}
					if (num < 1E-200)
					{
						double num5;
						num = global::a.m.d.a(num, out num5);
						num3 += num5;
					}
				}
				num2 = Math.Log(num2) + num4 * Math.Log(2.0);
				num = Math.Log(num) + num3 * Math.Log(2.0);
				if (A_0.Count > 0)
				{
					num = global::a.m.d.a(-2.0 * num, (uint)(2 * A_0.Count));
					num2 = global::a.m.d.a(-2.0 * num2, (uint)(2 * A_0.Count));
					return (int)((num - num2 + 1.0) / 2.0 * 100.0 + 0.5);
				}
			}
			return 50;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00047B74 File Offset: 0x00046B74
		private void a(ref ArrayList A_0)
		{
			int i = 0;
			while (i < A_0.Count)
			{
				if (global::a.m.d.a(((a)A_0[i]).d))
				{
					A_0.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00047BB6 File Offset: 0x00046BB6
		private static bool a(double A_0)
		{
			if (A_0 > 0.0)
			{
				return A_0 < 1E-07;
			}
			return A_0 > -1E-07;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00047BE0 File Offset: 0x00046BE0
		private static double a(double A_0, uint A_1)
		{
			A_0 /= 2.0;
			double num2;
			double num = num2 = Math.Exp(-A_0);
			for (uint num3 = 1U; num3 < A_1 / 2U; num3 += 1U)
			{
				num2 *= A_0 / num3;
				num += num2;
			}
			return Math.Min(1.0, num);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00047C30 File Offset: 0x00046C30
		private static double a(double A_0, out double A_1)
		{
			double num = (A_0 >= 0.0) ? 0.5 : -0.5;
			A_0 /= num;
			A_1 = Math.Log(A_0, 2.0);
			return num;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00047C78 File Offset: 0x00046C78
		public int a(ArrayList A_0, bool A_1)
		{
			if (this.i.c() == 0 || this.i.b() == 0)
			{
				return 50;
			}
			for (int i = 0; i < A_0.Count; i++)
			{
				a a = (a)A_0[i];
				int num;
				int num2;
				if (this.i.a(a.a, out num, out num2))
				{
					if (A_1)
					{
						a.e = this.a(num2, num);
					}
				}
				else
				{
					a.e = 0.4;
				}
				double num3 = 0.5 - a.e;
				a.d = ((num3 > 0.0) ? num3 : (-num3));
				if (a.e >= 0.5)
				{
					a.c = (double)num2 / (double)this.i.b();
				}
				else
				{
					a.c = (double)num / (double)this.i.c();
				}
				A_0[i] = a;
			}
			if (this.h)
			{
				this.a(ref A_0);
			}
			g comparer = new g();
			A_0.Sort(comparer);
			if (A_0.Count > this.f)
			{
				A_0.RemoveRange(this.f, A_0.Count - this.f);
			}
			return this.a(A_0);
		}

		// Token: 0x04000E63 RID: 3683
		private double a;

		// Token: 0x04000E64 RID: 3684
		private double b;

		// Token: 0x04000E65 RID: 3685
		private double c;

		// Token: 0x04000E66 RID: 3686
		private double d;

		// Token: 0x04000E67 RID: 3687
		private double e;

		// Token: 0x04000E68 RID: 3688
		private int f;

		// Token: 0x04000E69 RID: 3689
		private BayesAlgorithm g;

		// Token: 0x04000E6A RID: 3690
		private bool h;

		// Token: 0x04000E6B RID: 3691
		private c i;

		// Token: 0x04000E6C RID: 3692
		private BayesFilter j;
	}
}
