using System;

namespace a.b
{
	// Token: 0x0200032D RID: 813
	internal class g
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x0007EB14 File Offset: 0x0007DB14
		public g() : this(g.c)
		{
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0007EB21 File Offset: 0x0007DB21
		public g(g A_0) : this(A_0.a.Length)
		{
			Array.Copy(A_0.a, 0, this.a, 0, this.a.Length);
			this.b = A_0.b;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0007EB58 File Offset: 0x0007DB58
		public g(int A_0)
		{
			this.a = new short[A_0];
			this.b = 0;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0007EB74 File Offset: 0x0007DB74
		public void a(int A_0, short A_1)
		{
			if (A_0 > this.b)
			{
				throw new IndexOutOfRangeException();
			}
			if (A_0 == this.b)
			{
				this.b(A_1);
				return;
			}
			if (this.b == this.a.Length)
			{
				this.a(this.b * 2);
			}
			Array.Copy(this.a, A_0, this.a, A_0 + 1, this.b - A_0);
			this.a[A_0] = A_1;
			this.b++;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0007EBF4 File Offset: 0x0007DBF4
		public bool b(short A_0)
		{
			if (this.b == this.a.Length)
			{
				this.a(this.b * 2);
			}
			short[] array = this.a;
			int num = this.b;
			this.b = num + 1;
			array[num] = A_0;
			return true;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0007EC3C File Offset: 0x0007DC3C
		public bool c(g A_0)
		{
			if (A_0.b != 0)
			{
				if (this.b + A_0.b > this.a.Length)
				{
					this.a(this.b + A_0.b);
				}
				Array.Copy(A_0.a, 0, this.a, this.b, A_0.b);
				this.b += A_0.b;
			}
			return true;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0007ECB0 File Offset: 0x0007DCB0
		public bool a(int A_0, g A_1)
		{
			if (A_0 > this.b)
			{
				throw new IndexOutOfRangeException();
			}
			if (A_1.b != 0)
			{
				if (this.b + A_1.b > this.a.Length)
				{
					this.a(this.b + A_1.b);
				}
				Array.Copy(this.a, A_0, this.a, A_0 + A_1.b, this.b - A_0);
				Array.Copy(A_1.a, 0, this.a, A_0, A_1.b);
				this.b += A_1.b;
			}
			return true;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0007ED4D File Offset: 0x0007DD4D
		public void c()
		{
			this.b = 0;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0007ED58 File Offset: 0x0007DD58
		public bool d(short A_0)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this.b)
			{
				if (this.a[num] == A_0)
				{
					flag = true;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0007ED8C File Offset: 0x0007DD8C
		public bool b(g A_0)
		{
			bool flag = true;
			if (this != A_0)
			{
				int num = 0;
				while (flag && num < A_0.b)
				{
					if (!this.d(A_0.a[num]))
					{
						flag = false;
					}
					num++;
				}
			}
			return flag;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0007EDC8 File Offset: 0x0007DDC8
		public override bool Equals(object o)
		{
			bool flag = this == o;
			if (!flag && o != null && o.GetType() == base.GetType())
			{
				g g = (g)o;
				if (g.b == this.b)
				{
					flag = true;
					int num = 0;
					while (flag && num < this.b)
					{
						flag = (this.a[num] == g.a[num]);
						num++;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0007EE32 File Offset: 0x0007DE32
		public short b(int A_0)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException();
			}
			return this.a[A_0];
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0007EE4C File Offset: 0x0007DE4C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.b; i++)
			{
				num = 31 * num + (int)this.a[i];
			}
			return num;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0007EE7C File Offset: 0x0007DE7C
		public int e(short A_0)
		{
			int num = 0;
			while (num < this.b && A_0 != this.a[num])
			{
				num++;
			}
			if (num == this.b)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0007EEB1 File Offset: 0x0007DEB1
		public bool b()
		{
			return this.b == 0;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0007EEBC File Offset: 0x0007DEBC
		public int a(short A_0)
		{
			int num = this.b - 1;
			while (num >= 0 && A_0 != this.a[num])
			{
				num--;
			}
			return num;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0007EEE8 File Offset: 0x0007DEE8
		public short c(int A_0)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException();
			}
			short result = this.a[A_0];
			Array.Copy(this.a, A_0 + 1, this.a, A_0, this.b - A_0);
			this.b--;
			return result;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0007EF38 File Offset: 0x0007DF38
		public bool c(short A_0)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this.b)
			{
				if (A_0 == this.a[num])
				{
					Array.Copy(this.a, num + 1, this.a, num, this.b - num);
					this.b--;
					flag = true;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0007EF94 File Offset: 0x0007DF94
		public bool d(g A_0)
		{
			bool result = false;
			for (int i = 0; i < A_0.b; i++)
			{
				if (this.c(A_0.a[i]))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0007EFC8 File Offset: 0x0007DFC8
		public bool a(g A_0)
		{
			bool result = false;
			int i = 0;
			while (i < this.b)
			{
				if (!A_0.d(this.a[i]))
				{
					this.c(i);
					result = true;
				}
				else
				{
					i++;
				}
			}
			return result;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0007F005 File Offset: 0x0007E005
		public short b(int A_0, short A_1)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException();
			}
			short result = this.a[A_0];
			this.a[A_0] = A_1;
			return result;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0007F027 File Offset: 0x0007E027
		public int d()
		{
			return this.b;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0007F02F File Offset: 0x0007E02F
		public int a()
		{
			return this.b;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0007F038 File Offset: 0x0007E038
		public short[] e()
		{
			short[] array = new short[this.b];
			Array.Copy(this.a, 0, array, 0, this.b);
			return array;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0007F068 File Offset: 0x0007E068
		public short[] a(short[] A_0)
		{
			short[] result;
			if (A_0.Length == this.b)
			{
				Array.Copy(this.a, 0, A_0, 0, this.b);
				result = A_0;
			}
			else
			{
				result = this.e();
			}
			return result;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0007F0A0 File Offset: 0x0007E0A0
		private void a(int A_0)
		{
			short[] destinationArray = new short[(A_0 == this.a.Length) ? (A_0 + 1) : A_0];
			Array.Copy(this.a, 0, destinationArray, 0, this.b);
			this.a = destinationArray;
		}

		// Token: 0x04001384 RID: 4996
		private short[] a;

		// Token: 0x04001385 RID: 4997
		private int b;

		// Token: 0x04001386 RID: 4998
		private static int c = 128;
	}
}
