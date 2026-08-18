using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x020002D6 RID: 726
	[DefaultMember("Item")]
	internal class he
	{
		// Token: 0x06001972 RID: 6514 RVA: 0x000715B0 File Offset: 0x000705B0
		private he(int A_0, int A_1, int A_2, int A_3, byte[] A_4, int A_5)
		{
			if (A_3 < 0)
			{
				throw new ArgumentException();
			}
			this.e = A_3;
			this.e(A_2);
			this.b(A_1);
			if (A_0 >= 0)
			{
				if (A_0 > A_1)
				{
					throw new ArgumentException();
				}
				this.b = A_0;
			}
			this.a = A_4;
			this.f = A_5;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00071610 File Offset: 0x00070610
		public he(byte[] A_0, int A_1, int A_2) : this(-1, A_1, A_1 + A_2, A_0.Length, A_0, 0)
		{
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00071622 File Offset: 0x00070622
		public he(int A_0, int A_1) : this(-1, 0, A_1, A_0, new byte[A_0], 0)
		{
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00071635 File Offset: 0x00070635
		protected he(byte[] A_0, int A_1, int A_2, int A_3, int A_4, int A_5) : this(A_1, A_2, A_3, A_4, A_0, A_5)
		{
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00071646 File Offset: 0x00070646
		public int g()
		{
			return this.c;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0007164E File Offset: 0x0007064E
		public void b(int A_0)
		{
			if (A_0 < 0 || A_0 > this.d)
			{
				throw new ArgumentException();
			}
			this.c = A_0;
			if (this.b > this.c)
			{
				this.b = -1;
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0007167F File Offset: 0x0007067F
		public int l()
		{
			return this.d;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00071688 File Offset: 0x00070688
		public void e(int A_0)
		{
			if (A_0 > this.e || A_0 < 0)
			{
				throw new ArgumentException();
			}
			this.d = A_0;
			if (this.c > this.d)
			{
				this.c = this.d;
			}
			if (this.b > this.d)
			{
				this.b = -1;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000716DE File Offset: 0x000706DE
		public int d()
		{
			return this.d - this.c;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000716ED File Offset: 0x000706ED
		public bool m()
		{
			return this.c < this.d;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000716FD File Offset: 0x000706FD
		public static he a(int A_0)
		{
			if (A_0 < 0)
			{
				throw new ArgumentException();
			}
			return new he(A_0, A_0);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00071710 File Offset: 0x00070710
		public static he a(byte[] A_0, int A_1, int A_2)
		{
			he result;
			try
			{
				result = new he(A_0, A_1, A_2);
			}
			catch (ArgumentException)
			{
				throw new IndexOutOfRangeException();
			}
			return result;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00071740 File Offset: 0x00070740
		public static he a(byte[] A_0)
		{
			return he.a(A_0, 0, A_0.Length);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0007174C File Offset: 0x0007074C
		public he b()
		{
			return new he(this.a, -1, 0, this.n(), this.n(), this.g() + this.f);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00071774 File Offset: 0x00070774
		public he c()
		{
			return null;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00071778 File Offset: 0x00070778
		protected int h()
		{
			if (this.c >= this.d)
			{
				throw new IndexOutOfRangeException();
			}
			int num = this.c;
			this.c = num + 1;
			return num;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000717AA File Offset: 0x000707AA
		protected int f(int A_0)
		{
			if (this.d - this.c < A_0)
			{
				throw new IndexOutOfRangeException();
			}
			int result = this.c;
			this.c += A_0;
			return result;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000717D6 File Offset: 0x000707D6
		protected int i()
		{
			return this.h();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000717DE File Offset: 0x000707DE
		protected int g(int A_0)
		{
			return this.f(A_0);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000717E7 File Offset: 0x000707E7
		protected int d(int A_0)
		{
			return A_0 + this.f;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000717F1 File Offset: 0x000707F1
		protected int c(int A_0)
		{
			if (A_0 < 0 || A_0 >= this.d)
			{
				throw new IndexOutOfRangeException();
			}
			return A_0;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00071807 File Offset: 0x00070807
		protected int a(int A_0, int A_1)
		{
			if (A_0 < 0 || A_1 > this.d - A_0)
			{
				throw new IndexOutOfRangeException();
			}
			return A_0;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0007181F File Offset: 0x0007081F
		public byte j()
		{
			return this.a[this.d(this.h())];
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00071834 File Offset: 0x00070834
		public byte h(int A_0)
		{
			return this.a[this.d(this.c(A_0))];
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0007184A File Offset: 0x0007084A
		public void a(int A_0, byte A_1)
		{
			if (A_0 < 0 || A_0 >= this.d)
			{
				throw new IndexOutOfRangeException();
			}
			this.a[this.d(A_0)] = A_1;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007186E File Offset: 0x0007086E
		protected void a(int A_0, int A_1, int A_2)
		{
			if ((A_0 | A_1 | A_0 + A_1 | A_2 - (A_0 + A_1)) < 0)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00071886 File Offset: 0x00070886
		public he c(byte[] A_0)
		{
			return this.c(A_0, 0, A_0.Length);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00071894 File Offset: 0x00070894
		public he c(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_1, A_2, A_0.Length);
			if (A_2 > this.n())
			{
				throw new ArgumentException();
			}
			Array.Copy(this.a, this.d(this.c), A_0, A_1, A_2);
			this.b(this.g() + A_2);
			return this;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000718E4 File Offset: 0x000708E4
		public he a(byte A_0)
		{
			this.a[this.d(this.i())] = A_0;
			return this;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000718FC File Offset: 0x000708FC
		public he b(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_1, A_2, A_0.Length);
			if (A_2 > this.n())
			{
				throw new IndexOutOfRangeException();
			}
			Array.Copy(A_0, A_1, this.a, this.d(this.g()), A_2);
			this.b(this.g() + A_2);
			return this;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007194C File Offset: 0x0007094C
		public he a(he A_0)
		{
			if (A_0 == this)
			{
				throw new ArgumentException();
			}
			int num = A_0.n();
			if (num > this.n())
			{
				throw new IndexOutOfRangeException();
			}
			for (int i = 0; i < num; i++)
			{
				this.a(A_0.j());
			}
			return this;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00071993 File Offset: 0x00070993
		public he b(byte[] A_0)
		{
			return this.b(A_0, 0, A_0.Length);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000719A0 File Offset: 0x000709A0
		public int n()
		{
			return this.d - this.c;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000719AF File Offset: 0x000709AF
		public byte[] a()
		{
			return this.a;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000719B7 File Offset: 0x000709B7
		public int e()
		{
			return this.f;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000719BF File Offset: 0x000709BF
		public bool k()
		{
			return true;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000719C2 File Offset: 0x000709C2
		public int f()
		{
			return this.e;
		}

		// Token: 0x0400126F RID: 4719
		private byte[] a;

		// Token: 0x04001270 RID: 4720
		private int b = -1;

		// Token: 0x04001271 RID: 4721
		private int c;

		// Token: 0x04001272 RID: 4722
		private int d;

		// Token: 0x04001273 RID: 4723
		private int e;

		// Token: 0x04001274 RID: 4724
		private int f;
	}
}
