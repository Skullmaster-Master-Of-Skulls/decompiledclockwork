using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002D9 RID: 729
	internal class v
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x000721FC File Offset: 0x000711FC
		public static v a(POIFSFileSystem A_0)
		{
			return v.a(A_0.Root);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0007220C File Offset: 0x0007120C
		public static v a(DirectoryNode A_0)
		{
			bool a_ = false;
			try
			{
				A_0.el("\u0001Ole10ItemName");
				a_ = true;
			}
			catch (FileNotFoundException)
			{
				a_ = false;
			}
			h4 h = (h4)A_0.el(v.l);
			byte[] a_2 = new byte[h.oy()];
			A_0.a(h).a(a_2);
			return new v(a_2, 0, a_);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00072274 File Offset: 0x00071274
		public v(byte[] A_0, int A_1) : this(A_0, A_1, false)
		{
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00072280 File Offset: 0x00071280
		public v(byte[] A_0, int A_1, bool A_2)
		{
			if (A_0.Length < A_1 + 2)
			{
				throw new Ole10NativeException("data is too small");
			}
			this.a = p.i(A_0, A_1);
			int num = A_1 + 4;
			if (A_2)
			{
				this.j = new byte[this.a - 4];
				Array.Copy(A_0, 4, this.j, 0, this.j.Length);
				this.i = this.a - 4;
				byte[] array = new byte[8];
				Array.Copy(this.j, 0, array, 0, Math.Min(this.j.Length, 8));
				this.c = "ole-" + f5.a(array);
				this.d = this.c;
				this.h = this.c;
				return;
			}
			this.b = p.k(A_0, num);
			num += 2;
			int num2 = v.a(A_0, num);
			this.c = global::a.b.a.a(A_0, num, num2 - 1);
			num += num2;
			num2 = v.a(A_0, num);
			this.d = global::a.b.a.a(A_0, num, num2 - 1);
			num += num2;
			this.e = p.k(A_0, num);
			num += 2;
			num2 = (int)p.b(A_0, num);
			this.f = new byte[num2];
			num += num2;
			num2 = 3;
			this.g = new byte[num2];
			num += num2;
			num2 = v.a(A_0, num);
			this.h = global::a.b.a.a(A_0, num, num2 - 1);
			num += num2;
			if (this.a + 4 - num <= 4)
			{
				throw new Ole10NativeException("Invalid Ole10Native");
			}
			this.i = p.i(A_0, num);
			num += 4;
			if (this.i > this.a || this.i < 0)
			{
				throw new Ole10NativeException("Invalid Ole10Native");
			}
			this.j = new byte[this.i];
			Array.Copy(A_0, num, this.j, 0, this.i);
			num += this.i;
			if (this.f.Length != 0)
			{
				this.k = p.k(A_0, num);
				num += 2;
				return;
			}
			this.k = 0;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00072488 File Offset: 0x00071488
		private static int a(byte[] A_0, int A_1)
		{
			int num = 0;
			while (num + A_1 < A_0.Length && A_0[A_1 + num] != 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000724B1 File Offset: 0x000714B1
		public int b()
		{
			return this.a;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000724B9 File Offset: 0x000714B9
		public short i()
		{
			return this.b;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000724C1 File Offset: 0x000714C1
		public string e()
		{
			return this.c;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000724C9 File Offset: 0x000714C9
		public string d()
		{
			return this.d;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000724D1 File Offset: 0x000714D1
		public short f()
		{
			return this.e;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000724D9 File Offset: 0x000714D9
		public byte[] j()
		{
			return this.f;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000724E1 File Offset: 0x000714E1
		public byte[] c()
		{
			return this.g;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000724E9 File Offset: 0x000714E9
		public string a()
		{
			return this.h;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000724F1 File Offset: 0x000714F1
		public int h()
		{
			return this.i;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000724F9 File Offset: 0x000714F9
		public byte[] g()
		{
			return this.j;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00072501 File Offset: 0x00071501
		public short k()
		{
			return this.k;
		}

		// Token: 0x0400127F RID: 4735
		private int a;

		// Token: 0x04001280 RID: 4736
		private short b;

		// Token: 0x04001281 RID: 4737
		private string c;

		// Token: 0x04001282 RID: 4738
		private string d;

		// Token: 0x04001283 RID: 4739
		private short e;

		// Token: 0x04001284 RID: 4740
		private byte[] f;

		// Token: 0x04001285 RID: 4741
		private byte[] g;

		// Token: 0x04001286 RID: 4742
		private string h;

		// Token: 0x04001287 RID: 4743
		private int i;

		// Token: 0x04001288 RID: 4744
		private byte[] j;

		// Token: 0x04001289 RID: 4745
		private short k;

		// Token: 0x0400128A RID: 4746
		public static string l = "\u0001Ole10Native";
	}
}
