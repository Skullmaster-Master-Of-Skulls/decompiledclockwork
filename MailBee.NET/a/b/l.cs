using System;
using System.Collections.Generic;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002D1 RID: 721
	internal class l : az
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x0006FC84 File Offset: 0x0006EC84
		public l(h4 A_0)
		{
			if (!(A_0 is hz))
			{
				throw new IOException("Cannot open internal document storage, " + A_0 + " not a Document Node");
			}
			this.a = 0;
			this.b = 0;
			this.c = 0;
			this.d = 0;
			this.e = A_0.oy();
			this.f = false;
			hz hz = (hz)A_0;
			gg a_ = (gg)hz.Property;
			this.g = new hw(a_, ((DirectoryNode)hz.Parent).NFileSystem);
			this.h = this.g.f();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0006FD24 File Offset: 0x0006ED24
		public l(hw A_0)
		{
			this.a = 0;
			this.b = 0;
			this.c = 0;
			this.d = 0;
			this.e = A_0.c();
			this.f = false;
			this.g = A_0;
			this.h = this.g.f();
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006FD7E File Offset: 0x0006ED7E
		public override int aq()
		{
			if (this.f)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return this.e - this.a;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006FDA0 File Offset: 0x0006EDA0
		public override void Close()
		{
			this.f = true;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006FDA9 File Offset: 0x0006EDA9
		public override void ar(int A_0)
		{
			this.c = this.a;
			this.d = Math.Max(0, this.b - 1);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006FDCC File Offset: 0x0006EDCC
		public override int @as()
		{
			this.b();
			if (this.a())
			{
				return global::a.b.az.a;
			}
			byte[] array = new byte[1];
			int num = this.Read(array, 0, 1);
			if (num < 0)
			{
				return num;
			}
			if (array[0] < 0)
			{
				return (int)array[0] + 256;
			}
			return (int)array[0];
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0006FE18 File Offset: 0x0006EE18
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

		// Token: 0x06001914 RID: 6420 RVA: 0x0006FE80 File Offset: 0x0006EE80
		public override void at()
		{
			if (this.c == 0 && this.d == 0)
			{
				this.b = this.d;
				this.a = this.c;
				this.h = this.g.f();
				this.i = null;
				return;
			}
			this.h = this.g.f();
			this.a = 0;
			for (int i = 0; i < this.d; i++)
			{
				this.h.MoveNext();
				this.i = this.h.Current;
				this.a += this.i.n();
			}
			this.b = this.d;
			if (this.a != this.c)
			{
				this.h.MoveNext();
				this.i = this.h.Current;
				this.b++;
				int num = this.c - this.a;
				this.i.b(this.i.g() + num);
			}
			this.a = this.c;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0006FFA4 File Offset: 0x0006EFA4
		public override long au(long A_0)
		{
			this.b();
			if (A_0 < 0L)
			{
				return 0L;
			}
			int num = this.a + (int)A_0;
			if (num < this.a)
			{
				num = this.e;
			}
			else if (num > this.e)
			{
				num = this.e;
			}
			long num2 = (long)(num - this.a);
			byte[] a_ = new byte[(int)num2];
			this.ay(a_);
			return num2;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00070003 File Offset: 0x0006F003
		private new void b()
		{
			if (this.f)
			{
				throw new IOException("cannot perform requested operation on a closed stream");
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00070018 File Offset: 0x0006F018
		private new bool a()
		{
			return this.a == this.e;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00070028 File Offset: 0x0006F028
		private new void a(int A_0)
		{
			if (this.f)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			if (A_0 > this.e - this.a)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Buffer underrun - requested ",
					A_0,
					" bytes but ",
					this.e - this.a,
					" was available"
				}));
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000700A0 File Offset: 0x0006F0A0
		public override void av(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_2);
			int num;
			for (int i = 0; i < A_2; i += num)
			{
				if (this.i == null || this.i.n() == 0)
				{
					this.b++;
					this.h.MoveNext();
					this.i = this.h.Current;
				}
				num = Math.Min(A_2 - i, this.i.n());
				this.i.c(A_0, A_1 + i, num);
				this.a += num;
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00070133 File Offset: 0x0006F133
		public override int ReadByte()
		{
			return this.a2();
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0007013B File Offset: 0x0006F13B
		public override double aw()
		{
			return BitConverter.Int64BitsToDouble(this.ax());
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00070148 File Offset: 0x0006F148
		public override long ax()
		{
			this.a(global::a.b.az.d);
			byte[] a_ = new byte[global::a.b.az.d];
			this.av(a_, 0, global::a.b.az.d);
			return p.g(a_, 0);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0007017F File Offset: 0x0006F17F
		public override void ay(byte[] A_0)
		{
			this.av(A_0, 0, A_0.Length);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0007018C File Offset: 0x0006F18C
		public override short az()
		{
			this.a(global::a.b.az.b);
			byte[] a_ = new byte[global::a.b.az.b];
			this.av(a_, 0, global::a.b.az.b);
			return p.h(a_);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000701C4 File Offset: 0x0006F1C4
		public override int a0()
		{
			this.a(global::a.b.az.c);
			byte[] a_ = new byte[global::a.b.az.c];
			this.av(a_, 0, global::a.b.az.c);
			return p.f(a_);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000701FC File Offset: 0x0006F1FC
		public override int a1()
		{
			this.a(global::a.b.az.b);
			byte[] a_ = new byte[global::a.b.az.b];
			this.av(a_, 0, global::a.b.az.b);
			return p.g(a_);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00070234 File Offset: 0x0006F234
		public override int a2()
		{
			this.a(1);
			byte[] array = new byte[1];
			this.av(array, 0, 1);
			if (array[0] >= 0)
			{
				return (int)array[0];
			}
			return (int)array[0] + 256;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0007026B File Offset: 0x0006F26B
		public override long get_Length()
		{
			if (this.f)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return (long)this.e;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00070287 File Offset: 0x0006F287
		public override long get_Position()
		{
			if (this.f)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return (long)this.a;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000702A3 File Offset: 0x0006F2A3
		public override void set_Position(long value)
		{
			this.a = (int)value;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000702AD File Offset: 0x0006F2AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (offset == 0L)
			{
				this.at();
			}
			else
			{
				this.ar((int)offset);
			}
			return 0L;
		}

		// Token: 0x04001252 RID: 4690
		private new int a;

		// Token: 0x04001253 RID: 4691
		private new int b;

		// Token: 0x04001254 RID: 4692
		private new int c;

		// Token: 0x04001255 RID: 4693
		private new int d;

		// Token: 0x04001256 RID: 4694
		private new int e;

		// Token: 0x04001257 RID: 4695
		private bool f;

		// Token: 0x04001258 RID: 4696
		private hw g;

		// Token: 0x04001259 RID: 4697
		private IEnumerator<he> h;

		// Token: 0x0400125A RID: 4698
		private he i;
	}
}
