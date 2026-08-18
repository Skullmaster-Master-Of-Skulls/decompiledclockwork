using System;
using System.Net;
using System.Threading;
using MailBee;
using MailBee.Proxy;
using MailBee.Security;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x0200047C RID: 1148
	internal class d : al
	{
		// Token: 0x06002799 RID: 10137 RVA: 0x000B7FA0 File Offset: 0x000B6FA0
		public override int d9()
		{
			return 25;
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000B7FA4 File Offset: 0x000B6FA4
		public d() : this(Global.DefaultServerName)
		{
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000B7FB1 File Offset: 0x000B6FB1
		public d(string A_0) : this(A_0, 25, 0)
		{
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000B7FBD File Offset: 0x000B6FBD
		public d(string A_0, string A_1, string A_2) : this(A_0, A_1, A_2, AuthenticationMethods.Auto)
		{
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000B7FD0 File Offset: 0x000B6FD0
		public d(string A_0, string A_1, string A_2, AuthenticationMethods A_3) : this(A_0, 25, 0, Global.DefaultTimeout, Global.Pipelining, A_3, A_1, A_2, true, null, global::a.d.b.a())
		{
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000B7FFC File Offset: 0x000B6FFC
		public d(string A_0, int A_1, int A_2) : this(A_0, A_1, A_2, Global.DefaultTimeout, Global.Pipelining, AuthenticationMethods.None, string.Empty, string.Empty, true, null, global::a.d.b.a())
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000B8030 File Offset: 0x000B7030
		public d(string A_0, int A_1, int A_2, int A_3, bool A_4, AuthenticationMethods A_5, string A_6, string A_7, bool A_8, string A_9, ExtendedSmtpOptions A_10) : this(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, -1, -1, 0, null)
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000B805C File Offset: 0x000B705C
		public d(string A_0, int A_1, int A_2, int A_3, bool A_4, AuthenticationMethods A_5, string A_6, string A_7, bool A_8, string A_9, ExtendedSmtpOptions A_10, int A_11, int A_12, int A_13, EndPoint A_14)
		{
			base.e(A_0);
			base.g(A_1);
			base.f(A_3);
			base.e(A_3);
			this.d = A_4;
			this.e = A_5;
			if (Global.SafeMode)
			{
				this.f = AuthenticationOptions.PreferSimpleMethods;
			}
			else
			{
				this.f = AuthenticationOptions.None;
			}
			this.g = null;
			this.h = A_6;
			this.i = null;
			this.j = A_7;
			this.l = SslStartupMode.Manual;
			this.m = SecurityProtocol.Auto;
			this.n = new ClientServerCertificates();
			this.o = new ProxyServer();
			this.d = A_2;
			this.e = A_9;
			this.h = A_10;
			this.f = A_8;
			this.g = false;
			this.i = false;
			this.j = A_11;
			this.k = 0;
			this.l = A_12;
			this.m = A_13;
			this.n = DateTime.MinValue;
			this.p = A_14;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000B8155 File Offset: 0x000B7155
		public new int j()
		{
			return this.d;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000B815D File Offset: 0x000B715D
		public new void d(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000B8166 File Offset: 0x000B7166
		public new bool d()
		{
			return this.f;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000B816E File Offset: 0x000B716E
		public void c(bool A_0)
		{
			this.f = A_0;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000B8177 File Offset: 0x000B7177
		public new string h()
		{
			return this.e;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000B817F File Offset: 0x000B717F
		public void a(string A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000B8188 File Offset: 0x000B7188
		public new bool l()
		{
			return this.g;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000B8190 File Offset: 0x000B7190
		public void a(bool A_0)
		{
			this.g = A_0;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000B8199 File Offset: 0x000B7199
		public ExtendedSmtpOptions b()
		{
			return this.h;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000B81A1 File Offset: 0x000B71A1
		public void a(ExtendedSmtpOptions A_0)
		{
			this.h = A_0;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000B81AA File Offset: 0x000B71AA
		public bool c()
		{
			return this.i;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000B81B2 File Offset: 0x000B71B2
		public void b(bool A_0)
		{
			this.i = A_0;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000B81BB File Offset: 0x000B71BB
		public new int f()
		{
			return this.j;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B81C3 File Offset: 0x000B71C3
		public void b(int A_0)
		{
			if (A_0 == 0)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			this.j = A_0;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000B81D8 File Offset: 0x000B71D8
		public new bool e()
		{
			return (this.m > 0 && this.n.AddMilliseconds((double)this.m) > DateTime.Now) || (this.j >= 0 && this.k >= this.j);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000B8228 File Offset: 0x000B7228
		public bool a()
		{
			if (this.m > 0 && this.n.AddMilliseconds((double)this.m) > DateTime.Now)
			{
				return false;
			}
			if (this.j < 0)
			{
				return true;
			}
			for (;;)
			{
				int value = this.k + 1;
				int num = this.k;
				if (num >= this.j)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.k, value, num) == num)
				{
					return true;
				}
				if (this.k >= this.j)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000B82A5 File Offset: 0x000B72A5
		public new void g()
		{
			if (this.j > 0)
			{
				Interlocked.Decrement(ref this.k);
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000B82BC File Offset: 0x000B72BC
		public new void m()
		{
			if (this.m > 0)
			{
				this.n = DateTime.Now;
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000B82D2 File Offset: 0x000B72D2
		public new int i()
		{
			return this.l;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000B82DA File Offset: 0x000B72DA
		public void a(int A_0)
		{
			if (A_0 == 0)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			this.l = A_0;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000B82EE File Offset: 0x000B72EE
		public new int k()
		{
			return this.m;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000B82F6 File Offset: 0x000B72F6
		public void c(int A_0)
		{
			if (A_0 < 0)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			this.m = A_0;
		}

		// Token: 0x04001B1E RID: 6942
		public const int a = 25;

		// Token: 0x04001B1F RID: 6943
		public const int b = 465;

		// Token: 0x04001B20 RID: 6944
		public const int c = 587;

		// Token: 0x04001B21 RID: 6945
		private new int d;

		// Token: 0x04001B22 RID: 6946
		private new string e;

		// Token: 0x04001B23 RID: 6947
		private new bool f;

		// Token: 0x04001B24 RID: 6948
		private new bool g;

		// Token: 0x04001B25 RID: 6949
		private new ExtendedSmtpOptions h;

		// Token: 0x04001B26 RID: 6950
		private new bool i;

		// Token: 0x04001B27 RID: 6951
		private new int j;

		// Token: 0x04001B28 RID: 6952
		private new int k;

		// Token: 0x04001B29 RID: 6953
		private new int l;

		// Token: 0x04001B2A RID: 6954
		private new int m;

		// Token: 0x04001B2B RID: 6955
		private new DateTime n;
	}
}
