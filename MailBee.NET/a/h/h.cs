using System;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x020001FD RID: 509
	internal class h
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0004590D File Offset: 0x0004490D
		public g[] c()
		{
			return this.a;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00045915 File Offset: 0x00044915
		private int a()
		{
			return this.b;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00045920 File Offset: 0x00044920
		public h(n A_0)
		{
			long position = A_0.Position;
			int num = (int)A_0.e();
			this.a = new g[num];
			int i = 0;
			while (i < num)
			{
				g g = this.a[i] = new g();
				g.b((int)A_0.f());
				bool flag = (g.f() & 4096) != 0;
				g.b(g.f() & -4097);
				int num2 = g.f();
				if (num2 <= 30)
				{
					if (num2 == 13 || num2 == 30)
					{
						goto IL_94;
					}
				}
				else if (num2 == 31 || num2 == 258)
				{
					goto IL_94;
				}
				IL_97:
				g.a((int)A_0.f());
				if (g.c() >= 32768 && g.c() <= 65534)
				{
					l a_ = new l(A_0);
					g.a(a_);
				}
				int num3 = 1;
				if (flag)
				{
					num3 = (int)A_0.e();
				}
				g.a(new a[num3]);
				int j = 0;
				while (j < g.e().Length)
				{
					num2 = g.f();
					if (num2 <= 31)
					{
						switch (num2)
						{
						case 1:
							g.e()[j] = null;
							break;
						case 2:
						case 3:
						case 4:
						case 10:
						case 11:
							g.e()[j] = new a(g.f(), A_0, 4);
							break;
						case 5:
						case 6:
						case 7:
						case 20:
							goto IL_1B0;
						case 8:
						case 9:
						case 12:
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
							goto IL_216;
						case 13:
							goto IL_1E1;
						default:
							if (num2 != 30 && num2 != 31)
							{
								goto IL_216;
							}
							goto IL_1E1;
						}
					}
					else
					{
						if (num2 == 64)
						{
							goto IL_1B0;
						}
						if (num2 != 72)
						{
							if (num2 != 258)
							{
								goto IL_216;
							}
							goto IL_1E1;
						}
						else
						{
							g.e()[j] = new a(g.f(), A_0, 16);
						}
					}
					IL_23B:
					j++;
					continue;
					IL_1B0:
					g.e()[j] = new a(g.f(), A_0, 8);
					goto IL_23B;
					IL_1E1:
					int num4 = (int)A_0.e();
					g.e()[j] = new a(g.f(), A_0, num4);
					if (num4 % 4 != 0)
					{
						A_0.a((long)(4 - num4 % 4));
						goto IL_23B;
					}
					goto IL_23B;
					IL_216:
					throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefMapiTypeUnknown0, g.f()), 1005);
				}
				i++;
				continue;
				IL_94:
				flag = true;
				goto IL_97;
			}
			this.b = (int)(A_0.Position - position);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00045B97 File Offset: 0x00044B97
		public h(g[] A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00045BA6 File Offset: 0x00044BA6
		public g b(int A_0)
		{
			return g.a(this.a, A_0);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00045BB4 File Offset: 0x00044BB4
		public g b(l A_0)
		{
			return g.a(this.a, A_0);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00045BC4 File Offset: 0x00044BC4
		public object a(int A_0)
		{
			g g = this.b(A_0);
			if (g == null)
			{
				return null;
			}
			return g.g();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00045BE4 File Offset: 0x00044BE4
		public object a(l A_0)
		{
			g g = this.b(A_0);
			if (g == null)
			{
				return null;
			}
			return g.g();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00045C04 File Offset: 0x00044C04
		public void b()
		{
			if (this.a != null)
			{
				for (int i = 0; i < this.a.Length; i++)
				{
					this.a[i].a();
				}
			}
		}

		// Token: 0x04000E35 RID: 3637
		private g[] a;

		// Token: 0x04000E36 RID: 3638
		private int b;
	}
}
