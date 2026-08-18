using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.Mime;
using MailBee.Pop3Mail;
using MailBee.Security;

namespace a.a
{
	// Token: 0x020003BA RID: 954
	internal class c : ab
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x0008C7F4 File Offset: 0x0008B7F4
		public c(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.j = global::a.a.g.a();
			this.a = null;
			this.f = null;
			this.g = null;
			this.h = 0;
			this.i = 0L;
			this.b = false;
			this.c = false;
			this.d = false;
			this.e = false;
			this.j = Pop3InboxPreloadOptions.None;
			this.m = false;
			this.n = false;
			this.p = false;
			this.o = null;
			this.q = null;
			this.r = null;
			if (this.b != null)
			{
				this.q = (global::a.a.c.q)Delegate.Combine(this.q, new global::a.a.c.q(this.a));
				this.r = (global::a.a.c.k)Delegate.Combine(this.r, new global::a.a.c.k(this.a));
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0008C8D4 File Offset: 0x0008B8D4
		protected override void ff()
		{
			base.ff();
			this.a.b(new global::a.a.d());
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0008C8EC File Offset: 0x0008B8EC
		protected internal override bf fg(bool A_0)
		{
			return new global::a.a.a(A_0);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0008C8F4 File Offset: 0x0008B8F4
		protected override u fh()
		{
			return new global::a.a.f(this);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0008C8FC File Offset: 0x0008B8FC
		public override al fi()
		{
			return new global::a.a.l();
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0008C903 File Offset: 0x0008B903
		protected override void fj()
		{
			this.a = global::a.bb.a(base.ak().r(), '<', '>', true);
			this.i();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0008C928 File Offset: 0x0008B928
		protected override void fk()
		{
			base.fk();
			this.f = null;
			this.g = null;
			this.m = false;
			this.n = false;
			this.h = 0;
			this.i = 0L;
			this.b = false;
			this.c = false;
			this.d = false;
			this.e = false;
			if (!this.p)
			{
				this.o = null;
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0008C991 File Offset: 0x0008B991
		public override string er()
		{
			return "POP3";
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0008C998 File Offset: 0x0008B998
		public override TopLevelProtocolType fl()
		{
			return TopLevelProtocolType.Pop3;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0008C99B File Offset: 0x0008B99B
		public new void b(Pop3MessageDownloadedEventArgs A_0)
		{
			if (this.q != null)
			{
				base.a(this.q, new object[]
				{
					A_0
				});
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0008C9BC File Offset: 0x0008B9BC
		public new void a(Pop3MessageDownloadedEventArgs A_0)
		{
			global::a.a.b b = (global::a.a.b)this.b;
			if (this.b.bq() && b.ki() && !this.b.bf())
			{
				b.kj(A_0);
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0008CA00 File Offset: 0x0008BA00
		public new void a(int A_0, int A_1, int A_2, int A_3)
		{
			if (this.r != null)
			{
				base.a(this.r, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					this
				});
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0008CA54 File Offset: 0x0008BA54
		public new void a(int A_0, int A_1, int A_2, int A_3, bc A_4)
		{
			global::a.a.b b = (global::a.a.b)this.b;
			if (this.b.bq() && b.kk() && !this.b.bf())
			{
				Pop3MessageDataChunkReceivedEventArgs a_ = new Pop3MessageDataChunkReceivedEventArgs(A_0, A_1, A_2, A_3, A_4);
				b.kl(a_);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0008CAA4 File Offset: 0x0008BAA4
		protected override bool fm(string A_0, ref int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fm(A_0, ref A_1, A_2, ref A_3))
			{
				A_0 = A_0.ToLower();
				if (A_0.Equals("pop.gmail.com") || A_0.Equals("pop3.live.com") || A_0.Equals("pop.mail.yahoo.com") || A_0.Equals("pop-mail.outlook.com") || A_0.Equals("pop3-mail.outlook.com"))
				{
					A_1 = 995;
					A_3 = true;
					return true;
				}
				if (A_1 == 995)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0008CB24 File Offset: 0x0008BB24
		protected override bool fn(string A_0, int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fn(A_0, A_1, A_2, ref A_3))
			{
				A_0 = A_0.ToLower();
				if ((A_0.EndsWith(".office365.com") || A_0.EndsWith(".outlook.com")) && A_1 == 110)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0008CB64 File Offset: 0x0008BB64
		public new void d(bool A_0)
		{
			if (!A_0)
			{
				this.f();
				bool flag = this.k.ac() == SslStartupMode.UseStartTls || this.k.ac() == SslStartupMode.UseStartTlsIfSupported;
				this.fn(this.k.v(), this.k.w(), this.k.ac(), ref flag);
				if (flag && !this.d)
				{
					this.fp(this.k.ac() == SslStartupMode.UseStartTls);
					if (this.d)
					{
						this.f();
					}
				}
			}
			base.fo();
			if (!A_0)
			{
				this.g();
				if ((this.j & Pop3InboxPreloadOptions.List) > Pop3InboxPreloadOptions.None)
				{
					this.z();
					this.m = true;
				}
				if ((this.j & Pop3InboxPreloadOptions.Uidl) > Pop3InboxPreloadOptions.None)
				{
					this.h();
					this.n = true;
				}
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0008CC30 File Offset: 0x0008BC30
		public override void fo()
		{
			this.d(false);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0008CC39 File Offset: 0x0008BC39
		public new string k()
		{
			return this.a;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0008CC41 File Offset: 0x0008BC41
		protected new void i()
		{
			this.g = AuthenticationMethods.Regular;
			if (this.a != null && this.a != string.Empty)
			{
				this.g |= AuthenticationMethods.Apop;
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0008CC74 File Offset: 0x0008BC74
		public new bool b(SaslMethod A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_Pop3GetAdvertizedSaslMethodsViaAuth, new object[0]), null, LogMessageType.Info, this);
			if (base.b("AUTH", new global::a.a.a(true, true, true), false))
			{
				this.i();
				global::a.a.d d = (global::a.a.d)this.a.d();
				string[] a_ = d.h(d.x());
				this.g |= global::a.a.e.a(a_, A_0);
				return true;
			}
			base.c(new MailBeePop3OptionalCommandNotSupportedException(500, base.a1(), base.ak()));
			this.i();
			return false;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0008CD18 File Offset: 0x0008BD18
		public new bool a(SaslMethod A_0, bool A_1, bool A_2)
		{
			bool flag = (this.c || !this.b) && !A_1;
			if (flag)
			{
				this.d.b(string.Format(Resources.Instance.Log_Pop3GetCapabilitiesViaCapa, new object[0]), null, LogMessageType.Info, this);
				this.b = true;
				if (base.b("CAPA", new global::a.a.a(true, true, true), false))
				{
					this.c = true;
					global::a.a.d d = (global::a.a.d)this.a.d();
					string[] a_ = d.h(d.x());
					this.h = global::a.a.e.a(a_);
					this.i();
					this.g |= global::a.a.e.a(this.h[global::a.a.e.b], A_0);
					this.f = (A_2 && this.h.ContainsKey(global::a.a.e.a));
					return true;
				}
				this.c = false;
			}
			this.f = false;
			if (!A_1)
			{
				base.c(new MailBeePop3OptionalCommandNotSupportedException(501, base.a1(), base.ak()));
			}
			if (((this.e || !this.d) && A_1) || (flag && !this.d))
			{
				this.d = true;
				this.e = this.b(A_0);
			}
			return A_1 && this.e;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0008CE74 File Offset: 0x0008BE74
		public new bool f(bool A_0)
		{
			global::a.a.l l = (global::a.a.l)this.k;
			return this.a(l.r(), A_0, l.u());
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0008CEA0 File Offset: 0x0008BEA0
		private new void f()
		{
			global::a.a.l l = (global::a.a.l)this.k;
			bool flag = (l.ac() == SslStartupMode.UseStartTls || l.ac() == SslStartupMode.UseStartTlsIfSupported) && !this.d;
			bool flag2 = this.a(l.u(), flag);
			bool a_ = (l.ae() & AuthenticationOptions.TryUnsupportedMethods) > AuthenticationOptions.None;
			if ((flag2 && this.r() && this.a(l.x(), a_, l.r())) || (this.n() && this.a(l.x(), a_, l.r(), l.u(), flag)))
			{
				this.f(flag2);
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0008CF3E File Offset: 0x0008BF3E
		public bool r()
		{
			return !this.d && (!this.b || !this.c);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0008CF5B File Offset: 0x0008BF5B
		public bool n()
		{
			return !this.b;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0008CF66 File Offset: 0x0008BF66
		public new bool a(AuthenticationMethods A_0, bool A_1, SaslMethod A_2, bool A_3, bool A_4)
		{
			return (!A_1 && SaslMethod.a(A_0, A_2)) || A_3 || A_4;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0008CF7B File Offset: 0x0008BF7B
		public new bool a(AuthenticationMethods A_0, bool A_1, SaslMethod A_2)
		{
			return !A_1 && SaslMethod.a(A_0, A_2);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0008CF89 File Offset: 0x0008BF89
		public new bool a(bool A_0, bool A_1)
		{
			return !A_0 && !A_1;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0008CF94 File Offset: 0x0008BF94
		public override void fp(bool A_0)
		{
			if (!this.c || base.t(this.j.hj()) != null)
			{
				base.fp(true);
				this.b = false;
				this.d = false;
				return;
			}
			if (A_0)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(130, base.a1());
			}
			this.d.b(string.Format(Resources.Instance.ErrorDesc_StartTlsNotAvailable, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0008D009 File Offset: 0x0008C009
		private new void b(int A_0)
		{
			if (A_0 < 1 || A_0 > this.h)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0008D020 File Offset: 0x0008C020
		private new int a(int A_0)
		{
			if (A_0 == 0 || Math.Abs(A_0) > this.h)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			if (A_0 > 0)
			{
				return A_0;
			}
			return this.h + A_0 + 1;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0008D04C File Offset: 0x0008C04C
		public new void g()
		{
			this.f = null;
			this.g = null;
			this.m = false;
			this.n = false;
			this.h = 0;
			this.i = 0L;
			this.aw();
			this.d.b(string.Format(Resources.Instance.Log_Pop3DownloadStat, new object[0]), null, LogMessageType.Info, this);
			this.o1("STAT", true);
			string[] array = base.ak().r().Split(null);
			try
			{
				this.h = int.Parse(array[0]);
				this.i = long.Parse(array[1]);
			}
			catch (Exception)
			{
				throw new MailBeeInvalidTextResponseException(125, base.a1(), base.ak().r(), this.bg());
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0008D11C File Offset: 0x0008C11C
		private new void e()
		{
			global::a.a.d d = (global::a.a.d)this.a.d();
			string[] array = d.h(d.x());
			this.f = new int[this.h];
			for (int i = 0; i < this.h; i++)
			{
				this.f[i] = -1;
			}
			for (int j = 0; j < array.Length; j++)
			{
				string[] array2 = array[j].Split(null);
				try
				{
					string s = array2[1];
					int num = 1;
					while (s == string.Empty)
					{
						num++;
						s = array2[num];
					}
					this.f[int.Parse(array2[0]) - 1] = int.Parse(s);
				}
				catch (Exception)
				{
					throw new MailBeeInvalidTextResponseItemException(125, base.a1(), array[j], this.bg());
				}
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0008D1F0 File Offset: 0x0008C1F0
		public void z()
		{
			this.d.b(string.Format(Resources.Instance.Log_Pop3DownloadList, new object[0]), null, LogMessageType.Info, this);
			base.b("LIST", new global::a.a.a(true, true, false), true);
			this.e();
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0008D230 File Offset: 0x0008C230
		private new void d()
		{
			global::a.a.d d = (global::a.a.d)this.a.d();
			string[] array = d.h(d.x());
			this.g = new string[this.h];
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(null);
				try
				{
					string text = array2[1];
					int num = 1;
					while (text == string.Empty)
					{
						num++;
						text = array2[num];
					}
					this.g[int.Parse(array2[0]) - 1] = text;
				}
				catch (Exception)
				{
					throw new MailBeeInvalidTextResponseItemException(125, base.a1(), array[i], this.bg());
				}
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0008D2E0 File Offset: 0x0008C2E0
		public new void h()
		{
			this.d.b(string.Format(Resources.Instance.Log_Pop3DownloadUidl, new object[0]), null, LogMessageType.Info, this);
			base.b("UIDL", new global::a.a.a(true, true, false), true);
			this.d();
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0008D320 File Offset: 0x0008C320
		public new int h(int A_0)
		{
			A_0 = this.a(A_0);
			if (this.f == null)
			{
				this.z();
			}
			return this.f[A_0 - 1];
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0008D343 File Offset: 0x0008C343
		public new int a(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (this.g == null)
			{
				this.h();
			}
			return Array.IndexOf<string>(this.g, A_0) + 1;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0008D36C File Offset: 0x0008C36C
		public new string g(int A_0)
		{
			A_0 = this.a(A_0);
			if (this.g == null)
			{
				this.h();
			}
			return this.g[A_0 - 1];
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0008D390 File Offset: 0x0008C390
		public void ac()
		{
			this.d.b(string.Format(Resources.Instance.Log_Pop3ResetDeletes, new object[0]), null, LogMessageType.Info, this);
			this.o1("RSET", true);
			if (!this.m)
			{
				this.f = null;
			}
			if (!this.n)
			{
				this.g = null;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0008D3EB File Offset: 0x0008C3EB
		public new void j()
		{
			this.o1("STAT", false);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0008D3FC File Offset: 0x0008C3FC
		public int v()
		{
			this.o1("LAST", true);
			string[] array = base.ak().r().Split(null);
			int result;
			try
			{
				result = int.Parse(array[0]);
			}
			catch (Exception)
			{
				throw new MailBeeInvalidTextResponseException(125, base.a1(), base.ak().r(), this.bg());
			}
			return result;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0008D464 File Offset: 0x0008C464
		public new void f(int A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_Pop3WillDeleteMessageIndex0, A_0), null, LogMessageType.Info, this);
			A_0 = this.a(A_0);
			this.o1("DELE " + Convert.ToString(A_0), true);
			this.d.b(string.Format(Resources.Instance.Log_Pop3DeletedMessageIndex0, A_0), null, LogMessageType.Info, this);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0008D4D8 File Offset: 0x0008C4D8
		public new void d(int A_0, int A_1)
		{
			if (A_0 < 0 && A_1 < 0)
			{
				if (this.h <= 0)
				{
					this.d.b(string.Format(Resources.Instance.Log_Pop3NothingToDelete, new object[0]), null, LogMessageType.Info, this);
					return;
				}
				A_0 = 1;
			}
			if (A_1 < 0)
			{
				A_1 = this.h - A_0 + 1;
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3WillDeleteMessagesStartIndex0Count1, A_0, A_1), null, LogMessageType.Info, this);
			this.b(A_0);
			int num = A_0 + A_1 - 1;
			this.b(num);
			global::a.g g = this.a.d();
			g.cf();
			g.b = this.f;
			try
			{
				for (int i = A_0; i <= num; i++)
				{
					global::a.a.a a_ = new global::a.a.a(true);
					g.g(this.o2("DELE " + Convert.ToString(i), a_), a_, 0);
					if (!this.f)
					{
						g.g(base.ak());
					}
				}
				if (this.f)
				{
					g.o(0);
					g.s();
				}
			}
			finally
			{
				g.b = false;
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3DeletedMessagesStartIndex0Count1, A_0, A_1), null, LogMessageType.Info, this);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0008D628 File Offset: 0x0008C628
		private new string a(int A_0, int A_1)
		{
			if (A_1 < 0)
			{
				return "RETR " + Convert.ToString(A_0);
			}
			return "TOP " + Convert.ToString(A_0) + " " + Convert.ToString(A_1);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0008D65C File Offset: 0x0008C65C
		private new string a(int A_0, bool A_1)
		{
			if (A_0 < 0)
			{
				if (A_1)
				{
					return Resources.Instance.Log_Pop3EntireMessages;
				}
				return Resources.Instance.Log_Pop3EntireMessage;
			}
			else if (A_0 == 0)
			{
				if (A_1)
				{
					return Resources.Instance.Log_Pop3MessageHeaders;
				}
				return Resources.Instance.Log_Pop3MessageHeader;
			}
			else
			{
				if (A_1)
				{
					return string.Format(Resources.Instance.Log_Pop3MessageHeadersAnd0BodyLines, A_0);
				}
				return string.Format(Resources.Instance.Log_Pop3MessageHeaderAnd0BodyLines, A_0);
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0008D6D0 File Offset: 0x0008C6D0
		public new MailMessage c(int A_0, int A_1)
		{
			if (this.p)
			{
				this.o = null;
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3WillDownload0Index1, this.a(A_1, false), A_0), null, LogMessageType.Info, this);
			A_0 = this.a(A_0);
			if (A_1 > -1 && this.f == null)
			{
				this.z();
			}
			bool flag = this.c();
			if (flag)
			{
				if (A_1 < 0 && this.f == null)
				{
					this.z();
				}
				global::a.a.d d = (global::a.a.d)this.a.d();
				d.g((global::a.a.k)Delegate.Combine(d.g(), new global::a.a.k(this.a)));
			}
			global::a.a.d d2 = (global::a.a.d)this.a.d();
			d2.g((ay)Delegate.Combine(d2.k(), new ay(this.b)));
			this.l = A_1;
			this.k = A_0;
			MailMessage result = null;
			this.o = new MailMessageCollection();
			if (this.f != null)
			{
				this.a.d().p(this.f[A_0 - 1] + this.a.d().a);
			}
			try
			{
				base.b(this.a(A_0, A_1), new global::a.a.a(true, true, false), true);
				this.a.d().n(this.a.d().a * 8);
				if (this.o.Count > 0)
				{
					result = this.o[0];
				}
			}
			finally
			{
				global::a.a.d d3 = (global::a.a.d)this.a.d();
				d3.g((ay)Delegate.Remove(d3.k(), new ay(this.b)));
				if (flag)
				{
					global::a.a.d d4 = (global::a.a.d)this.a.d();
					d4.g((global::a.a.k)Delegate.Remove(d4.g(), new global::a.a.k(this.a)));
				}
				if (!this.p)
				{
					this.o = null;
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3Downloaded0Index1, this.a(A_1, false), A_0), null, LogMessageType.Info, this);
			return result;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0008D908 File Offset: 0x0008C908
		public new MailMessageCollection b(int A_0, int A_1, int A_2)
		{
			if (this.p)
			{
				this.o = null;
			}
			if (A_0 < 0 && A_1 < 0)
			{
				if (this.h <= 0)
				{
					this.d.b(string.Format(Resources.Instance.Log_Pop3ZeroMessagesDownloadedFromEmptyInbox, A_1, A_0), null, LogMessageType.Info, this);
					this.o = new MailMessageCollection();
					return this.o;
				}
				A_0 = 1;
			}
			if (A_1 < 0)
			{
				A_1 = this.h - A_0 + 1;
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3WillDownload0StartIndex1Count2, this.a(A_2, true), A_0, A_1), null, LogMessageType.Info, this);
			this.b(A_0);
			int num = A_0 + A_1 - 1;
			this.b(num);
			if (A_2 > -1 && this.f == null)
			{
				this.z();
			}
			global::a.a.d d = (global::a.a.d)this.a.d();
			bool flag = this.c();
			if (flag)
			{
				if (A_2 < 0 && this.f == null)
				{
					this.z();
				}
				global::a.a.d d2 = d;
				d2.g((global::a.a.k)Delegate.Combine(d2.g(), new global::a.a.k(this.a)));
			}
			global::a.a.d d3 = d;
			d3.g((ay)Delegate.Combine(d3.k(), new ay(this.b)));
			d.cf();
			d.b = this.f;
			this.k = A_0;
			this.l = A_2;
			this.o = new MailMessageCollection();
			MailMessageCollection result = null;
			if (this.f != null)
			{
				int num2 = 0;
				for (int i = A_0 - 1; i < A_0 + A_1 - 1; i++)
				{
					if (this.f[i] > num2)
					{
						num2 = this.f[i];
					}
				}
				this.a.d().p(num2 + this.a.d().a);
			}
			try
			{
				for (int j = A_0; j <= num; j++)
				{
					global::a.a.a a_ = new global::a.a.a(true, true, false);
					d.g(this.o2(this.a(j, A_2), a_), a_, 0);
					if (!this.f)
					{
						d.g(base.ak());
					}
				}
				if (this.f)
				{
					d.o(0);
					d.s();
				}
				result = this.o;
				this.a.d().n(this.a.d().a * 8);
			}
			finally
			{
				d.b = false;
				global::a.a.d d4 = d;
				d4.g((ay)Delegate.Remove(d4.k(), new ay(this.b)));
				if (flag)
				{
					global::a.a.d d5 = d;
					d5.g((global::a.a.k)Delegate.Remove(d5.g(), new global::a.a.k(this.a)));
				}
				if (!this.p)
				{
					this.o = null;
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_Pop3Downloaded0StartIndex1Count2, this.a(A_2, true), A_0, A_1), null, LogMessageType.Info, this);
			return result;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0008DC00 File Offset: 0x0008CC00
		private new MailMessage a(global::a.a.d A_0, global::a.a.j A_1, int A_2, int A_3)
		{
			if (A_1.t() != af.a)
			{
				return null;
			}
			ao ao = A_0.g(A_1);
			MailMessage mailMessage = new MailMessage(ao);
			if (A_3 < 0)
			{
				mailMessage.b(ao.e());
			}
			else
			{
				mailMessage.b(this.f[A_2 - 1]);
			}
			mailMessage.IndexOnServerInternal = A_2;
			if (this.g != null)
			{
				mailMessage.UidOnServerInternal = this.g[A_2 - 1];
			}
			return mailMessage;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0008DC69 File Offset: 0x0008CC69
		public int x()
		{
			return this.h;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0008DC71 File Offset: 0x0008CC71
		public new long p()
		{
			return this.i;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0008DC79 File Offset: 0x0008CC79
		public int[] y()
		{
			return this.f;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0008DC81 File Offset: 0x0008CC81
		public new string[] m()
		{
			return this.g;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0008DC89 File Offset: 0x0008CC89
		public Pop3InboxPreloadOptions ab()
		{
			return this.j;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0008DC91 File Offset: 0x0008CC91
		public new void a(Pop3InboxPreloadOptions A_0)
		{
			this.j = A_0;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0008DC9A File Offset: 0x0008CC9A
		public new bool o()
		{
			return this.p;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0008DCA2 File Offset: 0x0008CCA2
		public new void c(bool A_0)
		{
			if (!A_0)
			{
				this.o = null;
			}
			this.p = A_0;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0008DCB5 File Offset: 0x0008CCB5
		public MailMessageCollection q()
		{
			if (this.p)
			{
				return this.o;
			}
			return null;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0008DCC7 File Offset: 0x0008CCC7
		private new bool c()
		{
			return this.b != null && this.b.bq() && ((global::a.a.b)this.b).kk();
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0008DCF0 File Offset: 0x0008CCF0
		private new void a(byte[] A_0, int A_1, int A_2, int A_3, bc A_4)
		{
			global::a.a.b b = (global::a.a.b)this.b;
			global::a.a.h h = (global::a.a.h)this.b;
			if (b.kk() && !h.bf())
			{
				int a_ = -1;
				if (this.l < 0)
				{
					if (this.k - 1 >= this.f.Length)
					{
						throw new MailBeeInvalidTextResponseException(124, base.a1(), this.bg().GetString(A_0, A_2, A_3), this.bg());
					}
					a_ = this.f[this.k - 1];
				}
				this.a(this.k, A_3, A_2 + A_3 - A_1, a_);
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0008DD88 File Offset: 0x0008CD88
		private new void b(at A_0, bc A_1)
		{
			global::a.a.d a_ = (global::a.a.d)this.a.d();
			global::a.a.j a_2 = (global::a.a.j)A_0;
			MailMessage mailMessage = this.a(a_, a_2, this.k, this.l);
			global::a.a.h h = (global::a.a.h)this.b;
			if (h != null && h.bq() && ((global::a.a.b)this.b).ki() && !h.bf())
			{
				Pop3MessageDownloadedEventArgs pop3MessageDownloadedEventArgs = new Pop3MessageDownloadedEventArgs(this.k, A_0.q().Length, mailMessage, A_1);
				this.b(pop3MessageDownloadedEventArgs);
				mailMessage = pop3MessageDownloadedEventArgs.DownloadedMessage;
			}
			if (mailMessage != null)
			{
				this.o.Add(mailMessage);
			}
			if (A_0.t() != af.c)
			{
				A_0.ag();
			}
			this.k++;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0008DE46 File Offset: 0x0008CE46
		protected override Task fq()
		{
			this.fj();
			return Task.FromResult<int>(0);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0008DE54 File Offset: 0x0008CE54
		private new Task a(at A_0, bc A_1)
		{
			this.b(A_0, A_1);
			return Task.FromResult<int>(0);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0008DE64 File Offset: 0x0008CE64
		public new Task b(bool A_0)
		{
			global::a.a.c.b b;
			b.d = this;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0008DEB1 File Offset: 0x0008CEB1
		public override Task fr()
		{
			return this.b(false);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0008DEBC File Offset: 0x0008CEBC
		public new Task<bool> a(SaslMethod A_0)
		{
			global::a.a.c.g g;
			g.c = this;
			g.d = A_0;
			g.b = AsyncTaskMethodBuilder<bool>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = g.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0008DF0C File Offset: 0x0008CF0C
		public new Task<bool> b(SaslMethod A_0, bool A_1, bool A_2)
		{
			global::a.a.c.r r;
			r.c = this;
			r.e = A_0;
			r.d = A_1;
			r.f = A_2;
			r.b = AsyncTaskMethodBuilder<bool>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = r.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0008DF6C File Offset: 0x0008CF6C
		public new Task<bool> e(bool A_0)
		{
			global::a.a.l l = (global::a.a.l)this.k;
			return this.b(l.r(), A_0, l.u());
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0008DF98 File Offset: 0x0008CF98
		private new Task b()
		{
			global::a.a.l l = (global::a.a.l)this.k;
			bool flag = (l.ac() == SslStartupMode.UseStartTls || l.ac() == SslStartupMode.UseStartTlsIfSupported) && !this.d;
			bool flag2 = this.a(l.u(), flag);
			bool a_ = (l.ae() & AuthenticationOptions.TryUnsupportedMethods) > AuthenticationOptions.None;
			if ((flag2 && this.r() && this.a(l.x(), a_, l.r())) || (this.n() && this.a(l.x(), a_, l.r(), l.u(), flag)))
			{
				return this.e(flag2);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0008E03C File Offset: 0x0008D03C
		public override Task fs(bool A_0)
		{
			global::a.a.c.i i;
			i.c = this;
			i.d = A_0;
			i.b = AsyncTaskMethodBuilder.Create();
			i.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = i.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0008E08C File Offset: 0x0008D08C
		public Task u()
		{
			global::a.a.c.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder.Create();
			f.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = f.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0008E0D4 File Offset: 0x0008D0D4
		public new Task l()
		{
			global::a.a.c.n n;
			n.c = this;
			n.b = AsyncTaskMethodBuilder.Create();
			n.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0008E11C File Offset: 0x0008D11C
		public Task s()
		{
			global::a.a.c.l l;
			l.c = this;
			l.b = AsyncTaskMethodBuilder.Create();
			l.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = l.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0008E164 File Offset: 0x0008D164
		public new Task<int> c(int A_0)
		{
			global::a.a.c.e e;
			e.d = this;
			e.c = A_0;
			e.b = AsyncTaskMethodBuilder<int>.Create();
			e.a = -1;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0008E1B4 File Offset: 0x0008D1B4
		public new Task<int> b(string A_0)
		{
			global::a.a.c.j j;
			j.d = this;
			j.c = A_0;
			j.b = AsyncTaskMethodBuilder<int>.Create();
			j.a = -1;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = j.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0008E204 File Offset: 0x0008D204
		public new Task<string> e(int A_0)
		{
			global::a.a.c.p p;
			p.d = this;
			p.c = A_0;
			p.b = AsyncTaskMethodBuilder<string>.Create();
			p.a = -1;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = p.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0008E254 File Offset: 0x0008D254
		public Task t()
		{
			global::a.a.c.o o;
			o.c = this;
			o.b = AsyncTaskMethodBuilder.Create();
			o.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = o.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0008E299 File Offset: 0x0008D299
		public Task w()
		{
			return this.o4("STAT", false);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0008E2A8 File Offset: 0x0008D2A8
		public Task<int> aa()
		{
			global::a.a.c.h h;
			h.c = this;
			h.b = AsyncTaskMethodBuilder<int>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = h.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0008E2F0 File Offset: 0x0008D2F0
		public new Task d(int A_0)
		{
			global::a.a.c.c c;
			c.c = this;
			c.d = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0008E340 File Offset: 0x0008D340
		public new Task b(int A_0, int A_1)
		{
			global::a.a.c.m m;
			m.e = this;
			m.c = A_0;
			m.d = A_1;
			m.b = AsyncTaskMethodBuilder.Create();
			m.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = m.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0008E398 File Offset: 0x0008D398
		public new Task<MailMessage> e(int A_0, int A_1)
		{
			global::a.a.c.a a;
			a.c = this;
			a.e = A_0;
			a.d = A_1;
			a.b = AsyncTaskMethodBuilder<MailMessage>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<MailMessage> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0008E3F0 File Offset: 0x0008D3F0
		public new Task<MailMessageCollection> a(int A_0, int A_1, int A_2)
		{
			global::a.a.c.d d;
			d.c = this;
			d.d = A_0;
			d.e = A_1;
			d.f = A_2;
			d.b = AsyncTaskMethodBuilder<MailMessageCollection>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<MailMessageCollection> asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.a.c.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0008E44D File Offset: 0x0008D44D
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a()
		{
			return base.fr();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0008E455 File Offset: 0x0008D455
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(bool A_0)
		{
			return base.fs(A_0);
		}

		// Token: 0x04001691 RID: 5777
		protected new string a;

		// Token: 0x04001692 RID: 5778
		private new bool b;

		// Token: 0x04001693 RID: 5779
		private new bool c;

		// Token: 0x04001694 RID: 5780
		private new bool d;

		// Token: 0x04001695 RID: 5781
		private new bool e;

		// Token: 0x04001696 RID: 5782
		private new int[] f;

		// Token: 0x04001697 RID: 5783
		private new string[] g;

		// Token: 0x04001698 RID: 5784
		private new int h;

		// Token: 0x04001699 RID: 5785
		private new long i;

		// Token: 0x0400169A RID: 5786
		private new Pop3InboxPreloadOptions j;

		// Token: 0x0400169B RID: 5787
		private new int k;

		// Token: 0x0400169C RID: 5788
		private new int l;

		// Token: 0x0400169D RID: 5789
		private new bool m;

		// Token: 0x0400169E RID: 5790
		private bool n;

		// Token: 0x0400169F RID: 5791
		private new MailMessageCollection o;

		// Token: 0x040016A0 RID: 5792
		private new bool p;

		// Token: 0x040016A1 RID: 5793
		private global::a.a.c.q q;

		// Token: 0x040016A2 RID: 5794
		private global::a.a.c.k r;

		// Token: 0x020003BB RID: 955
		// (Invoke) Token: 0x060022BE RID: 8894
		protected delegate void q(Pop3MessageDownloadedEventArgs A_0);

		// Token: 0x020003BC RID: 956
		// (Invoke) Token: 0x060022C2 RID: 8898
		protected new delegate void k(int A_0, int A_1, int A_2, int A_3, bc A_4);
	}
}
