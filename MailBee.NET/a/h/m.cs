using System;
using System.Collections;
using System.Text;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x020001FA RID: 506
	internal class m
	{
		// Token: 0x06001044 RID: 4164 RVA: 0x00044DF0 File Offset: 0x00043DF0
		public byte c()
		{
			return this.at;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00044DF8 File Offset: 0x00043DF8
		public int f()
		{
			return this.au;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00044E00 File Offset: 0x00043E00
		public int b()
		{
			return this.aw;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00044E08 File Offset: 0x00043E08
		public n a()
		{
			return this.ax;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00044E10 File Offset: 0x00043E10
		public object g()
		{
			if (this.ax != null)
			{
				this.a(this.ax);
			}
			return this.ay;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00044E2C File Offset: 0x00043E2C
		public m(byte A_0, int A_1, int A_2, object A_3)
		{
			this.at = A_0;
			this.au = A_1;
			this.av = A_2;
			this.ay = A_3;
			this.aw = -1;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00044E58 File Offset: 0x00043E58
		public m(byte A_0, int A_1, int A_2, n A_3)
		{
			this.at = A_0;
			this.au = A_1;
			this.av = A_2;
			this.ax = A_3;
			this.aw = ((A_3 != null) ? ((int)A_3.Length) : 0);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00044E92 File Offset: 0x00043E92
		public int d()
		{
			return this.av;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00044E9C File Offset: 0x00043E9C
		public void e()
		{
			if (this.ax != null)
			{
				this.ax.Close();
			}
			if (this.ay is n)
			{
				((n)this.ay).Close();
				return;
			}
			if (this.ay is h)
			{
				((h)this.ay).b();
				return;
			}
			if (this.ay is h[])
			{
				h[] array = (h[])this.ay;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].b();
				}
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00044F28 File Offset: 0x00043F28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Attr:").Append(" level=").Append(this.c()).Append(" type=").Append(global::a.h.f.a((long)this.f())).Append(" ID=").Append(global::a.h.f.a((long)this.d())).Append(" length=").Append(this.b());
			stringBuilder.Append(" value=");
			try
			{
				object obj = this.g();
				if (obj is h[])
				{
					h[] array = (h[])obj;
					for (int i = 0; i < array.Length; i++)
					{
						g[] array2 = array[i].c();
						for (int j = 0; j < array2.Length; j++)
						{
							stringBuilder.Append("\n  #").Append(i).Append(": ").Append(array2[j]);
						}
						array[i].b();
					}
				}
				else if (obj is h)
				{
					h h = (h)obj;
					g[] array3 = h.c();
					for (int k = 0; k < array3.Length; k++)
					{
						stringBuilder.Append("\n  ").Append(array3[k]);
					}
					h.b();
				}
				else if (obj is n)
				{
					stringBuilder.Append(obj);
					((n)obj).Close();
				}
				else
				{
					stringBuilder.Append(obj);
				}
			}
			catch (MailBeeTnefParsingException value)
			{
				stringBuilder.Append("INVALID VALUE: ").Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000450C4 File Offset: 0x000440C4
		protected void a(n A_0)
		{
			n n = new n(A_0);
			object obj = null;
			try
			{
				int num = this.av;
				if (num <= 1)
				{
					if (num == 0 || num == 1)
					{
						obj = new d(n);
					}
				}
				else if (num != 32768)
				{
					switch (num)
					{
					case 36866:
						obj = new e(n);
						break;
					case 36867:
						obj = new h(n);
						break;
					case 36868:
					{
						int num2 = (int)n.e();
						h[] array = new h[num2];
						for (int i = 0; i < num2; i++)
						{
							array[i] = new h(n);
						}
						obj = array;
						break;
					}
					}
				}
				else
				{
					obj = new j(n);
				}
				if (obj == null)
				{
					switch (this.au)
					{
					case 0:
						break;
					case 1:
					case 2:
					case 7:
						obj = n.a(this.aw);
						goto IL_171;
					case 3:
						if (n.Length < 14L)
						{
							goto IL_168;
						}
						try
						{
							obj = new DateTime((int)n.f(), (int)(n.f() - 1), (int)n.f(), (int)n.f(), (int)n.f(), (int)n.f());
							goto IL_171;
						}
						catch (ArgumentOutOfRangeException)
						{
							obj = DateTime.Now;
							goto IL_171;
						}
						break;
					case 4:
						obj = (short)n.f();
						goto IL_171;
					case 5:
						obj = (int)n.e();
						goto IL_171;
					case 6:
						obj = new n(n);
						goto IL_171;
					case 8:
						obj = (int)n.e();
						goto IL_171;
					default:
						goto IL_171;
					}
					obj = new j(n);
				}
				IL_168:;
			}
			finally
			{
				n.Close();
			}
			IL_171:
			this.ay = obj;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00045280 File Offset: 0x00044280
		public static m a(ArrayList A_0, int A_1)
		{
			int num = 0;
			while (A_0 != null && num < A_0.Count)
			{
				m m = (m)A_0[num];
				if (m.d() == A_1)
				{
					return m;
				}
				num++;
			}
			return null;
		}

		// Token: 0x04000BEF RID: 3055
		public const int a = 0;

		// Token: 0x04000BF0 RID: 3056
		public const int b = 1;

		// Token: 0x04000BF1 RID: 3057
		public const int c = 2;

		// Token: 0x04000BF2 RID: 3058
		public const int d = 3;

		// Token: 0x04000BF3 RID: 3059
		public const int e = 4;

		// Token: 0x04000BF4 RID: 3060
		public const int f = 5;

		// Token: 0x04000BF5 RID: 3061
		public const int g = 6;

		// Token: 0x04000BF6 RID: 3062
		public const int h = 7;

		// Token: 0x04000BF7 RID: 3063
		public const int i = 8;

		// Token: 0x04000BF8 RID: 3064
		public const int j = 9;

		// Token: 0x04000BF9 RID: 3065
		public const byte k = 1;

		// Token: 0x04000BFA RID: 3066
		public const byte l = 2;

		// Token: 0x04000BFB RID: 3067
		public const int m = 0;

		// Token: 0x04000BFC RID: 3068
		public const int n = 32768;

		// Token: 0x04000BFD RID: 3069
		public const int o = 32772;

		// Token: 0x04000BFE RID: 3070
		public const int p = 32773;

		// Token: 0x04000BFF RID: 3071
		public const int q = 32774;

		// Token: 0x04000C00 RID: 3072
		public const int r = 32775;

		// Token: 0x04000C01 RID: 3073
		public const int s = 32776;

		// Token: 0x04000C02 RID: 3074
		public const int t = 32777;

		// Token: 0x04000C03 RID: 3075
		public const int u = 32778;

		// Token: 0x04000C04 RID: 3076
		public const int v = 32779;

		// Token: 0x04000C05 RID: 3077
		public const int w = 32780;

		// Token: 0x04000C06 RID: 3078
		public const int x = 32781;

		// Token: 0x04000C07 RID: 3079
		public const int y = 32783;

		// Token: 0x04000C08 RID: 3080
		public const int z = 32784;

		// Token: 0x04000C09 RID: 3081
		public const int aa = 32785;

		// Token: 0x04000C0A RID: 3082
		public const int ab = 32786;

		// Token: 0x04000C0B RID: 3083
		public const int ac = 32787;

		// Token: 0x04000C0C RID: 3084
		public const int ad = 32800;

		// Token: 0x04000C0D RID: 3085
		public const int ae = 36865;

		// Token: 0x04000C0E RID: 3086
		public const int af = 36866;

		// Token: 0x04000C0F RID: 3087
		public const int ag = 36867;

		// Token: 0x04000C10 RID: 3088
		public const int ah = 36868;

		// Token: 0x04000C11 RID: 3089
		public const int ai = 36869;

		// Token: 0x04000C12 RID: 3090
		public const int aj = 36870;

		// Token: 0x04000C13 RID: 3091
		public const int ak = 36871;

		// Token: 0x04000C14 RID: 3092
		public const int al = 6;

		// Token: 0x04000C15 RID: 3093
		public const int am = 0;

		// Token: 0x04000C16 RID: 3094
		public const int an = 1;

		// Token: 0x04000C17 RID: 3095
		public const int ao = 2;

		// Token: 0x04000C18 RID: 3096
		public const int ap = 6;

		// Token: 0x04000C19 RID: 3097
		public const int aq = 7;

		// Token: 0x04000C1A RID: 3098
		public const int ar = 8;

		// Token: 0x04000C1B RID: 3099
		public const int @as = 9;

		// Token: 0x04000C1C RID: 3100
		private byte at;

		// Token: 0x04000C1D RID: 3101
		private int au;

		// Token: 0x04000C1E RID: 3102
		private int av;

		// Token: 0x04000C1F RID: 3103
		private int aw;

		// Token: 0x04000C20 RID: 3104
		private n ax;

		// Token: 0x04000C21 RID: 3105
		private object ay;
	}
}
