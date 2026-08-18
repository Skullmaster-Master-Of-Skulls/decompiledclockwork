using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x020003F5 RID: 1013
	internal class p : be
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x00096B48 File Offset: 0x00095B48
		public p(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.a = 1;
			this.b = null;
			this.e = null;
			this.c = null;
			this.d = h.a;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00096B78 File Offset: 0x00095B78
		public override string er()
		{
			return "DNSQ";
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00096B7F File Offset: 0x00095B7F
		public override TopLevelProtocolType fl()
		{
			return TopLevelProtocolType.Dns;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00096B82 File Offset: 0x00095B82
		protected override void fw(MailBeeException A_0)
		{
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00096B84 File Offset: 0x00095B84
		private new void a(string A_0, byte[] A_1, short A_2, bool A_3)
		{
			try
			{
				this.b = global::a.g.k.a(A_1, this.l, A_2, A_3);
			}
			catch (global::a.g.a a)
			{
				throw new MailBeeInvalidBinaryResponseException(a.ErrorCode, a, base.a1(), a.a());
			}
			catch (t t)
			{
				throw new MailBeeDnsQueryMismatchException(t.ErrorCode, base.a1(), A_0, t.a(), t.b());
			}
			catch (global::a.g.g g)
			{
				throw new MailBeeDnsNameErrorException(g.ErrorCode, base.a1(), A_0, g.c(), g.b(), g.a());
			}
			catch (i i)
			{
				throw new MailBeeDnsProtocolNegativeResponseException(i.ErrorCode, base.a1(), A_0, i.c(), i.b(), i.a());
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00096C68 File Offset: 0x00095C68
		private new void b(string A_0)
		{
			if (this.d.Enabled)
			{
				global::a.g.b b = this.b.e();
				string str;
				if (b != global::a.g.b.b)
				{
					if (b != global::a.g.b.c)
					{
						str = Resources.Instance.Log_DnsRecursionStatusUnknown;
					}
					else
					{
						str = Resources.Instance.Log_DnsRecursionIsNotSupported;
					}
				}
				else
				{
					str = Resources.Instance.Log_DnsRecursionIsSupported;
				}
				this.d.b(string.Format(Resources.Instance.Log_Dns0RecordsFoundForHost1, this.b.Count, A_0) + " " + str, null, LogMessageType.Info, this);
				for (int i = 0; i < this.b.Count; i++)
				{
					m m = this.b.b(i);
					if (m == null)
					{
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfUnknownType, new object[0]), null, LogMessageType.Info, this);
					}
					else if (m is r)
					{
						r r = (r)m;
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfATypeHasIP0, r.a()), null, LogMessageType.Info, this);
					}
					else if (m is n)
					{
						n n = (n)m;
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfCNameTypeIsAliasFor0, n.a()), null, LogMessageType.Info, this);
					}
					else if (m is q)
					{
						q q = (q)m;
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfMXTypeHasSmtpHost0OfPreference1, q.a(), Convert.ToString(q.get_Priority())), null, LogMessageType.Info, this);
					}
					else if (m is l)
					{
						l l = (l)m;
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfTxtTypeHas0Strings, l.a().Length), null, LogMessageType.Info, this);
					}
					else if (m is global::a.g.c)
					{
						global::a.g.c c = (global::a.g.c)m;
						this.d.b(string.Format(Resources.Instance.Log_DnsRecordOfPtrTypeDenotesDomain0, c.a()), null, LogMessageType.Info, this);
					}
				}
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00096E74 File Offset: 0x00095E74
		private new void d(string A_0, h A_1, bool A_2)
		{
			short num = this.a;
			if (this.a < 32767)
			{
				this.a += 1;
			}
			else
			{
				this.a = 1;
			}
			this.d.b(string.Format(Resources.Instance.Log_DnsCreatingQueryAboutHost0, A_0), null, LogMessageType.Info, this);
			int a_2;
			byte[] a_ = global::a.g.k.a(num, A_0, this.k, A_1, 1, out a_2);
			IPEndPoint ipendPoint = new IPEndPoint(this.e.IP, Global.DnsPort);
			base.a1().a(this.e.Host);
			this.a.e().ho(ipendPoint);
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_DnsSendingQueryToEndPoint0, ipendPoint.ToString()), null, LogMessageType.Info, this);
				this.a.e().hp(a_, 0, a_2);
				byte[] array = new byte[512];
				this.a.e().n(array);
				this.d.b(string.Format(Resources.Instance.Log_DnsParsingReceivedResponse, new object[0]), null, LogMessageType.Info, this);
				this.a(A_0, array, num, A_2);
				this.b(A_0);
			}
			finally
			{
				this.a.e().hr();
			}
			this.d.b(string.Format(Resources.Instance.Log_DnsQueryDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00096FEC File Offset: 0x00095FEC
		private new void c(string A_0, h A_1, bool A_2)
		{
			short num = this.a;
			if (this.a < 32767)
			{
				this.a += 1;
			}
			else
			{
				this.a = 1;
			}
			this.d.b(string.Format(Resources.Instance.Log_DnsCreatingQueryAboutHost0, A_0), null, LogMessageType.Info, this);
			int num2;
			byte[] a_ = global::a.g.k.a(num, A_0, this.k, A_1, 1, out num2);
			IPEndPoint ipendPoint = new IPEndPoint(this.e.IP, Global.DnsPort);
			base.a1().a(this.e.Host);
			this.a.e().hz(this.a9());
			this.a.e().d1(ipendPoint);
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_DnsSendingQueryToEndPoint0, ipendPoint.ToString()), null, LogMessageType.Info, this);
				byte[] array = new byte[]
				{
					(byte)(num2 >> 8 & 255),
					(byte)(num2 & 255)
				};
				this.a.e().d4(array, 0, 2);
				this.a.e().d4(a_, 0, num2);
				this.a.e().j(array);
				int num3 = (int)array[0] << 8 | (int)array[1];
				byte[] array2 = new byte[num3];
				if (num3 > 0)
				{
					this.a.e().j(array2);
				}
				this.d.b(string.Format(Resources.Instance.Log_DnsParsingReceivedResponse, new object[0]), null, LogMessageType.Info, this);
				this.a(A_0, array2, num, A_2);
				this.b(A_0);
			}
			finally
			{
				this.a.e().d2();
			}
			this.d.b(string.Format(Resources.Instance.Log_DnsQueryDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000971D0 File Offset: 0x000961D0
		public virtual void o5(string A_0, h A_1, bool A_2, bool A_3)
		{
			this.b = null;
			this.c = A_0;
			this.d = A_1;
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(403);
			}
			if (this.e.b())
			{
				throw new MailBeeDnsServerDisabledException(213, this.e);
			}
			if (this.e.UdpRetryCount > 0)
			{
				this.pd(this.e.UdpTimeout);
				int num = 1;
				for (;;)
				{
					try
					{
						string text = null;
						do
						{
							this.d(A_0, A_1, A_3);
							this.e.Reset();
							if (A_2)
							{
								text = this.a(A_1);
								if (text != null)
								{
									A_0 = text;
								}
							}
						}
						while (text != null);
						if ((this.b != null && this.b.Count > 0) || !this.e.TryTcp)
						{
							return;
						}
						break;
					}
					catch (MailBeeDnsProtocolNegativeResponseException ex)
					{
						if (ex.ResponseCode != DnsReplyCode.ServerFailure || !this.e.TryTcp)
						{
							throw;
						}
						break;
					}
					catch (MailBeeNetworkException a_)
					{
						if (num >= this.e.UdpRetryCount)
						{
							this.e.a();
							throw;
						}
						base.c(a_);
					}
					num++;
				}
			}
			this.pd(this.e.TcpTimeout);
			try
			{
				string text2 = null;
				do
				{
					this.c(A_0, A_1, A_3);
					this.e.Reset();
					if (A_2)
					{
						text2 = this.a(A_1);
						if (text2 != null)
						{
							A_0 = text2;
						}
					}
				}
				while (text2 != null);
			}
			catch (MailBeeDnsProtocolNegativeResponseException)
			{
				throw;
			}
			catch (MailBeeNetworkException)
			{
				this.e.a();
				throw;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00097370 File Offset: 0x00096370
		public new IAsyncResult a(string A_0, h A_1, bool A_2, bool A_3, AsyncCallback A_4, object A_5)
		{
			base.k(true);
			p.f f = new p.f(this.o5);
			this.g = new o(f, null);
			this.g.a(f.BeginInvoke(A_0, A_1, A_2, A_3, A_4, A_5));
			return this.g;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000973C0 File Offset: 0x000963C0
		public new void d()
		{
			if (this.g == null || !(this.g.c() is p.f))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.g.d() == null)
			{
				Thread.Sleep(0);
			}
			try
			{
				((p.f)this.g.c()).EndInvoke(this.g.d());
			}
			finally
			{
				this.g = null;
				base.k(false);
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00097444 File Offset: 0x00096444
		private new string a(h A_0)
		{
			string text = null;
			foreach (object obj in this.b)
			{
				m m = (m)obj;
				if (m is n)
				{
					if (text == null)
					{
						text = ((n)m).a();
					}
				}
				else if (m != null && m.a5() == A_0)
				{
					return null;
				}
			}
			return text;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000974C8 File Offset: 0x000964C8
		public new void c(string A_0)
		{
			if (this.b != null && this.b.Count == 0)
			{
				if (this.b.e() == global::a.g.b.c)
				{
					this.o5(A_0, h.a, true, true);
					if (this.b.Count <= 0)
					{
						this.b = null;
						throw new MailBeeDnsLackOfRecursionException(211, base.a1(), A_0);
					}
					this.b.Clear();
				}
				this.b.a(new q(0, A_0));
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00097548 File Offset: 0x00096548
		public new global::a.g.f e()
		{
			return this.b;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00097550 File Offset: 0x00096550
		public new void a(global::a.g.f A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0009755C File Offset: 0x0009655C
		public new void d(string A_0)
		{
			if (this.b == null)
			{
				throw new InvalidOperationException();
			}
			int num = 999;
			for (int i = this.b.Count - 1; i > -1; i--)
			{
				q q = this.b.b(i) as q;
				if (q == null)
				{
					this.b.a(i);
				}
				else if (q.a() == A_0 && num > q.get_Priority())
				{
					num = q.get_Priority();
				}
			}
			if (num < 999)
			{
				for (int j = this.b.Count - 1; j > -1; j--)
				{
					q q2 = (q)this.b.b(j);
					if (q2.get_Priority() >= num && q2.a() != A_0)
					{
						this.b.a(j);
					}
				}
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0009762E File Offset: 0x0009662E
		public new DnsServer g()
		{
			return this.e;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00097636 File Offset: 0x00096636
		public new void a(DnsServer A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0009763F File Offset: 0x0009663F
		public new string c()
		{
			return this.c;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00097647 File Offset: 0x00096647
		public new h f()
		{
			return this.d;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00097650 File Offset: 0x00096650
		protected override Task f1(MailBeeException A_0)
		{
			p.d d;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<p.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00097690 File Offset: 0x00096690
		private new Task a(string A_0)
		{
			p.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<p.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000976E0 File Offset: 0x000966E0
		private new Task b(string A_0, h A_1, bool A_2)
		{
			p.b b;
			b.c = this;
			b.d = A_0;
			b.f = A_1;
			b.k = A_2;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<p.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00097740 File Offset: 0x00096740
		private new Task a(string A_0, h A_1, bool A_2)
		{
			p.g g;
			g.c = this;
			g.d = A_0;
			g.f = A_1;
			g.l = A_2;
			g.b = AsyncTaskMethodBuilder.Create();
			g.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = g.b;
			asyncTaskMethodBuilder.Start<p.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000977A0 File Offset: 0x000967A0
		public virtual Task o6(string A_0, h A_1, bool A_2, bool A_3)
		{
			p.c c;
			c.c = this;
			c.d = A_0;
			c.e = A_1;
			c.g = A_2;
			c.f = A_3;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<p.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00097808 File Offset: 0x00096808
		public new Task e(string A_0)
		{
			p.e e;
			e.c = this;
			e.d = A_0;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<p.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x040017A8 RID: 6056
		protected new short a;

		// Token: 0x040017A9 RID: 6057
		protected new global::a.g.f b;

		// Token: 0x040017AA RID: 6058
		protected new string c;

		// Token: 0x040017AB RID: 6059
		protected new h d;

		// Token: 0x040017AC RID: 6060
		protected new DnsServer e;

		// Token: 0x020003F7 RID: 1015
		// (Invoke) Token: 0x06002405 RID: 9221
		public new delegate void f(string A_0, h A_1, bool A_2, bool A_3);
	}
}
