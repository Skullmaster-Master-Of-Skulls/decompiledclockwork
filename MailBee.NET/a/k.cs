using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;
using MailBee.Proxy;
using MailBee.Security;

namespace a
{
	// Token: 0x020000B8 RID: 184
	internal abstract class k : h
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0001C794 File Offset: 0x0001B794
		public k()
		{
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001C79C File Offset: 0x0001B79C
		public new bool a(bool A_0, string A_1, int A_2, bool A_3, Socket A_4, EndPoint A_5)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				al al = ((ab)this.p).av();
				al.e(A_1);
				al.g(A_2);
				al.d(A_3);
				al.a(A_5);
				al.a(A_4);
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

		// Token: 0x060006BF RID: 1727 RVA: 0x0001C854 File Offset: 0x0001B854
		public new bool a(bool A_0, string A_1, int A_2, bool A_3)
		{
			return this.a(A_0, A_1, A_2, A_3, null, null);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001C863 File Offset: 0x0001B863
		public new bool a(bool A_0, string A_1, int A_2)
		{
			return this.a(A_0, A_1, A_2, Global.Pipelining);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001C873 File Offset: 0x0001B873
		public bool g(bool A_0, string A_1)
		{
			return this.a(A_0, A_1, ((ab)this.p).av().d9());
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001C894 File Offset: 0x0001B894
		public new IAsyncResult a(string A_0, int A_1, bool A_2, Socket A_3, EndPoint A_4, AsyncCallback A_5, object A_6)
		{
			this.p.k(true);
			base.bl();
			k.b b = new k.b(this.a);
			this.q = new o(b, null);
			this.q.a(b.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6));
			return this.q;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001C8F0 File Offset: 0x0001B8F0
		public bool ar()
		{
			if (this.q == null || !(this.q.c() is k.b))
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
				result = ((k.b)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001C988 File Offset: 0x0001B988
		public new bool a(bool A_0, string A_1, string A_2, string A_3, string A_4, AuthenticationMethods A_5, AuthenticationOptions A_6, SaslMethod A_7)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				al al = ((ab)this.p).av();
				al.b(A_1);
				al.f(A_2);
				al.c(A_3);
				al.d(A_4);
				al.a(A_5);
				al.a(A_6);
				al.a(A_7);
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001CA50 File Offset: 0x0001BA50
		public new bool a(bool A_0, string A_1, string A_2, AuthenticationMethods A_3, AuthenticationOptions A_4, SaslMethod A_5)
		{
			return this.a(A_0, null, null, A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001CA6E File Offset: 0x0001BA6E
		public new bool a(bool A_0, string A_1, string A_2, AuthenticationMethods A_3)
		{
			return this.a(A_0, A_1, A_2, A_3, ((ab)this.p).av().t(), null);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001CA91 File Offset: 0x0001BA91
		public bool f(bool A_0, string A_1, string A_2)
		{
			return this.a(A_0, A_1, A_2, AuthenticationMethods.Auto, ((ab)this.p).av().t(), null);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001CAB8 File Offset: 0x0001BAB8
		public new IAsyncResult a(string A_0, string A_1, string A_2, string A_3, AuthenticationMethods A_4, AuthenticationOptions A_5, SaslMethod A_6, AsyncCallback A_7, object A_8)
		{
			this.p.k(true);
			base.bl();
			k.c c = new k.c(this.a);
			this.q = new o(c, null);
			this.q.a(c.BeginInvoke(false, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8));
			return this.q;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001CB18 File Offset: 0x0001BB18
		public bool au()
		{
			if (this.q == null || !(this.q.c() is k.c))
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
				result = ((k.c)this.q.c()).EndInvoke(this.q.d());
			}
			finally
			{
				this.q = null;
				base.bj();
				this.p.k(false);
			}
			return result;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001CBB0 File Offset: 0x0001BBB0
		public SslStartupMode aq()
		{
			return ((ab)this.p).av().ac();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001CBC7 File Offset: 0x0001BBC7
		public new void a(SslStartupMode A_0)
		{
			((ab)this.p).av().a(A_0);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001CBDF File Offset: 0x0001BBDF
		public SecurityProtocol @as()
		{
			return ((ab)this.p).av().af();
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001CBF6 File Offset: 0x0001BBF6
		public new void a(SecurityProtocol A_0)
		{
			((ab)this.p).av().a(A_0);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001CC0E File Offset: 0x0001BC0E
		public ClientServerCertificates at()
		{
			return ((ab)this.p).av().p();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001CC25 File Offset: 0x0001BC25
		public ProxyServer ap()
		{
			return ((ab)this.p).av().y();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001CC3C File Offset: 0x0001BC3C
		public virtual int ao()
		{
			return ((ab)this.p).av().ab();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001CC53 File Offset: 0x0001BC53
		public virtual void f(int A_0)
		{
			((ab)this.p).av().f(A_0);
			this.p.pd(A_0);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001CC78 File Offset: 0x0001BC78
		public new Task<bool> a(string A_0, int A_1, bool A_2, Socket A_3, EndPoint A_4)
		{
			k.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.h = A_3;
			a.g = A_4;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<k.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CCE7 File Offset: 0x0001BCE7
		public new Task<bool> a(string A_0, int A_1, bool A_2)
		{
			return this.a(A_0, A_1, A_2, null, null);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001CCF4 File Offset: 0x0001BCF4
		public new Task<bool> a(string A_0, int A_1)
		{
			return this.a(A_0, A_1, Global.Pipelining);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CD03 File Offset: 0x0001BD03
		public Task<bool> t(string A_0)
		{
			return this.a(A_0, ((ab)this.p).av().d9());
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001CD24 File Offset: 0x0001BD24
		public new Task<bool> a(string A_0, string A_1, string A_2, string A_3, AuthenticationMethods A_4, AuthenticationOptions A_5, SaslMethod A_6)
		{
			k.d d;
			d.c = this;
			d.d = A_0;
			d.e = A_1;
			d.f = A_2;
			d.g = A_3;
			d.h = A_4;
			d.i = A_5;
			d.j = A_6;
			d.b = AsyncTaskMethodBuilder<bool>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<bool> b = d.b;
			b.Start<k.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001CDA5 File Offset: 0x0001BDA5
		public new Task<bool> a(string A_0, string A_1, AuthenticationMethods A_2, AuthenticationOptions A_3, SaslMethod A_4)
		{
			return this.a(null, null, A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001CDB6 File Offset: 0x0001BDB6
		public new Task<bool> a(string A_0, string A_1, AuthenticationMethods A_2)
		{
			return this.a(A_0, A_1, A_2, ((ab)this.p).av().t(), null);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001CDD7 File Offset: 0x0001BDD7
		public Task<bool> g(string A_0, string A_1)
		{
			return this.a(A_0, A_1, AuthenticationMethods.Auto, ((ab)this.p).av().t(), null);
		}

		// Token: 0x020004F0 RID: 1264
		// (Invoke) Token: 0x06002A27 RID: 10791
		protected new delegate bool b(bool A_0, string A_1, int A_2, bool A_3, Socket A_4, EndPoint A_5);

		// Token: 0x020004F1 RID: 1265
		// (Invoke) Token: 0x06002A2B RID: 10795
		protected new delegate bool c(bool A_0, string A_1, string A_2, string A_3, string A_4, AuthenticationMethods A_5, AuthenticationOptions A_6, SaslMethod A_7);
	}
}
