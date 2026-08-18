using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002EE RID: 750
	internal class gx : o
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x00074440 File Offset: 0x00073440
		protected gx()
		{
			this.f = new byte[512];
			for (int i = 0; i < this.f.Length; i++)
			{
				this.f[i] = gx.d;
			}
			this.e = new id[gx.a];
			int num = 0;
			for (int j = 0; j < gx.a; j++)
			{
				this.e[j] = new id(num);
				num += 4;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000744B8 File Offset: 0x000734B8
		protected gx(y A_0) : base(A_0)
		{
			int num = A_0.e();
			this.g = new int[num];
			this.h = true;
			for (int i = 0; i < this.g.Length; i++)
			{
				this.g[i] = -1;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00074504 File Offset: 0x00073504
		protected gx(y A_0, int[] A_1, int A_2, int A_3) : this(A_0)
		{
			for (int i = A_2; i < A_3; i++)
			{
				this.g[i - A_2] = A_1[i];
			}
			if (A_3 - A_2 == this.g.Length)
			{
				this.e();
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00074548 File Offset: 0x00073548
		private void e()
		{
			bool flag = false;
			for (int i = 0; i < this.g.Length; i++)
			{
				if (this.g[i] == -1)
				{
					flag = true;
					break;
				}
			}
			this.h = flag;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00074580 File Offset: 0x00073580
		public new static gx a(y A_0, BinaryReader A_1)
		{
			gx gx = new gx(A_0);
			byte[] array = new byte[4];
			for (int i = 0; i < gx.g.Length; i++)
			{
				A_1.Read(array, 0, array.Length);
				gx.g[i] = p.f(array);
			}
			gx.e();
			return gx;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000745D0 File Offset: 0x000735D0
		public new static gx a(y A_0, he A_1)
		{
			gx gx = new gx(A_0);
			byte[] a_ = new byte[4];
			for (int i = 0; i < gx.g.Length; i++)
			{
				A_1.c(a_);
				gx.g[i] = p.f(a_);
			}
			gx.e();
			return gx;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0007461C File Offset: 0x0007361C
		public new static gx a(y A_0, bool A_1)
		{
			gx gx = new gx(A_0);
			if (A_1)
			{
				gx.a(A_0, -2);
			}
			return gx;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00074640 File Offset: 0x00073640
		public new static gx[] a(y A_0, int[] A_1)
		{
			gx[] array = new gx[gx.c(A_1.Length)];
			int num = 0;
			int num2 = A_1.Length;
			for (int i = 0; i < A_1.Length; i += gx.a)
			{
				array[num++] = new gx(A_0, A_1, i, (num2 > gx.a) ? (i + gx.a) : A_1.Length);
				num2 -= gx.a;
			}
			return array;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000746A0 File Offset: 0x000736A0
		public new static gx[] a(y A_0, int[] A_1, int A_2)
		{
			int num = gx.b(A_1.Length);
			gx[] array = new gx[num];
			int i = 0;
			int num2 = A_1.Length;
			if (num != 0)
			{
				for (int j = 0; j < A_1.Length; j += gx.b)
				{
					array[i++] = new gx(A_0, A_1, j, (num2 > gx.b) ? (j + gx.b) : A_1.Length);
					num2 -= gx.b;
				}
				for (i = 0; i < array.Length - 1; i++)
				{
					array[i].a(A_0, A_2 + i + 1);
				}
				array[i].a(A_0, -2);
			}
			return array;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0007472B File Offset: 0x0007372B
		public static int c(int A_0)
		{
			return (A_0 + gx.a - 1) / gx.a;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0007473C File Offset: 0x0007373C
		public static int d(y A_0, int A_1)
		{
			int num = A_0.e();
			return (A_1 + num - 1) / num;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00074757 File Offset: 0x00073757
		public static int b(int A_0)
		{
			return (A_0 + gx.b - 1) / gx.b;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00074768 File Offset: 0x00073768
		public static int c(y A_0, int A_1)
		{
			int num = A_0.b();
			return (A_1 + num - 1) / num;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00074783 File Offset: 0x00073783
		public static int b(y A_0, int A_1)
		{
			return (1 + A_1 * A_0.e()) * A_0.f();
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00074796 File Offset: 0x00073796
		public new static int a(c3 A_0)
		{
			return gx.b(A_0.b(), A_0.f());
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000747AC File Offset: 0x000737AC
		public static ct b(int A_0, c3 A_1, List<gx> A_2)
		{
			y y = A_1.b();
			int index = (int)Math.Floor(1.0 * (double)A_0 / (double)y.e());
			return new ct(A_0 % y.e(), A_2[index]);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000747F0 File Offset: 0x000737F0
		public new static ct a(int A_0, c3 A_1, List<gx> A_2)
		{
			y y = A_1.b();
			int index = (int)Math.Floor(1.0 * (double)A_0 / (double)y.e());
			return new ct(A_0 % y.e(), A_2[index]);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00074833 File Offset: 0x00073833
		public static int d()
		{
			return gx.a;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0007483A File Offset: 0x0007383A
		public static int c()
		{
			return gx.b;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00074841 File Offset: 0x00073841
		public static int b()
		{
			return gx.c;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00074848 File Offset: 0x00073848
		private new void a(int A_0)
		{
			this.e[gx.b].a(A_0, this.f);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00074864 File Offset: 0x00073864
		private new void a(y A_0, int A_1)
		{
			int num = A_0.b();
			this.g[num] = A_1;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00074881 File Offset: 0x00073881
		public bool g()
		{
			return this.h;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0007488C File Offset: 0x0007388C
		public int e(int A_0)
		{
			if (A_0 >= this.g.Length)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Unable to fetch offset ",
					A_0,
					" as the BAT only contains ",
					this.g.Length,
					" entries"
				}));
			}
			return this.g[A_0];
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000748F0 File Offset: 0x000738F0
		public new void a(int A_0, int A_1)
		{
			int num = this.g[A_0];
			this.g[A_0] = A_1;
			if (A_1 == -1)
			{
				this.h = true;
				return;
			}
			if (num == -1)
			{
				this.e();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00074925 File Offset: 0x00073925
		public int f()
		{
			return this.i;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0007492D File Offset: 0x0007392D
		public void d(int A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00074938 File Offset: 0x00073938
		private gx(int[] A_0, int A_1, int A_2) : this()
		{
			for (int i = A_1; i < A_2; i++)
			{
				this.e[i - A_1].a(A_0[i], this.f);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0007496F File Offset: 0x0007396F
		public new void a(he A_0)
		{
			A_0.b(this.a());
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00074980 File Offset: 0x00073980
		public override void bc(Stream A_0)
		{
			byte[] array = this.a();
			A_0.Write(array, 0, array.Length);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000749A0 File Offset: 0x000739A0
		public new void a(byte[] A_0)
		{
			byte[] array = this.a();
			for (int i = 0; i < array.Length; i++)
			{
				A_0[i] = array[i];
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000749C8 File Offset: 0x000739C8
		private new byte[] a()
		{
			byte[] array = new byte[this.a.f()];
			int num = 0;
			for (int i = 0; i < this.g.Length; i++)
			{
				p.c(array, num, this.g[i]);
				num += 4;
			}
			return array;
		}

		// Token: 0x040012DA RID: 4826
		private new static int a = 128;

		// Token: 0x040012DB RID: 4827
		private static int b = gx.a - 1;

		// Token: 0x040012DC RID: 4828
		private static int c = gx.b * 4;

		// Token: 0x040012DD RID: 4829
		private static byte d = byte.MaxValue;

		// Token: 0x040012DE RID: 4830
		private id[] e;

		// Token: 0x040012DF RID: 4831
		private byte[] f;

		// Token: 0x040012E0 RID: 4832
		private int[] g;

		// Token: 0x040012E1 RID: 4833
		private bool h;

		// Token: 0x040012E2 RID: 4834
		private int i;
	}
}
