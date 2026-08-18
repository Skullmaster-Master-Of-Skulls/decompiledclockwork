using System;
using System.Net;
using System.Net.Sockets;
using MailBee;
using MailBee.Proxy;
using MailBee.Security;

namespace a
{
	// Token: 0x020000F1 RID: 241
	internal abstract class al
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000253F0 File Offset: 0x000243F0
		public al()
		{
			this.b = this.d9();
			this.d = Global.Pipelining;
			this.e = AuthenticationMethods.Auto;
			this.f = (Global.SafeMode ? AuthenticationOptions.PreferSimpleMethods : AuthenticationOptions.None);
			this.i = string.Empty;
			this.k = null;
			this.c = Global.DefaultTimeout;
			this.l = SslStartupMode.Manual;
			this.m = SecurityProtocol.Auto;
			this.n = new ClientServerCertificates();
			this.o = new ProxyServer();
			this.p = null;
			this.q = null;
			this.r = 0;
		}

		// Token: 0x060007F4 RID: 2036
		public abstract int d9();

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002548C File Offset: 0x0002448C
		public AuthenticationOptions t()
		{
			if (!Global.SafeMode)
			{
				return AuthenticationOptions.None;
			}
			return AuthenticationOptions.PreferSimpleMethods;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00025498 File Offset: 0x00024498
		public string v()
		{
			return this.a;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000254A0 File Offset: 0x000244A0
		public void e(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a = A_0;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000254B4 File Offset: 0x000244B4
		public int w()
		{
			return this.b;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000254BC File Offset: 0x000244BC
		public void g(int A_0)
		{
			if (A_0 > 65535 || A_0 < 0)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			this.b = A_0;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000254D9 File Offset: 0x000244D9
		public int ab()
		{
			return this.c;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000254E1 File Offset: 0x000244E1
		public void f(int A_0)
		{
			if (A_0 < 0)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			this.c = A_0;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000254F6 File Offset: 0x000244F6
		public bool u()
		{
			return this.d;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000254FE File Offset: 0x000244FE
		public void d(bool A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00025507 File Offset: 0x00024507
		public AuthenticationMethods x()
		{
			return this.e;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002550F File Offset: 0x0002450F
		public void a(AuthenticationMethods A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00025518 File Offset: 0x00024518
		public AuthenticationOptions ae()
		{
			return this.f;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00025520 File Offset: 0x00024520
		public void a(AuthenticationOptions A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00025529 File Offset: 0x00024529
		public SaslMethod r()
		{
			return this.g;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00025531 File Offset: 0x00024531
		public void a(SaslMethod A_0)
		{
			this.g = A_0;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002553A File Offset: 0x0002453A
		public string ad()
		{
			return this.k;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00025542 File Offset: 0x00024542
		public void b(string A_0)
		{
			this.k = A_0;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002554B File Offset: 0x0002454B
		internal string z()
		{
			return this.i;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00025553 File Offset: 0x00024553
		internal void f(string A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002555C File Offset: 0x0002455C
		public string q()
		{
			return this.h;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00025564 File Offset: 0x00024564
		public void c(string A_0)
		{
			this.h = A_0;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002556D File Offset: 0x0002456D
		public string aa()
		{
			return this.j;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00025575 File Offset: 0x00024575
		public void d(string A_0)
		{
			this.j = A_0;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002557E File Offset: 0x0002457E
		public SslStartupMode ac()
		{
			return this.l;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00025586 File Offset: 0x00024586
		public void a(SslStartupMode A_0)
		{
			this.l = A_0;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002558F File Offset: 0x0002458F
		public SecurityProtocol af()
		{
			return this.m;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00025597 File Offset: 0x00024597
		public void a(SecurityProtocol A_0)
		{
			this.m = A_0;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000255A0 File Offset: 0x000245A0
		public ClientServerCertificates p()
		{
			return this.n;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000255A8 File Offset: 0x000245A8
		public ProxyServer y()
		{
			return this.o;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000255B0 File Offset: 0x000245B0
		public EndPoint s()
		{
			return this.p;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000255B8 File Offset: 0x000245B8
		public void a(EndPoint A_0)
		{
			this.p = A_0;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000255C1 File Offset: 0x000245C1
		public Socket n()
		{
			return this.q;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000255C9 File Offset: 0x000245C9
		public void a(Socket A_0)
		{
			this.q = A_0;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000255D2 File Offset: 0x000245D2
		public int o()
		{
			return this.r;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000255DA File Offset: 0x000245DA
		public void e(int A_0)
		{
			this.r = A_0;
		}

		// Token: 0x04000550 RID: 1360
		private string a;

		// Token: 0x04000551 RID: 1361
		private int b;

		// Token: 0x04000552 RID: 1362
		private int c;

		// Token: 0x04000553 RID: 1363
		protected bool d;

		// Token: 0x04000554 RID: 1364
		protected AuthenticationMethods e;

		// Token: 0x04000555 RID: 1365
		protected AuthenticationOptions f;

		// Token: 0x04000556 RID: 1366
		protected SaslMethod g;

		// Token: 0x04000557 RID: 1367
		protected string h;

		// Token: 0x04000558 RID: 1368
		protected string i;

		// Token: 0x04000559 RID: 1369
		protected string j;

		// Token: 0x0400055A RID: 1370
		protected string k;

		// Token: 0x0400055B RID: 1371
		protected SslStartupMode l;

		// Token: 0x0400055C RID: 1372
		protected SecurityProtocol m;

		// Token: 0x0400055D RID: 1373
		protected ClientServerCertificates n;

		// Token: 0x0400055E RID: 1374
		protected ProxyServer o;

		// Token: 0x0400055F RID: 1375
		protected EndPoint p;

		// Token: 0x04000560 RID: 1376
		protected Socket q;

		// Token: 0x04000561 RID: 1377
		protected int r;
	}
}
