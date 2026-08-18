using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002DF RID: 735
	internal class gp : cl
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x000731AE File Offset: 0x000721AE
		public gp(byte[] A_0, int A_1)
		{
			this.a = A_0;
			this.b = (long)A_1;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000731C5 File Offset: 0x000721C5
		public gp(byte[] A_0) : this(A_0, A_0.Length)
		{
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000731D4 File Offset: 0x000721D4
		public override he m3(int A_0, long A_1)
		{
			if (A_1 >= this.b)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Unable to read ",
					A_0,
					" bytes from ",
					A_1,
					" in stream of length ",
					this.b
				}));
			}
			int a_ = (int)Math.Min((long)A_0, this.b - A_1);
			return he.a(this.a, (int)A_1, a_);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00073254 File Offset: 0x00072254
		public override void m4(he A_0, long A_1)
		{
			long num = A_1 + (long)A_0.f();
			if (num > (long)this.a.Length)
			{
				this.a(num);
			}
			A_0.c(this.a, (int)A_1, A_0.f());
			if (num > this.b)
			{
				this.b = num;
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000732A4 File Offset: 0x000722A4
		private void a(long A_0)
		{
			long num = A_0 - (long)this.a.Length;
			if ((double)num < (double)this.a.Length * 0.25)
			{
				num = (long)((double)this.a.Length * 0.25);
			}
			if (num < 4096L)
			{
				num = 4096L;
			}
			byte[] destinationArray = new byte[(int)(num + (long)this.a.Length)];
			Array.Copy(this.a, 0, destinationArray, 0, (int)this.b);
			this.a = destinationArray;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00073327 File Offset: 0x00072327
		public override void m5(Stream A_0)
		{
			A_0.Write(this.a, 0, (int)this.b);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0007333D File Offset: 0x0007233D
		public override long m6()
		{
			return this.b;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00073345 File Offset: 0x00072345
		public override void m7()
		{
			this.a = null;
			this.b = -1L;
		}

		// Token: 0x040012A1 RID: 4769
		private byte[] a;

		// Token: 0x040012A2 RID: 4770
		private long b;
	}
}
