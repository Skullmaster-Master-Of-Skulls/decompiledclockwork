using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using a.g;
using MailBee;
using MailBee.AddressCheck;
using MailBee.AntiSpam;
using MailBee.DnsMX;
using MailBee.Mime;
using MailBee.Pop3Mail;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000452 RID: 1106
	internal class n : global::a.h, global::a.d.o
	{
		// Token: 0x06002602 RID: 9730 RVA: 0x000AE674 File Offset: 0x000AD674
		public n(Smtp A_0, EmailAddressValidator A_1, RblFilter A_2)
		{
			this.a = A_0;
			this.c = A_1;
			this.e = A_2;
			this.q = new DnsServerCollection();
			this.r = new SmtpServerCollection();
			this.o = new DeliveryNotificationOptions();
			this.p = Smtp8bitDataConversion.DoNothing;
			this.g = new object();
			this.i = new SendMailJobCollection(false, this.g);
			this.k = new SendMailJobCollection(false, this.g);
			this.m = new SendMailJobCollection(true, this.g);
			this.n = new SendMailJobCollection(false, this.g);
			this.t = new ae();
			this.u = 1;
			this.x = false;
			this.s = new DirectSendServerConfig();
			this.v = new MailMessage();
			this.w = string.Empty;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000AE751 File Offset: 0x000AD751
		protected override void f9()
		{
			this.p = null;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000AE75C File Offset: 0x000AD75C
		internal override void ll(bf A_0, byte[] A_1, bc A_2)
		{
			global::a.d.m m = A_0 as global::a.d.m;
			if (A_2.a8().Enabled)
			{
				if (m != null && m.c && !m.j)
				{
					if (A_0.h > Global.MaxMultiLineDataLength)
					{
						A_2.a8().b(base.a(A_0, A_1), string.Format(Resources.Instance.Log_0BytesSent, Convert.ToString(A_0.h)), LogMessageType.Send, A_2);
						return;
					}
					base.ll(A_0, A_1, A_2);
					return;
				}
				else
				{
					base.ll(A_0, A_1, A_2);
				}
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000AE7E0 File Offset: 0x000AD7E0
		public override void lm(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (A_3.a8().Enabled && A_3 is global::a.g.p)
			{
				A_3.a8().a(A_0, A_1, A_2, LogMessageType.Recv, A_3);
			}
			base.lm(A_0, A_1, A_2, A_3);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000AE816 File Offset: 0x000AD816
		public override void ln(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (A_3.a8().Enabled && A_3 is global::a.g.p)
			{
				A_3.a8().a(A_0, A_1, A_2, LogMessageType.Send, A_3);
			}
			base.ln(A_0, A_1, A_2, A_3);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000AE84C File Offset: 0x000AD84C
		public new bool a(bool A_0)
		{
			if (A_0)
			{
				this.z();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					base.a5();
				}
				else
				{
					try
					{
						base.a5();
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

		// Token: 0x06002608 RID: 9736 RVA: 0x000AE8D8 File Offset: 0x000AD8D8
		public new IAsyncResult a(AsyncCallback A_0, object A_1)
		{
			this.z();
			this.p.k(true);
			base.bl();
			global::a.h.d d = new global::a.h.d(this.a);
			this.q = new global::a.o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000AE932 File Offset: 0x000AD932
		public bool u()
		{
			return base.a3();
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000AE93A File Offset: 0x000AD93A
		public override bool lo(bool A_0)
		{
			if (A_0 && !(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.lo(A_0);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000AE95B File Offset: 0x000AD95B
		public override IAsyncResult lp(AsyncCallback A_0, object A_1)
		{
			if (this.p is global::a.d.h)
			{
				return base.lp(A_0, A_1);
			}
			throw new MailBeeInvalidStateException(9);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000AE97A File Offset: 0x000AD97A
		public override bool lq(bool A_0)
		{
			if (A_0 && !(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.lq(A_0);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000AE99B File Offset: 0x000AD99B
		private void b(string A_0, int A_1, string A_2, string A_3)
		{
			global::a.d.h h = (global::a.d.h)this.p;
			h.pa();
			h.ay();
			h.f4(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000AE9C0 File Offset: 0x000AD9C0
		public new bool a(bool A_0, string A_1, int A_2, string A_3, string A_4)
		{
			if (A_0)
			{
				this.z();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						this.b(A_1, A_2, A_3, A_4);
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

		// Token: 0x0600260F RID: 9743 RVA: 0x000AEA58 File Offset: 0x000ADA58
		public new IAsyncResult a(string A_0, int A_1, string A_2, string A_3, AsyncCallback A_4, object A_5)
		{
			this.z();
			this.p.k(true);
			base.bl();
			global::a.d.n.q q = new global::a.d.n.q(this.a);
			this.q = new global::a.o(q, null);
			this.q.a(q.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5));
			return this.q;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000AEABC File Offset: 0x000ADABC
		public bool at()
		{
			if (this.q == null || !(this.q.c() is global::a.d.n.q))
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
				result = ((global::a.d.n.q)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000AEB54 File Offset: 0x000ADB54
		private void h(string A_0)
		{
			this.p.pa();
			((global::a.ab)this.p).au();
			((global::a.ab)this.p).o0(A_0, true);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000AEB84 File Offset: 0x000ADB84
		public new bool c(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.z();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.h(A_1);
				}
				else
				{
					try
					{
						this.h(A_1);
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

		// Token: 0x06002613 RID: 9747 RVA: 0x000AEC10 File Offset: 0x000ADC10
		public new IAsyncResult a(string A_0, AsyncCallback A_1, object A_2)
		{
			this.z();
			this.p.k(true);
			base.bl();
			global::a.d.n.g g = new global::a.d.n.g(this.c);
			this.q = new global::a.o(g, null);
			this.q.a(g.BeginInvoke(false, A_0, A_1, A_2));
			return this.q;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000AEC6C File Offset: 0x000ADC6C
		public bool ap()
		{
			if (this.q == null || !(this.q.c() is global::a.d.n.g))
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
				result = ((global::a.d.n.g)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000AED04 File Offset: 0x000ADD04
		private void g()
		{
			global::a.d.h h = (global::a.d.h)this.p;
			h.pa();
			h.o();
			h.e();
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000AED24 File Offset: 0x000ADD24
		public bool f(bool A_0)
		{
			if (A_0)
			{
				this.z();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.g();
				}
				else
				{
					try
					{
						this.g();
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

		// Token: 0x06002617 RID: 9751 RVA: 0x000AEDB0 File Offset: 0x000ADDB0
		public IAsyncResult b(AsyncCallback A_0, object A_1)
		{
			this.z();
			this.p.k(true);
			base.bl();
			global::a.h.d d = new global::a.h.d(this.f);
			this.q = new global::a.o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000AEE0A File Offset: 0x000ADE0A
		public bool aj()
		{
			return base.a3();
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000AEE12 File Offset: 0x000ADE12
		public override bool lr(bool A_0)
		{
			if (A_0 && !(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.lr(A_0);
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000AEE34 File Offset: 0x000ADE34
		public new bool c(bool A_0)
		{
			if (A_0)
			{
				this.z();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					base.a7();
				}
				else
				{
					try
					{
						base.a7();
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

		// Token: 0x0600261B RID: 9755 RVA: 0x000AEEC0 File Offset: 0x000ADEC0
		public new IAsyncResult c(AsyncCallback A_0, object A_1)
		{
			this.z();
			this.p.k(true);
			base.bl();
			global::a.h.d d = new global::a.h.d(this.c);
			this.q = new global::a.o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000AEF1A File Offset: 0x000ADF1A
		public bool ac()
		{
			return base.a3();
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000AEF24 File Offset: 0x000ADF24
		protected new void e(bool A_0)
		{
			global::a.d.p p = this.p as global::a.d.p;
			if (p != null)
			{
				if (p.be())
				{
					throw new MailBeeInvalidStateException(9);
				}
				this.p = null;
			}
			if (!A_0)
			{
				global::a.d.h h = this.p as global::a.d.h;
				if (h != null)
				{
					if (h.be())
					{
						throw new MailBeeInvalidStateException(9);
					}
					this.p = null;
				}
			}
			if (this.p == null)
			{
				this.p = new global::a.d.f(this, null, this.m, 0);
				global::a.d.f f = (global::a.d.f)this.p;
				f.a(this.r);
				f.a(this.q);
				f.a(this.s);
			}
			this.p.hc(this.n);
			this.p.hd(this.o);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000AEFEC File Offset: 0x000ADFEC
		protected void ao()
		{
			this.e(true);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000AEFF8 File Offset: 0x000ADFF8
		protected global::a.d.h z()
		{
			global::a.d.h h = this.p as global::a.d.h;
			if (this.p == null || h != null || !this.p.be())
			{
				if (h == null)
				{
					if (this.p != null)
					{
						this.p.he();
					}
					h = new global::a.d.i(this, null, this.m, 0);
					this.p = h;
					((global::a.d.i)h).a(this.r);
				}
				h.hc(this.n);
				h.hd(this.o);
				return h;
			}
			throw new MailBeeInvalidStateException(9);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000AF088 File Offset: 0x000AE088
		protected global::a.d.p an()
		{
			global::a.d.p p = this.p as global::a.d.p;
			if (this.p == null || p != null || !this.p.be())
			{
				if (p == null)
				{
					if (this.p != null)
					{
						this.p.he();
					}
					p = new global::a.d.p(this, null, this.m, 0);
					this.p = p;
					p.a(this.q);
					p.a(this.r);
					p.a(this.g);
					p.c(this.k);
					p.b(this.i);
					p.d(this.m);
					p.a(this.n);
				}
				p.hc(this.n);
				p.hd(this.o);
				p.a(this.s);
				p.c(this.x);
				p.a(this.t);
				p.b(this.u);
				return p;
			}
			throw new MailBeeInvalidStateException(9);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000AF190 File Offset: 0x000AE190
		protected new void a(string A_0, string A_1, EmailAddressCollection A_2)
		{
			this.p.pa();
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			MailMessage mailMessage = new MailMessage();
			mailMessage.LoadMessage(A_0);
			if (this.p is global::a.d.h)
			{
				((global::a.d.h)this.p).f5(mailMessage, A_1, A_2, this.o, this.p, true, true, SendFailureThreshold.Default, false, null, null, null);
				return;
			}
			if (this.p is global::a.d.f)
			{
				((global::a.d.f)this.p).a(mailMessage, A_1, A_2, this.o, this.p, true, true, true, SendFailureThreshold.Default, this.u, this.t, false, null, null, null);
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000AF244 File Offset: 0x000AE244
		public new bool a(bool A_0, string A_1, string A_2, EmailAddressCollection A_3)
		{
			if (A_0)
			{
				this.ao();
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						this.a(A_1, A_2, A_3);
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

		// Token: 0x06002623 RID: 9763 RVA: 0x000AF2D8 File Offset: 0x000AE2D8
		public new IAsyncResult a(string A_0, string A_1, EmailAddressCollection A_2, AsyncCallback A_3, object A_4)
		{
			this.ao();
			this.p.k(true);
			base.bl();
			global::a.d.n.m m = new global::a.d.n.m(this.a);
			this.q = new global::a.o(m, null);
			this.q.a(m.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4));
			return this.q;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000AF338 File Offset: 0x000AE338
		public bool ay()
		{
			if (this.q == null || !(this.q.c() is global::a.d.n.m))
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
				result = ((global::a.d.n.m)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000AF3D0 File Offset: 0x000AE3D0
		private void b(string A_0, EmailAddressCollection A_1)
		{
			if (A_0 == null)
			{
				A_0 = this.v.From.Email;
			}
			if (A_1 == null)
			{
				A_1 = this.v.GetAllRecipients();
			}
			EmailAddressCollection emailAddressCollection = global::a.d.a.a(this.v, ref this.w);
			try
			{
				if (this.p is global::a.d.h)
				{
					((global::a.d.h)this.p).f5(this.v, A_0, A_1, this.o, this.p, true, true, SendFailureThreshold.Default, false, null, null, null);
				}
				else if (this.p is global::a.d.f)
				{
					((global::a.d.f)this.p).a(this.v, A_0, A_1, this.o, this.p, true, true, true, SendFailureThreshold.Default, this.u, this.t, false, null, null, null);
				}
			}
			finally
			{
				if (emailAddressCollection != null)
				{
					this.v.Bcc.Add(emailAddressCollection);
				}
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000AF4BC File Offset: 0x000AE4BC
		public new bool a(bool A_0, string A_1, EmailAddressCollection A_2)
		{
			if (A_0)
			{
				this.ao();
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

		// Token: 0x06002627 RID: 9767 RVA: 0x000AF54C File Offset: 0x000AE54C
		public new bool d(bool A_0)
		{
			return this.a(A_0, this.v.From.Email, this.v.GetAllRecipients());
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000AF570 File Offset: 0x000AE570
		public new IAsyncResult a(string A_0, EmailAddressCollection A_1, AsyncCallback A_2, object A_3)
		{
			this.ao();
			this.p.k(true);
			base.bl();
			global::a.d.n.aa aa = new global::a.d.n.aa(this.a);
			this.q = new global::a.o(aa, null);
			this.q.a(aa.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000AF5CC File Offset: 0x000AE5CC
		public bool aw()
		{
			if (this.q == null || !(this.q.c() is global::a.d.n.aa))
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
				result = ((global::a.d.n.aa)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000AF664 File Offset: 0x000AE664
		private string[] g(string A_0)
		{
			return ((global::a.d.f)this.p).a(A_0, this.u, this.t);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000AF684 File Offset: 0x000AE684
		public new string[] d(bool A_0, string A_1)
		{
			string[] result = null;
			if (A_0)
			{
				this.e(false);
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.g(A_1);
				}
				else
				{
					try
					{
						result = this.g(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
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

		// Token: 0x0600262C RID: 9772 RVA: 0x000AF714 File Offset: 0x000AE714
		private string[] f(string A_0)
		{
			return ((global::a.d.f)this.p).c(A_0);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000AF728 File Offset: 0x000AE728
		public string[] b(bool A_0, string A_1)
		{
			string[] result = null;
			if (A_0)
			{
				this.e(false);
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.f(A_1);
				}
				else
				{
					try
					{
						result = this.f(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
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

		// Token: 0x0600262E RID: 9774 RVA: 0x000AF7B8 File Offset: 0x000AE7B8
		private new string[] e(string A_0)
		{
			return ((global::a.d.f)this.p).b(A_0);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000AF7CC File Offset: 0x000AE7CC
		public new string[] a(bool A_0, string A_1)
		{
			string[] result = null;
			if (A_0)
			{
				this.e(false);
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.e(A_1);
				}
				else
				{
					try
					{
						result = this.e(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
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

		// Token: 0x06002630 RID: 9776 RVA: 0x000AF85C File Offset: 0x000AE85C
		private bool b(string A_0, string A_1)
		{
			return ((global::a.d.f)this.p).a(A_0, A_1);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000AF870 File Offset: 0x000AE870
		public new bool a(bool A_0, string A_1, string A_2)
		{
			bool result = false;
			if (A_0)
			{
				this.e(false);
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2);
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
			return result;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000AF904 File Offset: 0x000AE904
		private RblStatusCollection b(string A_0, string[] A_1)
		{
			return ((global::a.d.f)this.p).a(A_0, A_1, this.u, this.t);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000AF924 File Offset: 0x000AE924
		public RblStatusCollection b(bool A_0, string A_1, string[] A_2)
		{
			RblStatusCollection result = null;
			if (A_0)
			{
				this.e(false);
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
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

		// Token: 0x06002634 RID: 9780 RVA: 0x000AF9B8 File Offset: 0x000AE9B8
		public new void c(string A_0, string A_1, EmailAddressCollection A_2)
		{
			this.an().a(A_0, this.v.Clone(), null, false, A_1, A_2, this.o.a(), this.p, true, true, true, SendFailureThreshold.Default, 1);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000AF9F8 File Offset: 0x000AE9F8
		public new void a(string A_0, MailMessage A_1, string A_2, EmailAddressCollection A_3)
		{
			this.an().a(A_0, A_1, null, false, A_2, A_3, this.o.a(), this.p, true, true, true, SendFailureThreshold.Default, 1);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000AFA30 File Offset: 0x000AEA30
		public new void a(string A_0, string A_1, bool A_2, string A_3, EmailAddressCollection A_4)
		{
			this.an().a(A_0, null, A_1, A_2, A_3, A_4, this.o.a(), this.p, true, true, true, SendFailureThreshold.Default, 1);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000AFA68 File Offset: 0x000AEA68
		public new void a(string A_0, string A_1, EmailAddressCollection A_2, DataTable A_3, IDataReader A_4)
		{
			this.a(A_0, A_1, A_2, A_3, null, A_4, true, true, true, AddressValidationLevel.OK, null, -1, null, true, false);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000AFA8C File Offset: 0x000AEA8C
		public new void a(string A_0, string A_1, EmailAddressCollection A_2, DataTable A_3, object A_4, IDataReader A_5, bool A_6, bool A_7, bool A_8, AddressValidationLevel A_9, string A_10, int A_11, Regex A_12, bool A_13, bool A_14)
		{
			if (A_3 == null && A_5 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string[] array = null;
			if (A_5 != null)
			{
				array = new string[A_5.FieldCount];
				for (int i = 0; i < A_5.FieldCount; i++)
				{
					array[i] = A_5.GetName(i);
				}
			}
			this.an().a(A_0, (A_9 == AddressValidationLevel.OK) ? this.v.Clone() : null, A_1, A_2, (A_9 == AddressValidationLevel.OK) ? this.o.a() : null, A_3, A_4, A_5, array, this.p, A_6, A_7, A_8, A_9, A_10, A_11, A_12, SendFailureThreshold.Default, -1, A_13, A_14);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000AFB2E File Offset: 0x000AEB2E
		public void af()
		{
			this.an().p();
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000AFB3C File Offset: 0x000AEB3C
		public bool y()
		{
			global::a.d.p p = this.an();
			this.p.k(true);
			try
			{
				if (this.i && this.k)
				{
					p.l();
					p.e();
					p.r();
				}
				else
				{
					try
					{
						p.l();
						p.e();
						p.r();
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
				this.p.k(false);
			}
			return true;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000AFBD8 File Offset: 0x000AEBD8
		public new IAsyncResult d(AsyncCallback A_0, object A_1)
		{
			global::a.d.p p = this.an();
			this.p.k(true);
			base.bl();
			p.l();
			this.q = new global::a.o(p.d(), A_0, A_1);
			p.a(this.q);
			this.t.a++;
			new Thread(new ThreadStart(p.e)).Start();
			return this.q;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000AFC54 File Offset: 0x000AEC54
		public bool @as()
		{
			if (this.q == null || this.q.c() != null)
			{
				throw new MailBeeInvalidStateException(4);
			}
			global::a.d.p p = this.p as global::a.d.p;
			if (p == null)
			{
				throw new MailBeeInvalidStateException(9);
			}
			base.bh();
			p.d().WaitOne();
			try
			{
				p.r();
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
			finally
			{
				this.t.a--;
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return false;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000AFD10 File Offset: 0x000AED10
		public void r()
		{
			global::a.d.p p = this.p as global::a.d.p;
			if (p == null)
			{
				throw new MailBeeInvalidStateException(9);
			}
			p.v();
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000AFD3C File Offset: 0x000AED3C
		private new string a(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4)
		{
			return ((global::a.d.f)this.p).a(this.v, A_0, A_1, A_2, A_3, A_4, null, null, ref this.w);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000AFD70 File Offset: 0x000AED70
		public new string c(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4)
		{
			this.e(false);
			this.p.k(true);
			string result;
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_0, A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						result = this.a(A_0, A_1, A_2, A_3, A_4);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						result = null;
					}
				}
			}
			finally
			{
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000AFE04 File Offset: 0x000AEE04
		public new bool a(string A_0, bool A_1)
		{
			global::a.d.p p = this.an();
			this.p.k(true);
			try
			{
				p.l();
				p.a(A_0, A_1);
				p.r();
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
			finally
			{
				this.p.k(false);
			}
			return true;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000AFE7C File Offset: 0x000AEE7C
		private TestSendResult b(string A_0, EmailAddressCollection A_1, SendFailureThreshold A_2)
		{
			bool flag = false;
			this.p.pa();
			TestSendResult testSendResult = TestSendResult.OK;
			try
			{
				try
				{
					if (this.p is global::a.d.h)
					{
						flag = ((global::a.d.h)this.p).ao();
						((global::a.d.h)this.p).f5(this.v, A_0, A_1, this.o, this.p, true, false, A_2, false, null, null, null);
					}
					else
					{
						((global::a.d.f)this.p).a(this.v, A_0, A_1, this.o, this.p, true, true, false, A_2, this.u, this.t, false, null, null, null);
					}
				}
				catch (MailBeeInvalidArgumentException ex)
				{
					if (ex.ErrorCode == 312)
					{
						testSendResult = TestSendResult.NoSender;
					}
					else if (ex.ErrorCode == 314)
					{
						testSendResult = TestSendResult.NoRecipients;
					}
					else if (ex.ErrorCode == 403)
					{
						testSendResult = TestSendResult.NoDomainInRecipientEmail;
					}
					else
					{
						testSendResult = TestSendResult.UnknownError;
					}
					throw;
				}
				catch (MailBeeSmtpRefusedSenderException)
				{
					testSendResult = TestSendResult.BadSender;
					throw;
				}
				catch (MailBeeSmtpRefusedRecipientException)
				{
					testSendResult = TestSendResult.BadRecipient;
					throw;
				}
				catch (MailBeeSmtpNoAcceptedRecipientsException)
				{
					testSendResult = TestSendResult.NoAcceptedRecipients;
					throw;
				}
				catch (MailBeeLoginNoCredentialsException)
				{
					testSendResult = TestSendResult.NoCredentials;
					throw;
				}
				catch (MailBeeLoginNoSupportedMethodsException)
				{
					testSendResult = TestSendResult.NoSupportedAuth;
					throw;
				}
				catch (MailBeeSmtpLoginBadMethodException)
				{
					testSendResult = TestSendResult.BadAuthMethod;
					throw;
				}
				catch (MailBeeSmtpLoginBadCredentialsException)
				{
					testSendResult = TestSendResult.BadCredentials;
					throw;
				}
				catch (MailBeePop3LoginBadCredentialsException)
				{
					testSendResult = TestSendResult.BadCredentials;
					throw;
				}
				catch (MailBeeSmtpNegativeResponseException)
				{
					testSendResult = TestSendResult.NegativeSmtpResponse;
					throw;
				}
				catch (MailBeePop3NegativeResponseException)
				{
					testSendResult = TestSendResult.NegativePop3Response;
					throw;
				}
				catch (MailBeeDnsNameErrorException)
				{
					testSendResult = TestSendResult.NoMXRecord;
					throw;
				}
				catch (MailBeeDnsProtocolException)
				{
					testSendResult = TestSendResult.DnsProtocolError;
					throw;
				}
				catch (MailBeeConnectionException ex2)
				{
					switch (ex2.Protocol)
					{
					case TopLevelProtocolType.Dns:
						testSendResult = TestSendResult.DnsConnectionError;
						break;
					case TopLevelProtocolType.Smtp:
						testSendResult = TestSendResult.SmtpConnectionError;
						break;
					case TopLevelProtocolType.Pop3:
						testSendResult = TestSendResult.Pop3ConnectionError;
						break;
					default:
						testSendResult = TestSendResult.UnknownError;
						break;
					}
					throw;
				}
				catch (MailBeeGetRemoteHostNameException ex3)
				{
					TopLevelProtocolType hostProtocol = ex3.HostProtocol;
					if (hostProtocol != TopLevelProtocolType.Smtp)
					{
						if (hostProtocol != TopLevelProtocolType.Pop3)
						{
							testSendResult = TestSendResult.UnknownError;
						}
						else
						{
							testSendResult = TestSendResult.Pop3ResolveHostError;
						}
					}
					else
					{
						testSendResult = TestSendResult.SmtpResolveHostError;
					}
					throw;
				}
				catch (MailBeeException)
				{
					testSendResult = TestSendResult.UnknownError;
					throw;
				}
			}
			catch (MailBeeException a_)
			{
				base.b(a_);
			}
			if (testSendResult == TestSendResult.OK && flag)
			{
				try
				{
					((global::a.d.h)this.p).k();
				}
				catch (MailBeeException a_2)
				{
					base.b(a_2);
				}
			}
			return testSendResult;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000B01DC File Offset: 0x000AF1DC
		public new TestSendResult a(bool A_0, SendFailureThreshold A_1)
		{
			if (A_0)
			{
				this.ao();
				this.p.k(true);
			}
			TestSendResult result = this.b(this.v.From.Email, this.v.GetAllRecipients(), A_1);
			if (A_0)
			{
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000B022F File Offset: 0x000AF22F
		public Smtp8bitDataConversion ae()
		{
			return this.p;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000B0237 File Offset: 0x000AF237
		public new void a(Smtp8bitDataConversion A_0)
		{
			this.p = A_0;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000B0240 File Offset: 0x000AF240
		public new int m()
		{
			return this.u;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000B0248 File Offset: 0x000AF248
		public new void a(int A_0)
		{
			if (A_0 == 0)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			if (this.p != null && this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			this.u = A_0;
			this.a.a(this.u < 0 || this.u > 1);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000B02A3 File Offset: 0x000AF2A3
		public new DeliveryNotificationOptions o()
		{
			return this.o;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000B02AB File Offset: 0x000AF2AB
		public DirectSendServerConfig aa()
		{
			return this.s;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000B02B3 File Offset: 0x000AF2B3
		public new MailMessage p()
		{
			return this.v;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000B02BB File Offset: 0x000AF2BB
		public new void a(MailMessage A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.v = A_0;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000B02CF File Offset: 0x000AF2CF
		public override void ls(bool A_0)
		{
			base.ls(A_0);
			this.v.ThrowExceptions = A_0;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000B02E4 File Offset: 0x000AF2E4
		public new EmailAddressCollection k()
		{
			if (this.p is global::a.d.h)
			{
				return ((global::a.d.h)this.p).r();
			}
			if (this.p is global::a.d.f)
			{
				return ((global::a.d.f)this.p).k();
			}
			return new EmailAddressCollection();
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000B0334 File Offset: 0x000AF334
		public EmailAddressCollection w()
		{
			if (this.p is global::a.d.h)
			{
				return ((global::a.d.h)this.p).m();
			}
			if (this.p is global::a.d.f)
			{
				return ((global::a.d.f)this.p).e();
			}
			return new EmailAddressCollection();
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000B0384 File Offset: 0x000AF384
		public int au()
		{
			global::a.g g = this.c().a5().d();
			if (g.p().Count > 0)
			{
				return ((global::a.d.j)g.q()).a;
			}
			return 0;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000B03C2 File Offset: 0x000AF3C2
		public DnsServerCollection aq()
		{
			return this.q;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000B03CA File Offset: 0x000AF3CA
		public SmtpServerCollection av()
		{
			return this.r;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000B03D2 File Offset: 0x000AF3D2
		public override void lt(Encoding A_0)
		{
			if (this.p == null)
			{
				this.n = A_0;
				return;
			}
			base.lt(A_0);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000B03EB File Offset: 0x000AF3EB
		public override void lu(Encoding A_0)
		{
			if (this.p == null)
			{
				this.o = A_0;
				return;
			}
			base.lu(A_0);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000B0404 File Offset: 0x000AF404
		private new bc e()
		{
			if (this.u > 1 || this.u < 0)
			{
				throw new MailBeeInvalidStateException(8);
			}
			if (this.p == null)
			{
				return null;
			}
			bc bc = this.p;
			if (this.p is global::a.d.f)
			{
				bc = ((global::a.d.f)this.p).c();
			}
			if (!(bc is global::a.d.h))
			{
				return bc;
			}
			if (((global::a.d.h)bc).l() != null)
			{
				return ((global::a.d.h)bc).l();
			}
			return bc;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000B047D File Offset: 0x000AF47D
		private new global::a.d.h c()
		{
			if (this.p == null)
			{
				return this.z();
			}
			global::a.d.h h = this.e() as global::a.d.h;
			if (h == null)
			{
				throw new MailBeeInvalidStateException(9);
			}
			return h;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000B04A4 File Offset: 0x000AF4A4
		public bool am()
		{
			return this.u <= 1 && this.u >= 0 && (this.p == null || this.e() is global::a.d.h);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000B04D2 File Offset: 0x000AF4D2
		public int t()
		{
			return this.c().f();
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000B04DF File Offset: 0x000AF4DF
		public override Socket lv()
		{
			return this.c().a7();
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000B04EC File Offset: 0x000AF4EC
		public override int lw()
		{
			return this.c().a4();
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000B04FC File Offset: 0x000AF4FC
		public override bool lx()
		{
			if (this.u < 0 || this.u > 1)
			{
				return false;
			}
			global::a.d.h h = this.e() as global::a.d.h;
			return h != null && h.ao();
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000B0534 File Offset: 0x000AF534
		public override bool ly()
		{
			if (this.u < 0 || this.u > 1)
			{
				return false;
			}
			global::a.d.h h = this.e() as global::a.d.h;
			return h != null && h.ah();
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000B056C File Offset: 0x000AF56C
		public override bool lz()
		{
			if (this.u < 0 || this.u > 1)
			{
				return false;
			}
			global::a.d.h h = this.e() as global::a.d.h;
			return h != null && h.ar();
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000B05A4 File Offset: 0x000AF5A4
		public override StringDictionary ke()
		{
			return this.c().ax();
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000B05B1 File Offset: 0x000AF5B1
		public override string kf(string A_0)
		{
			return this.c().t(A_0);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000B05BF File Offset: 0x000AF5BF
		public override string l0()
		{
			return this.c().an();
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000B05CC File Offset: 0x000AF5CC
		public override AuthenticationMethods kh()
		{
			return this.c().ap();
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000B05D9 File Offset: 0x000AF5D9
		public override string l1()
		{
			if (this.p != null)
			{
				return base.l1();
			}
			return string.Empty;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000B05EF File Offset: 0x000AF5EF
		public override int l2()
		{
			if (this.p != null)
			{
				return base.l2();
			}
			return 0;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000B0601 File Offset: 0x000AF601
		public void al()
		{
			if (this.p != null && this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			this.v.Reset();
			this.o.Reset();
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000B0638 File Offset: 0x000AF638
		public override void cb()
		{
			bc bc = this.p as global::a.d.h;
			if (bc != null)
			{
				base.cb();
				this.p = null;
				return;
			}
			bc = (this.p as global::a.d.f);
			if (bc == null)
			{
				bc = (this.p as global::a.d.p);
			}
			if (bc == null)
			{
				this.c = false;
				return;
			}
			if (bc.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			this.c = false;
			this.p = null;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000B06A5 File Offset: 0x000AF6A5
		public int ag()
		{
			return ((global::a.d.i)this.c()).c();
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000B06B7 File Offset: 0x000AF6B7
		public bool ad()
		{
			return this.x;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000B06BF File Offset: 0x000AF6BF
		public void b(bool A_0)
		{
			this.x = A_0;
			if (this.p is global::a.d.p)
			{
				((global::a.d.p)this.p).c(A_0);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000B06E6 File Offset: 0x000AF6E6
		public ae ai()
		{
			return this.t;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000B06EE File Offset: 0x000AF6EE
		public new object q()
		{
			return this.g;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000B06F6 File Offset: 0x000AF6F6
		public SendMailJobCollection ah()
		{
			return this.k;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000B06FE File Offset: 0x000AF6FE
		public SendMailJobCollection ax()
		{
			return this.m;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000B0706 File Offset: 0x000AF706
		public SendMailJobCollection ar()
		{
			return this.i;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000B070E File Offset: 0x000AF70E
		public SendMailJobCollection s()
		{
			return this.n;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000B0716 File Offset: 0x000AF716
		public new string n()
		{
			return this.w;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000B071E File Offset: 0x000AF71E
		public void l(string A_0)
		{
			this.w = A_0;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000B0728 File Offset: 0x000AF728
		private new AddressValidationLevel a(MailBeeException A_0)
		{
			if (A_0 is MailBeeDataSyntaxException)
			{
				return AddressValidationLevel.RegexCheck;
			}
			AddressValidationLevel result = AddressValidationLevel.DnsQuery;
			if (A_0 is MailBeeSmtpRefusedSenderException)
			{
				result = AddressValidationLevel.SendAttempt;
			}
			else if (A_0 is MailBeeSmtpRefusedRecipientException)
			{
				result = AddressValidationLevel.SendAttempt;
			}
			else if (A_0 is MailBeeSmtpNoAcceptedRecipientsException)
			{
				result = AddressValidationLevel.SendAttempt;
			}
			else if (A_0 is MailBeeLoginNoCredentialsException)
			{
				result = AddressValidationLevel.SmtpConnection;
			}
			else if (A_0 is MailBeeLoginNoSupportedMethodsException)
			{
				result = AddressValidationLevel.SmtpConnection;
			}
			else if (A_0 is MailBeeSmtpLoginBadMethodException)
			{
				result = AddressValidationLevel.SmtpConnection;
			}
			else if (A_0 is MailBeeSmtpLoginBadCredentialsException)
			{
				result = AddressValidationLevel.SmtpConnection;
			}
			else if (A_0 is MailBeeSmtpNegativeResponseException)
			{
				result = AddressValidationLevel.SendAttempt;
			}
			else if (!(A_0 is MailBeeDnsNameErrorException) && !(A_0 is MailBeeDnsProtocolException))
			{
				if (A_0 is MailBeeConnectionException)
				{
					MailBeeConnectionException ex = (MailBeeConnectionException)A_0;
					TopLevelProtocolType topLevelProtocolType = ex.Protocol;
					if (topLevelProtocolType != TopLevelProtocolType.Dns && topLevelProtocolType == TopLevelProtocolType.Smtp)
					{
						result = (ex.WasConnected ? AddressValidationLevel.SendAttempt : AddressValidationLevel.SmtpConnection);
					}
				}
				else if (A_0 is MailBeeGetRemoteHostNameException)
				{
					TopLevelProtocolType topLevelProtocolType = ((MailBeeGetRemoteHostNameException)A_0).HostProtocol;
					if (topLevelProtocolType == TopLevelProtocolType.Smtp)
					{
						result = AddressValidationLevel.SmtpConnection;
					}
				}
			}
			return result;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000B0808 File Offset: 0x000AF808
		private AddressValidationLevel b(string A_0, string A_1, AddressValidationLevel A_2)
		{
			this.p.pa();
			AddressValidationLevel result = AddressValidationLevel.DnsQuery;
			try
			{
				((global::a.d.f)this.p).a(this.v, A_0, new EmailAddressCollection(A_1), this.o, this.p, A_2 > AddressValidationLevel.DnsQuery, A_2 > AddressValidationLevel.SmtpConnection, false, SendFailureThreshold.Default, this.u, this.t, false, null, null, null);
				result = AddressValidationLevel.OK;
			}
			catch (MailBeeException ex)
			{
				result = this.a(ex);
				if (ex is MailBeeInvalidArgumentException || ex is MailBeeUserAbortException)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000B0898 File Offset: 0x000AF898
		public new AddressValidationLevel a(bool A_0, string A_1, string A_2, AddressValidationLevel A_3)
		{
			if (this.q.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(212);
			}
			if (A_0)
			{
				this.ao();
				this.p.k(true);
			}
			AddressValidationLevel result;
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
						return AddressValidationLevel.RegexCheck;
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

		// Token: 0x06002672 RID: 9842 RVA: 0x000B0944 File Offset: 0x000AF944
		public override bool j()
		{
			if (this.a != null)
			{
				return this.a.aa();
			}
			if (this.c != null)
			{
				return this.c.e();
			}
			return this.e != null && this.e.b();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000B0983 File Offset: 0x000AF983
		public override void k(ErrorEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnErrorOccurred(A_0);
				return;
			}
			if (this.c != null)
			{
				this.c.OnErrorOccurred(A_0);
				return;
			}
			if (this.e != null)
			{
				this.e.OnErrorOccurred(A_0);
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000B09C3 File Offset: 0x000AF9C3
		public override bool l()
		{
			if (this.a != null)
			{
				return this.a.g();
			}
			if (this.c != null)
			{
				return this.c.f();
			}
			return this.e != null && this.e.c();
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000B0A02 File Offset: 0x000AFA02
		public override void m(LogNewEntryEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLogNewEntry(A_0);
				return;
			}
			if (this.c != null)
			{
				this.c.OnLogNewEntry(A_0);
				return;
			}
			if (this.e != null)
			{
				this.e.OnLogNewEntry(A_0);
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000B0A42 File Offset: 0x000AFA42
		public override bool b()
		{
			if (this.a != null)
			{
				return this.a.a();
			}
			if (this.c != null)
			{
				return this.c.c();
			}
			return this.e != null && this.e.a();
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000B0A81 File Offset: 0x000AFA81
		public override void c(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataReceived(A_0);
				return;
			}
			if (this.c != null)
			{
				this.c.OnDataReceived(A_0);
				return;
			}
			if (this.e != null)
			{
				this.e.OnDataReceived(A_0);
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000B0AC1 File Offset: 0x000AFAC1
		public override bool d()
		{
			if (this.a != null)
			{
				return this.a.b();
			}
			if (this.c != null)
			{
				return this.c.a();
			}
			return this.e != null && this.e.d();
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000B0B00 File Offset: 0x000AFB00
		public override void e(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataSent(A_0);
				return;
			}
			if (this.c != null)
			{
				this.c.OnDataSent(A_0);
				return;
			}
			if (this.e != null)
			{
				this.e.OnDataSent(A_0);
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000B0B40 File Offset: 0x000AFB40
		public override bool f()
		{
			return this.a != null && this.a.o();
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000B0B57 File Offset: 0x000AFB57
		public override void g(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataReceived(A_0);
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000B0B6D File Offset: 0x000AFB6D
		public override bool h()
		{
			return this.a != null && this.a.y();
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000B0B84 File Offset: 0x000AFB84
		public override void i(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataSent(A_0);
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000B0B9A File Offset: 0x000AFB9A
		public override bool bx()
		{
			return this.a != null && this.a.i();
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000B0BB1 File Offset: 0x000AFBB1
		public override void by(HostResolvedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnHostResolved(A_0);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000B0BC7 File Offset: 0x000AFBC7
		public override bool bz()
		{
			return this.a != null && this.a.h();
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000B0BDE File Offset: 0x000AFBDE
		public override void b0(SocketCreatingEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketCreating(A_0);
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000B0BF4 File Offset: 0x000AFBF4
		public override bool b1()
		{
			return this.a != null && this.a.x();
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000B0C0B File Offset: 0x000AFC0B
		public override void b2(SocketConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketConnected(A_0);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000B0C21 File Offset: 0x000AFC21
		public override bool b3()
		{
			return this.a != null && this.a.p();
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000B0C38 File Offset: 0x000AFC38
		public override void b4(ConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnConnected(A_0);
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000B0C4E File Offset: 0x000AFC4E
		public override bool b5()
		{
			return this.a != null && this.a.d();
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000B0C65 File Offset: 0x000AFC65
		public override void b6(DisconnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDisconnected(A_0);
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000B0C7B File Offset: 0x000AFC7B
		public override bool b7()
		{
			return this.a != null && this.a.s();
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000B0C92 File Offset: 0x000AFC92
		public override void b8(TlsStartedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnTlsStarted(A_0);
			}
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000B0CA8 File Offset: 0x000AFCA8
		public override bool b9()
		{
			return this.a != null && this.a.u();
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000B0CBF File Offset: 0x000AFCBF
		public override void ca(LoggedInEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLoggedIn(A_0);
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000B0CD5 File Offset: 0x000AFCD5
		public bool l3()
		{
			return this.a != null && this.a.q();
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000B0CEC File Offset: 0x000AFCEC
		public void l4(SmtpSendingMessageEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSendingMessage(A_0);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000B0D02 File Offset: 0x000AFD02
		public bool l5()
		{
			return this.a != null && this.a.n();
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000B0D19 File Offset: 0x000AFD19
		public void l6(SmtpMessageSenderSubmittedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageSenderSubmitted(A_0);
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000B0D2F File Offset: 0x000AFD2F
		public bool l7()
		{
			return this.a != null && this.a.e();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000B0D46 File Offset: 0x000AFD46
		public void l8(SmtpMessageRecipientSubmittedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageRecipientSubmitted(A_0);
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000B0D5C File Offset: 0x000AFD5C
		public bool l9()
		{
			return this.a != null && this.a.f();
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000B0D73 File Offset: 0x000AFD73
		public void ma(SmtpMessageDataChunkSentEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageDataChunkSent(A_0);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000B0D89 File Offset: 0x000AFD89
		public bool mb()
		{
			return this.a != null && this.a.k();
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000B0DA0 File Offset: 0x000AFDA0
		public void mc(SmtpMessageSubmittedToServerEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageSubmittedToServer(A_0);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000B0DB8 File Offset: 0x000AFDB8
		private new VerifiedEventArgs a(SmtpMessageSentEventArgs A_0, SmtpMessageNotSentEventArgs A_1)
		{
			string a_ = null;
			if (A_0 != null)
			{
				if (A_0.MergeTable != null)
				{
					a_ = (string)A_0.MergeTable.Rows[A_0.MergeRowIndex][A_0.AddrCheck.a()];
				}
				else if (A_0.MergeDataReaderRowValues != null)
				{
					a_ = A_0.MergeDataReaderRowValues[A_0.AddrCheck.d()].ToString();
				}
				return new VerifiedEventArgs(a_, A_0.Merge, AddressValidationLevel.OK, null, A_0.Context);
			}
			if (A_1.MergeTable != null)
			{
				a_ = (string)A_1.MergeTable.Rows[A_1.MergeRowIndex][A_1.AddrCheck.a()];
			}
			else if (A_1.MergeDataReaderRowValues != null)
			{
				a_ = A_1.MergeDataReaderRowValues[A_1.AddrCheck.d()].ToString();
			}
			return new VerifiedEventArgs(a_, A_1.Merge, this.a(A_1.Reason), A_1.Reason, A_1.Context);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000B0EAF File Offset: 0x000AFEAF
		public bool md()
		{
			if (this.a == null)
			{
				return this.c != null && this.c.d();
			}
			return this.a.r();
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000B0EDA File Offset: 0x000AFEDA
		public void me(SmtpMessageSentEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageSent(A_0);
				return;
			}
			if (this.c != null && A_0.AddrCheck != null)
			{
				this.c.OnVerified(this.a(A_0, null));
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000B0F14 File Offset: 0x000AFF14
		public bool mf()
		{
			if (this.a == null)
			{
				return this.c != null && this.c.d();
			}
			return this.a.v();
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000B0F3F File Offset: 0x000AFF3F
		public void mg(SmtpMessageNotSentEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageNotSent(A_0);
				return;
			}
			if (this.c != null && A_0.AddrCheck != null)
			{
				this.c.OnVerified(this.a(null, A_0));
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000B0F79 File Offset: 0x000AFF79
		public bool mh()
		{
			return this.a != null && this.a.c();
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000B0F90 File Offset: 0x000AFF90
		public void mi(SmtpTransientErrorOccurredEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnTransientErrorOccurred(A_0);
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000B0FA8 File Offset: 0x000AFFA8
		private new VerifyingEventArgs a(SmtpMergingMessageEventArgs A_0)
		{
			string a_ = null;
			if (A_0.MergeTable != null)
			{
				a_ = (string)A_0.MergeTable.Rows[A_0.MergeRowIndex][A_0.AddrCheck.a()];
			}
			else if (A_0.MergeDataReaderRowValues != null)
			{
				a_ = A_0.MergeDataReaderRowValues[A_0.AddrCheck.d()].ToString();
			}
			return new VerifyingEventArgs(a_, A_0.Merge, A_0.Context);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000B101F File Offset: 0x000B001F
		public bool mj()
		{
			if (this.a == null)
			{
				return this.c != null && this.c.b();
			}
			return this.a.w();
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000B104C File Offset: 0x000B004C
		public void mk(SmtpMergingMessageEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMergingMessage(A_0);
				return;
			}
			if (this.c != null && A_0.AddrCheck != null)
			{
				VerifyingEventArgs verifyingEventArgs = this.a(A_0);
				this.c.OnVerifying(verifyingEventArgs);
				if (!verifyingEventArgs.VerifyIt)
				{
					A_0.MergeIt = false;
				}
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000B10A1 File Offset: 0x000B00A1
		public bool ml()
		{
			return this.a != null && this.a.j();
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000B10B8 File Offset: 0x000B00B8
		public void mm(SmtpSubmittingMessageToPickupFolderEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSubmittingMessageToPickupFolder(A_0);
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000B10CE File Offset: 0x000B00CE
		public bool mn()
		{
			return this.a != null && this.a.m();
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000B10E5 File Offset: 0x000B00E5
		public void mo(SmtpMessageSubmittedToPickupFolderEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageSubmittedToPickupFolder(A_0);
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000B10FB File Offset: 0x000B00FB
		public bool mp()
		{
			return this.a != null && this.a.l();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000B1112 File Offset: 0x000B0112
		public void mq(SmtpFinishingJobEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnFinishingJob(A_0);
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000B1128 File Offset: 0x000B0128
		public bool mr()
		{
			return this.a != null && this.a.t();
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000B113F File Offset: 0x000B013F
		public void ms(SmtpMessageMXLookupDoneEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageMXLookupDone(A_0);
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000B1155 File Offset: 0x000B0155
		public bool mt()
		{
			return this.a != null && this.a.z();
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000B116C File Offset: 0x000B016C
		public void mu(SmtpMessageDirectSendDoneEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageDirectSendDone(A_0);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000B1184 File Offset: 0x000B0184
		internal override Task mv(bf A_0, byte[] A_1, bc A_2)
		{
			global::a.d.n.d d;
			d.e = this;
			d.c = A_0;
			d.f = A_1;
			d.d = A_2;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder b = d.b;
			b.Start<global::a.d.n.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000B11E4 File Offset: 0x000B01E4
		public override Task mw(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			global::a.d.n.z z;
			z.g = this;
			z.d = A_0;
			z.e = A_1;
			z.f = A_2;
			z.c = A_3;
			z.b = AsyncTaskMethodBuilder.Create();
			z.a = -1;
			AsyncTaskMethodBuilder b = z.b;
			b.Start<global::a.d.n.z>(ref z);
			return z.b.Task;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000B124C File Offset: 0x000B024C
		public override Task mx(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			global::a.d.n.b b;
			b.g = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.c = A_3;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<global::a.d.n.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000B12B4 File Offset: 0x000B02B4
		public Task<bool> v()
		{
			global::a.d.n.v v;
			v.c = this;
			v.b = AsyncTaskMethodBuilder<bool>.Create();
			v.a = -1;
			AsyncTaskMethodBuilder<bool> b = v.b;
			b.Start<global::a.d.n.v>(ref v);
			return v.b.Task;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000B12F9 File Offset: 0x000B02F9
		public override Task<bool> my()
		{
			if (!(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.my();
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000B1316 File Offset: 0x000B0316
		public override Task<bool> mz()
		{
			if (!(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.mz();
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000B1333 File Offset: 0x000B0333
		private new Task a(string A_0, int A_1, string A_2, string A_3)
		{
			global::a.d.h h = (global::a.d.h)this.p;
			h.pa();
			h.ay();
			return h.f7(A_0, A_1, A_2, A_3);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000B1358 File Offset: 0x000B0358
		public new Task<bool> c(string A_0, int A_1, string A_2, string A_3)
		{
			global::a.d.n.k k;
			k.c = this;
			k.d = A_0;
			k.e = A_1;
			k.f = A_2;
			k.g = A_3;
			k.b = AsyncTaskMethodBuilder<bool>.Create();
			k.a = -1;
			AsyncTaskMethodBuilder<bool> b = k.b;
			b.Start<global::a.d.n.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000B13BE File Offset: 0x000B03BE
		private new Task d(string A_0)
		{
			this.p.pa();
			((global::a.ab)this.p).au();
			return ((global::a.ab)this.p).o3(A_0, true);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000B13F0 File Offset: 0x000B03F0
		public Task<bool> j(string A_0)
		{
			global::a.d.n.t t;
			t.c = this;
			t.d = A_0;
			t.b = AsyncTaskMethodBuilder<bool>.Create();
			t.a = -1;
			AsyncTaskMethodBuilder<bool> b = t.b;
			b.Start<global::a.d.n.t>(ref t);
			return t.b.Task;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000B143D File Offset: 0x000B043D
		private new Task a()
		{
			global::a.d.h h = (global::a.d.h)this.p;
			h.pa();
			h.o();
			return h.j();
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000B145C File Offset: 0x000B045C
		public Task<bool> x()
		{
			global::a.d.n.e e;
			e.c = this;
			e.b = AsyncTaskMethodBuilder<bool>.Create();
			e.a = -1;
			AsyncTaskMethodBuilder<bool> b = e.b;
			b.Start<global::a.d.n.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000B14A1 File Offset: 0x000B04A1
		public override Task<bool> m0()
		{
			if (!(this.p is global::a.d.h))
			{
				throw new MailBeeInvalidStateException(9);
			}
			return base.m0();
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000B14C0 File Offset: 0x000B04C0
		public new Task<bool> i()
		{
			global::a.d.n.s s;
			s.c = this;
			s.b = AsyncTaskMethodBuilder<bool>.Create();
			s.a = -1;
			AsyncTaskMethodBuilder<bool> b = s.b;
			b.Start<global::a.d.n.s>(ref s);
			return s.b.Task;
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000B1508 File Offset: 0x000B0508
		protected Task b(string A_0, string A_1, EmailAddressCollection A_2)
		{
			global::a.d.n.f f;
			f.c = this;
			f.d = A_0;
			f.f = A_1;
			f.g = A_2;
			f.b = AsyncTaskMethodBuilder.Create();
			f.a = -1;
			AsyncTaskMethodBuilder b = f.b;
			b.Start<global::a.d.n.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000B1568 File Offset: 0x000B0568
		public new Task<bool> d(string A_0, string A_1, EmailAddressCollection A_2)
		{
			global::a.d.n.n n;
			n.c = this;
			n.d = A_0;
			n.e = A_1;
			n.f = A_2;
			n.b = AsyncTaskMethodBuilder<bool>.Create();
			n.a = -1;
			AsyncTaskMethodBuilder<bool> b = n.b;
			b.Start<global::a.d.n.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000B15C8 File Offset: 0x000B05C8
		private new Task a(string A_0, EmailAddressCollection A_1)
		{
			global::a.d.n.p p;
			p.d = this;
			p.c = A_0;
			p.e = A_1;
			p.b = AsyncTaskMethodBuilder.Create();
			p.a = -1;
			AsyncTaskMethodBuilder b = p.b;
			b.Start<global::a.d.n.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000B1620 File Offset: 0x000B0620
		public new Task<bool> c(string A_0, EmailAddressCollection A_1)
		{
			global::a.d.n.h h;
			h.c = this;
			h.d = A_0;
			h.e = A_1;
			h.b = AsyncTaskMethodBuilder<bool>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<bool> b = h.b;
			b.Start<global::a.d.n.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000B1675 File Offset: 0x000B0675
		public Task<bool> ak()
		{
			return this.c(this.v.From.Email, this.v.GetAllRecipients());
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000B1698 File Offset: 0x000B0698
		private new Task<string[]> c(string A_0)
		{
			return ((global::a.d.f)this.p).c(A_0, this.u);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000B16B4 File Offset: 0x000B06B4
		public new Task<string[]> i(string A_0)
		{
			global::a.d.n.r r;
			r.c = this;
			r.d = A_0;
			r.b = AsyncTaskMethodBuilder<string[]>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<string[]> b = r.b;
			b.Start<global::a.d.n.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000B1701 File Offset: 0x000B0701
		private Task<string[]> b(string A_0)
		{
			return ((global::a.d.f)this.p).b(A_0, this.u);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000B171C File Offset: 0x000B071C
		public new Task<string[]> k(string A_0)
		{
			global::a.d.n.u u;
			u.c = this;
			u.d = A_0;
			u.b = AsyncTaskMethodBuilder<string[]>.Create();
			u.a = -1;
			AsyncTaskMethodBuilder<string[]> b = u.b;
			b.Start<global::a.d.n.u>(ref u);
			return u.b.Task;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000B1769 File Offset: 0x000B0769
		private new Task<string[]> a(string A_0)
		{
			return ((global::a.d.f)this.p).a(A_0, this.u);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000B1784 File Offset: 0x000B0784
		public new Task<string[]> m(string A_0)
		{
			global::a.d.n.y y;
			y.c = this;
			y.d = A_0;
			y.b = AsyncTaskMethodBuilder<string[]>.Create();
			y.a = -1;
			AsyncTaskMethodBuilder<string[]> b = y.b;
			b.Start<global::a.d.n.y>(ref y);
			return y.b.Task;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000B17D1 File Offset: 0x000B07D1
		private new Task<bool> a(string A_0, string A_1)
		{
			return ((global::a.d.f)this.p).b(A_0, A_1);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000B17E8 File Offset: 0x000B07E8
		public Task<bool> b(bool A_0, string A_1, string A_2)
		{
			global::a.d.n.x x;
			x.d = this;
			x.c = A_0;
			x.e = A_1;
			x.f = A_2;
			x.b = AsyncTaskMethodBuilder<bool>.Create();
			x.a = -1;
			AsyncTaskMethodBuilder<bool> b = x.b;
			b.Start<global::a.d.n.x>(ref x);
			return x.b.Task;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000B1845 File Offset: 0x000B0845
		private new Task<RblStatusCollection> a(string A_0, string[] A_1)
		{
			return ((global::a.d.f)this.p).a(A_0, A_1, this.u);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000B1860 File Offset: 0x000B0860
		public new Task<RblStatusCollection> a(bool A_0, string A_1, string[] A_2)
		{
			global::a.d.n.ab ab;
			ab.d = this;
			ab.c = A_0;
			ab.e = A_1;
			ab.f = A_2;
			ab.b = AsyncTaskMethodBuilder<RblStatusCollection>.Create();
			ab.a = -1;
			AsyncTaskMethodBuilder<RblStatusCollection> b = ab.b;
			b.Start<global::a.d.n.ab>(ref ab);
			return ab.b.Task;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000B18C0 File Offset: 0x000B08C0
		public Task<bool> ab()
		{
			global::a.d.n.c c;
			c.c = this;
			c.b = AsyncTaskMethodBuilder<bool>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<bool> b = c.b;
			b.Start<global::a.d.n.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000B1908 File Offset: 0x000B0908
		private new Task<string> a(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4, global::a.d.f.m A_5)
		{
			return ((global::a.d.f)this.p).a(this.v, A_0, A_1, A_2, A_3, A_4, null, null, A_5);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000B1938 File Offset: 0x000B0938
		public Task<string> b(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4)
		{
			global::a.d.n.o o;
			o.c = this;
			o.d = A_0;
			o.e = A_1;
			o.f = A_2;
			o.g = A_3;
			o.h = A_4;
			o.b = AsyncTaskMethodBuilder<string>.Create();
			o.a = -1;
			AsyncTaskMethodBuilder<string> b = o.b;
			b.Start<global::a.d.n.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000B19A8 File Offset: 0x000B09A8
		public Task<bool> b(string A_0, bool A_1)
		{
			global::a.d.n.i i;
			i.c = this;
			i.d = A_0;
			i.e = A_1;
			i.b = AsyncTaskMethodBuilder<bool>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<bool> b = i.b;
			b.Start<global::a.d.n.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000B1A00 File Offset: 0x000B0A00
		private new Task<TestSendResult> a(string A_0, EmailAddressCollection A_1, SendFailureThreshold A_2)
		{
			global::a.d.n.l l;
			l.c = this;
			l.d = A_0;
			l.e = A_1;
			l.f = A_2;
			l.b = AsyncTaskMethodBuilder<TestSendResult>.Create();
			l.a = -1;
			AsyncTaskMethodBuilder<TestSendResult> b = l.b;
			b.Start<global::a.d.n.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000B1A60 File Offset: 0x000B0A60
		public new Task<TestSendResult> a(SendFailureThreshold A_0)
		{
			global::a.d.n.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder<TestSendResult>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<TestSendResult> b = a.b;
			b.Start<global::a.d.n.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000B1AB0 File Offset: 0x000B0AB0
		private new Task<AddressValidationLevel> a(string A_0, string A_1, AddressValidationLevel A_2)
		{
			global::a.d.n.j j;
			j.c = this;
			j.d = A_0;
			j.e = A_1;
			j.f = A_2;
			j.b = AsyncTaskMethodBuilder<AddressValidationLevel>.Create();
			j.a = -1;
			AsyncTaskMethodBuilder<AddressValidationLevel> b = j.b;
			b.Start<global::a.d.n.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000B1B10 File Offset: 0x000B0B10
		public new Task<AddressValidationLevel> c(string A_0, string A_1, AddressValidationLevel A_2)
		{
			global::a.d.n.w w;
			w.c = this;
			w.d = A_0;
			w.e = A_1;
			w.f = A_2;
			w.b = AsyncTaskMethodBuilder<AddressValidationLevel>.Create();
			w.a = -1;
			AsyncTaskMethodBuilder<AddressValidationLevel> b = w.b;
			b.Start<global::a.d.n.w>(ref w);
			return w.b.Task;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000B1B6D File Offset: 0x000B0B6D
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(bf A_0, byte[] A_1, bc A_2)
		{
			return base.mv(A_0, A_1, A_2);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000B1B78 File Offset: 0x000B0B78
		[DebuggerHidden]
		[CompilerGenerated]
		private Task b(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			return base.mw(A_0, A_1, A_2, A_3);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000B1B85 File Offset: 0x000B0B85
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task a(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			return base.mx(A_0, A_1, A_2, A_3);
		}

		// Token: 0x04001A10 RID: 6672
		private new Smtp a;

		// Token: 0x04001A11 RID: 6673
		private new EmailAddressValidator c;

		// Token: 0x04001A12 RID: 6674
		private new RblFilter e;

		// Token: 0x04001A13 RID: 6675
		protected new object g;

		// Token: 0x04001A14 RID: 6676
		protected new SendMailJobCollection i;

		// Token: 0x04001A15 RID: 6677
		protected new SendMailJobCollection k;

		// Token: 0x04001A16 RID: 6678
		protected new SendMailJobCollection m;

		// Token: 0x04001A17 RID: 6679
		protected new SendMailJobCollection n;

		// Token: 0x04001A18 RID: 6680
		protected new DeliveryNotificationOptions o;

		// Token: 0x04001A19 RID: 6681
		protected new Smtp8bitDataConversion p;

		// Token: 0x04001A1A RID: 6682
		protected new DnsServerCollection q;

		// Token: 0x04001A1B RID: 6683
		protected SmtpServerCollection r;

		// Token: 0x04001A1C RID: 6684
		protected DirectSendServerConfig s;

		// Token: 0x04001A1D RID: 6685
		protected ae t;

		// Token: 0x04001A1E RID: 6686
		protected int u;

		// Token: 0x04001A1F RID: 6687
		protected MailMessage v;

		// Token: 0x04001A20 RID: 6688
		protected string w;

		// Token: 0x04001A21 RID: 6689
		protected bool x;

		// Token: 0x02000455 RID: 1109
		// (Invoke) Token: 0x06002723 RID: 10019
		protected new delegate bool q(bool A_0, string A_1, int A_2, string A_3, string A_4);

		// Token: 0x02000456 RID: 1110
		// (Invoke) Token: 0x06002727 RID: 10023
		protected new delegate bool m(bool A_0, string A_1, string A_2, EmailAddressCollection A_3);

		// Token: 0x02000457 RID: 1111
		// (Invoke) Token: 0x0600272B RID: 10027
		protected delegate bool aa(bool A_0, string A_1, EmailAddressCollection A_2);

		// Token: 0x02000458 RID: 1112
		// (Invoke) Token: 0x0600272F RID: 10031
		protected new delegate bool g(bool A_0, string A_1);
	}
}
