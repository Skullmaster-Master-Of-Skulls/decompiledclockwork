using System;
using System.Net;
using a.d;

namespace MailBee.SmtpMail
{
	// Token: 0x0200013D RID: 317
	public class DirectSendServerConfig
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0002E360 File Offset: 0x0002D360
		internal DirectSendServerConfig()
		{
			this.a = Global.DefaultTimeout;
			this.b = Global.Pipelining;
			this.c = string.Empty;
			this.d = global::a.d.b.a();
			DirectSendServerConfig.e = false;
			this.f = null;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002E3AC File Offset: 0x0002D3AC
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0002E3B4 File Offset: 0x0002D3B4
		public int Timeout
		{
			get
			{
				return this.a;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.a = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002E3C9 File Offset: 0x0002D3C9
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0002E3D1 File Offset: 0x0002D3D1
		public bool Pipelining
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002E3DA File Offset: 0x0002D3DA
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0002E3E2 File Offset: 0x0002D3E2
		public string HelloDomain
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002E3EB File Offset: 0x0002D3EB
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0002E3F3 File Offset: 0x0002D3F3
		public ExtendedSmtpOptions SmtpOptions
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002E3FC File Offset: 0x0002D3FC
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0002E403 File Offset: 0x0002D403
		public bool EnableStartTls
		{
			get
			{
				return DirectSendServerConfig.e;
			}
			set
			{
				DirectSendServerConfig.e = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002E40B File Offset: 0x0002D40B
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0002E413 File Offset: 0x0002D413
		public EndPoint LocalEndPoint
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x040007DB RID: 2011
		private int a;

		// Token: 0x040007DC RID: 2012
		private bool b;

		// Token: 0x040007DD RID: 2013
		private string c;

		// Token: 0x040007DE RID: 2014
		private ExtendedSmtpOptions d;

		// Token: 0x040007DF RID: 2015
		private static bool e;

		// Token: 0x040007E0 RID: 2016
		private EndPoint f;
	}
}
