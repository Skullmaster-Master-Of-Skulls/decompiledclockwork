using System;
using System.Collections;

namespace a.m
{
	// Token: 0x0200020F RID: 527
	internal class b
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x0004B1CC File Offset: 0x0004A1CC
		private static bool a(double A_0)
		{
			return A_0 >= 0.4 && A_0 <= 0.6;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004B1EB File Offset: 0x0004A1EB
		public b()
		{
			this.h = 0U;
			this.i = 0U;
			this.j = new ArrayList();
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004B20C File Offset: 0x0004A20C
		private string c(int A_0)
		{
			return ((a)this.j[A_0]).a;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004B224 File Offset: 0x0004A224
		private double b(int A_0)
		{
			return ((a)this.j[A_0]).e;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004B23C File Offset: 0x0004A23C
		private bool a(int A_0)
		{
			return ((a)this.j[A_0]).f;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0004B254 File Offset: 0x0004A254
		private void a(int A_0, bool A_1)
		{
			a a = (a)this.j[A_0];
			a.f = A_1;
			this.j[A_0] = a;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004B288 File Offset: 0x0004A288
		public void a(ArrayList A_0)
		{
			int num = -1;
			bool flag = false;
			int num2 = 1;
			int num3 = -1;
			this.h = 0U;
			this.i = 0U;
			this.j = A_0;
			int count = this.j.Count;
			for (int i = 0; i < count; i++)
			{
				int num4 = i;
				int num5;
				if (i + 1 < count)
				{
					num5 = i + 1;
				}
				else
				{
					num5 = num4;
				}
				if (flag && num != -1 && (this.b(num4) >= 0.8 || (num5 != -1 && this.b(num4) <= 0.25 && this.b(num5) <= 0.25)))
				{
					flag = false;
					int num6 = i;
					if ((long)num2 < 4L && num2 > 1)
					{
						int num7 = num3 + 1;
						while (num7 != num6 && num7 < count)
						{
							if (this.a(num7))
							{
								this.a(num7, false);
								this.i -= 1U;
							}
							num7++;
						}
					}
				}
				if (flag)
				{
					num2++;
				}
				if (flag || (-1 == this.c(num4).IndexOf('*') && this.b(num4) <= 0.3 && ((num != -1 && global::a.m.b.a(this.b(num))) || (num5 != -1 && global::a.m.b.a(this.b(num5))))))
				{
					if (!this.a(num4))
					{
						this.i += 1U;
						this.a(num4, true);
					}
					if (this.b(num4) <= 0.3)
					{
						if (flag && -1 == this.c(num4).IndexOf('*'))
						{
							num3 = i;
						}
						else
						{
							num3 = i;
							flag = true;
							num2 = 1;
						}
					}
				}
				else
				{
					this.h += 1U;
				}
				num = num4;
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004B43C File Offset: 0x0004A43C
		public ArrayList a()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.j.Count; i++)
			{
				a a = (a)this.j[i];
				if (!a.f)
				{
					arrayList.Add(a);
				}
			}
			return arrayList;
		}

		// Token: 0x04000EB7 RID: 3767
		private const uint a = 4U;

		// Token: 0x04000EB8 RID: 3768
		private const double b = 0.3;

		// Token: 0x04000EB9 RID: 3769
		private const double c = 0.9;

		// Token: 0x04000EBA RID: 3770
		private const double d = 0.4;

		// Token: 0x04000EBB RID: 3771
		private const double e = 0.6;

		// Token: 0x04000EBC RID: 3772
		private const double f = 0.25;

		// Token: 0x04000EBD RID: 3773
		private const double g = 0.8;

		// Token: 0x04000EBE RID: 3774
		private uint h;

		// Token: 0x04000EBF RID: 3775
		private uint i;

		// Token: 0x04000EC0 RID: 3776
		private ArrayList j;
	}
}
