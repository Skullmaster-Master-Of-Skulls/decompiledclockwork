using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200027C RID: 636
	internal class cv : co
	{
		// Token: 0x060016A3 RID: 5795 RVA: 0x00067BC4 File Offset: 0x00066BC4
		public new virtual int d()
		{
			return this.h(this.u.b(33025, 10));
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00067BDE File Offset: 0x00066BDE
		public new virtual double n()
		{
			return this.k(this.u.b(33026, 10));
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00067BF8 File Offset: 0x00066BF8
		public new virtual bool g()
		{
			return this.e(this.u.b(33027, 10));
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00067C12 File Offset: 0x00066C12
		public new virtual DateTime c()
		{
			return this.f(this.u.b(33039, 10));
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00067C2C File Offset: 0x00066C2C
		public new virtual int b()
		{
			return this.h(this.u.b(33040, 10));
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00067C46 File Offset: 0x00066C46
		public new virtual int f()
		{
			return this.h(this.u.b(33041, 10));
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00067C60 File Offset: 0x00066C60
		public new virtual int a()
		{
			return this.h(this.u.b(33042, 10));
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00067C7A File Offset: 0x00066C7A
		public new virtual bool e()
		{
			return this.e(this.u.b(33052, 10));
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00067C94 File Offset: 0x00066C94
		public new virtual string k()
		{
			return this.d(this.u.b(33055, 10));
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00067CAE File Offset: 0x00066CAE
		public new virtual string m()
		{
			return this.d(this.u.b(33057, 10));
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00067CC8 File Offset: 0x00066CC8
		public new virtual string h()
		{
			return this.d(this.u.b(33058, 10));
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00067CE2 File Offset: 0x00066CE2
		public new virtual int l()
		{
			return this.h(this.u.b(33059, 10));
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00067CFC File Offset: 0x00066CFC
		public new virtual bool i()
		{
			return this.e(this.u.b(33062, 10));
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00067D16 File Offset: 0x00066D16
		public new virtual string p()
		{
			return this.d(this.u.b(33063, 10));
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00067D30 File Offset: 0x00066D30
		public new virtual int j()
		{
			return this.h(this.u.b(33065, 10));
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00067D4A File Offset: 0x00066D4A
		public new virtual int o()
		{
			return this.h(this.u.b(33066, 10));
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00067D64 File Offset: 0x00066D64
		public override string[] g9()
		{
			string[] array = new string[0];
			if (this.x.a(32940))
			{
				try
				{
					e2 e = this.x.b(32940);
					if (e.h.Length == 0)
					{
						return array;
					}
					int num = (int)e.h[0];
					if (num > 0)
					{
						array = new string[num];
						int[] array2 = new int[num];
						for (int i = 0; i < num; i++)
						{
							array2[i] = (int)ii.a(e.h, i * 4 + 1, (i + 1) * 4 + 1);
						}
						for (int j = 0; j < array2.Length - 1; j++)
						{
							int num2 = array2[j];
							int num3 = array2[j + 1] - num2;
							byte[] array3 = new byte[num3];
							Array.Copy(e.h, num2, array3, 0, num3);
							string @string = Encoding.GetEncoding("UTF-16LE").GetString(array3, 0, array3.Length);
							array[j] = @string;
						}
						int num4 = array2[array2.Length - 1];
						int num5 = e.h.Length - num4;
						byte[] array4 = new byte[num5];
						Array.Copy(e.h, num4, array4, 0, num5);
						string string2 = Encoding.GetEncoding("UTF-16LE").GetString(array4, 0, array4.Length);
						array[array.Length - 1] = string2;
					}
				}
				catch (Exception)
				{
				}
				return array;
			}
			return array;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00067ED8 File Offset: 0x00066ED8
		public cv(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00067EE2 File Offset: 0x00066EE2
		public cv(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00067EF0 File Offset: 0x00066EF0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Status Integer 32-bit signed 0x0 => Not started [TODO]: ",
				this.d(),
				"\nPercent Complete Floating point double precision (64-bit): ",
				this.n(),
				"\nIs team task Boolean: ",
				this.g().ToString(),
				"\nStart date Filetime: ",
				this.co().ToString("r"),
				"\nDue date Filetime: ",
				this.da().ToString("r"),
				"\nDate completed Filetime: ",
				this.c().ToString("r"),
				"\nActual effort in minutes Integer 32-bit signed: ",
				this.b(),
				"\nTotal effort in minutes Integer 32-bit signed: ",
				this.f(),
				"\nTask version Integer 32-bit signed FTK: Access count: ",
				this.a(),
				"\nComplete Boolean: ",
				this.e().ToString(),
				"\nOwner ASCII or Unicode string: ",
				this.k(),
				"\nDelegator ASCII or Unicode string: ",
				this.m(),
				"\nUnknown ASCII or Unicode string: ",
				this.h(),
				"\nOrdinal Integer 32-bit signed: ",
				this.l(),
				"\nIs recurring Boolean: ",
				this.i().ToString(),
				"\nRole ASCII or Unicode string: ",
				this.p(),
				"\nOwnership Integer 32-bit signed: ",
				this.j(),
				"\nDelegation State: ",
				this.o()
			});
		}
	}
}
