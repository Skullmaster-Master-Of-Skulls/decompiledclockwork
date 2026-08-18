using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x02000453 RID: 1107
	internal abstract class h : bk, a9
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x000B1B92 File Offset: 0x000B0B92
		public h()
		{
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000B1B9C File Offset: 0x000B0B9C
		public new virtual void a(ac A_0, bc A_1)
		{
			if (this.e && ((a9)this).bz() && !this.c)
			{
				((ab)A_1).a(A_0);
			}
		}

		// Token: 0x060026D4 RID: 9940
		public abstract bool bx();

		// Token: 0x060026D5 RID: 9941
		public abstract void by(HostResolvedEventArgs A_0);

		// Token: 0x060026D6 RID: 9942
		public abstract bool bz();

		// Token: 0x060026D7 RID: 9943
		public abstract void b0(SocketCreatingEventArgs A_0);

		// Token: 0x060026D8 RID: 9944
		public abstract bool b1();

		// Token: 0x060026D9 RID: 9945
		public abstract void b2(SocketConnectedEventArgs A_0);

		// Token: 0x060026DA RID: 9946
		public abstract bool b3();

		// Token: 0x060026DB RID: 9947
		public abstract void b4(ConnectedEventArgs A_0);

		// Token: 0x060026DC RID: 9948
		public abstract bool b5();

		// Token: 0x060026DD RID: 9949
		public abstract void b6(DisconnectedEventArgs A_0);

		// Token: 0x060026DE RID: 9950
		public abstract bool b7();

		// Token: 0x060026DF RID: 9951
		public abstract void b8(TlsStartedEventArgs A_0);

		// Token: 0x060026E0 RID: 9952
		public abstract bool b9();

		// Token: 0x060026E1 RID: 9953
		public abstract void ca(LoggedInEventArgs A_0);

		// Token: 0x060026E2 RID: 9954 RVA: 0x000B1BD0 File Offset: 0x000B0BD0
		public override void cb()
		{
			base.cb();
			ab ab = (ab)this.p;
			if (ab.ao())
			{
				ab.fz(false);
			}
			ab.j(false);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000B1C08 File Offset: 0x000B0C08
		protected bool a3()
		{
			if (this.q == null || !(this.q.c() is h.d))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			bool result;
			try
			{
				base.bh();
				result = ((h.d)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000B1CA0 File Offset: 0x000B0CA0
		protected void a5()
		{
			this.p.pa();
			((ab)this.p).ay();
			((ab)this.p).fy();
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000B1CCD File Offset: 0x000B0CCD
		private new void c()
		{
			this.p.pa();
			((ab)this.p).aq();
			((ab)this.p).fz(true);
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000B1CFC File Offset: 0x000B0CFC
		public virtual bool lo(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.c();
				}
				else
				{
					try
					{
						this.c();
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000B1D80 File Offset: 0x000B0D80
		public virtual IAsyncResult lp(AsyncCallback A_0, object A_1)
		{
			this.p.k(true);
			base.bl();
			h.d d = new h.d(this.lo);
			this.q = new o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000B1DD4 File Offset: 0x000B0DD4
		public bool az()
		{
			return this.a3();
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000B1DDC File Offset: 0x000B0DDC
		protected virtual void a4()
		{
			this.p.pa();
			((ab)this.p).fu();
			((ab)this.p).fp(true);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000B1E0C File Offset: 0x000B0E0C
		public virtual bool lr(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a4();
				}
				else
				{
					try
					{
						this.a4();
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000B1E90 File Offset: 0x000B0E90
		public new virtual IAsyncResult e(AsyncCallback A_0, object A_1)
		{
			this.p.k(true);
			base.bl();
			h.d d = new h.d(this.lr);
			this.q = new o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000B1EE4 File Offset: 0x000B0EE4
		public bool a8()
		{
			return this.a3();
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000B1EEC File Offset: 0x000B0EEC
		protected void a7()
		{
			this.p.pa();
			((ab)this.p).fv();
			((ab)this.p).fo();
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000B1F19 File Offset: 0x000B0F19
		protected virtual void a9()
		{
			this.p.pa();
			((ab)this.p).au();
			((ab)this.p).o1("NOOP", false);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000B1F50 File Offset: 0x000B0F50
		public virtual bool lq(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a9();
				}
				else
				{
					try
					{
						this.a9();
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000B1FD4 File Offset: 0x000B0FD4
		protected new string a(byte[] A_0, int A_1, int A_2)
		{
			return this.bm().GetString(A_0, A_1, Global.MaxMultiLineDataLength / 2) + " ... " + this.bm().GetString(A_0, A_1 + A_2 - Global.MaxMultiLineDataLength / 2, Global.MaxMultiLineDataLength / 2);
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000B2014 File Offset: 0x000B1014
		protected new string a(bf A_0, byte[] A_1)
		{
			if (!A_0.l && !A_0.m)
			{
				return string.Empty;
			}
			return (A_0.l ? (this.bk().GetString(A_1, A_0.g, Global.MaxMultiLineDataLength / 2) + " ") : string.Empty) + "..." + (A_0.m ? (" " + this.bk().GetString(A_1, A_0.g + A_0.h - Global.MaxMultiLineDataLength / 2, Global.MaxMultiLineDataLength / 2)) : string.Empty);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000B20B4 File Offset: 0x000B10B4
		internal virtual void kd(at A_0, bc A_1)
		{
			if (A_1.a8().Enabled)
			{
				A_1.a8().b(this.bm().GetString(A_0.q(), 0, A_0.q().Length), null, LogMessageType.Recv, A_1);
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000B20EC File Offset: 0x000B10EC
		internal virtual void ll(bf A_0, byte[] A_1, bc A_2)
		{
			if (A_2.a8().Enabled)
			{
				if (A_0.j && A_2.a8().HidePasswords)
				{
					A_2.a8().b(this.bk().GetString(A_0.k, 0, A_0.k.Length), null, LogMessageType.Send, A_2);
					return;
				}
				A_2.a8().b(this.bk().GetString(A_1, A_0.g, A_0.h), null, LogMessageType.Send, A_2);
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000B216A File Offset: 0x000B116A
		public virtual bool lx()
		{
			return ((ab)this.p).ao();
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000B217C File Offset: 0x000B117C
		public virtual bool ly()
		{
			return ((ab)this.p).ah();
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000B218E File Offset: 0x000B118E
		public virtual bool lz()
		{
			return ((ab)this.p).ar();
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000B21A0 File Offset: 0x000B11A0
		public virtual StringDictionary ke()
		{
			return ((ab)this.p).ax();
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000B21B2 File Offset: 0x000B11B2
		public virtual string kf(string A_0)
		{
			return ((ab)this.p).t(A_0);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000B21C5 File Offset: 0x000B11C5
		public virtual string kg(string A_0)
		{
			return ((ab)this.p).s(A_0);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000B21D8 File Offset: 0x000B11D8
		public virtual AuthenticationMethods kh()
		{
			return ((ab)this.p).ap();
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000B21EA File Offset: 0x000B11EA
		public virtual string l0()
		{
			return ((ab)this.p).an();
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000B21FC File Offset: 0x000B11FC
		internal virtual Task km(at A_0, bc A_1)
		{
			h.e e;
			e.d = this;
			e.e = A_0;
			e.c = A_1;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder b = e.b;
			b.Start<h.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000B2254 File Offset: 0x000B1254
		internal virtual Task mv(bf A_0, byte[] A_1, bc A_2)
		{
			h.b b;
			b.e = this;
			b.d = A_0;
			b.f = A_1;
			b.c = A_2;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<h.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000B22B1 File Offset: 0x000B12B1
		protected Task a2()
		{
			this.p.pa();
			((ab)this.p).ay();
			return ((ab)this.p).f2();
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000B22DE File Offset: 0x000B12DE
		private new Task a()
		{
			this.p.pa();
			((ab)this.p).aq();
			return ((ab)this.p).f3(true);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000B230C File Offset: 0x000B130C
		public virtual Task<bool> my()
		{
			h.a a;
			a.c = this;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<h.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000B2351 File Offset: 0x000B1351
		protected virtual Task a1()
		{
			this.p.pa();
			((ab)this.p).fu();
			return ((ab)this.p).fs(true);
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000B2380 File Offset: 0x000B1380
		public virtual Task<bool> m0()
		{
			h.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder<bool>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<bool> b = f.b;
			b.Start<h.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000B23C5 File Offset: 0x000B13C5
		protected Task a6()
		{
			this.p.pa();
			((ab)this.p).fv();
			return ((ab)this.p).fr();
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000B23F2 File Offset: 0x000B13F2
		protected virtual Task a0()
		{
			this.p.pa();
			((ab)this.p).au();
			return ((ab)this.p).o4("NOOP", false);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000B2428 File Offset: 0x000B1428
		public virtual Task<bool> mz()
		{
			h.c c;
			c.c = this;
			c.b = AsyncTaskMethodBuilder<bool>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<bool> b = c.b;
			b.Start<h.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x020004E3 RID: 1251
		// (Invoke) Token: 0x06002A09 RID: 10761
		protected new delegate bool d(bool A_0);
	}
}
