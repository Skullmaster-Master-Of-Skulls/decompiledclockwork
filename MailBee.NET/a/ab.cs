using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using a.j;
using MailBee;
using MailBee.Security;

namespace a
{
	// Token: 0x0200008F RID: 143
	internal abstract class ab : be
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00011900 File Offset: 0x00010900
		public ab(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.d = false;
			this.e = false;
			this.f = false;
			this.g = AuthenticationMethods.None;
			this.h = null;
			this.i = false;
			this.l = null;
			this.m = null;
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = null;
			this.r = null;
			if (this.b != null)
			{
				this.az();
				this.l = (ab.g)Delegate.Combine(this.l, new ab.g(this.a));
				this.m = (global::a.m)Delegate.Combine(this.m, new global::a.m(this.a));
				this.n = (ab.i)Delegate.Combine(this.n, new ab.i(this.b));
				this.o = (ab.p)Delegate.Combine(this.o, new ab.p(this.c));
				this.p = (ab.j)Delegate.Combine(this.p, new ab.j(this.a));
				this.q = (ab.o)Delegate.Combine(this.q, new ab.o(this.e));
				this.r = (ab.f)Delegate.Combine(this.r, new ab.f(this.d));
			}
			this.k = this.fi();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011A7E File Offset: 0x00010A7E
		public override void ha()
		{
			if (this.ao())
			{
				this.fz(false);
			}
			base.ha();
		}

		// Token: 0x06000559 RID: 1369
		public abstract al fi();

		// Token: 0x0600055A RID: 1370 RVA: 0x00011A95 File Offset: 0x00010A95
		protected override void fw(MailBeeException A_0)
		{
			base.a5().d().b = false;
			this.b(A_0);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00011AB0 File Offset: 0x00010AB0
		protected void az()
		{
			global::a.h a_ = (global::a.h)this.b;
			a8 a = this.a.b();
			a.h3((global::a.m)Delegate.Combine(a.h2(), new global::a.m(a_.a)));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011AF8 File Offset: 0x00010AF8
		protected override void hb()
		{
			base.hb();
			if (this.b != null)
			{
				global::a.h a_ = (global::a.h)this.b;
				global::a.g g = this.a.d();
				g.g((ay)Delegate.Combine(g.k(), new ay(a_.kd)));
				global::a.g g2 = this.a.d();
				g2.g((global::a.c)Delegate.Combine(g2.aa(), new global::a.c(a_.ll)));
				global::a.g g3 = this.a.d();
				g3.g((a4)Delegate.Combine(g3.n(), new a4(a_.km)));
				global::a.g g4 = this.a.d();
				g4.g((global::a.b)Delegate.Combine(g4.w(), new global::a.b(a_.mv)));
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011BD6 File Offset: 0x00010BD6
		public new void a(IPHostEntry A_0)
		{
			if (this.l != null)
			{
				base.a(this.l, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011BFC File Offset: 0x00010BFC
		public new void a(IPHostEntry A_0, bc A_1)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.bx() && !this.b.bf())
			{
				HostResolvedEventArgs a_ = new HostResolvedEventArgs(A_0, A_1);
				a.by(a_);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00011C46 File Offset: 0x00010C46
		public new void a(ac A_0)
		{
			if (this.m != null)
			{
				base.a(this.m, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00011C6C File Offset: 0x00010C6C
		public new void a(ac A_0, bc A_1)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.bz() && !this.b.bf())
			{
				SocketCreatingEventArgs a_ = new SocketCreatingEventArgs(A_0, ((ab)A_1).a1(), A_1);
				a.b0(a_);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00011CC1 File Offset: 0x00010CC1
		public void ai()
		{
			if (this.n != null)
			{
				base.a(this.n, new object[]
				{
					this
				});
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00011CE4 File Offset: 0x00010CE4
		public new void b(bc A_0)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.b1() && !this.b.bf())
			{
				SocketConnectedEventArgs a_ = new SocketConnectedEventArgs(((be)A_0).a1(), A_0);
				a.b2(a_);
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011D38 File Offset: 0x00010D38
		public void am()
		{
			if (this.o != null)
			{
				base.a(this.o, new object[]
				{
					this
				});
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00011D5C File Offset: 0x00010D5C
		public new void c(bc A_0)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.b3() && !this.b.bf())
			{
				ConnectedEventArgs a_ = new ConnectedEventArgs(((be)A_0).a1(), A_0);
				a.b4(a_);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00011DB0 File Offset: 0x00010DB0
		public new void i(bool A_0)
		{
			if (this.p != null)
			{
				base.a(this.p, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00011DDC File Offset: 0x00010DDC
		public new void a(bool A_0, bc A_1)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.b5() && !this.b.bf())
			{
				DisconnectedEventArgs a_ = new DisconnectedEventArgs(A_0, ((be)A_1).a1(), A_1);
				a.b6(a_);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011E31 File Offset: 0x00010E31
		public void at()
		{
			if (this.q != null)
			{
				base.a(this.q, new object[]
				{
					this
				});
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011E54 File Offset: 0x00010E54
		public new void e(bc A_0)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.b7() && !this.b.bf())
			{
				TlsStartedEventArgs a_ = new TlsStartedEventArgs(((be)A_0).a1(), A_0);
				a.b8(a_);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00011EA8 File Offset: 0x00010EA8
		public void ag()
		{
			if (this.r != null)
			{
				base.a(this.r, new object[]
				{
					this
				});
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00011ECC File Offset: 0x00010ECC
		public new void d(bc A_0)
		{
			a9 a = (a9)this.b;
			if (this.b.bq() && a.b9() && !this.b.bf())
			{
				LoggedInEventArgs a_ = new LoggedInEventArgs(((be)A_0).a1(), A_0);
				a.ca(a_);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00011F20 File Offset: 0x00010F20
		protected virtual void fj()
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00011F22 File Offset: 0x00010F22
		protected virtual void fk()
		{
			this.d = false;
			this.e = false;
			this.g = AuthenticationMethods.None;
			this.h = null;
			this.f = false;
			base.a5().d().b = false;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00011F58 File Offset: 0x00010F58
		public virtual al av()
		{
			return this.k;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00011F60 File Offset: 0x00010F60
		public virtual void f0(al A_0)
		{
			this.k = A_0;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00011F69 File Offset: 0x00010F69
		public at ak()
		{
			return base.a5().d().q();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00011F7C File Offset: 0x00010F7C
		public string an()
		{
			global::a.g g = base.a5().d();
			if (g.o())
			{
				return this.bg().GetString(g.r(), 0, g.v());
			}
			for (int i = g.p().Count - 1; i > -1; i--)
			{
				if (g.p().a(i).q() != null)
				{
					return this.bg().GetString(g.p().a(i).q(), 0, g.p().a(i).q().Length);
				}
			}
			return null;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00012013 File Offset: 0x00011013
		public override void hc(Encoding A_0)
		{
			base.hc(A_0);
			this.a.d().c = A_0;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001202D File Offset: 0x0001102D
		public override void hd(Encoding A_0)
		{
			base.hd(A_0);
			this.a.d().d = A_0;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00012047 File Offset: 0x00011047
		public bool ao()
		{
			return base.a5() != null && !base.a5().a() && base.a5().d().hl();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00012070 File Offset: 0x00011070
		public bool ah()
		{
			return this.d;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00012078 File Offset: 0x00011078
		public bool ar()
		{
			return this.e;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00012080 File Offset: 0x00011080
		protected internal virtual string o2(string A_0, bf A_1)
		{
			return A_0 + "\r\n";
		}

		// Token: 0x06000577 RID: 1399
		protected internal abstract bf fg(bool A_0);

		// Token: 0x06000578 RID: 1400 RVA: 0x0001208D File Offset: 0x0001108D
		internal bh aj()
		{
			return this.j;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012095 File Offset: 0x00011095
		public StringDictionary ax()
		{
			return this.h;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000120A0 File Offset: 0x000110A0
		public string t(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			A_0 = A_0.ToLower();
			if (this.h == null)
			{
				return null;
			}
			if (!this.h.ContainsKey(A_0))
			{
				return null;
			}
			string text = this.h[A_0];
			if (text.Length > 0)
			{
				return A_0 + " " + text;
			}
			return A_0;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000120FE File Offset: 0x000110FE
		public string s(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			A_0 = A_0.ToLower();
			if (this.h == null)
			{
				return null;
			}
			if (this.h.ContainsKey(A_0))
			{
				return this.h[A_0];
			}
			return null;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00012139 File Offset: 0x00011139
		protected virtual bool fm(string A_0, ref int A_1, SslStartupMode A_2, ref bool A_3)
		{
			return Global.AutodetectPortAndSslMode && A_2 == SslStartupMode.Manual;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00012148 File Offset: 0x00011148
		protected virtual bool fn(string A_0, int A_1, SslStartupMode A_2, ref bool A_3)
		{
			return Global.AutodetectPortAndSslMode && A_2 == SslStartupMode.Manual;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00012158 File Offset: 0x00011158
		public virtual void fy()
		{
			string a_ = this.k.v();
			int a_2 = this.k.w();
			bool a_3 = this.k.ac() == SslStartupMode.OnConnect;
			this.fm(a_, ref a_2, this.k.ac(), ref a_3);
			this.a(this.k.v(), a_2, a_3, this.k.n(), this.k.s(), this.k.o());
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000121D8 File Offset: 0x000111D8
		protected new void a(string A_0, int A_1, bool A_2, Socket A_3, EndPoint A_4, int A_5)
		{
			global::a.h h = (global::a.h)this.b;
			a9 a = (a9)this.b;
			this.g = AuthenticationMethods.None;
			base.a1().a();
			IPHostEntry iphostEntry = null;
			IPAddress address;
			bool flag = IPAddress.TryParse(A_0, out address);
			if (!flag)
			{
				this.d.b(string.Format(Resources.Instance.Log_WillResolveHost0, A_0), null, LogMessageType.Info, this);
				base.a5().d().cf();
				iphostEntry = base.a5().d().hk(A_0);
				if (this.d.Enabled)
				{
					string text = string.Empty;
					foreach (IPAddress ipaddress in iphostEntry.AddressList)
					{
						if (text.Length > 0)
						{
							text = text + ", " + ipaddress.ToString();
						}
						else
						{
							text = ipaddress.ToString();
						}
					}
					this.d.b(string.Format(Resources.Instance.Log_Host0ResolvedToIP1, A_0, text), null, LogMessageType.Info, this);
				}
				if (a != null && a.bx() && !h.bf())
				{
					this.a(iphostEntry);
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_WillConnectToHost0OnPort1, A_0, A_1.ToString()), null, LogMessageType.Info, this);
			r r = this.k.y().a();
			if (r != null)
			{
				this.d.b(string.Format(Resources.Instance.Log_WillConnectVia0ProxyAtHost1OnPort2, this.k.y().ProtocolName, this.k.y().Name, this.k.y().Port.ToString()), null, LogMessageType.Info, this);
				this.a.d(r);
			}
			base.a5().d().cf();
			base.a1().a(A_0);
			base.a5().d().hx(A_4);
			base.a5().d().hu(A_3);
			if (flag)
			{
				base.a5().d().d1(new IPEndPoint(address, A_1));
			}
			else
			{
				base.a5().d().g(iphostEntry, A_1);
			}
			this.d.b(string.Format(Resources.Instance.Log_SocketConnectedToIPAddress0OnPort1, base.a1().d().Address.ToString(), base.a1().d().Port.ToString()), null, LogMessageType.Info, this);
			if (a != null && a.b1() && !h.bf())
			{
				this.ai();
			}
			if (A_2)
			{
				this.c();
			}
			base.a5().d().ci();
			base.a5().d().m(A_5);
			base.a5().d().o(A_5);
			base.a5().d().s();
			this.fj();
			base.a1().a(true);
			this.d.b(string.Format(Resources.Instance.Log_ConnectedToServerAtHost0OnPort1, A_0, A_1.ToString()), null, LogMessageType.Info, this);
			if (a != null && a.b3() && !h.bf())
			{
				this.am();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001250C File Offset: 0x0001150C
		public virtual void fz(bool A_0)
		{
			bool a_ = false;
			global::a.h h = (global::a.h)this.b;
			a9 a = (a9)this.b;
			if (A_0)
			{
				try
				{
					this.o1(this.j.jx(), true);
					base.a5().d().o(0);
					int num = this.a.d().hy();
					try
					{
						this.a.d().hz(1000);
						base.a5().d().ci();
						try
						{
							base.a5().d().o(0);
						}
						catch (MailBeeSocketTimeoutException a_2)
						{
							base.c(a_2);
						}
					}
					finally
					{
						if (num != 1000)
						{
							this.a.d().hz(num);
						}
					}
				}
				catch (MailBeeAbortedByRemoteHostException a_3)
				{
					base.c(a_3);
				}
				catch (MailBeeSocketResetException a_4)
				{
					base.c(a_4);
				}
				catch (MailBeeEmailProtocolNegativeResponseException a_5)
				{
					base.c(a_5);
				}
				a_ = true;
				this.i = false;
			}
			this.d.b(string.Format(Resources.Instance.Log_WillDisconnectFromHost0, base.a1().b()), null, LogMessageType.Info, this);
			try
			{
				base.a5().d().d2();
			}
			catch (MailBeeException a_6)
			{
				base.c(a_6);
			}
			base.a6();
			this.fk();
			this.d.b(string.Format(Resources.Instance.Log_DisconnectedFromHost0, base.a1().b()), null, LogMessageType.Info, this);
			if (a != null && a.b5() && !h.bf())
			{
				this.i(a_);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000126E0 File Offset: 0x000116E0
		public void @as()
		{
			if (this.ao())
			{
				this.fz(false);
				this.i = true;
				return;
			}
			if (this.a.e().i())
			{
				this.fk();
				this.d.b(string.Format(Resources.Instance.Log_DisconnectedFromHost0, base.a1().b()), null, LogMessageType.Info, this);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00012744 File Offset: 0x00011744
		public override void he()
		{
			if (this.ao())
			{
				try
				{
					base.a5().d().d2();
				}
				catch (MailBeeException)
				{
				}
				base.a6();
				this.fk();
			}
			else if (this.a.e().i())
			{
				this.fk();
			}
			base.he();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000127AC File Offset: 0x000117AC
		public new void b(Exception A_0)
		{
			bool flag = A_0 is IMailBeeNegativeResponseException && this.a.d().p().Count > 0 && this.a.d().q().t() == af.d;
			if (A_0 is IMailBeeFatalException || A_0 is IMailBeeSocketMustCloseException || flag)
			{
				if (flag)
				{
					base.c(new MailBeeAbortedByRemoteHostException(55, base.a1()));
				}
				this.@as();
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001282C File Offset: 0x0001182C
		public bool a0()
		{
			return this.i;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00012834 File Offset: 0x00011834
		public new void j(bool A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001283D File Offset: 0x0001183D
		protected virtual void oz()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00012840 File Offset: 0x00011840
		public new bool c(string A_0, bf A_1, bool A_2)
		{
			base.a5().d().cf();
			base.a5().d().g(A_0, A_1, 0);
			base.a5().d().o(0);
			this.oz();
			switch (this.a.d().m())
			{
			case af.a:
				return true;
			case af.b:
				return true;
			case af.c:
				if (A_2)
				{
					base.a5().d().s();
				}
				return false;
			case af.d:
				throw new MailBeeAbortedByRemoteHostException(55, base.a1());
			case af.e:
				base.a5().d().s();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000128EE File Offset: 0x000118EE
		public virtual bool o0(string A_0, bool A_1)
		{
			return this.c(A_0, this.fg(true), A_1);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000128FF File Offset: 0x000118FF
		public new bool b(string A_0, bf A_1, bool A_2)
		{
			return this.c(this.o2(A_0, A_1), A_1, A_2);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00012911 File Offset: 0x00011911
		public virtual bool o1(string A_0, bool A_1)
		{
			return this.b(A_0, this.fg(true), A_1);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00012922 File Offset: 0x00011922
		public void ay()
		{
			if (this.ao())
			{
				throw new MailBeeInvalidStateException(101);
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00012934 File Offset: 0x00011934
		public void aq()
		{
			if (!this.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00012946 File Offset: 0x00011946
		public virtual void fu()
		{
			if (!this.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
			if (this.d)
			{
				throw new MailBeeInvalidStateException(102);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00012968 File Offset: 0x00011968
		public virtual void fv()
		{
			if (!this.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
			if (this.e)
			{
				throw new MailBeeInvalidStateException(111);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001298A File Offset: 0x0001198A
		public virtual void au()
		{
			if (!this.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001299C File Offset: 0x0001199C
		public virtual void aw()
		{
			if (!this.ao())
			{
				throw new MailBeeInvalidStateException(100);
			}
			if (!this.e)
			{
				throw new MailBeeInvalidStateException(110);
			}
		}

		// Token: 0x06000591 RID: 1425
		protected abstract u fh();

		// Token: 0x06000592 RID: 1426 RVA: 0x000129C0 File Offset: 0x000119C0
		private new void b(AuthenticationMethods A_0, AuthenticationMethods A_1, SaslMethod A_2, AuthenticationOptions A_3, string A_4, string A_5, string A_6, string A_7)
		{
			global::a.h h = (global::a.h)this.b;
			a9 a = (a9)this.b;
			if ((A_3 & AuthenticationOptions.BypassLoginProcedure) > AuthenticationOptions.None)
			{
				this.e = true;
				return;
			}
			if (A_0 != AuthenticationMethods.None)
			{
				this.d.b(string.Format(Resources.Instance.Log_WillLoginAs0, A_6), null, LogMessageType.Info, this);
				this.fh().b(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				this.e = true;
				this.d.b(string.Format(Resources.Instance.Log_LoggedInAs0, A_6), null, LogMessageType.Info, this);
				if (a != null && a.b9() && !h.bf())
				{
					this.ag();
				}
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00012A70 File Offset: 0x00011A70
		public virtual void fo()
		{
			this.b(this.k.x(), this.g, this.k.r(), this.k.ae(), this.k.ad(), this.k.z(), this.k.q(), this.k.aa());
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00012AD6 File Offset: 0x00011AD6
		public AuthenticationMethods ap()
		{
			return this.g;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00012AE0 File Offset: 0x00011AE0
		private new void c()
		{
			global::a.j.h h = new global::a.j.h(this.k.p());
			this.a.d(h);
			h.g(this.k.af(), this.k.v());
			this.d = true;
			global::a.h h2 = (global::a.h)this.b;
			if (h2 != null && h2.bq() && !h2.bf() && (a9)this.b != null)
			{
				this.at();
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00012B60 File Offset: 0x00011B60
		public virtual void fp(bool A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_StartTls, new object[0]), null, LogMessageType.Info, this);
			this.o1(this.j.hj(), true);
			this.c();
			this.h = null;
			this.g = AuthenticationMethods.None;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00012BB8 File Offset: 0x00011BB8
		public override Task hf()
		{
			ab.m m;
			m.c = this;
			m.b = AsyncTaskMethodBuilder.Create();
			m.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = m.b;
			asyncTaskMethodBuilder.Start<ab.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00012C00 File Offset: 0x00011C00
		protected override Task f1(MailBeeException A_0)
		{
			ab.k k;
			k.c = this;
			k.d = A_0;
			k.b = AsyncTaskMethodBuilder.Create();
			k.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = k.b;
			asyncTaskMethodBuilder.Start<ab.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00012C4D File Offset: 0x00011C4D
		protected virtual Task fq()
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00012C58 File Offset: 0x00011C58
		public virtual Task f2()
		{
			string a_ = this.k.v();
			int a_2 = this.k.w();
			bool a_3 = this.k.ac() == SslStartupMode.OnConnect;
			this.fm(a_, ref a_2, this.k.ac(), ref a_3);
			return this.b(this.k.v(), a_2, a_3, this.k.n(), this.k.s(), this.k.o());
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012CD8 File Offset: 0x00011CD8
		protected new Task b(string A_0, int A_1, bool A_2, Socket A_3, EndPoint A_4, int A_5)
		{
			ab.n n;
			n.c = this;
			n.d = A_0;
			n.h = A_1;
			n.n = A_2;
			n.k = A_3;
			n.j = A_4;
			n.o = A_5;
			n.b = AsyncTaskMethodBuilder.Create();
			n.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<ab.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00012D50 File Offset: 0x00011D50
		public virtual Task f3(bool A_0)
		{
			ab.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<ab.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00012DA0 File Offset: 0x00011DA0
		public new Task a(Exception A_0)
		{
			ab.h h;
			h.d = this;
			h.c = A_0;
			h.b = AsyncTaskMethodBuilder.Create();
			h.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = h.b;
			asyncTaskMethodBuilder.Start<ab.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00012DF0 File Offset: 0x00011DF0
		public Task al()
		{
			ab.e e;
			e.c = this;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<ab.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00012E38 File Offset: 0x00011E38
		public new Task<bool> d(string A_0, bf A_1, bool A_2)
		{
			ab.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<ab.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00012E95 File Offset: 0x00011E95
		public virtual Task<bool> o3(string A_0, bool A_1)
		{
			return this.d(A_0, this.fg(true), A_1);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00012EA6 File Offset: 0x00011EA6
		public new Task<bool> a(string A_0, bf A_1, bool A_2)
		{
			return this.d(this.o2(A_0, A_1), A_1, A_2);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00012EB8 File Offset: 0x00011EB8
		public virtual Task<bool> o4(string A_0, bool A_1)
		{
			return this.a(A_0, this.fg(true), A_1);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00012ECC File Offset: 0x00011ECC
		private new Task a(AuthenticationMethods A_0, AuthenticationMethods A_1, SaslMethod A_2, AuthenticationOptions A_3, string A_4, string A_5, string A_6, string A_7)
		{
			ab.c c;
			c.c = this;
			c.e = A_0;
			c.g = A_1;
			c.h = A_2;
			c.d = A_3;
			c.i = A_4;
			c.j = A_5;
			c.f = A_6;
			c.k = A_7;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<ab.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00012F58 File Offset: 0x00011F58
		public virtual Task fr()
		{
			return this.a(this.k.x(), this.g, this.k.r(), this.k.ae(), this.k.ad(), this.k.z(), this.k.q(), this.k.aa());
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00012FC0 File Offset: 0x00011FC0
		private new Task b()
		{
			ab.a a;
			a.c = this;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<ab.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00013008 File Offset: 0x00012008
		public virtual Task fs(bool A_0)
		{
			ab.l l;
			l.c = this;
			l.b = AsyncTaskMethodBuilder.Create();
			l.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = l.b;
			asyncTaskMethodBuilder.Start<ab.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001304D File Offset: 0x0001204D
		[DebuggerHidden]
		[CompilerGenerated]
		private new Task a()
		{
			return base.hf();
		}

		// Token: 0x040002D7 RID: 727
		protected new const string a = ".office365.com";

		// Token: 0x040002D8 RID: 728
		protected new const string b = ".outlook.com";

		// Token: 0x040002D9 RID: 729
		private new const int c = 1000;

		// Token: 0x040002DA RID: 730
		protected new bool d;

		// Token: 0x040002DB RID: 731
		protected new bool e;

		// Token: 0x040002DC RID: 732
		protected new bool f;

		// Token: 0x040002DD RID: 733
		protected new AuthenticationMethods g;

		// Token: 0x040002DE RID: 734
		protected StringDictionary h;

		// Token: 0x040002DF RID: 735
		protected new bool i;

		// Token: 0x040002E0 RID: 736
		protected new bh j;

		// Token: 0x040002E1 RID: 737
		protected new al k;

		// Token: 0x040002E2 RID: 738
		private new ab.g l;

		// Token: 0x040002E3 RID: 739
		private new global::a.m m;

		// Token: 0x040002E4 RID: 740
		private ab.i n;

		// Token: 0x040002E5 RID: 741
		private ab.p o;

		// Token: 0x040002E6 RID: 742
		private ab.j p;

		// Token: 0x040002E7 RID: 743
		private ab.o q;

		// Token: 0x040002E8 RID: 744
		private ab.f r;

		// Token: 0x020004D3 RID: 1235
		// (Invoke) Token: 0x060029DD RID: 10717
		protected new delegate void g(IPHostEntry A_0, bc A_1);

		// Token: 0x020004D4 RID: 1236
		// (Invoke) Token: 0x060029E1 RID: 10721
		protected new delegate void i(bc A_0);

		// Token: 0x020004D5 RID: 1237
		// (Invoke) Token: 0x060029E5 RID: 10725
		protected delegate void p(bc A_0);

		// Token: 0x020004D6 RID: 1238
		// (Invoke) Token: 0x060029E9 RID: 10729
		protected new delegate void j(bool A_0, bc A_1);

		// Token: 0x020004D7 RID: 1239
		// (Invoke) Token: 0x060029ED RID: 10733
		protected delegate void o(bc A_0);

		// Token: 0x020004D8 RID: 1240
		// (Invoke) Token: 0x060029F1 RID: 10737
		protected new delegate void f(bc A_0);
	}
}
