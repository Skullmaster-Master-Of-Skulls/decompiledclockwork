using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020000EB RID: 235
	internal abstract class g : a7
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x000236A4 File Offset: 0x000226A4
		public g()
		{
			this.b = false;
			this.ce();
			this.cf();
			this.n = null;
			this.o = null;
			this.p = null;
			this.r = null;
			this.q = null;
			this.c = Global.DefaultEncoding;
			this.d = Global.DefaultEncoding;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002370E File Offset: 0x0002270E
		protected virtual void ce()
		{
			this.e = new byte[this.a];
			this.i = new byte[this.a];
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00023734 File Offset: 0x00022734
		public void h(byte[] A_0, int A_1, int A_2, bf A_3)
		{
			int num = this.g(A_0, A_1, A_2);
			A_3.g = num;
			A_3.h = A_2;
			this.l.a(A_3);
			if (!this.b)
			{
				this.l();
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00023776 File Offset: 0x00022776
		public void h(byte[] A_0, bf A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			this.h(A_0, 0, A_0.Length, A_1);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002378D File Offset: 0x0002278D
		public void h(string A_0, bf A_1)
		{
			if (this.c == null)
			{
				throw new InvalidOperationException();
			}
			this.h(this.c.GetBytes(A_0), A_1);
		}

		// Token: 0x060007A7 RID: 1959
		public abstract void ci();

		// Token: 0x060007A8 RID: 1960 RVA: 0x000237B0 File Offset: 0x000227B0
		private int g(byte[] A_0, int A_1, int A_2)
		{
			this.e = global::a.w.b(this.e, this.f, this.f + A_2, true);
			Buffer.BlockCopy(A_0, A_1, this.e, this.f, A_2);
			this.f += A_2;
			return this.f - A_2;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00023808 File Offset: 0x00022808
		protected void l()
		{
			if (this.g < this.f)
			{
				int a_ = this.f - this.g;
				this.d4(this.e, this.g, a_);
				if (this.o != null)
				{
					this.o(this.e, this.g, a_, this.f.c());
				}
				if (this.q != null)
				{
					for (int i = this.h; i < this.l.Count; i++)
					{
						bf a_2 = this.l.b(i);
						this.q(a_2, this.e, this.f.c());
					}
				}
				this.g = this.f;
				this.h = this.l.Count;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000238DD File Offset: 0x000228DD
		public void u()
		{
			this.f = 0;
			this.g = 0;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000238ED File Offset: 0x000228ED
		public void m(int A_0)
		{
			if (!this.b)
			{
				this.j(A_0);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000238FE File Offset: 0x000228FE
		protected virtual bool cg()
		{
			return this.m.Count < this.l.a();
		}

		// Token: 0x060007AD RID: 1965
		protected abstract int ch(int A_0, out int A_1);

		// Token: 0x060007AE RID: 1966
		protected abstract int cj();

		// Token: 0x060007AF RID: 1967 RVA: 0x00023918 File Offset: 0x00022918
		public void p(int A_0)
		{
			this.i = global::a.w.b(this.i, this.j, A_0, true);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00023933 File Offset: 0x00022933
		public void n(int A_0)
		{
			this.i = global::a.w.a(this.i, this.j, A_0, true);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00023950 File Offset: 0x00022950
		protected void j(int A_0)
		{
			int num = 0;
			DateTime t = DateTime.MinValue;
			if (A_0 > 0)
			{
				t = DateTime.Now.AddMilliseconds((double)A_0);
			}
			bool flag = false;
			for (;;)
			{
				bool flag2 = false;
				while (flag || this.cg())
				{
					if (num < this.j + this.a / 2)
					{
						num = this.j + this.a / 2;
					}
					this.i = global::a.w.b(this.i, this.j, num, true);
					int num2 = this.d3(this.i, this.j);
					flag2 = true;
					if (this.n != null)
					{
						this.n(this.i, this.j, num2, this.f.c());
					}
					if (num2 <= 0)
					{
						if (flag)
						{
							flag = false;
						}
						this.cj();
						break;
					}
					this.j += num2;
					int count = this.m.Count;
					int num3 = this.ch(num2, out num);
					int count2 = this.m.Count;
					if (flag)
					{
						flag = (count2 == count);
					}
					if (num3 > 0)
					{
						Buffer.BlockCopy(this.i, num3, this.i, 0, this.j - num3);
						this.k -= num3;
						this.j -= num3;
					}
					if (this.h0() != null && !this.h0()())
					{
						goto Block_9;
					}
					if (A_0 > 0 && DateTime.Now > t)
					{
						goto Block_11;
					}
				}
				if (flag2 && this.m.Count > 0 && this.q().t() != af.d && ((this.m.Count > 0 && this.q().t() == af.c) || (this.m.Count > 1 && this.m.a(this.m.Count - 2).t() == af.c)) && this.hm(1000))
				{
					if (this.hn())
					{
						this.cj();
					}
					else
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			Block_9:
			throw new MailBeeUserAbortException(5);
			Block_11:
			throw new MailBeeRemoteHostResponseTimeoutException(61, this.hs());
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00023B6F File Offset: 0x00022B6F
		public void g(byte[] A_0, int A_1, int A_2, bf A_3, int A_4)
		{
			this.h(A_0, A_1, A_2, A_3);
			this.m(A_4);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00023B84 File Offset: 0x00022B84
		public void g(byte[] A_0, bf A_1, int A_2)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			this.g(A_0, 0, A_0.Length, A_1, A_2);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00023B9C File Offset: 0x00022B9C
		public void g(string A_0, bf A_1, int A_2)
		{
			if (this.c == null)
			{
				throw new InvalidOperationException();
			}
			this.g(this.c.GetBytes(A_0), A_1, A_2);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00023BC0 File Offset: 0x00022BC0
		public void o(int A_0)
		{
			this.l();
			this.j(A_0);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00023BCF File Offset: 0x00022BCF
		public void t()
		{
			this.l();
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00023BD7 File Offset: 0x00022BD7
		public bool o()
		{
			return this.j > 0;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00023BE2 File Offset: 0x00022BE2
		public byte[] r()
		{
			return this.i;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00023BEA File Offset: 0x00022BEA
		public int v()
		{
			return this.j;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00023BF2 File Offset: 0x00022BF2
		public virtual void cf()
		{
			this.f = 0;
			this.g = 0;
			this.j = 0;
			this.k = 0;
			this.l = new y();
			this.m = new z();
			this.h = 0;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00023C2D File Offset: 0x00022C2D
		public override e a6()
		{
			return global::a.e.d;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00023C30 File Offset: 0x00022C30
		public override bool a7()
		{
			return true;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00023C33 File Offset: 0x00022C33
		public override a1 a8()
		{
			return this.n;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00023C3B File Offset: 0x00022C3B
		public override void a9(a1 A_0)
		{
			this.n = A_0;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00023C44 File Offset: 0x00022C44
		public override bd ba()
		{
			return this.o;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00023C4C File Offset: 0x00022C4C
		public override void bb(bd A_0)
		{
			this.o = A_0;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00023C55 File Offset: 0x00022C55
		public ay k()
		{
			return this.p;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00023C5D File Offset: 0x00022C5D
		public void g(ay A_0)
		{
			this.p = A_0;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00023C66 File Offset: 0x00022C66
		public global::a.c aa()
		{
			return this.q;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00023C6E File Offset: 0x00022C6E
		public void g(global::a.c A_0)
		{
			this.q = A_0;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00023C77 File Offset: 0x00022C77
		public y y()
		{
			return this.l;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00023C7F File Offset: 0x00022C7F
		public z p()
		{
			return this.m;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00023C87 File Offset: 0x00022C87
		public at q()
		{
			return this.m.a(this.m.Count - 1);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00023CA1 File Offset: 0x00022CA1
		public int x()
		{
			return this.m.Count - 1;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00023CB0 File Offset: 0x00022CB0
		public af m()
		{
			int i = 0;
			while (i < this.m.Count)
			{
				af af = this.m.a(i).t();
				if (af != af.a && af != af.b)
				{
					if (af == af.c && i < this.m.Count - 1 && this.m.a(this.m.Count - 1).t() == af.d)
					{
						return af.e;
					}
					return this.m.a(i).t();
				}
				else
				{
					i++;
				}
			}
			return af.a;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00023D38 File Offset: 0x00022D38
		public void g(at A_0)
		{
			switch (A_0.t())
			{
			case af.a:
				return;
			case af.b:
				return;
			case af.c:
				throw this.ck(120, this.hs(), A_0);
			case af.d:
				throw new MailBeeAbortedByRemoteHostException(55, this.hs());
			default:
				return;
			}
		}

		// Token: 0x060007CB RID: 1995
		protected abstract MailBeeEmailProtocolNegativeResponseException ck(int A_0, ai A_1, at A_2);

		// Token: 0x060007CC RID: 1996 RVA: 0x00023D82 File Offset: 0x00022D82
		public void q(int A_0)
		{
			this.g(this.m.a(A_0));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00023D98 File Offset: 0x00022D98
		public void s()
		{
			for (int i = 0; i < this.m.Count; i++)
			{
				this.g(this.m.a(i));
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00023DCD File Offset: 0x00022DCD
		public a4 n()
		{
			return this.r;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00023DD5 File Offset: 0x00022DD5
		public void g(a4 A_0)
		{
			this.r = A_0;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00023DDE File Offset: 0x00022DDE
		public global::a.b w()
		{
			return this.s;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00023DE6 File Offset: 0x00022DE6
		public void g(global::a.b A_0)
		{
			this.s = A_0;
		}

		// Token: 0x060007D2 RID: 2002
		protected abstract Task<int> cl(int A_0, aq<int> A_1);

		// Token: 0x060007D3 RID: 2003 RVA: 0x00023DF0 File Offset: 0x00022DF0
		public Task g(byte[] A_0, int A_1, int A_2, bf A_3)
		{
			int num = this.g(A_0, A_1, A_2);
			A_3.g = num;
			A_3.h = A_2;
			this.l.a(A_3);
			if (!this.b)
			{
				return this.j();
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00023E39 File Offset: 0x00022E39
		public Task g(byte[] A_0, bf A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.g(A_0, 0, A_0.Length, A_1);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00023E50 File Offset: 0x00022E50
		public Task g(string A_0, bf A_1)
		{
			if (this.c == null)
			{
				throw new InvalidOperationException();
			}
			return this.g(this.c.GetBytes(A_0), A_1);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00023E74 File Offset: 0x00022E74
		protected Task j()
		{
			g.b b;
			b.c = this;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<g.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00023EB9 File Offset: 0x00022EB9
		public Task r(int A_0)
		{
			if (!this.b)
			{
				return this.k(A_0);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00023ED4 File Offset: 0x00022ED4
		protected Task k(int A_0)
		{
			g.c c;
			c.e = this;
			c.c = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<g.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00023F24 File Offset: 0x00022F24
		public Task h(byte[] A_0, int A_1, int A_2, bf A_3, int A_4)
		{
			g.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.g = A_3;
			a.h = A_4;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<g.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00023F93 File Offset: 0x00022F93
		public Task h(byte[] A_0, bf A_1, int A_2)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.h(A_0, 0, A_0.Length, A_1, A_2);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00023FAB File Offset: 0x00022FAB
		public Task h(string A_0, bf A_1, int A_2)
		{
			if (this.c == null)
			{
				throw new InvalidOperationException();
			}
			return this.h(this.c.GetBytes(A_0), A_1, A_2);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00023FD0 File Offset: 0x00022FD0
		public Task l(int A_0)
		{
			g.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<g.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002401D File Offset: 0x0002301D
		public Task z()
		{
			return this.j();
		}

		// Token: 0x0400051E RID: 1310
		public int a = 8192;

		// Token: 0x0400051F RID: 1311
		public bool b;

		// Token: 0x04000520 RID: 1312
		public Encoding c;

		// Token: 0x04000521 RID: 1313
		public Encoding d;

		// Token: 0x04000522 RID: 1314
		protected new byte[] e;

		// Token: 0x04000523 RID: 1315
		protected new int f;

		// Token: 0x04000524 RID: 1316
		protected new int g;

		// Token: 0x04000525 RID: 1317
		protected new int h;

		// Token: 0x04000526 RID: 1318
		protected new byte[] i;

		// Token: 0x04000527 RID: 1319
		protected new int j;

		// Token: 0x04000528 RID: 1320
		protected new int k;

		// Token: 0x04000529 RID: 1321
		protected new y l;

		// Token: 0x0400052A RID: 1322
		protected new z m;

		// Token: 0x0400052B RID: 1323
		private new a1 n;

		// Token: 0x0400052C RID: 1324
		private bd o;

		// Token: 0x0400052D RID: 1325
		protected ay p;

		// Token: 0x0400052E RID: 1326
		protected global::a.c q;

		// Token: 0x0400052F RID: 1327
		protected a4 r;

		// Token: 0x04000530 RID: 1328
		protected global::a.b s;
	}
}
