using System;

namespace a.b
{
	// Token: 0x02000319 RID: 793
	internal class dz
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x0007C1A9 File Offset: 0x0007B1A9
		public dz() : this(dz.d)
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0007C1B6 File Offset: 0x0007B1B6
		public dz(int A_0) : this(A_0, 0)
		{
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0007C1C0 File Offset: 0x0007B1C0
		public dz(dz A_0) : this(A_0.a.Length)
		{
			Array.Copy(A_0.a, 0, this.a, 0, this.a.Length);
			this.b = A_0.b;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0007C1F7 File Offset: 0x0007B1F7
		public dz(int A_0, int A_1)
		{
			this.a = new int[A_0];
			if (this.c != 0)
			{
				this.c = A_1;
				this.a(this.c, this.a, 0);
			}
			this.b = 0;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0007C234 File Offset: 0x0007B234
		private void a(int A_0, int[] A_1, int A_2)
		{
			for (int i = A_2; i < A_1.Length; i++)
			{
				A_1[i] = A_0;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0007C254 File Offset: 0x0007B254
		public void a(int A_0, int A_1)
		{
			if (A_0 > this.b)
			{
				throw new IndexOutOfRangeException();
			}
			if (A_0 == this.b)
			{
				this.h(A_1);
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

		// Token: 0x06001C50 RID: 7248 RVA: 0x0007C2D4 File Offset: 0x0007B2D4
		public bool h(int A_0)
		{
			if (this.b == this.a.Length)
			{
				this.a(this.b * 2);
			}
			int[] array = this.a;
			int num = this.b;
			this.b = num + 1;
			array[num] = A_0;
			return true;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0007C31C File Offset: 0x0007B31C
		public bool c(dz A_0)
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

		// Token: 0x06001C52 RID: 7250 RVA: 0x0007C390 File Offset: 0x0007B390
		public bool a(int A_0, dz A_1)
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

		// Token: 0x06001C53 RID: 7251 RVA: 0x0007C42D File Offset: 0x0007B42D
		public void c()
		{
			this.b = 0;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0007C438 File Offset: 0x0007B438
		public bool b(int A_0)
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

		// Token: 0x06001C55 RID: 7253 RVA: 0x0007C46C File Offset: 0x0007B46C
		public bool b(dz A_0)
		{
			bool flag = true;
			if (this != A_0)
			{
				int num = 0;
				while (flag && num < A_0.b)
				{
					if (!this.b(A_0.a[num]))
					{
						flag = false;
					}
					num++;
				}
			}
			return flag;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0007C4A8 File Offset: 0x0007B4A8
		public override bool Equals(object o)
		{
			bool flag = this == o;
			if (!flag && o != null && o.GetType() == base.GetType())
			{
				dz dz = (dz)o;
				if (dz.b == this.b)
				{
					flag = true;
					int num = 0;
					while (flag && num < this.b)
					{
						flag = (this.a[num] == dz.a[num]);
						num++;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0007C512 File Offset: 0x0007B512
		public int d(int A_0)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException(A_0 + " not accessible in a list of length " + this.b);
			}
			return this.a[A_0];
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0007C548 File Offset: 0x0007B548
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.b; i++)
			{
				num = 31 * num + this.a[i];
			}
			return num;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0007C578 File Offset: 0x0007B578
		public int c(int A_0)
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

		// Token: 0x06001C5A RID: 7258 RVA: 0x0007C5AD File Offset: 0x0007B5AD
		public bool b()
		{
			return this.b == 0;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0007C5B8 File Offset: 0x0007B5B8
		public int e(int A_0)
		{
			int num = this.b - 1;
			while (num >= 0 && A_0 != this.a[num])
			{
				num--;
			}
			return num;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0007C5E4 File Offset: 0x0007B5E4
		public int f(int A_0)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException();
			}
			int result = this.a[A_0];
			Array.Copy(this.a, A_0 + 1, this.a, A_0, this.b - A_0);
			this.b--;
			return result;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0007C634 File Offset: 0x0007B634
		public bool g(int A_0)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this.b)
			{
				if (A_0 == this.a[num])
				{
					if (num + 1 < this.b)
					{
						Array.Copy(this.a, num + 1, this.a, num, this.b - num);
					}
					this.b--;
					flag = true;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007C69C File Offset: 0x0007B69C
		public bool d(dz A_0)
		{
			bool result = false;
			for (int i = 0; i < A_0.b; i++)
			{
				if (this.g(A_0.a[i]))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0007C6D0 File Offset: 0x0007B6D0
		public bool a(dz A_0)
		{
			bool result = false;
			int i = 0;
			while (i < this.b)
			{
				if (!A_0.b(this.a[i]))
				{
					this.f(i);
					result = true;
				}
				else
				{
					i++;
				}
			}
			return result;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007C70D File Offset: 0x0007B70D
		public int b(int A_0, int A_1)
		{
			if (A_0 >= this.b)
			{
				throw new IndexOutOfRangeException();
			}
			int result = this.a[A_0];
			this.a[A_0] = A_1;
			return result;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007C72F File Offset: 0x0007B72F
		public int d()
		{
			return this.b;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007C737 File Offset: 0x0007B737
		public int a()
		{
			return this.b;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0007C740 File Offset: 0x0007B740
		public int[] e()
		{
			int[] array = new int[this.b];
			Array.Copy(this.a, 0, array, 0, this.b);
			return array;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0007C770 File Offset: 0x0007B770
		public int[] a(int[] A_0)
		{
			int[] result;
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

		// Token: 0x06001C65 RID: 7269 RVA: 0x0007C7A8 File Offset: 0x0007B7A8
		private void a(int A_0)
		{
			int[] array = new int[(A_0 == this.a.Length) ? (A_0 + 1) : A_0];
			if (this.c != 0)
			{
				this.a(this.c, array, this.a.Length);
			}
			Array.Copy(this.a, 0, array, 0, this.b);
			this.a = array;
		}

		// Token: 0x04001357 RID: 4951
		private int[] a;

		// Token: 0x04001358 RID: 4952
		private int b;

		// Token: 0x04001359 RID: 4953
		private int c;

		// Token: 0x0400135A RID: 4954
		private static int d = 128;
	}
}
