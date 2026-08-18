using System;
using System.Globalization;
using System.Text;

namespace a.b
{
	// Token: 0x02000307 RID: 775
	internal class fh : IComparable<fh>
	{
		// Token: 0x06001B3E RID: 6974 RVA: 0x00076FC4 File Offset: 0x00075FC4
		static fh()
		{
			fh.e();
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00077308 File Offset: 0x00076308
		private static void e()
		{
			if (fh.l[63] == null)
			{
				for (int i = 1; i <= 16; i++)
				{
					int[] a_ = new int[]
					{
						i
					};
					fh.j[i] = new fh(a_, 1);
					fh.k[i] = new fh(a_, -1);
				}
				fh.l[63] = "000000000000000000000000000000000000000000000000000000000000000";
				for (int j = 0; j < 63; j++)
				{
					fh.l[j] = fh.l[63].Substring(0, j);
				}
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00077383 File Offset: 0x00076383
		public fh(int[] A_0, int A_1)
		{
			this.a = ((A_0.Length == 0) ? 0 : A_1);
			this.b = A_0;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000773A0 File Offset: 0x000763A0
		public fh(byte[] A_0)
		{
			if (A_0.Length == 0)
			{
				throw new ArgumentException("Zero length BigInteger");
			}
			if ((sbyte)A_0[0] < 0)
			{
				this.b = fh.a(A_0);
				this.a = -1;
				return;
			}
			this.b = fh.b(A_0);
			this.a = ((this.b.Length == 0) ? 0 : 1);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000773FC File Offset: 0x000763FC
		public fh(int[] A_0)
		{
			if (A_0.Length == 0)
			{
				throw new ArgumentException("Zero length BigInteger");
			}
			if (A_0[0] < 0)
			{
				this.b = fh.b(A_0);
				this.a = -1;
				return;
			}
			this.b = fh.d(A_0);
			this.a = ((this.b.Length == 0) ? 0 : 1);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00077458 File Offset: 0x00076458
		public fh(long A_0)
		{
			if (A_0 < 0L)
			{
				A_0 = -A_0;
				this.a = -1;
			}
			else
			{
				this.a = 1;
			}
			int num = (int)ak.a(A_0, 32);
			if (num == 0)
			{
				this.b = new int[1];
				this.b[0] = (int)A_0;
				return;
			}
			this.b = new int[2];
			this.b[0] = num;
			this.b[1] = (int)A_0;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000774C8 File Offset: 0x000764C8
		public fh(string A_0, int A_1)
		{
			int i = 0;
			int length = A_0.Length;
			if (A_1 < 2 || A_1 > 36)
			{
				throw new FormatException("Radix out of range");
			}
			if (length == 0)
			{
				throw new FormatException("Zero length BigInteger");
			}
			int num = 1;
			int num2 = A_0.LastIndexOf('-');
			int num3 = A_0.LastIndexOf('+');
			if (num2 + num3 > -1)
			{
				throw new FormatException("Illegal embedded sign character");
			}
			if (num2 == 0 || num3 == 0)
			{
				i = 1;
				if (length == 1)
				{
					throw new FormatException("Zero length BigInteger");
				}
			}
			if (num2 == 0)
			{
				num = -1;
			}
			while (i < length && A_0[i] == '0')
			{
				i++;
			}
			if (i == length)
			{
				this.a = 0;
				this.b = fh.m.b;
				return;
			}
			int num4 = length - i;
			this.a = num;
			int num5 = ak.a((int)(ak.a((long)num4 * fh.t[A_1], 10) + 1L) + 31, 5);
			int[] array = new int[num5];
			int num6 = num4 % fh.u[A_1];
			if (num6 == 0)
			{
				num6 = fh.u[A_1];
			}
			string text = A_0.Substring(i, i += num6);
			array[num5 - 1] = int.Parse(text, CultureInfo.InvariantCulture);
			if (array[num5 - 1] < 0)
			{
				throw new FormatException("Illegal digit");
			}
			int a_ = fh.v[A_1];
			while (i < length)
			{
				text = A_0.Substring(i, i += fh.u[A_1]);
				int num7 = int.Parse(text, CultureInfo.InvariantCulture);
				if (num7 < 0)
				{
					throw new FormatException("Illegal digit");
				}
				fh.b(array, a_, num7);
			}
			this.b = fh.d(array);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00077658 File Offset: 0x00076658
		private static int[] d(int[] A_0)
		{
			int num = A_0.Length;
			int num2 = 0;
			while (num2 < num && A_0[num2] == 0)
			{
				num2++;
			}
			if (num2 != 0)
			{
				return d4.a(A_0, num2, num);
			}
			return A_0;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00077688 File Offset: 0x00076688
		private static void b(int[] A_0, int A_1, int A_2)
		{
			long num = (long)A_1 & (long)((ulong)-1);
			long num2 = (long)A_2 & (long)((ulong)-1);
			int num3 = A_0.Length;
			long num4 = 0L;
			for (int i = num3 - 1; i >= 0; i--)
			{
				long num5 = num * ((long)A_0[i] & (long)((ulong)-1)) + num4;
				A_0[i] = (int)num5;
				num4 = ak.a(num5, 32);
			}
			long num6 = ((long)A_0[num3 - 1] & (long)((ulong)-1)) + num2;
			A_0[num3 - 1] = (int)num6;
			num4 = ak.a(num6, 32);
			for (int j = num3 - 2; j >= 0; j--)
			{
				num6 = ((long)A_0[j] & (long)((ulong)-1)) + num4;
				A_0[j] = (int)num6;
				num4 = ak.a(num6, 32);
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00077730 File Offset: 0x00076730
		public string f(int A_0)
		{
			if (this.a == 0)
			{
				return "0";
			}
			if (A_0 < 2 || A_0 > 36)
			{
				A_0 = 10;
			}
			if (A_0 != 10)
			{
				throw new ArgumentException("Only support 10 radix rendering");
			}
			string[] array = new string[(4 * this.b.Length + 6) / 7];
			fh fh = this.f();
			int num = 0;
			while (fh.a != 0)
			{
				fh fh2 = fh.s[A_0];
				bg bg = new bg();
				bg bg2 = new bg(fh.b);
				bg a_ = new bg(fh2.b);
				bg bg3 = bg2.b(a_, bg);
				fh fh3 = bg.k(fh.a * fh2.a);
				fh fh4 = bg3.k(fh.a * fh2.a);
				array[num++] = fh4.n().ToString(CultureInfo.InvariantCulture);
				fh = fh3;
			}
			StringBuilder stringBuilder = new StringBuilder(num * fh.r[A_0] + 1);
			if (this.a < 0)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append(array[num - 1]);
			for (int i = num - 2; i >= 0; i--)
			{
				int num2 = fh.r[A_0] - array[i].Length;
				if (num2 != 0)
				{
					stringBuilder.Append(fh.l[num2]);
				}
				stringBuilder.Append(array[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00077887 File Offset: 0x00076887
		public static fh b(long A_0)
		{
			fh.e();
			if (A_0 == 0L)
			{
				return fh.m;
			}
			if (A_0 > 0L && A_0 <= 16L)
			{
				return fh.j[(int)A_0];
			}
			if (A_0 < 0L && A_0 >= -16L)
			{
				return fh.k[(int)(-(int)A_0)];
			}
			return new fh(A_0);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000778C6 File Offset: 0x000768C6
		private static fh c(int[] A_0)
		{
			if (A_0[0] <= 0)
			{
				return new fh(A_0);
			}
			return new fh(A_0, 1);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000778DC File Offset: 0x000768DC
		public static int e(int A_0)
		{
			return 32 - fh.c(A_0);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000778E8 File Offset: 0x000768E8
		public int g()
		{
			int num = this.d - 1;
			if (num == -1)
			{
				int num2 = this.b.Length;
				if (num2 == 0)
				{
					num = 0;
				}
				else
				{
					int num3 = (num2 - 1 << 5) + fh.e(this.b[0]);
					if (this.a < 0)
					{
						bool flag = fh.a(this.b[0]) == 1;
						int num4 = 1;
						while (num4 < num2 && flag)
						{
							flag = (this.b[num4] == 0);
							num4++;
						}
						num = (flag ? (num3 - 1) : num3);
					}
					else
					{
						num = num3;
					}
				}
				this.d = num + 1;
			}
			return num;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0007797C File Offset: 0x0007697C
		public int k()
		{
			int num = this.c - 1;
			if (num == -1)
			{
				num = 0;
				for (int i = 0; i < this.b.Length; i++)
				{
					num += fh.a(this.b[i]);
				}
				if (this.a < 0)
				{
					int num2 = 0;
					int num3 = this.b.Length - 1;
					while (this.b[num3] == 0)
					{
						num2 += 32;
						num3--;
					}
					num2 += fh.b(this.b[num3]);
					num += num2 - 1;
				}
				this.c = num + 1;
			}
			return num;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00077A06 File Offset: 0x00076A06
		public fh f()
		{
			if (this.a < 0)
			{
				return this.j();
			}
			return this;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00077A19 File Offset: 0x00076A19
		public fh j()
		{
			return new fh(this.b, -this.a);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00077A30 File Offset: 0x00076A30
		public fh h(int A_0)
		{
			if (A_0 < 0)
			{
				throw new ArithmeticException("Negative exponent");
			}
			if (this.a != 0)
			{
				int a_ = (this.a < 0 && (A_0 & 1) == 1) ? -1 : 1;
				int[] array = this.b;
				int[] array2 = new int[]
				{
					1
				};
				while (A_0 != 0)
				{
					if ((A_0 & 1) == 1)
					{
						array2 = this.a(array2, array2.Length, array, array.Length, null);
						array2 = fh.d(array2);
					}
					A_0 = ak.a(A_0, 1);
					if (A_0 != 0)
					{
						array = fh.a(array, array.Length, null);
						array = fh.d(array);
					}
				}
				return new fh(array2, a_);
			}
			if (A_0 != 0)
			{
				return this;
			}
			return fh.n;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00077ACC File Offset: 0x00076ACC
		private int[] a(int[] A_0, int A_1, int[] A_2, int A_3, int[] A_4)
		{
			int num = A_1 - 1;
			int num2 = A_3 - 1;
			if (A_4 == null || A_4.Length < A_1 + A_3)
			{
				A_4 = new int[A_1 + A_3];
			}
			long num3 = 0L;
			int i = num2;
			int num4 = num2 + 1 + num;
			while (i >= 0)
			{
				long num5 = ((long)A_2[i] & (long)((ulong)-1)) * ((long)A_0[num] & (long)((ulong)-1)) + num3;
				A_4[num4] = (int)num5;
				num3 = ak.a(num5, 32);
				i--;
				num4--;
			}
			A_4[num] = (int)num3;
			for (int j = num - 1; j >= 0; j--)
			{
				num3 = 0L;
				int k = num2;
				int num6 = num2 + 1 + j;
				while (k >= 0)
				{
					long num7 = ((long)A_2[k] & (long)((ulong)-1)) * ((long)A_0[j] & (long)((ulong)-1)) + ((long)A_4[num6] & (long)((ulong)-1)) + num3;
					A_4[num6] = (int)num7;
					num3 = ak.a(num7, 32);
					k--;
					num6--;
				}
				A_4[j] = (int)num3;
			}
			return A_4;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00077BB0 File Offset: 0x00076BB0
		private static int a(int[] A_0, int[] A_1, int A_2, int A_3, int A_4)
		{
			long num = (long)A_4 & (long)((ulong)-1);
			long num2 = 0L;
			A_2 = A_0.Length - A_2 - 1;
			for (int i = A_3 - 1; i >= 0; i--)
			{
				long num3 = ((long)A_1[i] & (long)((ulong)-1)) * num + ((long)A_0[A_2] & (long)((ulong)-1)) + num2;
				A_0[A_2--] = (int)num3;
				num2 = ak.a(num3, 32);
			}
			return (int)num2;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00077C08 File Offset: 0x00076C08
		private static int[] a(int[] A_0, int A_1, int[] A_2)
		{
			int num = A_1 << 1;
			if (A_2 == null || A_2.Length < num)
			{
				A_2 = new int[num];
			}
			int num2 = 0;
			int i = 0;
			int num3 = 0;
			while (i < A_1)
			{
				long num4 = (long)A_0[i] & (long)((ulong)-1);
				long num5 = num4 * num4;
				A_2[num3++] = (num2 << 31 | (int)ak.a(num5, 33));
				A_2[num3++] = (int)ak.a(num5, 1);
				num2 = (int)num5;
				i++;
			}
			int j = A_1;
			int num6 = 1;
			while (j > 0)
			{
				int num7 = A_0[j - 1];
				num7 = fh.a(A_2, A_0, num6, j - 1, num7);
				fh.a(A_2, num6 - 1, j, num7);
				j--;
				num6 += 2;
			}
			fh.a(A_2, num, 1);
			A_2[num - 1] |= (A_0[A_1 - 1] & 1);
			return A_2;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00077CD0 File Offset: 0x00076CD0
		public static void a(int[] A_0, int A_1, int A_2)
		{
			if (A_1 == 0 || A_2 == 0)
			{
				return;
			}
			int a_ = 32 - A_2;
			int i = 0;
			int num = A_0[i];
			int num2 = i + A_1 - 1;
			while (i < num2)
			{
				int num3 = num;
				num = A_0[i + 1];
				A_0[i] = (num3 << A_2 | ak.a(num, a_));
				i++;
			}
			A_0[A_1 - 1] <<= A_2;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00077D2C File Offset: 0x00076D2C
		private static int a(int[] A_0, int A_1, int A_2, int A_3)
		{
			A_1 = A_0.Length - 1 - A_2 - A_1;
			long num = ((long)A_0[A_1] & (long)((ulong)-1)) + ((long)A_3 & (long)((ulong)-1));
			A_0[A_1] = (int)num;
			if (num >> 32 == 0L)
			{
				return 0;
			}
			while (--A_2 >= 0)
			{
				if (--A_1 < 0)
				{
					return 1;
				}
				A_0[A_1]++;
				if (A_0[A_1] != 0)
				{
					return 0;
				}
			}
			return 1;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00077D87 File Offset: 0x00076D87
		public int h()
		{
			return this.a;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00077D90 File Offset: 0x00076D90
		public byte[] l()
		{
			int num = this.g() / 8 + 1;
			byte[] array = new byte[num];
			int i = num - 1;
			int num2 = 4;
			int num3 = 0;
			int num4 = 0;
			while (i >= 0)
			{
				if (num2 == 4)
				{
					num3 = this.d(num4++);
					num2 = 1;
				}
				else
				{
					num3 = ak.a(num3, 8);
					num2++;
				}
				array[i] = (byte)num3;
				i--;
			}
			return array;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00077DEA File Offset: 0x00076DEA
		private int d()
		{
			return ak.a(this.g(), 5) + 1;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00077DFA File Offset: 0x00076DFA
		private int c()
		{
			if (this.a >= 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00077E08 File Offset: 0x00076E08
		private int b()
		{
			if (this.a >= 0)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00077E18 File Offset: 0x00076E18
		private int d(int A_0)
		{
			if (A_0 < 0)
			{
				return 0;
			}
			if (A_0 >= this.b.Length)
			{
				return this.b();
			}
			int num = this.b[this.b.Length - A_0 - 1];
			if (this.a >= 0)
			{
				return num;
			}
			if (A_0 > this.a())
			{
				return ~num;
			}
			return -num;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00077E6C File Offset: 0x00076E6C
		private int a()
		{
			int num = this.e - 2;
			if (num == -2)
			{
				int num2 = this.b.Length;
				int num3 = num2 - 1;
				while (num3 >= 0 && this.b[num3] == 0)
				{
					num3--;
				}
				num = num2 - num3 - 1;
				this.e = num + 2;
			}
			return num;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00077EBC File Offset: 0x00076EBC
		private static int[] b(byte[] A_0)
		{
			int num = A_0.Length;
			int num2 = 0;
			while (num2 < num && A_0[num2] == 0)
			{
				num2++;
			}
			int num3 = ak.a(num - num2 + 3, 2);
			int[] array = new int[num3];
			int num4 = num - 1;
			for (int i = num3 - 1; i >= 0; i--)
			{
				array[i] = (int)(A_0[num4--] & byte.MaxValue);
				int val = num4 - num2 + 1;
				int num5 = Math.Min(3, val);
				for (int j = 8; j <= num5 << 3; j += 8)
				{
					array[i] |= (int)(A_0[num4--] & byte.MaxValue) << j;
				}
			}
			return array;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00077F5C File Offset: 0x00076F5C
		private static int[] a(byte[] A_0)
		{
			int num = A_0.Length;
			int num2 = 0;
			while (num2 < num && (sbyte)A_0[num2] == -1)
			{
				num2++;
			}
			int num3 = num2;
			while (num3 < num && A_0[num3] == 0)
			{
				num3++;
			}
			int num4 = (num3 == num) ? 1 : 0;
			int num5 = (num - num2 + num4 + 3) / 4;
			int[] array = new int[num5];
			int num6 = num - 1;
			for (int i = num5 - 1; i >= 0; i--)
			{
				array[i] = (int)(A_0[num6--] & byte.MaxValue);
				int num7 = Math.Min(3, num6 - num2 + 1);
				if (num7 < 0)
				{
					num7 = 0;
				}
				for (int j = 8; j <= 8 * num7; j += 8)
				{
					array[i] |= (int)(A_0[num6--] & byte.MaxValue) << j;
				}
				int num8 = ak.a(-1, 8 * (3 - num7));
				array[i] = (~array[i] & num8);
			}
			for (int k = array.Length - 1; k >= 0; k--)
			{
				array[k] = (int)(((long)array[k] & (long)((ulong)-1)) + 1L);
				if (array[k] != 0)
				{
					break;
				}
			}
			return array;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00078074 File Offset: 0x00077074
		private static int[] b(int[] A_0)
		{
			int num = 0;
			while (num < A_0.Length && A_0[num] == -1)
			{
				num++;
			}
			int num2 = num;
			while (num2 < A_0.Length && A_0[num2] == 0)
			{
				num2++;
			}
			int num3 = (num2 == A_0.Length) ? 1 : 0;
			int[] array = new int[A_0.Length - num + num3];
			for (int i = num; i < A_0.Length; i++)
			{
				array[i - num + num3] = ~A_0[i];
			}
			int num4 = array.Length - 1;
			for (;;)
			{
				int[] array2 = array;
				int num5 = num4;
				int num6 = array2[num5] + 1;
				array2[num5] = num6;
				if (num6 != 0)
				{
					break;
				}
				num4--;
			}
			return array;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00078104 File Offset: 0x00077104
		public static int c(int A_0)
		{
			if (A_0 == 0)
			{
				return 32;
			}
			int num = 1;
			if (ak.a(A_0, 16) == 0)
			{
				num += 16;
				A_0 <<= 16;
			}
			if (ak.a(A_0, 24) == 0)
			{
				num += 8;
				A_0 <<= 8;
			}
			if (ak.a(A_0, 28) == 0)
			{
				num += 4;
				A_0 <<= 4;
			}
			if (ak.a(A_0, 30) == 0)
			{
				num += 2;
				A_0 <<= 2;
			}
			return num - ak.a(A_0, 31);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00078174 File Offset: 0x00077174
		public static int b(int A_0)
		{
			if (A_0 == 0)
			{
				return 32;
			}
			int num = 31;
			int num2 = A_0 << 16;
			if (num2 != 0)
			{
				num -= 16;
				A_0 = num2;
			}
			num2 = A_0 << 8;
			if (num2 != 0)
			{
				num -= 8;
				A_0 = num2;
			}
			num2 = A_0 << 4;
			if (num2 != 0)
			{
				num -= 4;
				A_0 = num2;
			}
			num2 = A_0 << 2;
			if (num2 != 0)
			{
				num -= 2;
				A_0 = num2;
			}
			return num - ak.a(A_0 << 1, 31);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000781D0 File Offset: 0x000771D0
		public static int a(int A_0)
		{
			uint num = (uint)(A_0 - (int)((uint)A_0 >> 1 & 1431655765U));
			num = (num & 858993459U) + (num >> 2 & 858993459U);
			num = (num + (num >> 4) & 252645135U);
			num += num >> 8;
			num += num >> 16;
			return (int)(num & 63U);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0007821C File Offset: 0x0007721C
		public int CompareTo(fh val)
		{
			if (this.a == val.a)
			{
				int num = this.a;
				if (num == -1)
				{
					return val.a(this);
				}
				if (num == 1)
				{
					return this.a(val);
				}
				return 0;
			}
			else
			{
				if (this.a <= val.a)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007826C File Offset: 0x0007726C
		private int a(fh A_0)
		{
			int[] array = this.b;
			int num = array.Length;
			int[] array2 = A_0.b;
			int num2 = array2.Length;
			if (num < num2)
			{
				return -1;
			}
			if (num > num2)
			{
				return 1;
			}
			int i = 0;
			while (i < num)
			{
				int num3 = array[i];
				int num4 = array2[i];
				if (num3 != num4)
				{
					if (((long)num3 & (long)((ulong)-1)) >= ((long)num4 & (long)((ulong)-1)))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000782D0 File Offset: 0x000772D0
		public override bool Equals(object x)
		{
			if (x == this)
			{
				return true;
			}
			if (!(x is fh) || x == null)
			{
				return false;
			}
			fh fh = (fh)x;
			if (fh.a != this.a)
			{
				return false;
			}
			int[] array = this.b;
			int num = array.Length;
			int[] array2 = fh.b;
			if (num != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (array2[i] != array[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0007833E File Offset: 0x0007733E
		public fh f(fh A_0)
		{
			if (this.CompareTo(A_0) >= 0)
			{
				return A_0;
			}
			return this;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0007834D File Offset: 0x0007734D
		public fh g(fh A_0)
		{
			if (this.CompareTo(A_0) <= 0)
			{
				return A_0;
			}
			return this;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0007835C File Offset: 0x0007735C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.b.Length; i++)
			{
				num = (int)((long)(31 * num) + ((long)this.b[i] & (long)((ulong)-1)));
			}
			return num * this.a;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0007839A File Offset: 0x0007739A
		public int m()
		{
			return this.d(0);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000783A4 File Offset: 0x000773A4
		public fh g(int A_0)
		{
			if (this.a == 0)
			{
				return fh.m;
			}
			if (A_0 == 0)
			{
				return this;
			}
			if (A_0 >= 0)
			{
				int num = ak.a(A_0, 5);
				int num2 = A_0 & 31;
				int num3 = this.b.Length;
				int[] array;
				if (num2 == 0)
				{
					array = new int[num3 + num];
					for (int i = 0; i < num3; i++)
					{
						array[i] = this.b[i];
					}
				}
				else
				{
					int num4 = 0;
					int a_ = 32 - num2;
					int num5 = ak.a(this.b[0], a_);
					if (num5 != 0)
					{
						array = new int[num3 + num + 1];
						array[num4++] = num5;
					}
					else
					{
						array = new int[num3 + num];
					}
					int j = 0;
					while (j < num3 - 1)
					{
						array[num4++] = (this.b[j++] << (num2 & 31) | ak.a(this.b[j], a_));
					}
					array[num4] = this.b[j] << num2;
				}
				return new fh(array, this.a);
			}
			if (A_0 == -2147483648)
			{
				throw new ArithmeticException("Shift distance of Integer.Min_VALUE not supported.");
			}
			return this.i(-A_0);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000784C0 File Offset: 0x000774C0
		public long n()
		{
			long num = 0L;
			for (int i = 1; i >= 0; i--)
			{
				num = (num << 32) + ((long)this.d(i) & (long)((ulong)-1));
			}
			return num;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000784F0 File Offset: 0x000774F0
		public fh i(int A_0)
		{
			if (A_0 == 0)
			{
				return this;
			}
			if (A_0 < 0)
			{
				if (A_0 == -2147483648)
				{
					throw new ArithmeticException("Shift distance of Integer.Min_VALUE not supported.");
				}
				return this.g(-A_0);
			}
			else
			{
				int num = ak.a(A_0, 5);
				int num2 = A_0 & 31;
				int num3 = this.b.Length;
				if (num < num3)
				{
					int[] array;
					if (num2 == 0)
					{
						int num4 = num3 - num;
						array = new int[num4];
						for (int i = 0; i < num4; i++)
						{
							array[i] = this.b[i];
						}
					}
					else
					{
						int num5 = 0;
						int num6 = ak.a(this.b[0], num2);
						if (num6 != 0)
						{
							array = new int[num3 - num];
							array[num5++] = num6;
						}
						else
						{
							array = new int[num3 - num - 1];
						}
						int num7 = 32 - num2;
						int j = 0;
						while (j < num3 - num - 1)
						{
							array[num5++] = (this.b[j++] << (num7 & 31) | ak.a(this.b[j], num2));
						}
					}
					if (this.a < 0)
					{
						bool flag = false;
						int num8 = num3 - 1;
						int num9 = num3 - num;
						while (num8 >= num9 && !flag)
						{
							flag = (this.b[num8] != 0);
							num8--;
						}
						if (!flag && num2 != 0)
						{
							flag = (this.b[num3 - num - 1] << 32 - num2 != 0);
						}
						if (flag)
						{
							array = this.a(array);
						}
					}
					return new fh(array, this.a);
				}
				if (this.a < 0)
				{
					return fh.k[1];
				}
				return fh.m;
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0007866C File Offset: 0x0007766C
		private int[] a(int[] A_0)
		{
			int num = 0;
			int num2 = A_0.Length - 1;
			while (num2 >= 0 && num == 0)
			{
				num = ++A_0[num2];
				num2--;
			}
			if (num == 0)
			{
				A_0 = new int[A_0.Length + 1];
				A_0[0] = 1;
			}
			return A_0;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000786B4 File Offset: 0x000776B4
		public fh d(fh A_0)
		{
			int[] array = new int[Math.Max(this.d(), A_0.d())];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (this.d(array.Length - i - 1) & A_0.d(array.Length - i - 1));
			}
			return fh.c(array);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0007870C File Offset: 0x0007770C
		public fh i()
		{
			int[] array = new int[this.d()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ~this.d(array.Length - i - 1);
			}
			return fh.c(array);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0007874C File Offset: 0x0007774C
		public fh h(fh A_0)
		{
			int[] array = new int[Math.Max(this.d(), A_0.d())];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (this.d(array.Length - i - 1) | A_0.d(array.Length - i - 1));
			}
			return fh.c(array);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000787A4 File Offset: 0x000777A4
		private fh a(long A_0)
		{
			if (A_0 == 0L || this.a == 0)
			{
				return fh.m;
			}
			if (A_0 == -9223372036854775808L)
			{
				return this.b(fh.b(A_0));
			}
			int a_ = (A_0 > 0L) ? this.a : (-this.a);
			if (A_0 < 0L)
			{
				A_0 = -A_0;
			}
			long num = ak.a(A_0, 32);
			long num2 = A_0 & (long)((ulong)-1);
			int num3 = this.b.Length;
			int[] array = this.b;
			int[] array2 = (num == 0L) ? new int[num3 + 1] : new int[num3 + 2];
			long num4 = 0L;
			int num5 = array2.Length - 1;
			for (int i = num3 - 1; i >= 0; i--)
			{
				long num6 = ((long)array[i] & (long)((ulong)-1)) * num2 + num4;
				array2[num5--] = (int)num6;
				num4 = ak.a(num6, 32);
			}
			array2[num5] = (int)num4;
			if (num != 0L)
			{
				num4 = 0L;
				num5 = array2.Length - 2;
				for (int j = num3 - 1; j >= 0; j--)
				{
					long num7 = ((long)array[j] & (long)((ulong)-1)) * num + ((long)array2[num5] & (long)((ulong)-1)) + num4;
					array2[num5--] = (int)num7;
					num4 = ak.a(num7, 32);
				}
				array2[0] = (int)num4;
			}
			if (num4 == 0L)
			{
				array2 = d4.a(array2, 1, array2.Length);
			}
			return new fh(array2, a_);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000788F4 File Offset: 0x000778F4
		public fh b(fh A_0)
		{
			if (A_0.a == 0 || this.a == 0)
			{
				return fh.m;
			}
			return new fh(fh.d(this.a(this.b, this.b.Length, A_0.b, A_0.b.Length, null)), (this.a == A_0.a) ? 1 : -1);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00078958 File Offset: 0x00077958
		public fh e(fh A_0)
		{
			if (A_0.a == 0)
			{
				return this;
			}
			if (this.a == 0)
			{
				return A_0;
			}
			if (A_0.a == this.a)
			{
				return new fh(fh.b(this.b, A_0.b), this.a);
			}
			int num = this.a(A_0);
			if (num == 0)
			{
				return fh.m;
			}
			return new fh(fh.d((num > 0) ? fh.a(this.b, A_0.b) : fh.a(A_0.b, this.b)), (num == this.a) ? 1 : -1);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000789F4 File Offset: 0x000779F4
		private static int[] b(int[] A_0, int[] A_1)
		{
			if (A_0.Length < A_1.Length)
			{
				int[] array = A_0;
				A_0 = A_1;
				A_1 = array;
			}
			int i = A_0.Length;
			int j = A_1.Length;
			int[] array2 = new int[i];
			long num = 0L;
			while (j > 0)
			{
				num = ((long)A_0[--i] & (long)((ulong)-1)) + ((long)A_1[--j] & (long)((ulong)-1)) + ak.a(num, 32);
				array2[i] = (int)num;
			}
			bool flag = ak.a(num, 32) != 0L;
			while (i > 0 && flag)
			{
				flag = ((array2[--i] = A_0[i] + 1) == 0);
			}
			while (i > 0)
			{
				array2[--i] = A_0[i];
			}
			if (flag)
			{
				int[] array3 = new int[array2.Length + 1];
				Array.Copy(array2, 0, array3, 1, array2.Length);
				array3[0] = 1;
				return array3;
			}
			return array2;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00078AB4 File Offset: 0x00077AB4
		public fh i(fh A_0)
		{
			if (A_0.a == 0)
			{
				return this;
			}
			if (this.a == 0)
			{
				return A_0.j();
			}
			if (A_0.a != this.a)
			{
				return new fh(fh.b(this.b, A_0.b), this.a);
			}
			int num = this.a(A_0);
			if (num == 0)
			{
				return fh.m;
			}
			return new fh(fh.d((num > 0) ? fh.a(this.b, A_0.b) : fh.a(A_0.b, this.b)), (num == this.a) ? 1 : -1);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00078B58 File Offset: 0x00077B58
		private static int[] a(int[] A_0, int[] A_1)
		{
			int i = A_0.Length;
			int[] array = new int[i];
			int j = A_1.Length;
			long num = 0L;
			while (j > 0)
			{
				num = ((long)A_0[--i] & (long)((ulong)-1)) - ((long)A_1[--j] & (long)((ulong)-1)) + (num >> 32);
				array[i] = (int)num;
			}
			bool flag = num >> 32 != 0L;
			while (i > 0 && flag)
			{
				flag = ((array[--i] = A_0[i] - 1) == -1);
			}
			while (i > 0)
			{
				array[--i] = A_0[i];
			}
			return array;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00078BDC File Offset: 0x00077BDC
		public fh c(fh A_0)
		{
			bg bg = new bg();
			bg bg2 = new bg(this.b);
			bg a_ = new bg(A_0.b);
			bg2.b(a_, bg);
			return bg.k((this.a == A_0.a) ? 1 : -1);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00078C26 File Offset: 0x00077C26
		public static fh b(fh A_0, int A_1)
		{
			return A_0.i(A_1);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00078C2F File Offset: 0x00077C2F
		public static fh a(fh A_0, int A_1)
		{
			return A_0.g(A_1);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00078C38 File Offset: 0x00077C38
		public static fh j(fh A_0, fh A_1)
		{
			return A_0.d(A_1);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00078C41 File Offset: 0x00077C41
		public static fh i(fh A_0, fh A_1)
		{
			return A_0.h(A_1);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00078C4A File Offset: 0x00077C4A
		public static fh h(fh A_0, fh A_1)
		{
			return A_0.b(A_1);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00078C53 File Offset: 0x00077C53
		public static fh g(fh A_0, fh A_1)
		{
			return A_0.e(A_1);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00078C5C File Offset: 0x00077C5C
		public static fh f(fh A_0, fh A_1)
		{
			return A_0.i(A_1);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00078C65 File Offset: 0x00077C65
		public static bool e(fh A_0, fh A_1)
		{
			return A_0.CompareTo(A_1) < 0;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00078C71 File Offset: 0x00077C71
		public static bool d(fh A_0, fh A_1)
		{
			return A_0.CompareTo(A_1) > 0;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00078C7D File Offset: 0x00077C7D
		public static fh c(fh A_0, fh A_1)
		{
			return A_0.c(A_1);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00078C86 File Offset: 0x00077C86
		public static bool b(fh A_0, fh A_1)
		{
			return A_0.Equals(A_1);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00078C8F File Offset: 0x00077C8F
		public static bool a(fh A_0, fh A_1)
		{
			return !fh.b(A_0, A_1);
		}

		// Token: 0x04001324 RID: 4900
		private int a;

		// Token: 0x04001325 RID: 4901
		internal int[] b;

		// Token: 0x04001326 RID: 4902
		[Obsolete]
		private int c;

		// Token: 0x04001327 RID: 4903
		[Obsolete]
		private int d;

		// Token: 0x04001328 RID: 4904
		[Obsolete]
		private int e;

		// Token: 0x04001329 RID: 4905
		public const long f = 4294967295L;

		// Token: 0x0400132A RID: 4906
		public const long g = -9223372036854775808L;

		// Token: 0x0400132B RID: 4907
		public const int h = 2;

		// Token: 0x0400132C RID: 4908
		public const int i = 36;

		// Token: 0x0400132D RID: 4909
		private static fh[] j = new fh[17];

		// Token: 0x0400132E RID: 4910
		private static fh[] k = new fh[17];

		// Token: 0x0400132F RID: 4911
		private static readonly string[] l = new string[64];

		// Token: 0x04001330 RID: 4912
		public static readonly fh m = new fh(new int[0], 0);

		// Token: 0x04001331 RID: 4913
		public static readonly fh n = fh.b(1L);

		// Token: 0x04001332 RID: 4914
		private static readonly fh o = fh.b(2L);

		// Token: 0x04001333 RID: 4915
		public static readonly fh p = fh.b(10L);

		// Token: 0x04001334 RID: 4916
		private const int q = 16;

		// Token: 0x04001335 RID: 4917
		private static readonly int[] r = new int[]
		{
			0,
			0,
			62,
			39,
			31,
			27,
			24,
			22,
			20,
			19,
			18,
			18,
			17,
			17,
			16,
			16,
			15,
			15,
			15,
			14,
			14,
			14,
			14,
			13,
			13,
			13,
			13,
			13,
			13,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12
		};

		// Token: 0x04001336 RID: 4918
		private static readonly fh[] s = new fh[]
		{
			null,
			null,
			fh.b(4611686018427387904L),
			fh.b(4052555153018976267L),
			fh.b(4611686018427387904L),
			fh.b(7450580596923828125L),
			fh.b(4738381338321616896L),
			fh.b(3909821048582988049L),
			fh.b(1152921504606846976L),
			fh.b(1350851717672992089L),
			fh.b(1000000000000000000L),
			fh.b(5559917313492231481L),
			fh.b(2218611106740436992L),
			fh.b(8650415919381337933L),
			fh.b(2177953337809371136L),
			fh.b(6568408355712890625L),
			fh.b(1152921504606846976L),
			fh.b(2862423051509815793L),
			fh.b(6746640616477458432L),
			fh.b(799006685782884121L),
			fh.b(1638400000000000000L),
			fh.b(3243919932521508681L),
			fh.b(6221821273427820544L),
			fh.b(504036361936467383L),
			fh.b(876488338465357824L),
			fh.b(1490116119384765625L),
			fh.b(2481152873203736576L),
			fh.b(4052555153018976267L),
			fh.b(6502111422497947648L),
			fh.b(353814783205469041L),
			fh.b(531441000000000000L),
			fh.b(787662783788549761L),
			fh.b(1152921504606846976L),
			fh.b(1667889514952984961L),
			fh.b(2386420683693101056L),
			fh.b(3379220508056640625L),
			fh.b(4738381338321616896L)
		};

		// Token: 0x04001337 RID: 4919
		private static readonly long[] t = new long[]
		{
			0L,
			0L,
			1024L,
			1624L,
			2048L,
			2378L,
			2648L,
			2875L,
			3072L,
			3247L,
			3402L,
			3543L,
			3672L,
			3790L,
			3899L,
			4001L,
			4096L,
			4186L,
			4271L,
			4350L,
			4426L,
			4498L,
			4567L,
			4633L,
			4696L,
			4756L,
			4814L,
			4870L,
			4923L,
			4975L,
			5025L,
			5074L,
			5120L,
			5166L,
			5210L,
			5253L,
			5295L
		};

		// Token: 0x04001338 RID: 4920
		private static readonly int[] u = new int[]
		{
			0,
			0,
			30,
			19,
			15,
			13,
			11,
			11,
			10,
			9,
			9,
			8,
			8,
			8,
			8,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			5
		};

		// Token: 0x04001339 RID: 4921
		private static readonly int[] v = new int[]
		{
			0,
			0,
			1073741824,
			1162261467,
			1073741824,
			1220703125,
			362797056,
			1977326743,
			1073741824,
			387420489,
			1000000000,
			214358881,
			429981696,
			815730721,
			1475789056,
			170859375,
			268435456,
			410338673,
			612220032,
			893871739,
			1280000000,
			1801088541,
			113379904,
			148035889,
			191102976,
			244140625,
			308915776,
			387420489,
			481890304,
			594823321,
			729000000,
			887503681,
			1073741824,
			1291467969,
			1544804416,
			1838265625,
			60466176
		};
	}
}
