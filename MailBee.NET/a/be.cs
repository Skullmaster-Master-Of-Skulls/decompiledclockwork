using System;
using System.IO;
using System.Net.Sockets;
using MailBee;

namespace a
{
	// Token: 0x020003F6 RID: 1014
	internal abstract class be : bc
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x00097858 File Offset: 0x00096858
		public be(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.b = 0;
			this.c = null;
			this.d = null;
			this.e = null;
			this.f = null;
			if (this.b != null)
			{
				this.c = (be.a)Delegate.Combine(this.c, new be.a(this.d));
				this.d = (be.a)Delegate.Combine(this.d, new be.a(this.a));
				this.e = (be.a)Delegate.Combine(this.e, new be.a(this.c));
				this.f = (be.a)Delegate.Combine(this.f, new be.a(this.b));
			}
			if (A_0 != null)
			{
				this.a2();
				this.hb();
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00097935 File Offset: 0x00096935
		protected override void ff()
		{
			base.ff();
			this.a = new a3(this);
			this.a.e().hs().a(this.fl());
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00097964 File Offset: 0x00096964
		protected void a6()
		{
			a8 a2;
			for (a8 a = this.a.b(); a != null; a = a2)
			{
				a2 = a.e;
				if (!a.a7())
				{
					this.a.a(a);
				}
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0009799E File Offset: 0x0009699E
		protected void a2()
		{
			a8 a = this.a.b();
			a.h1((global::a.a)Delegate.Combine(a.h0(), new global::a.a(this.b.bn)));
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000979D1 File Offset: 0x000969D1
		public override void fx()
		{
			base.fx();
			this.a.e().k();
			this.a6();
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000979F0 File Offset: 0x000969F0
		protected virtual void hb()
		{
			bk a_ = (bk)this.b;
			a8 a = this.a.f();
			a.a9((a1)Delegate.Combine(a.a8(), new a1(a_.lm)));
			a8 a2 = this.a.f();
			a2.bb((bd)Delegate.Combine(a2.ba(), new bd(a_.ln)));
			a8 a3 = this.a.b();
			a3.a9((a1)Delegate.Combine(a3.a8(), new a1(a_.d)));
			a8 a4 = this.a.b();
			a4.bb((bd)Delegate.Combine(a4.ba(), new bd(a_.f)));
			a8 a5 = this.a.f();
			a5.h5((ak)Delegate.Combine(a5.h4(), new ak(a_.mw)));
			a8 a6 = this.a.f();
			a6.h7((bl)Delegate.Combine(a6.h6(), new bl(a_.mx)));
			a8 a7 = this.a.b();
			a7.h5((ak)Delegate.Combine(a7.h4(), new ak(a_.e)));
			a8 a8 = this.a.b();
			a8.h7((bl)Delegate.Combine(a8.h6(), new bl(a_.c)));
		}

		// Token: 0x060023F1 RID: 9201
		public abstract TopLevelProtocolType fl();

		// Token: 0x060023F2 RID: 9202 RVA: 0x00097B6D File Offset: 0x00096B6D
		public ai a1()
		{
			return this.a.e().hs();
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00097B7F File Offset: 0x00096B7F
		public a3 a5()
		{
			return this.a;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00097B87 File Offset: 0x00096B87
		public Socket a7()
		{
			return this.a.f().ht();
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00097B99 File Offset: 0x00096B99
		public Stream a3()
		{
			return this.a.b().d0();
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00097BAB File Offset: 0x00096BAB
		public int a4()
		{
			return this.b;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00097BB3 File Offset: 0x00096BB3
		public override void pa()
		{
			base.pa();
			this.b = 0;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00097BC2 File Offset: 0x00096BC2
		protected new void f(int A_0, int A_1)
		{
			this.pb(A_0);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00097BCB File Offset: 0x00096BCB
		protected override void pb(int A_0)
		{
			base.pb(A_0);
			this.b = 0;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00097BDC File Offset: 0x00096BDC
		protected override void pc(MailBeeException A_0)
		{
			base.pc(A_0);
			SocketException ex = A_0.InnerException as SocketException;
			if (ex == null)
			{
				this.b = 0;
				return;
			}
			this.b = ex.ErrorCode;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00097C13 File Offset: 0x00096C13
		public override void pd(int A_0)
		{
			base.pd(A_0);
			this.a.b().hz(A_0);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00097C2D File Offset: 0x00096C2D
		public new void b(byte[] A_0)
		{
			if (this.c != null)
			{
				base.a(this.c, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00097C54 File Offset: 0x00096C54
		public new void d(byte[] A_0, bc A_1)
		{
			av av = (av)this.b;
			if (this.b.bq() && av.b() && !this.b.bf())
			{
				DataTransferEventArgs a_ = new DataTransferEventArgs(A_0, ((be)A_1).a1(), A_1);
				av.c(a_);
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00097CA9 File Offset: 0x00096CA9
		public new void d(byte[] A_0)
		{
			if (this.d != null)
			{
				base.a(this.d, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00097CD0 File Offset: 0x00096CD0
		public new void a(byte[] A_0, bc A_1)
		{
			av av = (av)this.b;
			if (this.b.bq() && av.d() && !this.b.bf())
			{
				DataTransferEventArgs a_ = new DataTransferEventArgs(A_0, ((be)A_1).a1(), A_1);
				av.e(a_);
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00097D25 File Offset: 0x00096D25
		public new void c(byte[] A_0)
		{
			if (this.e != null)
			{
				base.a(this.e, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00097D4C File Offset: 0x00096D4C
		public new void c(byte[] A_0, bc A_1)
		{
			av av = (av)this.b;
			if (this.b.bq() && av.f() && !this.b.bf())
			{
				DataTransferEventArgs a_ = new DataTransferEventArgs(A_0, ((be)A_1).a1(), A_1);
				av.g(a_);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00097DA1 File Offset: 0x00096DA1
		public new void a(byte[] A_0)
		{
			if (this.f != null)
			{
				base.a(this.f, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00097DC8 File Offset: 0x00096DC8
		public new void b(byte[] A_0, bc A_1)
		{
			av av = (av)this.b;
			if (this.b.bq() && av.h() && !this.b.bf())
			{
				DataTransferEventArgs a_ = new DataTransferEventArgs(A_0, ((be)A_1).a1(), A_1);
				av.i(a_);
			}
		}

		// Token: 0x040017AD RID: 6061
		protected new a3 a;

		// Token: 0x040017AE RID: 6062
		protected new int b;

		// Token: 0x040017AF RID: 6063
		private new be.a c;

		// Token: 0x040017B0 RID: 6064
		private new be.a d;

		// Token: 0x040017B1 RID: 6065
		private new be.a e;

		// Token: 0x040017B2 RID: 6066
		private new be.a f;

		// Token: 0x020004C5 RID: 1221
		// (Invoke) Token: 0x0600298B RID: 10635
		protected new delegate void a(byte[] A_0, bc A_1);
	}
}
