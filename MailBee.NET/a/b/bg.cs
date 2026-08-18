using System;

namespace a.b
{
	// Token: 0x02000308 RID: 776
	internal class bg
	{
		// Token: 0x06001B83 RID: 7043 RVA: 0x00078C9B File Offset: 0x00077C9B
		public bg()
		{
			this.a = new int[1];
			this.b = 0;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00078CB6 File Offset: 0x00077CB6
		public bg(int A_0)
		{
			this.a = new int[1];
			this.b = 1;
			this.a[0] = A_0;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00078CDA File Offset: 0x00077CDA
		public bg(int[] A_0)
		{
			this.a = A_0;
			this.b = A_0.Length;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00078CF4 File Offset: 0x00077CF4
		public static int[] b(int[] A_0, int A_1)
		{
			int[] array = new int[A_1];
			Array.Copy(A_0, 0, array, 0, Math.Min(A_0.Length, A_1));
			return array;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00078D1C File Offset: 0x00077D1C
		public static long[] a(long[] A_0, int A_1)
		{
			long[] array = new long[A_1];
			Array.Copy(A_0, 0, array, 0, Math.Min(A_0.Length, A_1));
			return array;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00078D44 File Offset: 0x00077D44
		public static int[] a(int[] A_0, int A_1, int A_2)
		{
			int num = A_2 - A_1;
			if (num < 0)
			{
				throw new ArgumentException(A_1 + " > " + A_2);
			}
			int[] array = new int[num];
			Array.Copy(A_0, A_1, array, 0, Math.Min(A_0.Length - A_1, num));
			return array;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00078D94 File Offset: 0x00077D94
		public static long[] a(long[] A_0, int A_1, int A_2)
		{
			int num = A_2 - A_1;
			if (num < 0)
			{
				throw new ArgumentException(A_1 + " > " + A_2);
			}
			long[] array = new long[num];
			Array.Copy(A_0, A_1, array, 0, Math.Min(A_0.Length - A_1, num));
			return array;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00078DE1 File Offset: 0x00077DE1
		private bg(fh A_0)
		{
			this.b = A_0.b.Length;
			this.a = bg.b(A_0.b, this.b);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00078E0E File Offset: 0x00077E0E
		private bg(bg A_0)
		{
			this.b = A_0.b;
			this.a = bg.a(A_0.a, A_0.c, A_0.c + this.b);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00078E46 File Offset: 0x00077E46
		private int[] l()
		{
			if (this.c > 0 || this.a.Length != this.b)
			{
				return bg.a(this.a, this.c, this.c + this.b);
			}
			return this.a;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00078E88 File Offset: 0x00077E88
		private long k()
		{
			if (this.b == 0)
			{
				return 0L;
			}
			long num = (long)this.a[this.c] & (long)((ulong)-1);
			if (this.b != 2)
			{
				return num;
			}
			return num << 32 | ((long)this.a[this.c + 1] & (long)((ulong)-1));
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00078ED5 File Offset: 0x00077ED5
		public fh k(int A_0)
		{
			if (this.b == 0 || A_0 == 0)
			{
				return fh.m;
			}
			return new fh(this.l(), A_0);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00078EF4 File Offset: 0x00077EF4
		private void j()
		{
			this.c = (this.b = 0);
			int i = 0;
			int num = this.a.Length;
			while (i < num)
			{
				this.a[i] = 0;
				i++;
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00078F30 File Offset: 0x00077F30
		private void i()
		{
			this.c = (this.b = 0);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00078F50 File Offset: 0x00077F50
		private int j(bg A_0)
		{
			int num = A_0.b;
			if (this.b < num)
			{
				return -1;
			}
			if (this.b > num)
			{
				return 1;
			}
			int[] array = A_0.a;
			int i = this.c;
			int num2 = A_0.c;
			while (i < this.b + this.c)
			{
				int num3 = (int)((long)this.a[i] + (long)((ulong)int.MinValue));
				int num4 = (int)((long)array[num2] + (long)((ulong)int.MinValue));
				if (num3 < num4)
				{
					return -1;
				}
				if (num3 > num4)
				{
					return 1;
				}
				i++;
				num2++;
			}
			return 0;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00078FDC File Offset: 0x00077FDC
		private int i(bg A_0)
		{
			int num = A_0.b;
			int num2 = this.b;
			if (num2 <= 0)
			{
				if (num > 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (num2 > num)
				{
					return 1;
				}
				if (num2 < num - 1)
				{
					return -1;
				}
				int[] array = A_0.a;
				int num3 = 0;
				int num4 = 0;
				if (num2 != num)
				{
					if (array[num3] != 1)
					{
						return -1;
					}
					num3++;
					num4 = int.MinValue;
				}
				int[] array2 = this.a;
				int i = this.c;
				int num5 = num3;
				while (i < num2 + this.c)
				{
					int num6 = array[num5++];
					long num7 = (long)(ak.a(num6, 1) + num4) & (long)((ulong)-1);
					long num8 = (long)array2[i++] & (long)((ulong)-1);
					if (num8 != num7)
					{
						if (num8 >= num7)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						num4 = (num6 & 1) << 31;
					}
				}
				if (num4 != 0)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000790A4 File Offset: 0x000780A4
		private int h()
		{
			if (this.b == 0)
			{
				return -1;
			}
			int num = this.b - 1;
			while (num > 0 && this.a[num + this.c] == 0)
			{
				num--;
			}
			int num2 = this.a[num + this.c];
			if (num2 == 0)
			{
				return -1;
			}
			return (this.b - 1 - num << 5) + fh.b(num2);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00079107 File Offset: 0x00078107
		private int j(int A_0)
		{
			return this.a[this.c + A_0];
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00079118 File Offset: 0x00078118
		private long i(int A_0)
		{
			return (long)this.a[this.c + A_0] & (long)((ulong)-1);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00079130 File Offset: 0x00078130
		private void g()
		{
			if (this.b == 0)
			{
				this.c = 0;
				return;
			}
			int num = this.c;
			if (this.a[num] != 0)
			{
				return;
			}
			int num2 = num + this.b;
			do
			{
				num++;
			}
			while (num < num2 && this.a[num] == 0);
			int num3 = num - this.c;
			this.b -= num3;
			this.c = ((this.b == 0) ? 0 : (this.c + num3));
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000791AA File Offset: 0x000781AA
		private void h(int A_0)
		{
			if (this.a.Length < A_0)
			{
				this.a = new int[A_0];
				this.c = 0;
				this.b = A_0;
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000791D4 File Offset: 0x000781D4
		private int[] f()
		{
			int[] array = new int[this.b];
			for (int i = 0; i < this.b; i++)
			{
				array[i] = this.a[this.c + i];
			}
			return array;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00079211 File Offset: 0x00078211
		private void b(int A_0, int A_1)
		{
			this.a[this.c + A_0] = A_1;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00079223 File Offset: 0x00078223
		private void a(int[] A_0, int A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = 0;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0007923C File Offset: 0x0007823C
		private void h(bg A_0)
		{
			int num = A_0.b;
			if (this.a.Length < num)
			{
				this.a = new int[num];
			}
			Array.Copy(A_0.a, A_0.c, this.a, 0, num);
			this.b = num;
			this.c = 0;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00079290 File Offset: 0x00078290
		private void a(int[] A_0)
		{
			int num = A_0.Length;
			if (this.a.Length < num)
			{
				this.a = new int[num];
			}
			Array.Copy(A_0, 0, this.a, 0, num);
			this.b = num;
			this.c = 0;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000792D5 File Offset: 0x000782D5
		private bool e()
		{
			return this.b == 1 && this.a[this.c] == 1;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000792F2 File Offset: 0x000782F2
		private bool d()
		{
			return this.b == 0;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000792FD File Offset: 0x000782FD
		private bool c()
		{
			return this.b == 0 || (this.a[this.c + this.b - 1] & 1) == 0;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00079324 File Offset: 0x00078324
		private bool b()
		{
			return !this.d() && (this.a[this.c + this.b - 1] & 1) == 1;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0007934B File Offset: 0x0007834B
		private bool a()
		{
			return this.b + this.c <= this.a.Length && (this.b == 0 || this.a[this.c] != 0);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00079380 File Offset: 0x00078380
		public string m()
		{
			return this.k(1).ToString();
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00079390 File Offset: 0x00078390
		private void g(int A_0)
		{
			if (this.b == 0)
			{
				return;
			}
			int num = ak.a(A_0, 5);
			int num2 = A_0 & 31;
			this.b -= num;
			if (num2 == 0)
			{
				return;
			}
			int num3 = fh.e(this.a[this.c]);
			if (num2 >= num3)
			{
				this.d(32 - num2);
				this.b--;
				return;
			}
			this.e(num2);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000793FC File Offset: 0x000783FC
		private void f(int A_0)
		{
			if (this.b == 0)
			{
				return;
			}
			int num = ak.a(A_0, 5);
			int num2 = A_0 & 31;
			int num3 = fh.e(this.a[this.c]);
			if (A_0 <= 32 - num3)
			{
				this.d(num2);
				return;
			}
			int num4 = this.b + num + 1;
			if (num2 <= 32 - num3)
			{
				num4--;
			}
			if (this.a.Length < num4)
			{
				int[] array = new int[num4];
				for (int i = 0; i < this.b; i++)
				{
					array[i] = this.a[this.c + i];
				}
				this.a(array, num4);
			}
			else if (this.a.Length - this.c >= num4)
			{
				for (int j = 0; j < num4 - this.b; j++)
				{
					this.a[this.c + this.b + j] = 0;
				}
			}
			else
			{
				for (int k = 0; k < this.b; k++)
				{
					this.a[k] = this.a[this.c + k];
				}
				for (int l = this.b; l < num4; l++)
				{
					this.a[l] = 0;
				}
				this.c = 0;
			}
			this.b = num4;
			if (num2 == 0)
			{
				return;
			}
			if (num2 <= 32 - num3)
			{
				this.d(num2);
				return;
			}
			this.e(32 - num2);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00079560 File Offset: 0x00078560
		private int a(int[] A_0, int[] A_1, int A_2)
		{
			long num = 0L;
			for (int i = A_0.Length - 1; i >= 0; i--)
			{
				long num2 = ((long)A_0[i] & (long)((ulong)-1)) + ((long)A_1[i + A_2] & (long)((ulong)-1)) + num;
				A_1[i + A_2] = (int)num2;
				num = ak.a(num2, 32);
			}
			return (int)num;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000795A8 File Offset: 0x000785A8
		private int a(int[] A_0, int[] A_1, int A_2, int A_3, int A_4)
		{
			long num = (long)A_2 & (long)((ulong)-1);
			long num2 = 0L;
			A_4 += A_3;
			for (int i = A_3 - 1; i >= 0; i--)
			{
				long num3 = ((long)A_1[i] & (long)((ulong)-1)) * num + num2;
				long num4 = (long)A_0[A_4] - num3;
				A_0[A_4--] = (int)num4;
				num2 = ak.a(num3, 32) + (((num4 & (long)((ulong)-1)) > ((long)(~(long)((int)num3)) & (long)((ulong)-1))) ? 1L : 0L);
			}
			return (int)num2;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00079614 File Offset: 0x00078614
		private void e(int A_0)
		{
			int[] array = this.a;
			int num = 32 - A_0;
			int i = this.c + this.b - 1;
			int num2 = array[i];
			while (i > this.c)
			{
				int a_ = num2;
				num2 = array[i - 1];
				array[i] = (num2 << num | ak.a(a_, A_0));
				i--;
			}
			array[this.c] = ak.a(array[this.c], A_0);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00079684 File Offset: 0x00078684
		private void d(int A_0)
		{
			int[] array = this.a;
			int a_ = 32 - A_0;
			int i = this.c;
			int num = array[i];
			int num2 = i + this.b - 1;
			while (i < num2)
			{
				int num3 = num;
				num = array[i + 1];
				array[i] = (num3 << A_0 | ak.a(num, a_));
				i++;
			}
			array[this.c + this.b - 1] <<= A_0;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000796F8 File Offset: 0x000786F8
		private void g(bg A_0)
		{
			int i = this.b;
			int j = A_0.b;
			int num = (this.b > A_0.b) ? this.b : A_0.b;
			int[] array = (this.a.Length < num) ? new int[num] : this.a;
			int num2 = array.Length - 1;
			long num3 = 0L;
			while (i > 0)
			{
				if (j <= 0)
				{
					break;
				}
				i--;
				j--;
				long num4 = ((long)this.a[i + this.c] & (long)((ulong)-1)) + ((long)A_0.a[j + A_0.c] & (long)((ulong)-1)) + num3;
				array[num2--] = (int)num4;
				num3 = ak.a(num4, 32);
			}
			while (i > 0)
			{
				i--;
				if (num3 == 0L && array == this.a && num2 == i + this.c)
				{
					return;
				}
				long num4 = ((long)this.a[i + this.c] & (long)((ulong)-1)) + num3;
				array[num2--] = (int)num4;
				num3 = ak.a(num4, 32);
			}
			while (j > 0)
			{
				j--;
				long num4 = ((long)A_0.a[j + A_0.c] & (long)((ulong)-1)) + num3;
				array[num2--] = (int)num4;
				num3 = ak.a(num4, 32);
			}
			if (num3 > 0L)
			{
				num++;
				if (array.Length < num)
				{
					int[] array2 = new int[num];
					Array.Copy(array, 0, array2, 1, array.Length);
					array2[0] = 1;
					array = array2;
				}
				else
				{
					array[num2--] = 1;
				}
			}
			this.a = array;
			this.b = num;
			this.c = array.Length - num;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00079888 File Offset: 0x00078888
		private int f(bg A_0)
		{
			bg bg = this;
			int[] array = this.a;
			int num = bg.j(A_0);
			if (num == 0)
			{
				this.i();
				return 0;
			}
			if (num < 0)
			{
				bg bg2 = bg;
				bg = A_0;
				A_0 = bg2;
			}
			int num2 = bg.b;
			if (array.Length < num2)
			{
				array = new int[num2];
			}
			long num3 = 0L;
			int i = bg.b;
			int j = A_0.b;
			int num4 = array.Length - 1;
			while (j > 0)
			{
				i--;
				j--;
				num3 = ((long)bg.a[i + bg.c] & (long)((ulong)-1)) - ((long)A_0.a[j + A_0.c] & (long)((ulong)-1)) - (long)((int)(-(int)(num3 >> 32)));
				array[num4--] = (int)num3;
			}
			while (i > 0)
			{
				i--;
				num3 = ((long)bg.a[i + bg.c] & (long)((ulong)-1)) - (long)((int)(-(int)(num3 >> 32)));
				array[num4--] = (int)num3;
			}
			this.a = array;
			this.b = num2;
			this.c = this.a.Length - num2;
			this.g();
			return num;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007999C File Offset: 0x0007899C
		private int e(bg A_0)
		{
			bg bg = this;
			int num = bg.j(A_0);
			if (num == 0)
			{
				return 0;
			}
			if (num < 0)
			{
				bg bg2 = bg;
				bg = A_0;
				A_0 = bg2;
			}
			long num2 = 0L;
			int i = bg.b;
			int j = A_0.b;
			while (j > 0)
			{
				i--;
				j--;
				num2 = ((long)bg.a[bg.c + i] & (long)((ulong)-1)) - ((long)A_0.a[A_0.c + j] & (long)((ulong)-1)) - (long)((int)(-(int)(num2 >> 32)));
				bg.a[bg.c + i] = (int)num2;
			}
			while (i > 0)
			{
				i--;
				num2 = ((long)bg.a[bg.c + i] & (long)((ulong)-1)) - (long)((int)(-(int)(num2 >> 32)));
				bg.a[bg.c + i] = (int)num2;
			}
			bg.g();
			return num;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00079A64 File Offset: 0x00078A64
		private void a(bg A_0, bg A_1)
		{
			int num = this.b;
			int num2 = A_0.b;
			int num3 = num + num2;
			if (A_1.a.Length < num3)
			{
				A_1.a = new int[num3];
			}
			A_1.c = 0;
			A_1.b = num3;
			long num4 = 0L;
			int i = num2 - 1;
			int num5 = num2 + num - 1;
			while (i >= 0)
			{
				long num6 = ((long)A_0.a[i + A_0.c] & (long)((ulong)-1)) * ((long)this.a[num - 1 + this.c] & (long)((ulong)-1)) + num4;
				A_1.a[num5] = (int)num6;
				num4 = ak.a(num6, 32);
				i--;
				num5--;
			}
			A_1.a[num - 1] = (int)num4;
			for (int j = num - 2; j >= 0; j--)
			{
				num4 = 0L;
				int k = num2 - 1;
				int num7 = num2 + j;
				while (k >= 0)
				{
					long num8 = ((long)A_0.a[k + A_0.c] & (long)((ulong)-1)) * ((long)this.a[j + this.c] & (long)((ulong)-1)) + ((long)A_1.a[num7] & (long)((ulong)-1)) + num4;
					A_1.a[num7] = (int)num8;
					num4 = ak.a(num8, 32);
					k--;
					num7--;
				}
				A_1.a[j] = (int)num4;
			}
			A_1.g();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00079BB8 File Offset: 0x00078BB8
		public void a(int A_0, bg A_1)
		{
			if (A_0 == 1)
			{
				A_1.h(this);
				return;
			}
			if (A_0 == 0)
			{
				A_1.j();
				return;
			}
			long num = (long)A_0 & (long)((ulong)-1);
			int[] array = (A_1.a.Length < this.b + 1) ? new int[this.b + 1] : A_1.a;
			long num2 = 0L;
			for (int i = this.b - 1; i >= 0; i--)
			{
				long num3 = num * ((long)this.a[i + this.c] & (long)((ulong)-1)) + num2;
				array[i + 1] = (int)num3;
				num2 = ak.a(num3, 32);
			}
			if (num2 == 0L)
			{
				A_1.c = 1;
				A_1.b = this.b;
			}
			else
			{
				A_1.c = 0;
				A_1.b = this.b + 1;
				array[0] = (int)num2;
			}
			A_1.a = array;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00079C84 File Offset: 0x00078C84
		public int b(int A_0, bg A_1)
		{
			long num = (long)A_0 & (long)((ulong)-1);
			if (this.b == 1)
			{
				long num2 = (long)this.a[this.c] & (long)((ulong)-1);
				int num3 = (int)(num2 / num);
				int result = (int)(num2 - (long)num3 * num);
				A_1.a[0] = num3;
				A_1.b = ((num3 == 0) ? 0 : 1);
				A_1.c = 0;
				return result;
			}
			if (A_1.a.Length < this.b)
			{
				A_1.a = new int[this.b];
			}
			A_1.c = 0;
			A_1.b = this.b;
			int num4 = fh.c(A_0);
			int num5 = this.a[this.c];
			long num6 = (long)num5 & (long)((ulong)-1);
			if (num6 < num)
			{
				A_1.a[0] = 0;
			}
			else
			{
				A_1.a[0] = (int)(num6 / num);
				num5 = (int)(num6 - (long)A_1.a[0] * num);
				num6 = ((long)num5 & (long)((ulong)-1));
			}
			int num7 = this.b;
			int[] array = new int[2];
			while (--num7 > 0)
			{
				long num8 = num6 << 32 | ((long)this.a[this.c + this.b - num7] & (long)((ulong)-1));
				if (num8 >= 0L)
				{
					array[0] = (int)(num8 / num);
					array[1] = (int)(num8 - (long)array[0] * num);
				}
				else
				{
					this.a(array, num8, A_0);
				}
				A_1.a[this.b - num7] = array[0];
				num5 = array[1];
				num6 = ((long)num5 & (long)((ulong)-1));
			}
			A_1.g();
			if (num4 > 0)
			{
				return num5 % A_0;
			}
			return num5;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00079DF4 File Offset: 0x00078DF4
		public bg b(bg A_0, bg A_1)
		{
			if (A_0.b == 0)
			{
				throw new ArithmeticException("BigInteger divide by zero");
			}
			if (this.b == 0)
			{
				A_1.b = A_1.c;
				return new bg();
			}
			int num = this.j(A_0);
			if (num < 0)
			{
				A_1.b = (A_1.c = 0);
				return new bg(this);
			}
			if (num == 0)
			{
				A_1.a[0] = (A_1.b = 1);
				A_1.c = 0;
				return new bg();
			}
			A_1.j();
			if (A_0.b != 1)
			{
				int[] a_ = bg.a(A_0.a, A_0.c, A_0.c + A_0.b);
				return this.a(a_, A_1);
			}
			int num2 = this.b(A_0.a[A_0.c], A_1);
			if (num2 == 0)
			{
				return new bg();
			}
			return new bg(num2);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00079ED0 File Offset: 0x00078ED0
		public long a(long A_0, bg A_1)
		{
			if (A_0 == 0L)
			{
				throw new ArithmeticException("BigInteger divide by zero");
			}
			if (this.b == 0)
			{
				A_1.b = (A_1.c = 0);
				return 0L;
			}
			if (A_0 < 0L)
			{
				A_0 = -A_0;
			}
			int num = (int)ak.a(A_0, 32);
			A_1.j();
			if (num == 0)
			{
				return (long)this.b((int)A_0, A_1) & (long)((ulong)-1);
			}
			int[] a_ = new int[]
			{
				num,
				(int)(A_0 & (long)((ulong)-1))
			};
			return this.a(a_, A_1).k();
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00079F50 File Offset: 0x00078F50
		private bg a(int[] A_0, bg A_1)
		{
			bg bg = new bg(new int[this.b + 1]);
			Array.Copy(this.a, this.c, bg.a, 1, this.b);
			bg.b = this.b;
			bg.c = 1;
			int num = bg.b;
			int num2 = A_0.Length;
			int num3 = num - num2 + 1;
			if (A_1.a.Length < num3)
			{
				A_1.a = new int[num3];
				A_1.c = 0;
			}
			A_1.b = num3;
			int[] array = A_1.a;
			int num4 = fh.c(A_0[0]);
			if (num4 > 0)
			{
				fh.a(A_0, num2, num4);
				bg.f(num4);
			}
			if (bg.b == num)
			{
				bg.c = 0;
				bg.a[0] = 0;
				bg.b++;
			}
			int num5 = A_0[0];
			long num6 = (long)num5 & (long)((ulong)-1);
			int num7 = A_0[1];
			int[] array2 = new int[2];
			for (int i = 0; i < num3; i++)
			{
				bool flag = false;
				int num8 = bg.a[i + bg.c];
				int num9 = (int)((long)num8 + (long)((ulong)int.MinValue));
				int num10 = bg.a[i + 1 + bg.c];
				int num11;
				int num12;
				if (num8 == num5)
				{
					num11 = -1;
					num12 = num8 + num10;
					flag = ((long)num12 + (long)((ulong)int.MinValue) < (long)num9);
				}
				else
				{
					long num13 = (long)num8 << 32 | ((long)num10 & (long)((ulong)-1));
					if (num13 >= 0L)
					{
						num11 = (int)(num13 / num6);
						num12 = (int)(num13 - (long)num11 * num6);
					}
					else
					{
						this.a(array2, num13, num5);
						num11 = array2[0];
						num12 = array2[1];
					}
				}
				if (num11 != 0)
				{
					if (!flag)
					{
						long num14 = (long)bg.a[i + 2 + bg.c] & (long)((ulong)-1);
						long a_ = ((long)num12 & (long)((ulong)-1)) << 32 | num14;
						long num15 = ((long)num7 & (long)((ulong)-1)) * ((long)num11 & (long)((ulong)-1));
						if (this.a(num15, a_))
						{
							num11--;
							num12 = (int)(((long)num12 & (long)((ulong)-1)) + num6);
							if (((long)num12 & (long)((ulong)-1)) >= num6)
							{
								num15 -= ((long)num7 & (long)((ulong)-1));
								a_ = (((long)num12 & (long)((ulong)-1)) << 32 | num14);
								if (this.a(num15, a_))
								{
									num11--;
								}
							}
						}
					}
					bg.a[i + bg.c] = 0;
					if ((int)((long)this.a(bg.a, A_0, num11, num2, i + bg.c) + (long)((ulong)-2147483648)) > num9)
					{
						this.a(A_0, bg.a, i + 1 + bg.c);
						num11--;
					}
					array[i] = num11;
				}
			}
			if (num4 > 0)
			{
				bg.g(num4);
			}
			A_1.g();
			bg.g();
			return bg;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0007A205 File Offset: 0x00079205
		private bool a(long A_0, long A_1)
		{
			return A_0 + long.MinValue > A_1 + long.MinValue;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0007A220 File Offset: 0x00079220
		private void a(int[] A_0, long A_1, int A_2)
		{
			long num = (long)A_2 & (long)((ulong)-1);
			if (num == 1L)
			{
				A_0[0] = (int)A_1;
				A_0[1] = 0;
				return;
			}
			long num2 = ak.a(A_1, 1) / ak.a(num, 1);
			long num3 = A_1 - num2 * num;
			while (num3 < 0L)
			{
				num3 += num;
				num2 -= 1L;
			}
			while (num3 >= num)
			{
				num3 -= num;
				num2 += 1L;
			}
			A_0[0] = (int)num2;
			A_0[1] = (int)num3;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0007A284 File Offset: 0x00079284
		private bg d(bg A_0)
		{
			bg bg = this;
			bg a_ = new bg();
			while (A_0.b != 0)
			{
				if (Math.Abs(bg.b - A_0.b) < 2)
				{
					return bg.c(A_0);
				}
				bg bg2 = bg.b(A_0, a_);
				bg = A_0;
				A_0 = bg2;
			}
			return bg;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0007A2D0 File Offset: 0x000792D0
		private bg c(bg A_0)
		{
			bg bg = this;
			bg bg2 = new bg();
			int num = bg.h();
			int num2 = A_0.h();
			int num3 = (num < num2) ? num : num2;
			if (num3 != 0)
			{
				bg.g(num3);
				A_0.g(num3);
			}
			bool flag = num3 == num;
			bg bg3 = flag ? A_0 : bg;
			int num4 = flag ? -1 : 1;
			int a_;
			while ((a_ = bg3.h()) >= 0)
			{
				bg3.g(a_);
				if (num4 > 0)
				{
					bg = bg3;
				}
				else
				{
					A_0 = bg3;
				}
				if (bg.b < 2 && A_0.b < 2)
				{
					int num5 = bg.a[bg.c];
					int a_2 = A_0.a[A_0.c];
					num5 = bg.a(num5, a_2);
					bg2.a[0] = num5;
					bg2.b = 1;
					bg2.c = 0;
					if (num3 > 0)
					{
						bg2.f(num3);
					}
					return bg2;
				}
				if ((num4 = bg.e(A_0)) == 0)
				{
					break;
				}
				bg3 = ((num4 >= 0) ? bg : A_0);
			}
			if (num3 > 0)
			{
				bg.f(num3);
			}
			return bg;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0007A3DC File Offset: 0x000793DC
		private static int a(int A_0, int A_1)
		{
			if (A_1 == 0)
			{
				return A_0;
			}
			if (A_0 == 0)
			{
				return A_1;
			}
			int num = fh.b(A_0);
			int num2 = fh.b(A_1);
			A_0 = ak.a(A_0, num);
			A_1 = ak.a(A_1, num2);
			int num3 = (num < num2) ? num : num2;
			while (A_0 != A_1)
			{
				if ((long)A_0 + (long)((ulong)-2147483648) > (long)A_1 + (long)((ulong)-2147483648))
				{
					A_0 -= A_1;
					A_0 = ak.a(A_0, fh.b(A_0));
				}
				else
				{
					A_1 -= A_0;
					A_1 = ak.a(A_1, fh.b(A_1));
				}
			}
			return A_0 << num3;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0007A464 File Offset: 0x00079464
		private bg b(bg A_0)
		{
			if (A_0.b())
			{
				return this.a(A_0);
			}
			if (this.c())
			{
				throw new ArithmeticException("BigInteger not invertible.");
			}
			int num = A_0.h();
			bg bg = new bg(A_0);
			bg.g(num);
			if (bg.e())
			{
				return this.c(num);
			}
			bg bg2 = this.a(bg);
			bg bg3 = this.c(num);
			bg a_ = bg.a(bg, num);
			bg a_2 = bg.c(num);
			bg bg4 = new bg();
			bg bg5 = new bg();
			bg bg6 = new bg();
			bg2.f(num);
			bg2.a(a_, bg6);
			bg3.a(bg, bg4);
			bg4.a(a_2, bg5);
			bg6.g(bg5);
			return bg6.b(A_0, bg4);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0007A524 File Offset: 0x00079524
		private bg c(int A_0)
		{
			if (this.c())
			{
				throw new ArithmeticException("Non-invertible. (GCD != 1)");
			}
			if (A_0 > 64)
			{
				return this.a(A_0);
			}
			int num = bg.b(this.a[this.c + this.b - 1]);
			if (A_0 < 33)
			{
				num = ((A_0 == 32) ? num : (num & (1 << A_0) - 1));
				return new bg(num);
			}
			long num2 = (long)this.a[this.c + this.b - 1] & (long)((ulong)-1);
			if (this.b > 1)
			{
				num2 |= (long)this.a[this.c + this.b - 2] << 32;
			}
			long num3 = (long)num & (long)((ulong)-1);
			num3 *= 2L - num2 * num3;
			num3 = ((A_0 == 64) ? num3 : (num3 & (1L << A_0) - 1L));
			bg bg = new bg(new int[2]);
			bg.a[0] = (int)ak.a(num3, 32);
			bg.a[1] = (int)num3;
			bg.b = 2;
			bg.g();
			return bg;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0007A624 File Offset: 0x00079624
		private static int b(int A_0)
		{
			int num = A_0 * (2 - A_0 * A_0);
			num *= 2 - A_0 * num;
			num *= 2 - A_0 * num;
			return num * (2 - A_0 * num);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0007A654 File Offset: 0x00079654
		private static bg a(bg A_0, int A_1)
		{
			return bg.a(new bg(1), new bg(A_0), A_1);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0007A668 File Offset: 0x00079668
		private bg a(bg A_0)
		{
			throw new NotImplementedException("This method uses SignedMutableBigInteger class.");
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0007A674 File Offset: 0x00079674
		private static bg a(bg A_0, bg A_1, int A_2)
		{
			bg bg = new bg();
			int num = -bg.b(A_1.a[A_1.c + A_1.b - 1]);
			int i = 0;
			int num2 = A_2 >> 5;
			while (i < num2)
			{
				int a_ = num * A_0.a[A_0.c + A_0.b - 1];
				A_1.a(a_, bg);
				A_0.g(bg);
				A_0.b--;
				i++;
			}
			int num3 = A_2 & 31;
			if (num3 != 0)
			{
				int num4 = num * A_0.a[A_0.c + A_0.b - 1];
				num4 &= (1 << num3) - 1;
				A_1.a(num4, bg);
				A_0.g(bg);
				A_0.g(num3);
			}
			while (A_0.j(A_1) >= 0)
			{
				A_0.f(A_1);
			}
			return A_0;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0007A74C File Offset: 0x0007974C
		private bg a(int A_0)
		{
			bg bg = new bg(1);
			bg.f(A_0);
			bg bg2 = new bg(bg);
			bg bg3 = new bg(this);
			bg bg4 = new bg();
			bg = bg.b(bg3, bg4);
			bg bg5 = new bg(bg4);
			bg bg6 = new bg(1);
			bg bg7 = new bg();
			while (!bg.e())
			{
				bg bg8 = bg3.b(bg, bg4);
				if (bg8.b == 0)
				{
					throw new ArithmeticException("BigInteger not invertible.");
				}
				bg bg9 = bg8;
				bg3 = bg9;
				if (bg4.b == 1)
				{
					bg5.a(bg4.a[bg4.c], bg7);
				}
				else
				{
					bg4.a(bg5, bg7);
				}
				bg9 = bg4;
				bg4 = bg7;
				bg7 = bg9;
				bg6.g(bg4);
				if (bg3.e())
				{
					return bg6;
				}
				bg bg10 = bg.b(bg3, bg4);
				if (bg10.b == 0)
				{
					throw new ArithmeticException("BigInteger not invertible.");
				}
				bg = bg10;
				if (bg4.b == 1)
				{
					bg6.a(bg4.a[bg4.c], bg7);
				}
				else
				{
					bg4.a(bg6, bg7);
				}
				bg9 = bg4;
				bg4 = bg7;
				bg7 = bg9;
				bg5.g(bg4);
			}
			bg2.f(bg5);
			return bg2;
		}

		// Token: 0x0400133A RID: 4922
		private int[] a;

		// Token: 0x0400133B RID: 4923
		private int b;

		// Token: 0x0400133C RID: 4924
		private int c;

		// Token: 0x0400133D RID: 4925
		private static readonly bg d = new bg(1);

		// Token: 0x0400133E RID: 4926
		private const long e = 4294967295L;

		// Token: 0x0400133F RID: 4927
		private const long f = -9223372036854775808L;
	}
}
