using System;
using System.Text;

namespace a.b
{
	// Token: 0x02000266 RID: 614
	internal class el : co
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x00062C68 File Offset: 0x00061C68
		public el(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00062CA2 File Offset: 0x00061CA2
		public el(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00062CDF File Offset: 0x00061CDF
		private new int b(byte[] A_0, int A_1)
		{
			while (A_1 < A_0.Length && (A_0[A_1] != 0 || A_0[A_1 + 1] != 0))
			{
				A_1 += 2;
			}
			return A_1;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00062CFC File Offset: 0x00061CFC
		private new static bool a(byte[] A_0, byte[] A_1)
		{
			if (A_0.Length != A_1.Length)
			{
				return false;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] != A_1[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00062D2C File Offset: 0x00061D2C
		private new el.a a(byte[] A_0, int A_1)
		{
			ii.b(A_0, A_1, A_1 + 2);
			A_1 += 2;
			int num = (int)ii.b(A_0, A_1, A_1 + 2);
			A_1 += 2;
			int num2 = this.b(A_0, A_1);
			byte[] array = new byte[num2 - A_1];
			Buffer.BlockCopy(A_0, A_1, array, 0, array.Length);
			string @string = Encoding.Unicode.GetString(array);
			A_1 = num2 + 2;
			int num3 = this.b(A_0, A_1);
			byte[] array2 = new byte[num3 - A_1];
			Buffer.BlockCopy(A_0, A_1, array2, 0, array2.Length);
			string string2 = Encoding.Unicode.GetString(array2);
			A_1 = num3 + 2;
			int num4 = this.b(A_0, A_1);
			byte[] array3 = new byte[num4 - A_1];
			Buffer.BlockCopy(A_0, A_1, array3, 0, array3.Length);
			string string3 = Encoding.Unicode.GetString(array3);
			A_1 = num4 + 2;
			return new el.a
			{
				a = @string,
				b = string2,
				c = string3,
				d = A_1
			};
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00062E08 File Offset: 0x00061E08
		public new object[] a()
		{
			e2 e = this.x.b(this.u.b(32852, 2));
			object[] array = new object[0];
			if (e != null)
			{
				int num = 0;
				int num2 = (int)ii.b(e.h, num, num + 4);
				array = new object[num2];
				num += 4;
				num = (int)ii.b(e.h, num, num + 4);
				for (int i = 0; i < num2; i++)
				{
					ii.b(e.h, num, num + 4);
					num += 4;
					byte[] array2 = new byte[16];
					Buffer.BlockCopy(e.h, num, array2, 0, array2.Length);
					num += 16;
					if (global::a.b.el.a(array2, this.b))
					{
						byte b = e.h[num];
						byte b2 = e.h[num];
						byte b3 = e.h[num];
						num++;
						ii.b(e.h, num, num + 3);
						num += 4;
						byte[] array3 = new byte[16];
						Buffer.BlockCopy(e.h, num, array3, 0, array3.Length);
						num += 16;
						int num3 = (int)ii.b(e.h, num, num + 3);
						num += 3;
						byte b4 = e.h[num];
						num++;
						array[i] = ii.a(this.u, (long)num3);
					}
					else if (global::a.b.el.a(array2, this.a))
					{
						el.a a = this.a(e.h, num);
						num = a.d;
						array[i] = a;
					}
				}
			}
			return array;
		}

		// Token: 0x0400107A RID: 4218
		private new byte[] a = new byte[]
		{
			129,
			43,
			31,
			164,
			190,
			163,
			16,
			25,
			157,
			110,
			0,
			221,
			1,
			15,
			84,
			2
		};

		// Token: 0x0400107B RID: 4219
		private new byte[] b = new byte[]
		{
			192,
			145,
			173,
			211,
			81,
			157,
			207,
			17,
			164,
			169,
			0,
			170,
			0,
			71,
			250,
			164
		};

		// Token: 0x02000267 RID: 615
		internal new class a
		{
			// Token: 0x06001604 RID: 5636 RVA: 0x00062F83 File Offset: 0x00061F83
			public string h()
			{
				return this.a;
			}

			// Token: 0x06001605 RID: 5637 RVA: 0x00062F8B File Offset: 0x00061F8B
			public string g()
			{
				return this.b;
			}

			// Token: 0x06001606 RID: 5638 RVA: 0x00062F93 File Offset: 0x00061F93
			public string f()
			{
				return this.c;
			}

			// Token: 0x06001607 RID: 5639 RVA: 0x00062F9B File Offset: 0x00061F9B
			public string e()
			{
				return string.Format("Display Name: %s\nAddress Type: %s\nEmail Address: %s\n", this.a, this.b, this.c);
			}

			// Token: 0x0400107C RID: 4220
			public string a = "";

			// Token: 0x0400107D RID: 4221
			public string b = "";

			// Token: 0x0400107E RID: 4222
			public string c = "";

			// Token: 0x0400107F RID: 4223
			public int d;
		}
	}
}
