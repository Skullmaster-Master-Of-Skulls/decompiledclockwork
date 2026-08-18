using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;
using MailBee.ImapMail;
using MailBee.Mime;

namespace a.f
{
	// Token: 0x020000B7 RID: 183
	internal class o : global::a.k, global::a.f.c
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00019632 File Offset: 0x00018632
		public o(Imap A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019641 File Offset: 0x00018641
		protected override void f9()
		{
			this.p = new global::a.f.t(this, null, this.m, 0);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00019658 File Offset: 0x00018658
		internal override void kd(at A_0, bc A_1)
		{
			global::a.f.a a = (global::a.f.a)A_0;
			if (A_1.a8().Enabled)
			{
				if (a.j() == null)
				{
					A_1.a8().b(a.o() + "\\r\\n", string.Format(Resources.Instance.Log_0BytesReceived, Convert.ToString(A_0.q().Length)), LogMessageType.Recv, A_1);
					return;
				}
				int num = 0;
				for (int i = 0; i < a.j().Count; i++)
				{
					ao ao = (ao)a.j()[i];
					int num2 = ao.b() - num;
					if (num2 > 0)
					{
						A_1.a8().b(A_0.p().GetString(a.q(), num, num2), null, LogMessageType.Recv, A_1);
					}
					if (ao.e() > Global.MaxMultiLineDataLength)
					{
						A_1.a8().b(base.a(ao.d(), ao.b(), ao.e()), string.Format(Resources.Instance.Log_ImapLiteralOfLength0, Convert.ToString(ao.e())), LogMessageType.Recv, A_1);
					}
					else
					{
						A_1.a8().b(A_0.p().GetString(ao.d(), ao.b(), ao.e()), string.Format(Resources.Instance.Log_ImapLiteralOfLength0, Convert.ToString(ao.e())), LogMessageType.Recv, A_1);
					}
					num = ao.b() + ao.e();
				}
				A_1.a8().b(A_0.p().GetString(a.q(), num, a.q().Length - num), string.Format(Resources.Instance.Log_0BytesReceived, Convert.ToString(A_0.q().Length)), LogMessageType.Recv, A_1);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00019810 File Offset: 0x00018810
		internal override void ll(bf A_0, byte[] A_1, bc A_2)
		{
			global::a.f.v v = (global::a.f.v)A_0;
			if (A_2.a8().Enabled)
			{
				if (v.f == null && !v.j)
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

		// Token: 0x0600060A RID: 1546 RVA: 0x00019894 File Offset: 0x00018894
		private new void d(string A_0, string A_1)
		{
			this.p.pa();
			((global::a.ab)this.p).au();
			if (A_1 == null)
			{
				((global::a.ab)this.p).b(A_0, new global::a.f.v(true), true);
				return;
			}
			if (A_1 == string.Empty)
			{
				((global::a.ab)this.p).o1(A_0, true);
				return;
			}
			((global::a.ab)this.p).c(A_1 + " " + A_0 + "\r\n", new global::a.f.v(true, A_1), true);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019924 File Offset: 0x00018924
		public new bool e(bool A_0, string A_1, string A_2)
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

		// Token: 0x0600060C RID: 1548 RVA: 0x000199AC File Offset: 0x000189AC
		public new IAsyncResult a(string A_0, string A_1, AsyncCallback A_2, object A_3)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.w w = new global::a.f.o.w(this.e);
			this.q = new global::a.o(w, null);
			this.q.a(w.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019A04 File Offset: 0x00018A04
		public new bool p()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.w))
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
				result = ((global::a.f.o.w)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019A9C File Offset: 0x00018A9C
		private void l(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).h(A_0);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019ACC File Offset: 0x00018ACC
		public new bool a(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.l(A_1);
				}
				else
				{
					try
					{
						this.l(A_1);
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

		// Token: 0x06000610 RID: 1552 RVA: 0x00019B54 File Offset: 0x00018B54
		private new void k(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).j(A_0);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00019B84 File Offset: 0x00018B84
		public bool f(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.k(A_1);
				}
				else
				{
					try
					{
						this.k(A_1);
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

		// Token: 0x06000612 RID: 1554 RVA: 0x00019C0C File Offset: 0x00018C0C
		private new void c(string A_0, string A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).a(A_0, A_1);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00019C3C File Offset: 0x00018C3C
		public new bool c(bool A_0, string A_1, string A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.c(A_1, A_2);
				}
				else
				{
					try
					{
						this.c(A_1, A_2);
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

		// Token: 0x06000614 RID: 1556 RVA: 0x00019CC4 File Offset: 0x00018CC4
		private void j(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).m(A_0);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019CF4 File Offset: 0x00018CF4
		public new bool b(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.j(A_1);
				}
				else
				{
					try
					{
						this.j(A_1);
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

		// Token: 0x06000616 RID: 1558 RVA: 0x00019D7C File Offset: 0x00018D7C
		private new void i(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).g(A_0);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019DAC File Offset: 0x00018DAC
		public new bool d(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.i(A_1);
				}
				else
				{
					try
					{
						this.i(A_1);
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

		// Token: 0x06000618 RID: 1560 RVA: 0x00019E34 File Offset: 0x00018E34
		private void f(string A_0, bool A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).f(A_0, A_1);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00019E64 File Offset: 0x00018E64
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
					this.f(A_1, A_2);
				}
				else
				{
					try
					{
						this.f(A_1, A_2);
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

		// Token: 0x0600061A RID: 1562 RVA: 0x00019EEC File Offset: 0x00018EEC
		public new IAsyncResult a(string A_0, bool A_1, AsyncCallback A_2, object A_3)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.ae ae = new global::a.f.o.ae(this.a);
			this.q = new global::a.o(ae, null);
			this.q.a(ae.BeginInvoke(false, A_0, A_1, A_2, A_3));
			return this.q;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00019F44 File Offset: 0x00018F44
		public bool v()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.ae))
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
				result = ((global::a.f.o.ae)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019FDC File Offset: 0x00018FDC
		private new void b(bool A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).t();
			((global::a.f.t)this.p).f(A_0);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001A00C File Offset: 0x0001900C
		public new bool a(bool A_0, bool A_1)
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

		// Token: 0x0600061E RID: 1566 RVA: 0x0001A094 File Offset: 0x00019094
		public new IAsyncResult a(bool A_0, AsyncCallback A_1, object A_2)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.ah ah = new global::a.f.o.ah(this.a);
			this.q = new global::a.o(ah, null);
			this.q.a(ah.BeginInvoke(false, A_0, A_1, A_2));
			return this.q;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001A0E8 File Offset: 0x000190E8
		public bool ak()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.ah))
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
				result = ((global::a.f.o.ah)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A180 File Offset: 0x00019180
		private new void e(string A_0, bool A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).t();
			((global::a.f.t)this.p).d(A_0, A_1);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001A1B0 File Offset: 0x000191B0
		public new bool b(bool A_0, string A_1, bool A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.e(A_1, A_2);
				}
				else
				{
					try
					{
						this.e(A_1, A_2);
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

		// Token: 0x06000622 RID: 1570 RVA: 0x0001A238 File Offset: 0x00019238
		private FolderStatus h(string A_0)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.n(A_0);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A25C File Offset: 0x0001925C
		public new FolderStatus e(bool A_0, string A_1)
		{
			FolderStatus result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.h(A_1);
				}
				else
				{
					try
					{
						result = this.h(A_1);
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

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A2E4 File Offset: 0x000192E4
		private FolderQuota g(string A_0)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.l(A_0);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A308 File Offset: 0x00019308
		public new FolderQuota c(bool A_0, string A_1)
		{
			FolderQuota result = null;
			if (A_0)
			{
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

		// Token: 0x06000626 RID: 1574 RVA: 0x0001A390 File Offset: 0x00019390
		private new FolderCollection b(bool A_0, string A_1, string A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.b(A_0, A_1, A_2);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A3B8 File Offset: 0x000193B8
		public new FolderCollection a(bool A_0, bool A_1, string A_2, string A_3)
		{
			FolderCollection result = null;
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

		// Token: 0x06000628 RID: 1576 RVA: 0x0001A444 File Offset: 0x00019444
		public new IAsyncResult a(bool A_0, string A_1, string A_2, AsyncCallback A_3, object A_4)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.e e = new global::a.f.o.e(this.a);
			this.q = new global::a.o(e, null);
			this.q.a(e.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4));
			return this.q;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001A49C File Offset: 0x0001949C
		public FolderCollection ae()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.e))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			FolderCollection result;
			try
			{
				base.bh();
				result = ((global::a.f.o.e)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001A534 File Offset: 0x00019534
		private new MessageIndexCollection b(bool A_0, string A_1, string A_2, string A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001A55C File Offset: 0x0001955C
		public new MessageIndexCollection a(bool A_0, bool A_1, string A_2, string A_3, string A_4)
		{
			MessageIndexCollection result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2, A_3, A_4);
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

		// Token: 0x0600062C RID: 1580 RVA: 0x0001A5EC File Offset: 0x000195EC
		public new IAsyncResult a(bool A_0, string A_1, string A_2, string A_3, AsyncCallback A_4, object A_5)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.c c = new global::a.f.o.c(this.a);
			this.q = new global::a.o(c, null);
			this.q.a(c.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5));
			return this.q;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001A648 File Offset: 0x00019648
		public MessageIndexCollection ai()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.c))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			MessageIndexCollection result;
			try
			{
				base.bh();
				result = ((global::a.f.o.c)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001A6E0 File Offset: 0x000196E0
		private new EnvelopeCollection b(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, A_3, A_4, A_5, true, false);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001A71C File Offset: 0x0001971C
		public new EnvelopeCollection a(bool A_0, string A_1, bool A_2, EnvelopeParts A_3, int A_4, string[] A_5, string[] A_6)
		{
			EnvelopeCollection result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2, A_3, A_4, A_5, A_6);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2, A_3, A_4, A_5, A_6);
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

		// Token: 0x06000630 RID: 1584 RVA: 0x0001A7B4 File Offset: 0x000197B4
		public new IAsyncResult a(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5, AsyncCallback A_6, object A_7)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.t t = new global::a.f.o.t(this.a);
			this.q = new global::a.o(t, null);
			this.q.a(t.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7));
			return this.q;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001A814 File Offset: 0x00019814
		public EnvelopeCollection s()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.t))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			EnvelopeCollection result;
			try
			{
				base.bh();
				result = ((global::a.f.o.t)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001A8AC File Offset: 0x000198AC
		private new EnvelopeCollection b(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, A_3, A_4, A_5, true, false);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001A8E8 File Offset: 0x000198E8
		public new EnvelopeCollection a(bool A_0, long[] A_1, bool A_2, EnvelopeParts[] A_3, int[] A_4, string[][] A_5, string[][] A_6)
		{
			EnvelopeCollection result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2, A_3, A_4, A_5, A_6);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2, A_3, A_4, A_5, A_6);
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

		// Token: 0x06000634 RID: 1588 RVA: 0x0001A980 File Offset: 0x00019980
		public new IAsyncResult a(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5, AsyncCallback A_6, object A_7)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.ab ab = new global::a.f.o.ab(this.a);
			this.q = new global::a.o(ab, null);
			this.q.a(ab.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7));
			return this.q;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001A9E0 File Offset: 0x000199E0
		public EnvelopeCollection u()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.ab))
			{
				throw new MailBeeInvalidStateException(4);
			}
			while (this.q.d() == null)
			{
				Thread.Sleep(0);
			}
			EnvelopeCollection result;
			try
			{
				base.bh();
				result = ((global::a.f.o.ab)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001AA78 File Offset: 0x00019A78
		private new MailMessage b(long A_0, bool A_1, int A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, true);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001AAA0 File Offset: 0x00019AA0
		public new MailMessage a(bool A_0, long A_1, bool A_2, int A_3)
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

		// Token: 0x06000638 RID: 1592 RVA: 0x0001AB2C File Offset: 0x00019B2C
		private new MailMessageCollection b(string A_0, bool A_1, int A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, true);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001AB54 File Offset: 0x00019B54
		public new MailMessageCollection a(bool A_0, string A_1, bool A_2, int A_3)
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

		// Token: 0x0600063A RID: 1594 RVA: 0x0001ABE0 File Offset: 0x00019BE0
		private new long k()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.s();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001AC04 File Offset: 0x00019C04
		public new long e(bool A_0)
		{
			long result = -1L;
			if (A_0)
			{
				this.p.k(true);
			}
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

		// Token: 0x0600063C RID: 1596 RVA: 0x0001AC88 File Offset: 0x00019C88
		private new ImapNamespaceCollectionSet i()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.h();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001ACAC File Offset: 0x00019CAC
		public new ImapNamespaceCollectionSet i(bool A_0)
		{
			ImapNamespaceCollectionSet result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.i();
				}
				else
				{
					try
					{
						result = this.i();
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

		// Token: 0x0600063E RID: 1598 RVA: 0x0001AD30 File Offset: 0x00019D30
		private new void b(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			((global::a.f.t)this.p).a(A_0, A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001AD68 File Offset: 0x00019D68
		public new bool a(bool A_0, MailMessage A_1, string A_2, string A_3, string A_4, bool A_5, UidPlusResult A_6)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1, A_2, A_3, A_4, A_5, A_6);
				}
				else
				{
					try
					{
						this.b(A_1, A_2, A_3, A_4, A_5, A_6);
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

		// Token: 0x06000640 RID: 1600 RVA: 0x0001AE00 File Offset: 0x00019E00
		public new IAsyncResult a(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5, AsyncCallback A_6, object A_7)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.n n = new global::a.f.o.n(this.a);
			this.q = new global::a.o(n, null);
			this.q.a(n.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7));
			return this.q;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001AE60 File Offset: 0x00019E60
		public bool y()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.n))
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
				result = ((global::a.f.o.n)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001AEF8 File Offset: 0x00019EF8
		private new void d(string A_0, bool A_1)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			t.g(A_0, A_1);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001AF20 File Offset: 0x00019F20
		public new bool c(bool A_0, string A_1, bool A_2)
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

		// Token: 0x06000644 RID: 1604 RVA: 0x0001AFA8 File Offset: 0x00019FA8
		private new void b(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			t.c(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001AFD4 File Offset: 0x00019FD4
		public new bool a(bool A_0, string A_1, bool A_2, string A_3, MessageFlagAction A_4, bool A_5)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1, A_2, A_3, A_4, A_5);
				}
				else
				{
					try
					{
						this.b(A_1, A_2, A_3, A_4, A_5);
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

		// Token: 0x06000646 RID: 1606 RVA: 0x0001B068 File Offset: 0x0001A068
		public new IAsyncResult a(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4, AsyncCallback A_5, object A_6)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.l l = new global::a.f.o.l(this.a);
			this.q = new global::a.o(l, null);
			this.q.a(l.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6));
			return this.q;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001B0C4 File Offset: 0x0001A0C4
		public bool ah()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.l))
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
				result = ((global::a.f.o.l)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001B15C File Offset: 0x0001A15C
		private new void d(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			t.b(A_0, A_1, A_2, A_3, true);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001B188 File Offset: 0x0001A188
		public new bool b(bool A_0, string A_1, bool A_2, string A_3, UidPlusResult A_4)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.d(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						this.d(A_1, A_2, A_3, A_4);
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

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B218 File Offset: 0x0001A218
		private new void c(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			t.b(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001B240 File Offset: 0x0001A240
		public new bool a(bool A_0, string A_1, bool A_2, string A_3, UidPlusResult A_4)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.c(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						this.c(A_1, A_2, A_3, A_4);
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

		// Token: 0x0600064C RID: 1612 RVA: 0x0001B2D0 File Offset: 0x0001A2D0
		public new IAsyncResult a(string A_0, bool A_1, string A_2, UidPlusResult A_3, bool A_4, AsyncCallback A_5, object A_6)
		{
			this.p.k(true);
			base.bl();
			global::a.f.o.u u;
			if (A_4)
			{
				u = new global::a.f.o.u(this.a);
			}
			else
			{
				u = new global::a.f.o.u(this.b);
			}
			this.q = new global::a.o(u, null);
			this.q.a(u.BeginInvoke(false, A_0, A_1, A_2, A_3, A_5, A_6));
			return this.q;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001B340 File Offset: 0x0001A340
		public bool z()
		{
			if (this.q == null || !(this.q.c() is global::a.f.o.u))
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
				result = ((global::a.f.o.u)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001B3D8 File Offset: 0x0001A3D8
		private void g()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			t.q();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001B3FC File Offset: 0x0001A3FC
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

		// Token: 0x06000650 RID: 1616 RVA: 0x0001B480 File Offset: 0x0001A480
		public new virtual IAsyncResult a(AsyncCallback A_0, object A_1)
		{
			this.p.k(true);
			base.bl();
			global::a.h.d d = new global::a.h.d(this.f);
			this.q = new global::a.o(d, null);
			this.q.a(d.BeginInvoke(false, A_0, A_1));
			return this.q;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001B4D3 File Offset: 0x0001A4D3
		public bool am()
		{
			return base.a3();
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001B4DB File Offset: 0x0001A4DB
		public void ad()
		{
			((global::a.f.t)this.p).g();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001B4ED File Offset: 0x0001A4ED
		public bool al()
		{
			return ((global::a.f.t)this.p).z();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001B4FF File Offset: 0x0001A4FF
		public bool aa()
		{
			return ((global::a.f.t)this.p).ad();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001B511 File Offset: 0x0001A511
		public string[] s(string A_0)
		{
			return ((global::a.f.t)this.p).i(A_0);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001B524 File Offset: 0x0001A524
		public new int o()
		{
			return ((global::a.f.t)this.p).l();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001B536 File Offset: 0x0001A536
		public int w()
		{
			return ((global::a.f.t)this.p).ac();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001B548 File Offset: 0x0001A548
		public int ac()
		{
			return ((global::a.f.t)this.p).u();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B55A File Offset: 0x0001A55A
		public long ag()
		{
			return ((global::a.f.t)this.p).n();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001B56C File Offset: 0x0001A56C
		public new long m()
		{
			return ((global::a.f.t)this.p).w();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001B57E File Offset: 0x0001A57E
		public MessageFlagSet x()
		{
			return ((global::a.f.t)this.p).j();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001B590 File Offset: 0x0001A590
		public MessageFlagSet af()
		{
			return ((global::a.f.t)this.p).k();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001B5A2 File Offset: 0x0001A5A2
		public bool r()
		{
			return ((global::a.f.t)this.p).ae();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001B5B4 File Offset: 0x0001A5B4
		public new void d(bool A_0)
		{
			if (this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			((global::a.f.t)this.p).e(A_0);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001B5DB File Offset: 0x0001A5DB
		public EnvelopeCollection ab()
		{
			return ((global::a.f.t)this.p).v();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001B5ED File Offset: 0x0001A5ED
		public bool an()
		{
			return ((global::a.f.t)this.p).m();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001B5FF File Offset: 0x0001A5FF
		public void h(bool A_0)
		{
			((global::a.f.t)this.p).h(A_0);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001B612 File Offset: 0x0001A612
		public new bool n()
		{
			return ((global::a.f.t)this.p).ab();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001B624 File Offset: 0x0001A624
		public new void c(bool A_0)
		{
			((global::a.f.t)this.p).d(A_0);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001B637 File Offset: 0x0001A637
		public override bool j()
		{
			return this.a != null && this.a.o();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001B64E File Offset: 0x0001A64E
		public override void k(ErrorEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnErrorOccurred(A_0);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001B664 File Offset: 0x0001A664
		public override bool l()
		{
			return this.a != null && this.a.r();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001B67B File Offset: 0x0001A67B
		public override void m(LogNewEntryEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLogNewEntry(A_0);
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001B691 File Offset: 0x0001A691
		public override bool b()
		{
			return this.a != null && this.a.a();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001B6A8 File Offset: 0x0001A6A8
		public override void c(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataReceived(A_0);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001B6BE File Offset: 0x0001A6BE
		public override bool d()
		{
			return this.a != null && this.a.f();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001B6D5 File Offset: 0x0001A6D5
		public override void e(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDataSent(A_0);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001B6EB File Offset: 0x0001A6EB
		public override bool f()
		{
			return this.a != null && this.a.h();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001B702 File Offset: 0x0001A702
		public override void g(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataReceived(A_0);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001B718 File Offset: 0x0001A718
		public override bool h()
		{
			return this.a != null && this.a.c();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001B72F File Offset: 0x0001A72F
		public override void i(DataTransferEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLowLevelDataSent(A_0);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001B745 File Offset: 0x0001A745
		public override bool bx()
		{
			return this.a != null && this.a.e();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001B75C File Offset: 0x0001A75C
		public override void by(HostResolvedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnHostResolved(A_0);
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001B772 File Offset: 0x0001A772
		public override bool bz()
		{
			return this.a != null && this.a.m();
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001B789 File Offset: 0x0001A789
		public override void b0(SocketCreatingEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketCreating(A_0);
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001B79F File Offset: 0x0001A79F
		public override bool b1()
		{
			return this.a != null && this.a.j();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B7B6 File Offset: 0x0001A7B6
		public override void b2(SocketConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnSocketConnected(A_0);
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001B7CC File Offset: 0x0001A7CC
		public override bool b3()
		{
			return this.a != null && this.a.d();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001B7E3 File Offset: 0x0001A7E3
		public override void b4(ConnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnConnected(A_0);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001B7F9 File Offset: 0x0001A7F9
		public override bool b5()
		{
			return this.a != null && this.a.k();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B810 File Offset: 0x0001A810
		public override void b6(DisconnectedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnDisconnected(A_0);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B826 File Offset: 0x0001A826
		public override bool b7()
		{
			return this.a != null && this.a.n();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B83D File Offset: 0x0001A83D
		public override void b8(TlsStartedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnTlsStarted(A_0);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001B853 File Offset: 0x0001A853
		public override bool b9()
		{
			return this.a != null && this.a.g();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B86A File Offset: 0x0001A86A
		public override void ca(LoggedInEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLoggedIn(A_0);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B880 File Offset: 0x0001A880
		public bool nj()
		{
			return this.a != null && this.a.q();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001B897 File Offset: 0x0001A897
		public void nk(ImapEnvelopeDownloadedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnEnvelopeDownloaded(A_0);
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001B8AD File Offset: 0x0001A8AD
		public bool nl()
		{
			return this.a != null && this.a.p();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001B8C4 File Offset: 0x0001A8C4
		public void nm(ImapEnvelopeDataChunkReceivedEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnEnvelopeDataChunkReceived(A_0);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B8DA File Offset: 0x0001A8DA
		public bool nn()
		{
			return this.a != null && this.a.b();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B8F1 File Offset: 0x0001A8F1
		public void no(ImapServerStatusEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnServerStatus(A_0);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001B907 File Offset: 0x0001A907
		public bool np()
		{
			return this.a != null && this.a.l();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B91E File Offset: 0x0001A91E
		public void nq(ImapMessageStatusEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnMessageStatus(A_0);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B934 File Offset: 0x0001A934
		public bool nr()
		{
			return this.a != null && this.a.i();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B94B File Offset: 0x0001A94B
		public void ns(ImapIdlingEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnIdling(A_0);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B964 File Offset: 0x0001A964
		internal override Task km(at A_0, bc A_1)
		{
			global::a.f.o.q q;
			q.g = this;
			q.c = A_0;
			q.d = A_1;
			q.b = AsyncTaskMethodBuilder.Create();
			q.a = -1;
			AsyncTaskMethodBuilder b = q.b;
			b.Start<global::a.f.o.q>(ref q);
			return q.b.Task;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B9BC File Offset: 0x0001A9BC
		internal override Task mv(bf A_0, byte[] A_1, bc A_2)
		{
			global::a.f.o.i i;
			i.e = this;
			i.c = A_0;
			i.f = A_1;
			i.d = A_2;
			i.b = AsyncTaskMethodBuilder.Create();
			i.a = -1;
			AsyncTaskMethodBuilder b = i.b;
			b.Start<global::a.f.o.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001BA1C File Offset: 0x0001AA1C
		private new Task b(string A_0, string A_1)
		{
			this.p.pa();
			((global::a.ab)this.p).au();
			if (A_1 == null)
			{
				return ((global::a.ab)this.p).a(A_0, new global::a.f.v(true), true);
			}
			if (A_1 == string.Empty)
			{
				return ((global::a.ab)this.p).o4(A_0, true);
			}
			return ((global::a.ab)this.p).d(A_1 + " " + A_0 + "\r\n", new global::a.f.v(true, A_1), true);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001BAAC File Offset: 0x0001AAAC
		public new Task<bool> e(string A_0, string A_1)
		{
			global::a.f.o.y y;
			y.c = this;
			y.d = A_0;
			y.e = A_1;
			y.b = AsyncTaskMethodBuilder<bool>.Create();
			y.a = -1;
			AsyncTaskMethodBuilder<bool> b = y.b;
			b.Start<global::a.f.o.y>(ref y);
			return y.b.Task;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001BB01 File Offset: 0x0001AB01
		private Task f(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).f(A_0);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001BB30 File Offset: 0x0001AB30
		public new Task<bool> p(string A_0)
		{
			global::a.f.o.ac ac;
			ac.c = this;
			ac.d = A_0;
			ac.b = AsyncTaskMethodBuilder<bool>.Create();
			ac.a = -1;
			AsyncTaskMethodBuilder<bool> b = ac.b;
			b.Start<global::a.f.o.ac>(ref ac);
			return ac.b.Task;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001BB7D File Offset: 0x0001AB7D
		private new Task e(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).q(A_0);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001BBAC File Offset: 0x0001ABAC
		public new Task<bool> m(string A_0)
		{
			global::a.f.o.ag ag;
			ag.c = this;
			ag.d = A_0;
			ag.b = AsyncTaskMethodBuilder<bool>.Create();
			ag.a = -1;
			AsyncTaskMethodBuilder<bool> b = ag.b;
			b.Start<global::a.f.o.ag>(ref ag);
			return ag.b.Task;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001BBF9 File Offset: 0x0001ABF9
		private new Task a(string A_0, string A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).b(A_0, A_1);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001BC28 File Offset: 0x0001AC28
		public Task<bool> f(string A_0, string A_1)
		{
			global::a.f.o.z z;
			z.c = this;
			z.d = A_0;
			z.e = A_1;
			z.b = AsyncTaskMethodBuilder<bool>.Create();
			z.a = -1;
			AsyncTaskMethodBuilder<bool> b = z.b;
			b.Start<global::a.f.o.z>(ref z);
			return z.b.Task;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001BC7D File Offset: 0x0001AC7D
		private new Task d(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).k(A_0);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001BCAC File Offset: 0x0001ACAC
		public new Task<bool> q(string A_0)
		{
			global::a.f.o.p p;
			p.c = this;
			p.d = A_0;
			p.b = AsyncTaskMethodBuilder<bool>.Create();
			p.a = -1;
			AsyncTaskMethodBuilder<bool> b = p.b;
			b.Start<global::a.f.o.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001BCF9 File Offset: 0x0001ACF9
		private new Task c(string A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).r(A_0);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001BD28 File Offset: 0x0001AD28
		public new Task<bool> o(string A_0)
		{
			global::a.f.o.r r;
			r.c = this;
			r.d = A_0;
			r.b = AsyncTaskMethodBuilder<bool>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<bool> b = r.b;
			b.Start<global::a.f.o.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001BD75 File Offset: 0x0001AD75
		private new Task c(string A_0, bool A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).e(A_0, A_1);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001BDA4 File Offset: 0x0001ADA4
		public Task<bool> h(string A_0, bool A_1)
		{
			global::a.f.o.k k;
			k.c = this;
			k.d = A_0;
			k.e = A_1;
			k.b = AsyncTaskMethodBuilder<bool>.Create();
			k.a = -1;
			AsyncTaskMethodBuilder<bool> b = k.b;
			b.Start<global::a.f.o.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001BDF9 File Offset: 0x0001ADF9
		private new Task a(bool A_0)
		{
			this.p.pa();
			((global::a.f.t)this.p).t();
			return ((global::a.f.t)this.p).g(A_0);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001BE28 File Offset: 0x0001AE28
		public Task<bool> g(bool A_0)
		{
			global::a.f.o.o o;
			o.c = this;
			o.d = A_0;
			o.b = AsyncTaskMethodBuilder<bool>.Create();
			o.a = -1;
			AsyncTaskMethodBuilder<bool> b = o.b;
			b.Start<global::a.f.o.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001BE75 File Offset: 0x0001AE75
		private new Task b(string A_0, bool A_1)
		{
			this.p.pa();
			((global::a.f.t)this.p).t();
			return ((global::a.f.t)this.p).c(A_0, A_1);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001BEA4 File Offset: 0x0001AEA4
		public new Task<bool> i(string A_0, bool A_1)
		{
			global::a.f.o.aj aj;
			aj.c = this;
			aj.d = A_0;
			aj.e = A_1;
			aj.b = AsyncTaskMethodBuilder<bool>.Create();
			aj.a = -1;
			AsyncTaskMethodBuilder<bool> b = aj.b;
			b.Start<global::a.f.o.aj>(ref aj);
			return aj.b.Task;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001BEF9 File Offset: 0x0001AEF9
		private new Task<FolderStatus> b(string A_0)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.p(A_0);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001BF20 File Offset: 0x0001AF20
		public Task<FolderStatus> r(string A_0)
		{
			global::a.f.o.g g;
			g.c = this;
			g.d = A_0;
			g.b = AsyncTaskMethodBuilder<FolderStatus>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<FolderStatus> b = g.b;
			b.Start<global::a.f.o.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001BF6D File Offset: 0x0001AF6D
		private new Task<FolderQuota> a(string A_0)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.o(A_0);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001BF94 File Offset: 0x0001AF94
		public new Task<FolderQuota> n(string A_0)
		{
			global::a.f.o.ai ai;
			ai.c = this;
			ai.d = A_0;
			ai.b = AsyncTaskMethodBuilder<FolderQuota>.Create();
			ai.a = -1;
			AsyncTaskMethodBuilder<FolderQuota> b = ai.b;
			b.Start<global::a.f.o.ai>(ref ai);
			return ai.b.Task;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BFE1 File Offset: 0x0001AFE1
		private new Task<FolderCollection> a(bool A_0, string A_1, string A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.a(A_0, A_1, A_2);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C008 File Offset: 0x0001B008
		public new Task<FolderCollection> d(bool A_0, string A_1, string A_2)
		{
			global::a.f.o.s s;
			s.c = this;
			s.d = A_0;
			s.e = A_1;
			s.f = A_2;
			s.b = AsyncTaskMethodBuilder<FolderCollection>.Create();
			s.a = -1;
			AsyncTaskMethodBuilder<FolderCollection> b = s.b;
			b.Start<global::a.f.o.s>(ref s);
			return s.b.Task;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001C065 File Offset: 0x0001B065
		private new Task<MessageIndexCollection> a(bool A_0, string A_1, string A_2, string A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, A_3);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001C090 File Offset: 0x0001B090
		public new Task<MessageIndexCollection> c(bool A_0, string A_1, string A_2, string A_3)
		{
			global::a.f.o.af af;
			af.c = this;
			af.d = A_0;
			af.e = A_1;
			af.f = A_2;
			af.g = A_3;
			af.b = AsyncTaskMethodBuilder<MessageIndexCollection>.Create();
			af.a = -1;
			AsyncTaskMethodBuilder<MessageIndexCollection> b = af.b;
			b.Start<global::a.f.o.af>(ref af);
			return af.b.Task;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C0F8 File Offset: 0x0001B0F8
		private new Task<EnvelopeCollection> a(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, A_3, A_4, A_5, true, false);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001C134 File Offset: 0x0001B134
		public new Task<EnvelopeCollection> c(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5)
		{
			global::a.f.o.h h;
			h.c = this;
			h.d = A_0;
			h.e = A_1;
			h.f = A_2;
			h.g = A_3;
			h.h = A_4;
			h.i = A_5;
			h.b = AsyncTaskMethodBuilder<EnvelopeCollection>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<EnvelopeCollection> b = h.b;
			b.Start<global::a.f.o.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C1AC File Offset: 0x0001B1AC
		private new Task<EnvelopeCollection> a(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, A_3, A_4, A_5, true, false);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001C1E8 File Offset: 0x0001B1E8
		public new Task<EnvelopeCollection> c(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5)
		{
			global::a.f.o.x x;
			x.c = this;
			x.d = A_0;
			x.e = A_1;
			x.f = A_2;
			x.g = A_3;
			x.h = A_4;
			x.i = A_5;
			x.b = AsyncTaskMethodBuilder<EnvelopeCollection>.Create();
			x.a = -1;
			AsyncTaskMethodBuilder<EnvelopeCollection> b = x.b;
			b.Start<global::a.f.o.x>(ref x);
			return x.b.Task;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001C260 File Offset: 0x0001B260
		private new Task<MailMessage> a(long A_0, bool A_1, int A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, true);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001C288 File Offset: 0x0001B288
		public new Task<MailMessage> c(long A_0, bool A_1, int A_2)
		{
			global::a.f.o.d d;
			d.c = this;
			d.d = A_0;
			d.e = A_1;
			d.f = A_2;
			d.b = AsyncTaskMethodBuilder<MailMessage>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<MailMessage> b = d.b;
			b.Start<global::a.f.o.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001C2E5 File Offset: 0x0001B2E5
		private new Task<MailMessageCollection> a(string A_0, bool A_1, int A_2)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, true);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001C30C File Offset: 0x0001B30C
		public new Task<MailMessageCollection> c(string A_0, bool A_1, int A_2)
		{
			global::a.f.o.j j;
			j.c = this;
			j.d = A_0;
			j.e = A_1;
			j.f = A_2;
			j.b = AsyncTaskMethodBuilder<MailMessageCollection>.Create();
			j.a = -1;
			AsyncTaskMethodBuilder<MailMessageCollection> b = j.b;
			b.Start<global::a.f.o.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001C369 File Offset: 0x0001B369
		private new Task<long> e()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.o();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001C38C File Offset: 0x0001B38C
		public new Task<long> q()
		{
			global::a.f.o.v v;
			v.c = this;
			v.b = AsyncTaskMethodBuilder<long>.Create();
			v.a = -1;
			AsyncTaskMethodBuilder<long> b = v.b;
			b.Start<global::a.f.o.v>(ref v);
			return v.b.Task;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001C3D1 File Offset: 0x0001B3D1
		private new Task<ImapNamespaceCollectionSet> c()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.aw();
			return t.y();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001C3F4 File Offset: 0x0001B3F4
		public Task<ImapNamespaceCollectionSet> t()
		{
			global::a.f.o.aa aa;
			aa.c = this;
			aa.b = AsyncTaskMethodBuilder<ImapNamespaceCollectionSet>.Create();
			aa.a = -1;
			AsyncTaskMethodBuilder<ImapNamespaceCollectionSet> b = aa.b;
			b.Start<global::a.f.o.aa>(ref aa);
			return aa.b.Task;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001C439 File Offset: 0x0001B439
		private new Task a(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5)
		{
			this.p.pa();
			((global::a.f.t)this.p).aw();
			return ((global::a.f.t)this.p).b(A_0, A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001C470 File Offset: 0x0001B470
		public new Task<bool> c(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5)
		{
			global::a.f.o.m m;
			m.c = this;
			m.d = A_0;
			m.e = A_1;
			m.f = A_2;
			m.g = A_3;
			m.h = A_4;
			m.i = A_5;
			m.b = AsyncTaskMethodBuilder<bool>.Create();
			m.a = -1;
			AsyncTaskMethodBuilder<bool> b = m.b;
			b.Start<global::a.f.o.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001C4E8 File Offset: 0x0001B4E8
		private new Task a(string A_0, bool A_1)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.h(A_0, A_1);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001C510 File Offset: 0x0001B510
		public Task<bool> g(string A_0, bool A_1)
		{
			global::a.f.o.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<global::a.f.o.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001C565 File Offset: 0x0001B565
		private new Task a(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.b(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001C590 File Offset: 0x0001B590
		public new Task<bool> c(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			global::a.f.o.ad ad;
			ad.c = this;
			ad.d = A_0;
			ad.e = A_1;
			ad.f = A_2;
			ad.g = A_3;
			ad.h = A_4;
			ad.b = AsyncTaskMethodBuilder<bool>.Create();
			ad.a = -1;
			AsyncTaskMethodBuilder<bool> b = ad.b;
			b.Start<global::a.f.o.ad>(ref ad);
			return ad.b.Task;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001C5FF File Offset: 0x0001B5FF
		private new Task b(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.c(A_0, A_1, A_2, A_3, true);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001C628 File Offset: 0x0001B628
		public Task<bool> f(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			global::a.f.o.ak ak;
			ak.c = this;
			ak.d = A_0;
			ak.e = A_1;
			ak.f = A_2;
			ak.g = A_3;
			ak.b = AsyncTaskMethodBuilder<bool>.Create();
			ak.a = -1;
			AsyncTaskMethodBuilder<bool> b = ak.b;
			b.Start<global::a.f.o.ak>(ref ak);
			return ak.b.Task;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001C68E File Offset: 0x0001B68E
		private new Task a(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.a(A_0, A_1, A_2, A_3);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001C6B8 File Offset: 0x0001B6B8
		public new Task<bool> e(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			global::a.f.o.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.g = A_3;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> b2 = b.b;
			b2.Start<global::a.f.o.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001C71E File Offset: 0x0001B71E
		private new Task a()
		{
			this.p.pa();
			global::a.f.t t = (global::a.f.t)this.p;
			t.t();
			return t.r();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001C744 File Offset: 0x0001B744
		public Task<bool> aj()
		{
			global::a.f.o.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder<bool>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<bool> b = f.b;
			b.Start<global::a.f.o.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001C789 File Offset: 0x0001B789
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(bf A_0, byte[] A_1, bc A_2)
		{
			return base.mv(A_0, A_1, A_2);
		}

		// Token: 0x040003F8 RID: 1016
		private new Imap a;

		// Token: 0x020000BA RID: 186
		// (Invoke) Token: 0x060006E5 RID: 1765
		protected delegate bool w(bool A_0, string A_1, string A_2);

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x060006E9 RID: 1769
		protected delegate bool ae(bool A_0, string A_1, bool A_2);

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060006ED RID: 1773
		protected delegate bool ah(bool A_0, bool A_1);

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060006F1 RID: 1777
		protected new delegate FolderCollection e(bool A_0, bool A_1, string A_2, string A_3);

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x060006F5 RID: 1781
		protected new delegate MessageIndexCollection c(bool A_0, bool A_1, string A_2, string A_3, string A_4);

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x060006F9 RID: 1785
		protected new delegate EnvelopeCollection t(bool A_0, string A_1, bool A_2, EnvelopeParts A_3, int A_4, string[] A_5, string[] A_6);

		// Token: 0x020000C0 RID: 192
		// (Invoke) Token: 0x060006FD RID: 1789
		protected delegate EnvelopeCollection ab(bool A_0, long[] A_1, bool A_2, EnvelopeParts[] A_3, int[] A_4, string[][] A_5, string[][] A_6);

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x06000701 RID: 1793
		protected new delegate bool n(bool A_0, MailMessage A_1, string A_2, string A_3, string A_4, bool A_5, UidPlusResult A_6);

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x06000705 RID: 1797
		protected new delegate bool l(bool A_0, string A_1, bool A_2, string A_3, MessageFlagAction A_4, bool A_5);

		// Token: 0x020000C3 RID: 195
		// (Invoke) Token: 0x06000709 RID: 1801
		protected delegate bool u(bool A_0, string A_1, bool A_2, string A_3, UidPlusResult A_4);
	}
}
