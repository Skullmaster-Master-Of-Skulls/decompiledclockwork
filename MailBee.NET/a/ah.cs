using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020004A9 RID: 1193
	internal class ah : a8
	{
		// Token: 0x060028CE RID: 10446 RVA: 0x000BDA84 File Offset: 0x000BCA84
		public ah()
		{
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = false;
			this.u = false;
			this.v = false;
			this.t = null;
			this.w = null;
			this.x = null;
			this.y = null;
			this.aa = null;
			this.ab = null;
			this.r = null;
			this.s = Global.DefaultTimeout;
			this.m = new ai();
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000BDB08 File Offset: 0x000BCB08
		public bool i()
		{
			return this.u;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000BDB10 File Offset: 0x000BCB10
		public void g(Exception A_0)
		{
			if (this.u || (this.h0() != null && !this.h0()()))
			{
				throw new MailBeeUserAbortException(5);
			}
			if (this.v)
			{
				throw new MailBeeSocketTimeoutException(new SocketException(10060), this.m);
			}
			if (A_0 is SocketException || (A_0 is IOException && A_0.InnerException is SocketException))
			{
				if (A_0 is IOException && A_0.InnerException is SocketException)
				{
					A_0 = A_0.InnerException;
				}
				SocketException ex = (SocketException)A_0;
				int errorCode = ex.ErrorCode;
				switch (errorCode)
				{
				case 10051:
					break;
				case 10052:
					goto IL_135;
				case 10053:
					throw new MailBeeSocketAbortedException(ex, this.m);
				case 10054:
					throw new MailBeeSocketResetException(ex, this.m);
				default:
					switch (errorCode)
					{
					case 10060:
						throw new MailBeeSocketTimeoutException(ex, this.m);
					case 10061:
						throw new MailBeeSocketRefusedException(ex, this.m);
					case 10062:
					case 10063:
						goto IL_135;
					case 10064:
						throw new MailBeeSocketHostDownException(ex, this.m);
					case 10065:
						break;
					default:
						if (errorCode != 11001)
						{
							goto IL_135;
						}
						throw new MailBeeSocketHostNotFoundException(ex, this.m);
					}
					break;
				}
				throw new MailBeeSocketHostUnreachableException(ex, this.m);
				IL_135:
				throw new MailBeeSocketException(50, ex, this.m);
			}
			if (A_0 is IOException)
			{
				throw new MailBeeSocketException(30, A_0, this.m);
			}
			if (A_0 is ObjectDisposedException)
			{
				throw new MailBeeSocketObjectDisposedException(60, A_0, this.m);
			}
			throw new MailBeeExternalException(7, A_0);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000BDC96 File Offset: 0x000BCC96
		public void h()
		{
			if (this.u)
			{
				throw new MailBeeUserAbortException(5);
			}
			if (this.o == null)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000BDCB8 File Offset: 0x000BCCB8
		public override IPHostEntry hk(string A_0)
		{
			if (this.h0() != null && !this.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			IPHostEntry hostEntry;
			try
			{
				hostEntry = Dns.GetHostEntry(A_0);
			}
			catch (SocketException a_)
			{
				throw new MailBeeGetRemoteHostNameException(50, a_, A_0, this.f.c().fl());
			}
			return hostEntry;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000BDD18 File Offset: 0x000BCD18
		public override void d1(IPEndPoint A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			this.u = false;
			this.v = false;
			this.m.a(A_0);
			if (this.h0() != null && !this.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			try
			{
				if (this.o == null)
				{
					this.p = this.g(A_0);
				}
				else
				{
					this.p = this.o;
				}
				if (this.r != null && this.p.LocalEndPoint == null)
				{
					this.p.Bind(this.r);
				}
				if (this.s == Global.c)
				{
					this.p.Connect(A_0);
				}
				else
				{
					IAsyncResult asyncResult = this.p.BeginConnect(A_0, null, null);
					if (!asyncResult.AsyncWaitHandle.WaitOne((this.s == 0) ? -1 : this.s, false))
					{
						this.p.Close();
						throw new SocketException(10060);
					}
					this.p.EndConnect(asyncResult);
				}
			}
			catch (SocketException a_)
			{
				this.o = null;
				this.p = null;
				this.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.o = null;
				this.p = null;
				this.g(a_2);
			}
			if (!this.p.Connected)
			{
				this.p = null;
				throw new a2();
			}
			this.o = this.p;
			this.q = true;
			this.p = null;
			this.n = new ad(this.o, this);
			this.g(this.hy());
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000BDEBC File Offset: 0x000BCEBC
		public override void d2()
		{
			if (this.u)
			{
				this.n = null;
				this.o = null;
				this.q = false;
				return;
			}
			if (this.o != null)
			{
				try
				{
					if (this.o.Connected)
					{
						this.o.Shutdown(SocketShutdown.Both);
					}
				}
				catch (SocketException a_)
				{
					this.g(a_);
				}
				catch (ObjectDisposedException a_2)
				{
					this.g(a_2);
				}
				finally
				{
					this.n = null;
					this.o.Close();
					this.o = null;
					this.z = null;
					this.q = false;
				}
			}
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000BDF70 File Offset: 0x000BCF70
		public void k()
		{
			Socket socket = this.o;
			if (socket != null)
			{
				this.u = true;
				this.n = null;
				socket.Close();
				this.q = false;
			}
			socket = this.p;
			if (socket != null)
			{
				this.u = true;
				socket.Close();
				this.q = false;
			}
			this.z = null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000BDFC7 File Offset: 0x000BCFC7
		public override bool hl()
		{
			return this.q;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000BDFD0 File Offset: 0x000BCFD0
		public override int d4(byte[] A_0, int A_1, int A_2)
		{
			int num = 0;
			if (this.n == null)
			{
				this.h();
			}
			if (A_2 == 0)
			{
				return 0;
			}
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.n == null && this.h0() != null && !this.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			if (this.n != null)
			{
				this.n.Write(A_0, A_1, A_2);
				num = A_2;
			}
			try
			{
				if (this.n == null)
				{
					num = this.o.Send(A_0, A_1, A_2, SocketFlags.None);
				}
			}
			catch (SocketException a_)
			{
				this.g(a_);
			}
			catch (IOException a_2)
			{
				this.g(a_2);
			}
			catch (ObjectDisposedException a_3)
			{
				this.g(a_3);
			}
			if (this.n == null && this.y != null)
			{
				this.y(A_0, A_1, num, this.f.c());
			}
			return num;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000BE0C4 File Offset: 0x000BD0C4
		public override int d3(byte[] A_0, int A_1)
		{
			int num = 0;
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.n == null)
			{
				this.h();
				if (this.h0() != null && !this.h0()())
				{
					throw new MailBeeUserAbortException(5);
				}
			}
			int num2 = A_0.Length - A_1;
			if (num2 > Global.TcpBufSize)
			{
				num2 = Global.TcpBufSize;
			}
			if (this.n != null)
			{
				num = this.n.Read(A_0, A_1, num2);
			}
			try
			{
				if (this.n == null)
				{
					num = this.o.Receive(A_0, A_1, num2, SocketFlags.None);
				}
			}
			catch (SocketException a_)
			{
				this.g(a_);
			}
			catch (IOException a_2)
			{
				this.g(a_2);
			}
			catch (ObjectDisposedException a_3)
			{
				this.g(a_3);
			}
			if (this.n == null && this.x != null)
			{
				this.x(A_0, A_1, num, this.f.c());
			}
			return num;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000BE1C0 File Offset: 0x000BD1C0
		public override bool hm(int A_0)
		{
			this.h();
			if (this.h0() != null && !this.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			try
			{
				return this.o.Poll(A_0, SelectMode.SelectRead);
			}
			catch (SocketException a_)
			{
				this.g(a_);
			}
			return true;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000BE220 File Offset: 0x000BD220
		public override bool hn()
		{
			this.h();
			if (this.h0() != null && !this.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			byte[] buffer = new byte[4];
			try
			{
				return this.o.Receive(buffer, SocketFlags.Peek) == 0;
			}
			catch (SocketException a_)
			{
				this.g(a_);
			}
			return true;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000BE288 File Offset: 0x000BD288
		public override void ho(IPEndPoint A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.o != null)
			{
				throw new InvalidOperationException();
			}
			this.u = false;
			this.v = false;
			this.m.a(A_0);
			this.o = new Socket(A_0.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
			this.g(this.hy());
			if (this.h0() != null && !this.h0()())
			{
				this.o = null;
				throw new MailBeeUserAbortException(5);
			}
			try
			{
				this.o.Connect(A_0);
			}
			catch (SocketException a_)
			{
				this.o = null;
				this.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.o = null;
				this.g(a_2);
			}
			this.q = true;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000BE360 File Offset: 0x000BD360
		public override int hp(byte[] A_0, int A_1, int A_2)
		{
			return this.d4(A_0, A_1, A_2);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000BE36B File Offset: 0x000BD36B
		public override int hq(byte[] A_0, int A_1)
		{
			return this.d3(A_0, A_1);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000BE375 File Offset: 0x000BD375
		public override void hr()
		{
			this.d2();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000BE37D File Offset: 0x000BD37D
		public override ai hs()
		{
			return this.m;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000BE385 File Offset: 0x000BD385
		public override Socket ht()
		{
			return this.o;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000BE38D File Offset: 0x000BD38D
		public override void hu(Socket A_0)
		{
			this.o = A_0;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000BE396 File Offset: 0x000BD396
		private Socket g()
		{
			return this.p;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000BE39E File Offset: 0x000BD39E
		public override Stream d0()
		{
			return this.n;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000BE3A6 File Offset: 0x000BD3A6
		public override void hv(Stream A_0)
		{
			this.n = (ad)A_0;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000BE3B4 File Offset: 0x000BD3B4
		public override EndPoint hw()
		{
			return this.r;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000BE3BC File Offset: 0x000BD3BC
		public override void hx(EndPoint A_0)
		{
			this.r = A_0;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000BE3C5 File Offset: 0x000BD3C5
		public override int hy()
		{
			return this.s;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000BE3CD File Offset: 0x000BD3CD
		public override void hz(int A_0)
		{
			this.s = A_0;
			if (this.o != null && !this.v)
			{
				this.g(this.s);
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000BE3F2 File Offset: 0x000BD3F2
		public override global::a.a h0()
		{
			return this.t;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000BE3FA File Offset: 0x000BD3FA
		public override void h1(global::a.a A_0)
		{
			this.t = A_0;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000BE403 File Offset: 0x000BD403
		public override m h2()
		{
			return this.w;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000BE40B File Offset: 0x000BD40B
		public override void h3(m A_0)
		{
			this.w = A_0;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000BE414 File Offset: 0x000BD414
		public override a1 a8()
		{
			return this.x;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000BE41C File Offset: 0x000BD41C
		public override void a9(a1 A_0)
		{
			this.x = A_0;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000BE425 File Offset: 0x000BD425
		public override bd ba()
		{
			return this.y;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000BE42D File Offset: 0x000BD42D
		public override void bb(bd A_0)
		{
			this.y = A_0;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000BE436 File Offset: 0x000BD436
		public override global::a.e a6()
		{
			return global::a.e.a;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000BE439 File Offset: 0x000BD439
		public override bool a7()
		{
			return true;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000BE43C File Offset: 0x000BD43C
		private void g(Socket A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			try
			{
				LingerOption optionValue = new LingerOption(false, 0);
				A_0.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, optionValue);
			}
			catch (ObjectDisposedException a_)
			{
				this.o = null;
				this.q = false;
				this.g(a_);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000BE498 File Offset: 0x000BD498
		private void g(int A_0)
		{
			if (this.o == null)
			{
				throw new ArgumentNullException();
			}
			try
			{
				if (A_0 == 0)
				{
					A_0 = -1;
				}
				if (this.n != null)
				{
					this.n.ReadTimeout = A_0;
					this.n.WriteTimeout = A_0;
				}
				else
				{
					this.o.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, A_0);
					this.o.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, A_0);
				}
			}
			catch (ObjectDisposedException a_)
			{
				this.o = null;
				this.q = false;
				this.g(a_);
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000BE534 File Offset: 0x000BD534
		private void g(Socket A_0, int A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			try
			{
				A_0.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, A_1);
				A_0.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, A_1);
			}
			catch (ObjectDisposedException a_)
			{
				this.o = null;
				this.q = false;
				this.g(a_);
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000BE598 File Offset: 0x000BD598
		public Socket g(IPEndPoint A_0)
		{
			Socket socket = null;
			if (this.w != null)
			{
				ac ac = new ac();
				this.w(ac, this.f.c());
				socket = ac.a();
			}
			if (socket == null)
			{
				socket = new Socket(A_0.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.g(socket);
			}
			return socket;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000BE5EC File Offset: 0x000BD5EC
		public @as g(IDisposable A_0, TimeSpan A_1)
		{
			ah.f f = new ah.f();
			f.b = this;
			f.a = A_0;
			if (A_1 == TimeSpan.Zero)
			{
				return null;
			}
			ah.e e = new ah.e();
			e.b = new CancellationTokenSource(A_1);
			e.a = e.b.Token.Register(new Action(f.c));
			return new @as(new Action(e.c));
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000BE662 File Offset: 0x000BD662
		public @as g(TimeSpan A_0)
		{
			return this.g(this.ht(), A_0);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000BE671 File Offset: 0x000BD671
		public override ak h4()
		{
			return this.aa;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000BE679 File Offset: 0x000BD679
		public override void h5(ak A_0)
		{
			this.aa = A_0;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000BE682 File Offset: 0x000BD682
		public override bl h6()
		{
			return this.ab;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000BE68A File Offset: 0x000BD68A
		public override void h7(bl A_0)
		{
			this.ab = A_0;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000BE694 File Offset: 0x000BD694
		public override Task<IPHostEntry> h8(string A_0)
		{
			ah.h h;
			h.c = this;
			h.d = A_0;
			h.b = AsyncTaskMethodBuilder<IPHostEntry>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<IPHostEntry> b = h.b;
			b.Start<ah.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000BE6E4 File Offset: 0x000BD6E4
		public override Task d5(IPEndPoint A_0)
		{
			ah.c c;
			c.d = this;
			c.c = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder b = c.b;
			b.Start<ah.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000BE734 File Offset: 0x000BD734
		public override Task<int> d7(byte[] A_0, int A_1, int A_2)
		{
			ah.d d;
			d.c = this;
			d.e = A_0;
			d.f = A_1;
			d.d = A_2;
			d.b = AsyncTaskMethodBuilder<int>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<int> b = d.b;
			b.Start<ah.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000BE794 File Offset: 0x000BD794
		public override Task<int> d6(byte[] A_0, int A_1)
		{
			ah.b b;
			b.d = this;
			b.c = A_0;
			b.e = A_1;
			b.b = AsyncTaskMethodBuilder<int>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<int> b2 = b.b;
			b2.Start<ah.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000BE7EC File Offset: 0x000BD7EC
		public override Task<bool> h9(int A_0, bool A_1)
		{
			ah.g g;
			g.c = this;
			g.f = A_0;
			g.d = A_1;
			g.b = AsyncTaskMethodBuilder<bool>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<bool> b = g.b;
			b.Start<ah.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000BE841 File Offset: 0x000BD841
		public void j()
		{
			this.z = null;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000BE84C File Offset: 0x000BD84C
		public override Task<bool> ia()
		{
			ah.j j;
			j.c = this;
			j.b = AsyncTaskMethodBuilder<bool>.Create();
			j.a = -1;
			AsyncTaskMethodBuilder<bool> b = j.b;
			b.Start<ah.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000BE894 File Offset: 0x000BD894
		public override Task ib(IPEndPoint A_0)
		{
			ah.a a;
			a.d = this;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<ah.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000BE8E1 File Offset: 0x000BD8E1
		public override Task<int> ic(byte[] A_0, int A_1, int A_2)
		{
			return this.d7(A_0, A_1, A_2);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000BE8EC File Offset: 0x000BD8EC
		public override Task<int> id(byte[] A_0, int A_1)
		{
			return this.d6(A_0, A_1);
		}

		// Token: 0x04001BBE RID: 7102
		private new const int e = 10051;

		// Token: 0x04001BBF RID: 7103
		private new const int f = 10053;

		// Token: 0x04001BC0 RID: 7104
		private new const int g = 10054;

		// Token: 0x04001BC1 RID: 7105
		private new const int h = 10060;

		// Token: 0x04001BC2 RID: 7106
		private new const int i = 10061;

		// Token: 0x04001BC3 RID: 7107
		private new const int j = 10064;

		// Token: 0x04001BC4 RID: 7108
		private new const int k = 10065;

		// Token: 0x04001BC5 RID: 7109
		private new const int l = 11001;

		// Token: 0x04001BC6 RID: 7110
		private new ai m;

		// Token: 0x04001BC7 RID: 7111
		private new ad n;

		// Token: 0x04001BC8 RID: 7112
		private Socket o;

		// Token: 0x04001BC9 RID: 7113
		private Socket p;

		// Token: 0x04001BCA RID: 7114
		private bool q;

		// Token: 0x04001BCB RID: 7115
		private EndPoint r;

		// Token: 0x04001BCC RID: 7116
		private int s;

		// Token: 0x04001BCD RID: 7117
		private global::a.a t;

		// Token: 0x04001BCE RID: 7118
		private bool u;

		// Token: 0x04001BCF RID: 7119
		private bool v;

		// Token: 0x04001BD0 RID: 7120
		private m w;

		// Token: 0x04001BD1 RID: 7121
		private a1 x;

		// Token: 0x04001BD2 RID: 7122
		private bd y;

		// Token: 0x04001BD3 RID: 7123
		private Task<int> z;

		// Token: 0x04001BD4 RID: 7124
		private ak aa;

		// Token: 0x04001BD5 RID: 7125
		private bl ab;

		// Token: 0x020004AB RID: 1195
		[CompilerGenerated]
		private new sealed class f
		{
			// Token: 0x0600293F RID: 10559 RVA: 0x000BEA99 File Offset: 0x000BDA99
			internal void c()
			{
				this.b.v = true;
				this.a.Dispose();
			}

			// Token: 0x04001BD8 RID: 7128
			public IDisposable a;

			// Token: 0x04001BD9 RID: 7129
			public ah b;
		}

		// Token: 0x020004AC RID: 1196
		[CompilerGenerated]
		private new sealed class e
		{
			// Token: 0x06002941 RID: 10561 RVA: 0x000BEABA File Offset: 0x000BDABA
			internal void c()
			{
				this.a.Dispose();
				this.b.Dispose();
			}

			// Token: 0x04001BDA RID: 7130
			public CancellationTokenRegistration a;

			// Token: 0x04001BDB RID: 7131
			public CancellationTokenSource b;
		}

		// Token: 0x020004B1 RID: 1201
		[CompilerGenerated]
		private new sealed class i
		{
			// Token: 0x0600294B RID: 10571 RVA: 0x000BF816 File Offset: 0x000BE816
			internal void c()
			{
				this.a.Dispose();
				this.b.Dispose();
			}

			// Token: 0x04001BFF RID: 7167
			public IDisposable a;

			// Token: 0x04001C00 RID: 7168
			public CancellationTokenSource b;
		}
	}
}
