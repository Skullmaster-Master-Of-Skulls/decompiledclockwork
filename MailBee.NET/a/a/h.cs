using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;
using MailBee.Mime;
using MailBee.Pop3Mail;

namespace a.a
{
	// Token: 0x020003CD RID: 973
	internal class h : global::a.k, global::a.a.b
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x0009109E File Offset: 0x0009009E
		public h(Pop3 A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000910AD File Offset: 0x000900AD
		protected override void f9()
		{
			this.p = new global::a.a.c(this, null, this.m, 0);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000910C4 File Offset: 0x000900C4
		internal override void kd(at A_0, bc A_1)
		{
			global::a.a.j j = (global::a.a.j)A_0;
			if (A_1.a8().Enabled)
			{
				if (j.b() && !j.c() && A_0.q().Length > Global.MaxMultiLineDataLength)
				{
					A_1.a8().b(base.a(A_0.q(), 0, A_0.q().Length), string.Format(Resources.Instance.Log_0BytesReceived, Convert.ToString(A_0.q().Length)), LogMessageType.Recv, A_1);
					return;
				}
				base.kd(A_0, A_1);
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0009114B File Offset: 0x0009014B
		private new void b(bool A_0, global::a.a.c A_1)
		{
			A_1.pa();
			A_1.au();
			A_1.f(A_0);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00091164 File Offset: 0x00090164
		private new void b(bool A_0)
		{
			global::a.a.c c = (global::a.a.c)this.p;
			if ((A_0 && c.r()) || (!A_0 && c.n()))
			{
				this.p.k(true);
				try
				{
					if (this.i && this.k)
					{
						this.b(A_0, c);
					}
					else
					{
						try
						{
							this.b(A_0, c);
						}
						catch (MailBeeException a_)
						{
							base.b(a_);
							if (this.i)
							{
								throw;
							}
						}
					}
				}
				finally
				{
					this.p.k(false);
				}
			}
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00091204 File Offset: 0x00090204
		public override StringDictionary ke()
		{
			this.b(false);
			return base.ke();
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00091213 File Offset: 0x00090213
		public override string kf(string A_0)
		{
			this.b(false);
			return base.kf(A_0);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00091223 File Offset: 0x00090223
		public override string kg(string A_0)
		{
			this.b(false);
			return base.kg(A_0);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00091233 File Offset: 0x00090233
		public override AuthenticationMethods kh()
		{
			this.b(true);
			return base.kh();
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00091242 File Offset: 0x00090242
		private new void b(int A_0)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			c.f(A_0);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00091268 File Offset: 0x00090268
		public new bool a(bool A_0, int A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1);
				}
				else
				{
					try
					{
						this.b(A_1);
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

		// Token: 0x060022F0 RID: 8944 RVA: 0x000912F0 File Offset: 0x000902F0
		public new IAsyncResult a(int A_0, AsyncCallback A_1, object A_2)
		{
			this.p.k(true);
			base.bl();
			global::a.a.h.b b = new global::a.a.h.b(this.a);
			this.q = new global::a.o(b, null);
			this.q.a(b.BeginInvoke(false, A_0, A_1, A_2));
			return this.q;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00091344 File Offset: 0x00090344
		public new bool p()
		{
			if (this.q == null || !(this.q.c() is global::a.a.h.b))
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
				result = ((global::a.a.h.b)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000913DC File Offset: 0x000903DC
		private new void d(int A_0, int A_1)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			c.d(A_0, A_1);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00091404 File Offset: 0x00090404
		public new bool b(bool A_0, int A_1, int A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.d(A_1, A_2);
				}
				else
				{
					try
					{
						this.d(A_1, A_2);
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

		// Token: 0x060022F4 RID: 8948 RVA: 0x0009148C File Offset: 0x0009048C
		public new IAsyncResult b(int A_0, int A_1, AsyncCallback A_2, object A_3)
		{
			this.p.k(true);
			base.bl();
			global::a.a.h.h h = new global::a.a.h.h(this.b);
			this.q = new global::a.o(h, null);
			this.q.a(h.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000914E4 File Offset: 0x000904E4
		public new bool q()
		{
			if (this.q == null || !(this.q.c() is global::a.a.h.h))
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
				result = ((global::a.a.h.h)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0009157C File Offset: 0x0009057C
		private new MailMessage c(int A_0, int A_1)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.c(A_0, A_1);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000915A4 File Offset: 0x000905A4
		public new MailMessage a(bool A_0, int A_1, int A_2)
		{
			MailMessage result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.c(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.c(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
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
			return result;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0009162C File Offset: 0x0009062C
		public new IAsyncResult a(int A_0, int A_1, AsyncCallback A_2, object A_3)
		{
			this.p.k(true);
			base.bl();
			global::a.a.h.d d = new global::a.a.h.d(this.a);
			this.q = new global::a.o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00091684 File Offset: 0x00090684
		public MailMessage ab()
		{
			if (this.q == null || !(this.q.c() is global::a.a.h.d))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			MailMessage result;
			try
			{
				base.bh();
				result = ((global::a.a.h.d)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0009171C File Offset: 0x0009071C
		private new MailMessageCollection b(int A_0, int A_1, int A_2)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.b(A_0, A_1, A_2);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00091744 File Offset: 0x00090744
		public new MailMessageCollection a(bool A_0, int A_1, int A_2, int A_3)
		{
			MailMessageCollection result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
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
			return result;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000917D0 File Offset: 0x000907D0
		public new IAsyncResult a(int A_0, int A_1, int A_2, AsyncCallback A_3, object A_4)
		{
			this.p.k(true);
			base.bl();
			global::a.a.h.e e = new global::a.a.h.e(this.a);
			this.q = new global::a.o(e, null);
			this.q.a(e.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4));
			return this.q;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00091828 File Offset: 0x00090828
		public MailMessageCollection w()
		{
			if (this.q == null || !(this.q.c() is global::a.a.h.e))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			MailMessageCollection result;
			try
			{
				base.bh();
				result = ((global::a.a.h.e)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000918C0 File Offset: 0x000908C0
		private new void b(string A_0, bool A_1)
		{
			this.p.pa();
			((ab)this.p).au();
			((ab)this.p).c(A_0, new global::a.a.a(true, A_1, false), true);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000918F8 File Offset: 0x000908F8
		public new bool a(bool A_0, string A_1, bool A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1, A_2);
				}
				else
				{
					try
					{
						this.b(A_1, A_2);
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

		// Token: 0x06002300 RID: 8960 RVA: 0x00091980 File Offset: 0x00090980
		public new IAsyncResult a(string A_0, bool A_1, AsyncCallback A_2, object A_3)
		{
			this.p.k(true);
			base.bl();
			global::a.a.h.j j = new global::a.a.h.j(this.a);
			this.q = new global::a.o(j, null);
			this.q.a(j.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000919D8 File Offset: 0x000909D8
		public bool s()
		{
			if (this.q == null || !(this.q.c() is global::a.a.h.j))
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
				result = ((global::a.a.h.j)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00091A70 File Offset: 0x00090A70
		private new void n()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			((global::a.a.c)this.p).j();
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00091AA0 File Offset: 0x00090AA0
		public new bool c(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.n();
				}
				else
				{
					try
					{
						this.n();
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

		// Token: 0x06002304 RID: 8964 RVA: 0x00091B24 File Offset: 0x00090B24
		private new void m()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			((global::a.a.c)this.p).ac();
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00091B54 File Offset: 0x00090B54
		public bool f(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.m();
				}
				else
				{
					try
					{
						this.m();
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

		// Token: 0x06002306 RID: 8966 RVA: 0x00091BD8 File Offset: 0x00090BD8
		private new int k()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			return ((global::a.a.c)this.p).v();
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00091C08 File Offset: 0x00090C08
		public new int d(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			int result;
			try
			{
				if (this.i && this.k)
				{
					result = this.k();
				}
				else
				{
					try
					{
						result = this.k();
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						result = -1;
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
			return result;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00091C8C File Offset: 0x00090C8C
		public new long o()
		{
			return ((global::a.a.c)this.p).p();
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00091C9E File Offset: 0x00090C9E
		public int r()
		{
			return ((global::a.a.c)this.p).x();
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x00091CB0 File Offset: 0x00090CB0
		private new void b(bool A_0, bool A_1, global::a.a.c A_2)
		{
			A_2.pa();
			A_2.aw();
			if (A_0)
			{
				A_2.z();
			}
			if (A_1)
			{
				A_2.h();
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00091CD0 File Offset: 0x00090CD0
		private new void b(bool A_0, bool A_1)
		{
			global::a.a.c c = (global::a.a.c)this.p;
			if ((A_0 && c.y() == null) || (A_1 && c.m() == null))
			{
				this.p.k(true);
				try
				{
					if (this.i && this.k)
					{
						this.b(A_0, A_1, c);
					}
					else
					{
						try
						{
							this.b(A_0, A_1, c);
						}
						catch (MailBeeException a_)
						{
							base.b(a_);
							if (this.i)
							{
								throw;
							}
						}
					}
				}
				finally
				{
					this.p.k(false);
				}
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00091D70 File Offset: 0x00090D70
		public new int e(int A_0)
		{
			this.b(true, false);
			return ((global::a.a.c)this.p).h(A_0);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00091D8B File Offset: 0x00090D8B
		public int[] ad()
		{
			this.b(true, false);
			return (int[])((global::a.a.c)this.p).y().Clone();
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00091DAF File Offset: 0x00090DAF
		public new int a(string A_0)
		{
			this.b(false, true);
			return ((global::a.a.c)this.p).a(A_0);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00091DCA File Offset: 0x00090DCA
		public new string d(int A_0)
		{
			this.b(false, true);
			return ((global::a.a.c)this.p).g(A_0);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00091DE5 File Offset: 0x00090DE5
		public string[] t()
		{
			this.b(false, true);
			return (string[])((global::a.a.c)this.p).m().Clone();
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00091E09 File Offset: 0x00090E09
		public Pop3InboxPreloadOptions ag()
		{
			return ((global::a.a.c)this.p).ab();
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00091E1B File Offset: 0x00090E1B
		public new void a(Pop3InboxPreloadOptions A_0)
		{
			((global::a.a.c)this.p).a(A_0);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00091E2E File Offset: 0x00090E2E
		public bool u()
		{
			return ((global::a.a.c)this.p).o();
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00091E40 File Offset: 0x00090E40
		public new void e(bool A_0)
		{
			if (this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			((global::a.a.c)this.p).c(A_0);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00091E67 File Offset: 0x00090E67
		public MailMessageCollection v()
		{
			return ((global::a.a.c)this.p).q();
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00091E79 File Offset: 0x00090E79
		public override bool j()
		{
			return this.a != null && this.a.m();
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00091E90 File Offset: 0x00090E90
		public override void k(ErrorEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnErrorOccurred(A_0);
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00091EA6 File Offset: 0x00090EA6
		public override bool l()
		{
			return this.a != null && this.a.o();
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00091EBD File Offset: 0x00090EBD
		public override void m(LogNewEntryEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLogNewEntry(A_0);
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00091ED3 File Offset: 0x00090ED3
		public override bool b()
		{
			return this.a != null && this.a.a();
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00091EEA File Offset: 0x00090EEA
		public override void c(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataReceived(A_0);
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00091F00 File Offset: 0x00090F00
		public override bool d()
		{
			return this.a != null && this.a.e();
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00091F17 File Offset: 0x00090F17
		public override void e(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataSent(A_0);
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00091F2D File Offset: 0x00090F2D
		public override bool f()
		{
			return this.a != null && this.a.g();
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00091F44 File Offset: 0x00090F44
		public override void g(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataReceived(A_0);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00091F5A File Offset: 0x00090F5A
		public override bool h()
		{
			return this.a != null && this.a.b();
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00091F71 File Offset: 0x00090F71
		public override void i(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataSent(A_0);
			}
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00091F87 File Offset: 0x00090F87
		public override bool bx()
		{
			return this.a != null && this.a.d();
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00091F9E File Offset: 0x00090F9E
		public override void by(HostResolvedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnHostResolved(A_0);
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00091FB4 File Offset: 0x00090FB4
		public override bool bz()
		{
			return this.a != null && this.a.k();
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00091FCB File Offset: 0x00090FCB
		public override void b0(SocketCreatingEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketCreating(A_0);
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00091FE1 File Offset: 0x00090FE1
		public override bool b1()
		{
			return this.a != null && this.a.h();
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00091FF8 File Offset: 0x00090FF8
		public override void b2(SocketConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketConnected(A_0);
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0009200E File Offset: 0x0009100E
		public override bool b3()
		{
			return this.a != null && this.a.c();
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00092025 File Offset: 0x00091025
		public override void b4(ConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnConnected(A_0);
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0009203B File Offset: 0x0009103B
		public override bool b5()
		{
			return this.a != null && this.a.j();
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00092052 File Offset: 0x00091052
		public override void b6(DisconnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDisconnected(A_0);
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00092068 File Offset: 0x00091068
		public override bool b7()
		{
			return this.a != null && this.a.l();
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0009207F File Offset: 0x0009107F
		public override void b8(TlsStartedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnTlsStarted(A_0);
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00092095 File Offset: 0x00091095
		public override bool b9()
		{
			return this.a != null && this.a.f();
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000920AC File Offset: 0x000910AC
		public override void ca(LoggedInEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLoggedIn(A_0);
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000920C2 File Offset: 0x000910C2
		public bool ki()
		{
			return this.a != null && this.a.i();
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000920D9 File Offset: 0x000910D9
		public void kj(Pop3MessageDownloadedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageDownloaded(A_0);
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000920EF File Offset: 0x000910EF
		public bool kk()
		{
			return this.a != null && this.a.n();
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00092106 File Offset: 0x00091106
		public void kl(Pop3MessageDataChunkReceivedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageDataChunkReceived(A_0);
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x0009211C File Offset: 0x0009111C
		internal override Task km(at A_0, bc A_1)
		{
			global::a.a.h.k k;
			k.e = this;
			k.c = A_0;
			k.d = A_1;
			k.b = AsyncTaskMethodBuilder.Create();
			k.a = -1;
			AsyncTaskMethodBuilder b = k.b;
			b.Start<global::a.a.h.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00092171 File Offset: 0x00091171
		private new Task a(bool A_0, global::a.a.c A_1)
		{
			A_1.pa();
			A_1.au();
			return A_1.e(A_0);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00092188 File Offset: 0x00091188
		private new Task a(bool A_0)
		{
			global::a.a.h.n n;
			n.c = this;
			n.d = A_0;
			n.b = AsyncTaskMethodBuilder.Create();
			n.a = -1;
			AsyncTaskMethodBuilder b = n.b;
			b.Start<global::a.a.h.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000921D8 File Offset: 0x000911D8
		public Task<StringDictionary> y()
		{
			global::a.a.h.m m;
			m.c = this;
			m.b = AsyncTaskMethodBuilder<StringDictionary>.Create();
			m.a = -1;
			AsyncTaskMethodBuilder<StringDictionary> b = m.b;
			b.Start<global::a.a.h.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00092220 File Offset: 0x00091220
		public Task<AuthenticationMethods> z()
		{
			global::a.a.h.p p;
			p.c = this;
			p.b = AsyncTaskMethodBuilder<AuthenticationMethods>.Create();
			p.a = -1;
			AsyncTaskMethodBuilder<AuthenticationMethods> b = p.b;
			b.Start<global::a.a.h.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00092265 File Offset: 0x00091265
		private new Task a(int A_0)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.d(A_0);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0009228C File Offset: 0x0009128C
		public new Task<bool> c(int A_0)
		{
			global::a.a.h.i i;
			i.c = this;
			i.d = A_0;
			i.b = AsyncTaskMethodBuilder<bool>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<bool> b = i.b;
			b.Start<global::a.a.h.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000922D9 File Offset: 0x000912D9
		private new Task b(int A_0, int A_1)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.b(A_0, A_1);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00092300 File Offset: 0x00091300
		public Task<bool> f(int A_0, int A_1)
		{
			global::a.a.h.r r;
			r.c = this;
			r.d = A_0;
			r.e = A_1;
			r.b = AsyncTaskMethodBuilder<bool>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<bool> b = r.b;
			b.Start<global::a.a.h.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00092355 File Offset: 0x00091355
		private new Task<MailMessage> a(int A_0, int A_1)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.e(A_0, A_1);
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0009237C File Offset: 0x0009137C
		public new Task<MailMessage> e(int A_0, int A_1)
		{
			global::a.a.h.g g;
			g.c = this;
			g.d = A_0;
			g.e = A_1;
			g.b = AsyncTaskMethodBuilder<MailMessage>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<MailMessage> b = g.b;
			b.Start<global::a.a.h.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000923D1 File Offset: 0x000913D1
		private new Task<MailMessageCollection> a(int A_0, int A_1, int A_2)
		{
			this.p.pa();
			global::a.a.c c = (global::a.a.c)this.p;
			c.aw();
			return c.a(A_0, A_1, A_2);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000923F8 File Offset: 0x000913F8
		public new Task<MailMessageCollection> c(int A_0, int A_1, int A_2)
		{
			global::a.a.h.q q;
			q.c = this;
			q.d = A_0;
			q.e = A_1;
			q.f = A_2;
			q.b = AsyncTaskMethodBuilder<MailMessageCollection>.Create();
			q.a = -1;
			AsyncTaskMethodBuilder<MailMessageCollection> b = q.b;
			b.Start<global::a.a.h.q>(ref q);
			return q.b.Task;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00092455 File Offset: 0x00091455
		private new Task a(string A_0, bool A_1)
		{
			this.p.pa();
			((ab)this.p).au();
			return ((ab)this.p).d(A_0, new global::a.a.a(true, A_1, false), true);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0009248C File Offset: 0x0009148C
		public new Task<bool> c(string A_0, bool A_1)
		{
			global::a.a.h.c c;
			c.c = this;
			c.d = A_0;
			c.e = A_1;
			c.b = AsyncTaskMethodBuilder<bool>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<bool> b = c.b;
			b.Start<global::a.a.h.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000924E1 File Offset: 0x000914E1
		private new Task i()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			return ((global::a.a.c)this.p).w();
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00092510 File Offset: 0x00091510
		public Task<bool> ac()
		{
			global::a.a.h.l l;
			l.c = this;
			l.b = AsyncTaskMethodBuilder<bool>.Create();
			l.a = -1;
			AsyncTaskMethodBuilder<bool> b = l.b;
			b.Start<global::a.a.h.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00092555 File Offset: 0x00091555
		private Task g()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			return ((global::a.a.c)this.p).t();
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00092584 File Offset: 0x00091584
		public Task<bool> x()
		{
			global::a.a.h.a a;
			a.c = this;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<global::a.a.h.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000925C9 File Offset: 0x000915C9
		private new Task<int> e()
		{
			this.p.pa();
			((global::a.a.c)this.p).au();
			return ((global::a.a.c)this.p).aa();
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000925F8 File Offset: 0x000915F8
		public Task<int> aa()
		{
			global::a.a.h.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder<int>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<int> b = f.b;
			b.Start<global::a.a.h.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0009263D File Offset: 0x0009163D
		private new Task a(bool A_0, bool A_1, global::a.a.c A_2)
		{
			A_2.pa();
			A_2.aw();
			if (A_0)
			{
				return A_2.l();
			}
			if (A_1)
			{
				return A_2.s();
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00092668 File Offset: 0x00091668
		private new Task a(bool A_0, bool A_1)
		{
			global::a.a.h.o o;
			o.c = this;
			o.d = A_0;
			o.e = A_1;
			o.b = AsyncTaskMethodBuilder.Create();
			o.a = -1;
			AsyncTaskMethodBuilder b = o.b;
			b.Start<global::a.a.h.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000926C0 File Offset: 0x000916C0
		public Task<int[]> af()
		{
			global::a.a.h.t t;
			t.c = this;
			t.b = AsyncTaskMethodBuilder<int[]>.Create();
			t.a = -1;
			AsyncTaskMethodBuilder<int[]> b = t.b;
			b.Start<global::a.a.h.t>(ref t);
			return t.b.Task;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00092708 File Offset: 0x00091708
		public Task<string[]> ae()
		{
			global::a.a.h.s s;
			s.c = this;
			s.b = AsyncTaskMethodBuilder<string[]>.Create();
			s.a = -1;
			AsyncTaskMethodBuilder<string[]> b = s.b;
			b.Start<global::a.a.h.s>(ref s);
			return s.b.Task;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0009274D File Offset: 0x0009174D
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task a(at A_0, bc A_1)
		{
			return base.km(A_0, A_1);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x00092757 File Offset: 0x00091757
		[CompilerGenerated]
		[DebuggerHidden]
		private new StringDictionary c()
		{
			return base.ke();
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0009275F File Offset: 0x0009175F
		[CompilerGenerated]
		[DebuggerHidden]
		private new AuthenticationMethods a()
		{
			return base.kh();
		}

		// Token: 0x04001708 RID: 5896
		private new Pop3 a;

		// Token: 0x020003CF RID: 975
		// (Invoke) Token: 0x06002355 RID: 9045
		protected new delegate bool j(bool A_0, string A_1, bool A_2);

		// Token: 0x020003D0 RID: 976
		// (Invoke) Token: 0x06002359 RID: 9049
		protected new delegate bool b(bool A_0, int A_1);

		// Token: 0x020003D1 RID: 977
		// (Invoke) Token: 0x0600235D RID: 9053
		protected new delegate bool h(bool A_0, int A_1, int A_2);

		// Token: 0x020003D2 RID: 978
		// (Invoke) Token: 0x06002361 RID: 9057
		protected new delegate MailMessage d(bool A_0, int A_1, int A_2);

		// Token: 0x020003D3 RID: 979
		// (Invoke) Token: 0x06002365 RID: 9061
		protected new delegate MailMessageCollection e(bool A_0, int A_1, int A_2, int A_3);
	}
}
