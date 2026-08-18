using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002D8 RID: 728
	internal class bo : az
	{
		// Token: 0x060019A0 RID: 6560 RVA: 0x00071B40 File Offset: 0x00070B40
		public bo(h4 A_0)
		{
			if (!(A_0 is hz))
			{
				throw new IOException("Cannot open internal document storage");
			}
			hz hz = (hz)A_0;
			if (hz.a() == null)
			{
				throw new IOException("Cannot open internal document storage");
			}
			this.a = 0L;
			this.b = 0L;
			this.c = A_0.oy();
			this.d = false;
			this.e = hz.a();
			this.f = this.a(0L);
			if (this.f == null)
			{
				this.e = true;
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00071BCC File Offset: 0x00070BCC
		public override long get_Length()
		{
			return (long)this.c;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00071BD5 File Offset: 0x00070BD5
		public bo(eg A_0)
		{
			this.a = 0L;
			this.b = 0L;
			this.c = A_0.a();
			this.d = false;
			this.e = A_0;
			this.f = this.a(0L);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00071C15 File Offset: 0x00070C15
		public override int aq()
		{
			if (this.d)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return this.c - (int)this.a;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00071C38 File Offset: 0x00070C38
		public override void Close()
		{
			this.d = true;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00071C41 File Offset: 0x00070C41
		public override void ar(int A_0)
		{
			this.b = this.a;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00071C4F File Offset: 0x00070C4F
		private new fd a(long A_0)
		{
			return this.e.a((int)A_0);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00071C60 File Offset: 0x00070C60
		public override int @as()
		{
			this.b();
			if (this.a())
			{
				return global::a.b.az.a;
			}
			int result = this.f.a();
			this.a += 1L;
			if (this.f.c() < 1)
			{
				this.f = this.a(this.a);
			}
			return result;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00071CBC File Offset: 0x00070CBC
		public override int Read(byte[] b, int off, int len)
		{
			this.b();
			if (b == null)
			{
				throw new ArgumentException("buffer must not be null");
			}
			if (off < 0 || len < 0 || b.Length < off + len)
			{
				throw new IndexOutOfRangeException("can't read past buffer boundaries");
			}
			if (len == 0)
			{
				return 0;
			}
			if (this.a())
			{
				return global::a.b.az.a;
			}
			int num = Math.Min(this.aq(), len);
			this.av(b, off, num);
			return num;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00071D22 File Offset: 0x00070D22
		public override void at()
		{
			this.a = this.b;
			this.f = this.a(this.a);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00071D44 File Offset: 0x00070D44
		public override long au(long A_0)
		{
			this.b();
			if (A_0 < 0L)
			{
				return 0L;
			}
			long num = this.a + (long)((int)A_0);
			if (num < this.a)
			{
				num = (long)this.c;
			}
			else if (num > (long)this.c)
			{
				num = (long)this.c;
			}
			long result = num - this.a;
			this.a = num;
			this.f = this.a(this.a);
			return result;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00071DB0 File Offset: 0x00070DB0
		private new void b()
		{
			if (this.d)
			{
				throw new IOException("cannot perform requested operation on a closed stream");
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00071DC5 File Offset: 0x00070DC5
		private new bool a()
		{
			return this.a == (long)this.c;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00071DD8 File Offset: 0x00070DD8
		private new void a(int A_0)
		{
			if (this.d)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			if ((long)A_0 > (long)this.c - this.a)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Buffer underrun - requested ",
					A_0,
					" bytes but ",
					(long)this.c - this.a,
					" was available"
				}));
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00071E52 File Offset: 0x00070E52
		public override int ReadByte()
		{
			return this.a2();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00071E5A File Offset: 0x00070E5A
		public override double aw()
		{
			return BitConverter.Int64BitsToDouble(this.ax());
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00071E67 File Offset: 0x00070E67
		public override short az()
		{
			return (short)this.a1();
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00071E70 File Offset: 0x00070E70
		public override void av(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_2);
			int num = this.f.c();
			if (num > A_2)
			{
				this.f.a(A_0, A_1, A_2);
				this.a += (long)A_2;
				return;
			}
			int i = A_2;
			int num2 = A_1;
			while (i > 0)
			{
				bool flag = i >= num;
				int num3;
				if (flag)
				{
					num3 = num;
				}
				else
				{
					num3 = i;
				}
				this.f.a(A_0, num2, num3);
				i -= num3;
				num2 += num3;
				this.a += (long)num3;
				if (flag)
				{
					if (this.a == (long)this.c)
					{
						if (i > 0)
						{
							throw new InvalidOperationException("reached end of document stream unexpectedly");
						}
						this.f = null;
						return;
					}
					else
					{
						this.f = this.a(this.a);
						num = this.f.c();
					}
				}
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00071F38 File Offset: 0x00070F38
		public override long ax()
		{
			this.a(global::a.b.az.d);
			int num = this.f.c();
			long result;
			if (num > global::a.b.az.d)
			{
				result = this.f.b();
			}
			else
			{
				fd fd = this.a(this.a + (long)num);
				if (num == global::a.b.az.d)
				{
					result = this.f.b();
				}
				else
				{
					result = fd.b(this.f, num);
				}
				this.f = fd;
			}
			this.a += (long)global::a.b.az.d;
			return result;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00071FC4 File Offset: 0x00070FC4
		public override int a0()
		{
			this.a(global::a.b.az.c);
			int num = this.f.c();
			int result;
			if (num > global::a.b.az.c)
			{
				result = this.f.d();
			}
			else
			{
				fd fd = this.a(this.a + (long)num);
				if (num == global::a.b.az.c)
				{
					result = this.f.d();
				}
				else
				{
					result = fd.a(this.f, num);
				}
				this.f = fd;
			}
			this.a += (long)global::a.b.az.c;
			return result;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00072050 File Offset: 0x00071050
		public override int a1()
		{
			this.a(global::a.b.az.b);
			int num = this.f.c();
			int result;
			if (num > global::a.b.az.b)
			{
				result = this.f.e();
			}
			else
			{
				fd fd = this.a(this.a + (long)num);
				if (num == global::a.b.az.b)
				{
					result = this.f.e();
				}
				else
				{
					result = fd.a(this.f);
				}
				this.f = fd;
			}
			this.a += (long)global::a.b.az.b;
			return result;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000720D8 File Offset: 0x000710D8
		public override int a2()
		{
			this.a(1);
			int result = this.f.a();
			this.a += 1L;
			if (this.f.c() < 1)
			{
				this.f = this.a(this.a);
			}
			return result;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00072128 File Offset: 0x00071128
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Current)
			{
				if (this.a + offset >= this.Length || this.a + offset < 0L)
				{
					throw new ArgumentException("invalid offset");
				}
				this.a += (long)((int)offset);
			}
			else if (origin == SeekOrigin.Begin)
			{
				if (offset >= this.Length || offset < 0L)
				{
					throw new ArgumentException("invalid offset");
				}
				this.a = offset;
			}
			else if (origin == SeekOrigin.End)
			{
				if (this.Length + offset >= this.Length || this.Length + offset < 0L)
				{
					throw new ArgumentException("invalid offset");
				}
				this.a = this.Length + offset;
			}
			return this.a;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000721D6 File Offset: 0x000711D6
		public override long get_Position()
		{
			if (this.d)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return this.a;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000721F1 File Offset: 0x000711F1
		public override void set_Position(long value)
		{
			this.a = (long)((int)value);
		}

		// Token: 0x04001279 RID: 4729
		private new long a;

		// Token: 0x0400127A RID: 4730
		private new long b;

		// Token: 0x0400127B RID: 4731
		private new int c;

		// Token: 0x0400127C RID: 4732
		private new bool d;

		// Token: 0x0400127D RID: 4733
		private new eg e;

		// Token: 0x0400127E RID: 4734
		private fd f;
	}
}
