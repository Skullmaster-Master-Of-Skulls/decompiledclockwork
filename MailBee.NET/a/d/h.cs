using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using a.a;
using a.n;
using MailBee;
using MailBee.Mime;
using MailBee.Security;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x0200043F RID: 1087
	internal class h : ab
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x000A7F64 File Offset: 0x000A6F64
		public h(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.j = global::a.d.q.a();
			this.b = false;
			this.c = false;
			this.d = false;
			this.e = false;
			this.f = false;
			this.g = false;
			this.h = false;
			this.i = 0;
			this.j = 0;
			this.l = new EmailAddressCollection();
			this.k = new EmailAddressCollection();
			this.m = new EmailAddressCollection();
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = null;
			this.r = null;
			this.s = null;
			this.t = null;
			if (this.b != null)
			{
				this.o = (global::a.d.h.k)Delegate.Combine(this.o, new global::a.d.h.k(this.a));
				this.p = (global::a.d.h.g)Delegate.Combine(this.p, new global::a.d.h.g(this.a));
				this.q = (global::a.d.h.h)Delegate.Combine(this.q, new global::a.d.h.h(this.a));
				this.r = (global::a.d.h.i)Delegate.Combine(this.r, new global::a.d.h.i(this.a));
				this.s = (global::a.d.h.p)Delegate.Combine(this.s, new global::a.d.h.p(this.a));
				this.t = (global::a.d.h.e)Delegate.Combine(this.t, new global::a.d.h.e(this.a));
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000A80EF File Offset: 0x000A70EF
		protected override void ff()
		{
			base.ff();
			this.a.b(new global::a.d.l());
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000A8107 File Offset: 0x000A7107
		protected internal override bf fg(bool A_0)
		{
			return new global::a.d.m(A_0, false, false, false);
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000A8112 File Offset: 0x000A7112
		protected override u fh()
		{
			return new global::a.d.e(this);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000A811A File Offset: 0x000A711A
		public override al fi()
		{
			return null;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000A811D File Offset: 0x000A711D
		public override string er()
		{
			return "SMTP";
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000A8124 File Offset: 0x000A7124
		public override TopLevelProtocolType fl()
		{
			return TopLevelProtocolType.Smtp;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000A8127 File Offset: 0x000A7127
		public new void o()
		{
			if (!base.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000A8139 File Offset: 0x000A7139
		public override void fu()
		{
			base.fu();
			if (!this.b)
			{
				throw new MailBeeInvalidStateException(310);
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000A8154 File Offset: 0x000A7154
		public override void fv()
		{
			base.fv();
			if (!this.b)
			{
				throw new MailBeeInvalidStateException(310);
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000A8170 File Offset: 0x000A7170
		protected override void fk()
		{
			base.fk();
			this.b = false;
			this.c = false;
			this.d = false;
			this.e = false;
			this.f = false;
			this.g = false;
			this.h = false;
			this.i = 0;
			this.j = 0;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000A81C4 File Offset: 0x000A71C4
		protected override void fw(MailBeeException A_0)
		{
			base.fw(A_0);
			if (base.ao() && A_0 is IMailBeeSmtpSendNeedsResetException)
			{
				try
				{
					this.k();
				}
				catch (MailBeeException ex)
				{
					if (ex is IMailBeeSmtpSendNeedsResetException)
					{
						throw;
					}
					base.d(ex);
				}
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000A8214 File Offset: 0x000A7214
		public override void fx()
		{
			global::a.a.c c = this.n;
			if (c != null)
			{
				c.fx();
			}
			base.fx();
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000A8237 File Offset: 0x000A7237
		public new void a(MailMessage A_0, string A_1)
		{
			if (this.o != null)
			{
				base.a(this.o, new object[]
				{
					A_0,
					A_1,
					this
				});
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000A8260 File Offset: 0x000A7260
		public new void a(MailMessage A_0, string A_1, bc A_2)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.l5() && !this.b.bf())
			{
				SmtpMessageSenderSubmittedEventArgs a_ = new SmtpMessageSenderSubmittedEventArgs(A_0, A_1, A_2);
				o.l6(a_);
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000A82AC File Offset: 0x000A72AC
		public new bool a(MailMessage A_0, string A_1, bool A_2, bool A_3, string A_4)
		{
			if (this.p != null)
			{
				SmtpMessageRecipientSubmittedEventArgs smtpMessageRecipientSubmittedEventArgs = new SmtpMessageRecipientSubmittedEventArgs(A_0, A_1, A_2, A_3, A_4, this);
				base.a(this.p, new object[]
				{
					smtpMessageRecipientSubmittedEventArgs,
					this
				});
				return smtpMessageRecipientSubmittedEventArgs.AllowRefusedRecipient;
			}
			return true;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000A82F4 File Offset: 0x000A72F4
		public new void a(SmtpMessageRecipientSubmittedEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.l7() && !this.b.bf())
			{
				o.l8(A_0);
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000A8338 File Offset: 0x000A7338
		public new void a(MailMessage A_0, int A_1, int A_2, int A_3)
		{
			if (this.q != null)
			{
				base.a(this.q, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					this
				});
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000A8384 File Offset: 0x000A7384
		public new void a(MailMessage A_0, int A_1, int A_2, int A_3, bc A_4)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.l9() && !this.b.bf())
			{
				SmtpMessageDataChunkSentEventArgs a_ = new SmtpMessageDataChunkSentEventArgs(A_0, A_1, A_2, A_3, A_4);
				o.ma(a_);
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000A83D3 File Offset: 0x000A73D3
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4)
		{
			if (this.r != null)
			{
				base.a(this.r, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					this
				});
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000A840C File Offset: 0x000A740C
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mb() && !this.b.bf())
			{
				SmtpMessageSubmittedToServerEventArgs a_ = new SmtpMessageSubmittedToServerEventArgs(A_0, A_1, A_2, A_3, A_4, A_5);
				o.mc(a_);
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000A8460 File Offset: 0x000A7460
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7)
		{
			if (this.s != null)
			{
				base.a(this.s, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					A_7,
					this
				});
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000A84B4 File Offset: 0x000A74B4
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7, bc A_8)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.md() && !this.b.bf())
			{
				SmtpMessageSentEventArgs a_ = new SmtpMessageSentEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
				o.me(a_);
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000A850C File Offset: 0x000A750C
		public virtual void f6(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6)
		{
			if (this.t != null)
			{
				base.a(this.t, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					this
				});
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000A8558 File Offset: 0x000A7558
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mf() && !this.b.bf())
			{
				SmtpMessageNotSentEventArgs a_ = new SmtpMessageNotSentEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				o.mg(a_);
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000A85AD File Offset: 0x000A75AD
		public bool n()
		{
			return this.b;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000A85B8 File Offset: 0x000A75B8
		private new void a(bool A_0, ExtendedSmtpOptions A_1, SaslMethod A_2)
		{
			this.h = global::a.d.b.a(base.ak().s());
			if ((A_1 & ExtendedSmtpOptions.NoDsn) == ExtendedSmtpOptions.Default && base.t(global::a.d.b.c) != null)
			{
				this.d = true;
			}
			if (A_0 && base.t(global::a.d.b.d) != null)
			{
				this.f = true;
			}
			if ((A_1 & ExtendedSmtpOptions.NoChunking) == ExtendedSmtpOptions.Default && base.t(global::a.d.b.e) != null)
			{
				this.e = true;
			}
			if (base.t(global::a.d.b.b) != null)
			{
				this.g = true;
			}
			if (base.t(global::a.d.b.a) != null)
			{
				this.f = true;
			}
			string text;
			if ((A_1 & ExtendedSmtpOptions.NoSize) == ExtendedSmtpOptions.Default && (text = base.s(global::a.d.b.f)) != null && text != string.Empty)
			{
				try
				{
					this.j = int.Parse(text);
				}
				catch (Exception)
				{
				}
			}
			this.g = global::a.d.b.a(base.t(global::a.d.b.g), A_2);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000A86A8 File Offset: 0x000A76A8
		private new void b(string A_0, bool A_1, ExtendedSmtpOptions A_2, SaslMethod A_3)
		{
			bool flag = (A_2 & ExtendedSmtpOptions.ClassicSmtpMode) == ExtendedSmtpOptions.Default;
			this.d.b(string.Format(Resources.Instance.Log_SmtpWillHello, new object[0]), null, LogMessageType.Info, this);
			if (A_0 == null || A_0 == string.Empty)
			{
				try
				{
					A_0 = Dns.GetHostName();
					foreach (IPAddress ipaddress in Dns.GetHostEntry(A_0).AddressList)
					{
						if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
						{
							A_0 = "[" + ipaddress.ToString() + "]";
							break;
						}
					}
				}
				catch (SocketException a_)
				{
					throw new MailBeeGetLocalHostNameException(50, a_);
				}
			}
			this.b = false;
			this.h = null;
			this.d = false;
			this.f = false;
			this.e = false;
			this.f = false;
			this.g = false;
			this.j = 0;
			this.g = AuthenticationMethods.None;
			if (flag)
			{
				if (this.o1("EHLO " + A_0, false))
				{
					this.b = true;
					this.a(A_1, A_2, A_3);
				}
				else
				{
					base.c(new MailBeeSmtpOptionalCommandNotSupportedException(311, base.a1(), base.ak()));
				}
			}
			if (!this.b)
			{
				this.o1("HELO " + A_0, true);
				this.b = true;
			}
			this.d.b(string.Format(Resources.Instance.SmtpHelloed, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000A8820 File Offset: 0x000A7820
		public new void e()
		{
			global::a.d.d d = (global::a.d.d)this.k;
			bool flag2;
			do
			{
				this.b(d.h(), d.u(), d.b(), d.r());
				bool flag = d.ac() == SslStartupMode.UseStartTls || d.ac() == SslStartupMode.UseStartTlsIfSupported;
				this.fn(d.v(), d.w(), d.ac(), ref flag);
				if (flag && !this.d)
				{
					this.fp(d.ac() == SslStartupMode.UseStartTls);
					flag2 = !this.b;
				}
				else
				{
					flag2 = false;
				}
			}
			while (flag2);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000A88B4 File Offset: 0x000A78B4
		public override void fp(bool A_0)
		{
			if (base.t(this.j.hj()) != null)
			{
				base.fp(true);
				this.b = false;
				this.c = false;
				this.d = false;
				this.e = false;
				this.f = false;
				this.g = false;
				this.h = false;
				this.i = 0;
				this.j = 0;
				return;
			}
			if (A_0)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(130, base.a1());
			}
			this.d.b(string.Format(Resources.Instance.ErrorDesc_StartTlsNotAvailable, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000A8954 File Offset: 0x000A7954
		public override void fo()
		{
			if (((global::a.d.d)this.k).l())
			{
				try
				{
					base.fo();
					this.c = false;
					return;
				}
				catch (MailBeeException ex)
				{
					if (ex is IMailBeeLoginException)
					{
						this.c = true;
						base.c(ex);
						return;
					}
					throw;
				}
			}
			bool flag = true;
			try
			{
				base.fo();
				flag = false;
			}
			finally
			{
				this.c = flag;
			}
			if (this.c)
			{
				this.d.b(string.Format(Resources.Instance.Log_SmtpLoginFailed, new object[0]), null, LogMessageType.Info, this);
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000A89FC File Offset: 0x000A79FC
		public new void k()
		{
			this.d.b(string.Format(Resources.Instance.Log_SmtpWillResetSmtpSession, new object[0]), null, LogMessageType.Info, this);
			this.o1("RSET", true);
			this.d.b(string.Format(Resources.Instance.Log_SmtpSessionReset, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000A8A5C File Offset: 0x000A7A5C
		public new bool g()
		{
			return ((global::a.d.d)this.k).e();
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000A8A70 File Offset: 0x000A7A70
		protected override bool fm(string A_0, ref int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fm(A_0, ref A_1, A_2, ref A_3))
			{
				A_0 = A_0.ToLower();
				if (A_0.Equals("smtp.mail.yahoo.com") && A_1 == 25)
				{
					A_1 = 465;
					A_3 = true;
					return true;
				}
				if (A_0.Equals("smtp-mail.outlook.com") && A_1 == 25)
				{
					A_1 = 587;
					return true;
				}
				if (A_1 == 465)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000A8AE0 File Offset: 0x000A7AE0
		protected override bool fn(string A_0, int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fn(A_0, A_1, A_2, ref A_3))
			{
				if (A_1 == 587)
				{
					A_3 = true;
					return true;
				}
				A_0 = A_0.ToLower();
				if ((A_0.Equals("smtp.gmail.com") || A_0.Equals("smtp.live.com") || A_0.EndsWith(".office365.com") || A_0.EndsWith(".outlook.com")) && A_1 == 25)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000A8B50 File Offset: 0x000A7B50
		public override void fy()
		{
			global::a.d.d d = (global::a.d.d)this.k;
			this.pd(d.ab());
			while (!d.a())
			{
				Thread.Sleep(1);
			}
			try
			{
				if (d.c())
				{
					this.b(d.v(), 110, d.q(), d.aa(), d.l());
				}
				base.fy();
			}
			catch (MailBeeNetworkException)
			{
				if (!base.ao())
				{
					d.m();
					d.g();
				}
				throw;
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000A8BE0 File Offset: 0x000A7BE0
		public override void fz(bool A_0)
		{
			base.fz(A_0);
			global::a.d.d d = (global::a.d.d)this.av();
			d.m();
			d.g();
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000A8C00 File Offset: 0x000A7C00
		public virtual void f4(string A_0, int A_1, string A_2, string A_3)
		{
			global::a.d.d d = (global::a.d.d)this.k;
			this.b(A_0, A_1, A_2, A_3, d.l());
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000A8C2C File Offset: 0x000A7C2C
		private new void b(string A_0, int A_1, string A_2, string A_3, bool A_4)
		{
			this.d.b(string.Format(Resources.Instance.Log_SmtpWillPerformAuthPopBeforeSmtp, new object[0]), null, LogMessageType.Info, this);
			this.n = new global::a.a.c(this.b, this, base.a8(), this.f);
			this.n.av().e(A_0);
			this.n.av().g(A_1);
			this.n.av().d(false);
			this.n.av().a(AuthenticationMethods.Regular);
			this.n.av().c(A_2);
			this.n.av().d(A_3);
			this.n.av().f(this.m);
			this.n.hc(this.k);
			this.n.hc(this.l);
			this.n.a(base.ba());
			bool flag = false;
			try
			{
				this.n.fy();
				this.n.d(true);
				this.n.fz(true);
			}
			catch (MailBeeNetworkException a_)
			{
				if (!A_4)
				{
					throw;
				}
				flag = true;
				this.n.c(a_);
			}
			finally
			{
				this.n.@as();
				this.n = null;
			}
			if (flag)
			{
				this.d.b(string.Format(Resources.Instance.Log_SmtpAuthPopBeforeSmtpFailed, new object[0]), null, LogMessageType.Info, this);
				return;
			}
			this.d.b(string.Format(Resources.Instance.Log_SmtpAuthPopBeforeSmtpSucceeded, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000A8DE4 File Offset: 0x000A7DE4
		public virtual void f5(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11)
		{
			this.h = false;
			this.l = A_2;
			this.k = new EmailAddressCollection();
			this.m = new EmailAddressCollection();
			try
			{
				if (A_1 == null)
				{
					throw new MailBeeInvalidArgumentException(312);
				}
				if (A_2 == null || A_2.Count == 0)
				{
					throw new MailBeeInvalidArgumentException(314);
				}
				bool flag = base.ao();
				if (!base.ao())
				{
					this.fy();
				}
				if (!this.n())
				{
					this.e();
				}
				if (!base.ar())
				{
					this.fo();
				}
				global::a.d.d d = (global::a.d.d)this.k;
				if (A_5)
				{
					bool a_ = A_2.Count > 1 && d.d();
					if (A_7 == SendFailureThreshold.AnyRecipientsFailed)
					{
						a_ = false;
					}
					else if (A_7 == SendFailureThreshold.AllRecipientsFailed && A_2.Count > 1)
					{
						a_ = true;
					}
					this.d.b(string.Format(Resources.Instance.Log_SmtpWillSendMailMessageToServer0, d.v()), null, LogMessageType.Info, this);
					this.b(A_0, A_1, A_2, A_3, A_4, a_, this.k, this.m, A_6, A_8, A_9, A_10, A_11);
				}
				if ((!flag && !this.i) || (d.i() > -1 && this.i >= d.i()))
				{
					this.fz(true);
				}
			}
			catch (MailBeeUserAbortException)
			{
				throw;
			}
			catch (MailBeeException ex)
			{
				if (!this.h)
				{
					global::a.d.o o = null;
					if (this.b != null && this.b.bq())
					{
						o = (global::a.d.o)this.b;
					}
					if (!A_8 && o != null && o.mf() && !this.b.bf())
					{
						this.f6(A_0, A_1, A_2, ex, A_9, A_10, A_11);
					}
					throw;
				}
				base.a8().b(string.Format(Resources.Instance.Log_Warning0, ex.Message), null, LogMessageType.Info, this);
				base.c(ex);
				this.fw(ex);
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000A8FE0 File Offset: 0x000A7FE0
		public new IAsyncResult a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11, AsyncCallback A_12, object A_13)
		{
			base.k(true);
			global::a.d.h.m m = new global::a.d.h.m(this.f5);
			this.g = new global::a.o(m, null);
			this.g.a(m.BeginInvoke(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, A_11, A_12, A_13));
			return this.g;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000A9040 File Offset: 0x000A8040
		public new void i()
		{
			if (this.g == null || !(this.g.c() is global::a.d.h.m))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.g.d() == null)
			{
				Thread.Sleep(0);
			}
			try
			{
				((global::a.d.h.m)this.g.c()).EndInvoke(this.g.d());
			}
			finally
			{
				this.g = null;
				base.k(false);
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000A90C4 File Offset: 0x000A80C4
		private new void b(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, EmailAddressCollection A_6, EmailAddressCollection A_7, bool A_8, bool A_9, global::a.d.k A_10, string A_11, global::a.n.a A_12)
		{
			ao ao = (A_0 == null) ? null : A_0.n();
			global::a.d.n n = (global::a.d.n)this.b;
			int num = -1;
			int a_ = 0;
			int num2 = 1;
			global::a.d.o o = null;
			if (this.b != null && this.b.bq())
			{
				o = (global::a.d.o)this.b;
			}
			A_6.Clear();
			A_7.Clear();
			this.d.b(string.Format(Resources.Instance.Log_SmtpSubmittingSenderAndRecipients, new object[0]), null, LogMessageType.Info, this);
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(312);
			}
			if (A_2 == null || A_2.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(314);
			}
			if (A_8 && this.j > 0 && ao.e() > this.j)
			{
				throw new MailBeeSmtpMessageSizeOutOfRangeException(320, base.a1(), A_0, A_1, A_2, this.j);
			}
			this.a.d().cf();
			string text = string.Empty;
			if (A_8 && this.j > 0)
			{
				text = " SIZE=" + ao.e().ToString();
			}
			string text2 = string.Empty;
			if (this.d && A_3 != null)
			{
				text2 = A_3.a(this.k);
			}
			string text3 = string.Empty;
			if (A_4 != Smtp8bitDataConversion.DoNothing)
			{
				if (!this.e && this.g)
				{
					text3 = " BODY=8BITMIME";
				}
				else if (this.e && this.f)
				{
					text3 = " BODY=BINARYMIME";
				}
			}
			this.a.d().b = this.f;
			this.a.d().g(string.Concat(new string[]
			{
				"MAIL FROM:<",
				A_1,
				">",
				text,
				text2,
				text3,
				"\r\n"
			}), new global::a.d.m(true, false, false, false), 0);
			num++;
			at at;
			if (!this.f)
			{
				at = this.a.d().p().a(num);
				if (at.t() == af.c)
				{
					throw new MailBeeSmtpRefusedSenderException(313, base.a1(), at, A_0, A_1, A_2);
				}
				this.a.d().q(num);
				if (o != null && o.l5() && !n.bf())
				{
					this.a(A_0, A_1);
				}
			}
			for (int i = 0; i < A_2.Count; i++)
			{
				if (this.d && A_3 != null)
				{
					text2 = A_3.a(A_2[i].Email, this.k);
				}
				this.a.d().g(string.Concat(new string[]
				{
					"RCPT TO:<",
					A_2[i].Email,
					">",
					text2,
					"\r\n"
				}), new global::a.d.m(true, false, false, false), 0);
				num++;
				if (!this.f)
				{
					at = this.a.d().p().a(num);
					switch (at.t())
					{
					case af.a:
						A_6.Add(A_2[i]);
						if (o != null && o.l7() && !n.bf())
						{
							this.a(A_0, A_2[i].Email, true, true, at.o());
						}
						break;
					case af.c:
					{
						bool flag = A_5;
						MailBeeException ex = new MailBeeSmtpRefusedRecipientException(315, base.a1(), at, A_0, A_1, A_2, i);
						A_7.Add(A_2[i]);
						if (o != null && o.l7() && !n.bf())
						{
							flag = this.a(A_0, A_2[i].Email, false, flag, at.o());
						}
						bool flag2 = num < this.a.d().p().Count - 1 && this.a.d().p().a(num + 1).t() == af.d;
						if (!flag || flag2)
						{
							throw ex;
						}
						base.c(ex);
						break;
					}
					case af.d:
						this.a.d().q(num);
						break;
					}
				}
			}
			if (!this.f && this.a.d().q().t() == af.d)
			{
				this.a.d().g(this.a.d().q());
			}
			if (this.f)
			{
				this.a.d().o(0);
				at = this.a.d().p().a(a_);
				if (at.t() == af.c)
				{
					throw new MailBeeSmtpRefusedSenderException(313, base.a1(), at, A_0, A_1, A_2);
				}
				this.a.d().q(a_);
				if (o != null && o.l5() && !n.bf())
				{
					this.a(A_0, A_1);
				}
				for (int j = 0; j < A_2.Count; j++)
				{
					at = this.a.d().p().a(num2 + j);
					switch (at.t())
					{
					case af.a:
					case af.b:
						A_6.Add(A_2[j]);
						if (o != null && o.l7() && !n.bf())
						{
							this.a(A_0, A_2[j].Email, true, true, at.o());
						}
						break;
					case af.c:
					{
						bool flag3 = A_5;
						MailBeeException ex2 = new MailBeeSmtpRefusedRecipientException(315, base.a1(), at, A_0, A_1, A_2, j);
						A_7.Add(A_2[j]);
						if (o != null && o.l7() && !n.bf())
						{
							flag3 = this.a(A_0, A_2[j].Email, false, flag3, at.o());
						}
						bool flag4 = num2 + j < this.a.d().p().Count - 1 && this.a.d().p().a(num2 + j + 1).t() == af.d;
						if (!flag3 || flag4)
						{
							throw ex2;
						}
						base.c(ex2);
						break;
					}
					case af.d:
						this.a.d().q(num2 + j);
						break;
					}
				}
				if (this.a.d().q().t() == af.d)
				{
					this.a.d().g(this.a.d().q());
				}
			}
			if (A_6.Count == 0)
			{
				throw new MailBeeSmtpNoAcceptedRecipientsException(316, base.a1(), A_0, A_1, A_2);
			}
			this.d.b(string.Format(Resources.Instance.Log_SmtpSenderAndRecipientsAccepted, new object[0]), null, LogMessageType.Info, this);
			if (!A_8)
			{
				this.h = true;
				this.i++;
				this.a.d().b = false;
				this.d.b(string.Format(Resources.Instance.Log_SmtpTestSendDone, new object[0]), null, LogMessageType.Info, this);
				this.k();
				return;
			}
			this.d.b(string.Format(Resources.Instance.Log_SmtpSubmittingMessageData, new object[0]), null, LogMessageType.Info, this);
			if (!this.e)
			{
				this.a.d().g("DATA\r\n", new global::a.d.m(true, false, false, false), 0);
				this.a.d().o(0);
				num++;
				at = this.a.d().p().a(num);
				if (at.t() == af.c)
				{
					throw new MailBeeSmtpRefusedDataException(317, base.a1(), at, A_0, A_1, A_2);
				}
				this.a.d().g(at);
			}
			this.a.d().cf();
			num = -1;
			if (((this.e && !this.f) || (!this.e && !this.g)) && A_4 != Smtp8bitDataConversion.DoNothing && w.a(ao.d(), ao.b(), ao.e()))
			{
				if (A_4 != Smtp8bitDataConversion.ConvertAndForget)
				{
					MailBeeException ex3 = new MailBeeSmtp8bitDataNotSupportedException(330, base.a1(), A_0, A_1, A_2);
					if (A_4 == Smtp8bitDataConversion.ThrowException)
					{
						throw ex3;
					}
					base.c(ex3);
				}
				if (A_4 == Smtp8bitDataConversion.ConvertAndForget || A_4 == Smtp8bitDataConversion.ConvertAndWarn)
				{
					ao = new ao(Encoding.Convert(Encoding.GetEncoding(1252), Encoding.ASCII, ao.d(), ao.b(), ao.e()));
				}
			}
			if (this.e)
			{
				int num3 = this.a.d().hy();
				try
				{
					bool a_2 = true;
					bool a_3 = false;
					int k = ao.b();
					int num4 = 0;
					while (k < ao.e())
					{
						int num5;
						string a_4;
						if (k < ao.b() + ao.e() - Global.TcpBufSize)
						{
							num5 = Global.TcpBufSize;
							a_4 = "BDAT " + Convert.ToString(Global.TcpBufSize) + "\r\n";
						}
						else
						{
							num5 = ao.b() + ao.e() - k;
							a_4 = "BDAT " + Convert.ToString(num5) + " LAST\r\n";
							if (this.a.d().hy() < 35000)
							{
								this.a.d().hz(35000);
							}
							a_3 = true;
						}
						this.a.d().h(a_4, new global::a.d.m(false, false, false, false));
						this.a.d().g(ao.d(), k, num5, new global::a.d.m(true, true, a_2, a_3), 0);
						num++;
						a_2 = false;
						if (this.f)
						{
							this.a.d().t();
						}
						else
						{
							at = this.a.d().p().a(num);
							if (at.t() == af.c)
							{
								throw new MailBeeSmtpRefusedDataException(319, base.a1(), at, A_0, A_1, A_2);
							}
							this.a.d().g(at);
						}
						if (o != null && o.l9() && !n.bf())
						{
							this.a(A_0, num5, num5 + k - ao.b(), ao.e());
						}
						this.a.d().u();
						k += Global.TcpBufSize;
						num4++;
					}
					if (this.f)
					{
						this.a.d().o(0);
						for (int l = 0; l <= num; l++)
						{
							at = this.a.d().p().a(l);
							if (at.t() == af.c)
							{
								throw new MailBeeSmtpRefusedDataException(319, base.a1(), at, A_0, A_1, A_2);
							}
							this.a.d().q(l);
						}
					}
					goto IL_DCF;
				}
				finally
				{
					if (num3 < this.a.d().hy())
					{
						this.a.d().hz(num3);
					}
				}
			}
			ArrayList arrayList = w.a(ao.d(), ao.b(), ao.e(), w.a);
			int num6 = 0;
			byte[] a_5 = new byte[]
			{
				46
			};
			bool a_6 = true;
			bool a_7 = false;
			int m = ao.b();
			int num7 = 0;
			while (m < ao.e())
			{
				int num8 = 0;
				int num9 = 0;
				bool flag5 = false;
				if (num6 < arrayList.Count && (int)arrayList[num6] < m + Global.TcpBufSize)
				{
					num8 = (int)arrayList[num6] - m + 3;
					num9 = num8 + 1;
					flag5 = true;
					num6++;
				}
				if (!flag5)
				{
					if (m < ao.b() + ao.e() - Global.TcpBufSize)
					{
						num8 = Global.TcpBufSize;
					}
					else
					{
						num8 = ao.b() + ao.e() - m;
						a_7 = true;
					}
					num9 = num8;
				}
				this.a.d().h(ao.d(), m, num8, new global::a.d.m(false, true, a_6, a_7));
				a_6 = false;
				if (flag5)
				{
					this.a.d().h(a_5, new global::a.d.m(false, true, true, true));
				}
				if (this.f)
				{
					this.a.d().t();
				}
				if (o != null && o.l9() && !n.bf())
				{
					this.a(A_0, num9, num9 + m - ao.b(), ao.e());
				}
				this.a.d().u();
				m += num8;
				num7++;
			}
			int num10 = this.a.d().hy();
			if (this.a.d().hy() < 35000)
			{
				this.a.d().hz(35000);
			}
			try
			{
				if (ao.e() >= 2 && w.b(ao.d(), ao.b() + ao.e() - 2, 2) > -1)
				{
					this.a.d().g(".\r\n", new global::a.d.m(true, true, true, true), 0);
				}
				else
				{
					this.a.d().g("\r\n.\r\n", new global::a.d.m(true, true, true, true), 0);
				}
				num++;
				this.a.d().o(0);
			}
			finally
			{
				if (num10 < this.a.d().hy())
				{
					this.a.d().hz(num10);
				}
			}
			at = this.a.d().p().a(num);
			if (at.t() == af.c)
			{
				throw new MailBeeSmtpRefusedDataException(318, base.a1(), at, A_0, A_1, A_2);
			}
			this.a.d().q(num);
			IL_DCF:
			this.h = true;
			this.i++;
			this.a.d().b = false;
			this.d.b(string.Format(Resources.Instance.Log_SmtpSendDone, new object[0]), null, LogMessageType.Info, this);
			if (o != null && o.mb() && !n.bf())
			{
				this.a(A_0, A_1, A_2, A_6, A_7);
			}
			if (!A_9 && o != null && o.md() && !n.bf())
			{
				this.a(A_0, A_1, A_2, A_6, A_7, A_10, A_11, A_12);
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000A9F6C File Offset: 0x000A8F6C
		public new int f()
		{
			return this.j;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000A9F74 File Offset: 0x000A8F74
		public EmailAddressCollection r()
		{
			return this.k;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000A9F7C File Offset: 0x000A8F7C
		public new EmailAddressCollection h()
		{
			return this.l;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000A9F84 File Offset: 0x000A8F84
		public new EmailAddressCollection m()
		{
			return this.m;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000A9F8C File Offset: 0x000A8F8C
		public new global::a.a.c l()
		{
			return this.n;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000A9F94 File Offset: 0x000A8F94
		public new bool p()
		{
			return this.h;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000A9F9C File Offset: 0x000A8F9C
		public override void f0(al A_0)
		{
			base.f0(A_0);
			this.pd(A_0.ab());
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000A9FB4 File Offset: 0x000A8FB4
		protected override Task f1(MailBeeException A_0)
		{
			global::a.d.h.l l;
			l.c = this;
			l.d = A_0;
			l.b = AsyncTaskMethodBuilder.Create();
			l.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = l.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000AA004 File Offset: 0x000A9004
		private new Task a(string A_0, bool A_1, ExtendedSmtpOptions A_2, SaslMethod A_3)
		{
			global::a.d.h.a a;
			a.d = this;
			a.e = A_0;
			a.g = A_1;
			a.c = A_2;
			a.h = A_3;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000AA06C File Offset: 0x000A906C
		public new Task j()
		{
			global::a.d.h.r r;
			r.c = this;
			r.b = AsyncTaskMethodBuilder.Create();
			r.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = r.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000AA0B4 File Offset: 0x000A90B4
		public override Task fs(bool A_0)
		{
			global::a.d.h.j j;
			j.c = this;
			j.d = A_0;
			j.b = AsyncTaskMethodBuilder.Create();
			j.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = j.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000AA104 File Offset: 0x000A9104
		public override Task fr()
		{
			global::a.d.h.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder.Create();
			f.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = f.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000AA14C File Offset: 0x000A914C
		public Task q()
		{
			global::a.d.h.q q;
			q.c = this;
			q.b = AsyncTaskMethodBuilder.Create();
			q.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = q.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.q>(ref q);
			return q.b.Task;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000AA194 File Offset: 0x000A9194
		public override Task f2()
		{
			global::a.d.h.n n;
			n.c = this;
			n.b = AsyncTaskMethodBuilder.Create();
			n.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000AA1DC File Offset: 0x000A91DC
		public override Task f3(bool A_0)
		{
			global::a.d.h.o o;
			o.c = this;
			o.d = A_0;
			o.b = AsyncTaskMethodBuilder.Create();
			o.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = o.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000AA22C File Offset: 0x000A922C
		public virtual Task f7(string A_0, int A_1, string A_2, string A_3)
		{
			global::a.d.d d = (global::a.d.d)this.k;
			return this.a(A_0, A_1, A_2, A_3, d.l());
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000AA258 File Offset: 0x000A9258
		private new Task a(string A_0, int A_1, string A_2, string A_3, bool A_4)
		{
			global::a.d.h.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.g = A_3;
			b.h = A_4;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000AA2C8 File Offset: 0x000A92C8
		public virtual Task f8(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11)
		{
			global::a.d.h.c c;
			c.c = this;
			c.h = A_0;
			c.e = A_1;
			c.d = A_2;
			c.i = A_3;
			c.j = A_4;
			c.f = A_5;
			c.l = A_6;
			c.g = A_7;
			c.m = A_8;
			c.n = A_9;
			c.o = A_10;
			c.p = A_11;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000AA378 File Offset: 0x000A9378
		private new Task a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, EmailAddressCollection A_6, EmailAddressCollection A_7, bool A_8, bool A_9, global::a.d.k A_10, string A_11, global::a.n.a A_12)
		{
			global::a.d.h.d d;
			d.d = this;
			d.c = A_0;
			d.g = A_1;
			d.h = A_2;
			d.k = A_3;
			d.l = A_4;
			d.r = A_5;
			d.e = A_6;
			d.f = A_7;
			d.i = A_8;
			d.am = A_9;
			d.an = A_10;
			d.ao = A_11;
			d.ap = A_12;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.d.h.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000AA42F File Offset: 0x000A942F
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(MailBeeException A_0)
		{
			return base.f1(A_0);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000AA438 File Offset: 0x000A9438
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task b(bool A_0)
		{
			return base.fs(A_0);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000AA441 File Offset: 0x000A9441
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task b()
		{
			return base.fr();
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000AA449 File Offset: 0x000A9449
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task a()
		{
			return base.f2();
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000AA451 File Offset: 0x000A9451
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task a(bool A_0)
		{
			return base.f3(A_0);
		}

		// Token: 0x0400197B RID: 6523
		private new const int a = 35000;

		// Token: 0x0400197C RID: 6524
		protected new bool b;

		// Token: 0x0400197D RID: 6525
		protected new bool c;

		// Token: 0x0400197E RID: 6526
		protected new bool d;

		// Token: 0x0400197F RID: 6527
		protected new bool e;

		// Token: 0x04001980 RID: 6528
		protected new bool f;

		// Token: 0x04001981 RID: 6529
		protected new bool g;

		// Token: 0x04001982 RID: 6530
		protected new bool h;

		// Token: 0x04001983 RID: 6531
		protected new int i;

		// Token: 0x04001984 RID: 6532
		protected new int j;

		// Token: 0x04001985 RID: 6533
		protected new EmailAddressCollection k;

		// Token: 0x04001986 RID: 6534
		protected new EmailAddressCollection l;

		// Token: 0x04001987 RID: 6535
		protected new EmailAddressCollection m;

		// Token: 0x04001988 RID: 6536
		protected global::a.a.c n;

		// Token: 0x04001989 RID: 6537
		private new global::a.d.h.k o;

		// Token: 0x0400198A RID: 6538
		private new global::a.d.h.g p;

		// Token: 0x0400198B RID: 6539
		private global::a.d.h.h q;

		// Token: 0x0400198C RID: 6540
		private global::a.d.h.i r;

		// Token: 0x0400198D RID: 6541
		private new global::a.d.h.p s;

		// Token: 0x0400198E RID: 6542
		private new global::a.d.h.e t;

		// Token: 0x02000440 RID: 1088
		// (Invoke) Token: 0x060025D1 RID: 9681
		protected new delegate void k(MailMessage A_0, string A_1, bc A_2);

		// Token: 0x02000441 RID: 1089
		// (Invoke) Token: 0x060025D5 RID: 9685
		protected new delegate void g(SmtpMessageRecipientSubmittedEventArgs A_0, bc A_1);

		// Token: 0x02000442 RID: 1090
		// (Invoke) Token: 0x060025D9 RID: 9689
		protected new delegate void h(MailMessage A_0, int A_1, int A_2, int A_3, bc A_4);

		// Token: 0x02000443 RID: 1091
		// (Invoke) Token: 0x060025DD RID: 9693
		protected new delegate void i(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5);

		// Token: 0x02000444 RID: 1092
		// (Invoke) Token: 0x060025E1 RID: 9697
		protected new delegate void p(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7, bc A_8);

		// Token: 0x02000445 RID: 1093
		// (Invoke) Token: 0x060025E5 RID: 9701
		protected new delegate void e(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7);

		// Token: 0x02000446 RID: 1094
		// (Invoke) Token: 0x060025E9 RID: 9705
		public new delegate void m(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11);
	}
}
