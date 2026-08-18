using System;
using System.Net;
using a;
using a.d;
using MailBee.Proxy;
using MailBee.Security;

namespace MailBee.SmtpMail
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public class SmtpServer : ax
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x00031B8C File Offset: 0x00030B8C
		public SmtpServer()
		{
			this.m_server = new global::a.d.d();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00031B9F File Offset: 0x00030B9F
		public SmtpServer(string name)
		{
			this.m_server = new global::a.d.d(name);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00031BB3 File Offset: 0x00030BB3
		public SmtpServer(string name, string accountName, string password)
		{
			this.m_server = new global::a.d.d(name, accountName, password, AuthenticationMethods.Auto);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00031BCE File Offset: 0x00030BCE
		public SmtpServer(string name, string accountName, string password, AuthenticationMethods authMethods)
		{
			this.m_server = new global::a.d.d(name, accountName, password, authMethods);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00031BE6 File Offset: 0x00030BE6
		public SmtpServer(string name, int port, int priority)
		{
			this.m_server = new global::a.d.d(name, port, priority);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00031BFC File Offset: 0x00030BFC
		public SmtpServer(string name, int port, int priority, int timeout, bool pipelining, AuthenticationMethods authMethods, string accountName, string password, bool allowRefusedRecipients, string helloDomain, ExtendedSmtpOptions smtpOptions)
		{
			this.m_server = new global::a.d.d(name, port, priority, timeout, pipelining, authMethods, accountName, password, allowRefusedRecipients, helloDomain, smtpOptions);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00031C30 File Offset: 0x00030C30
		public SmtpServer(string name, int port, int priority, int timeout, bool pipelining, AuthenticationMethods authMethods, string accountName, string password, bool allowRefusedRecipients, string helloDomain, ExtendedSmtpOptions smtpOptions, int maxConnectionCount, int maxSendPerSessionCount, int pauseInterval)
		{
			this.m_server = new global::a.d.d(name, port, priority, timeout, pipelining, authMethods, accountName, password, allowRefusedRecipients, helloDomain, smtpOptions, maxConnectionCount, maxSendPerSessionCount, pauseInterval, null);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00031C68 File Offset: 0x00030C68
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00031C75 File Offset: 0x00030C75
		public string Name
		{
			get
			{
				return this.m_server.v();
			}
			set
			{
				this.m_server.e(value);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00031C83 File Offset: 0x00030C83
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00031C90 File Offset: 0x00030C90
		public int Port
		{
			get
			{
				return this.m_server.w();
			}
			set
			{
				this.m_server.g(value);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00031C9E File Offset: 0x00030C9E
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00031CAB File Offset: 0x00030CAB
		public int Timeout
		{
			get
			{
				return this.m_server.ab();
			}
			set
			{
				this.m_server.f(value);
				this.m_server.e(value);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00031CC5 File Offset: 0x00030CC5
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00031CD2 File Offset: 0x00030CD2
		public bool Pipelining
		{
			get
			{
				return this.m_server.u();
			}
			set
			{
				this.m_server.d(value);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00031CE0 File Offset: 0x00030CE0
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x00031CED File Offset: 0x00030CED
		public AuthenticationMethods AuthMethods
		{
			get
			{
				return this.m_server.x();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00031CFB File Offset: 0x00030CFB
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00031D08 File Offset: 0x00030D08
		public AuthenticationOptions AuthOptions
		{
			get
			{
				return this.m_server.ae();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00031D16 File Offset: 0x00030D16
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00031D23 File Offset: 0x00030D23
		public SaslMethod AuthUserDefined
		{
			get
			{
				return this.m_server.r();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00031D31 File Offset: 0x00030D31
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x00031D3E File Offset: 0x00030D3E
		public string TargetName
		{
			get
			{
				return this.m_server.ad();
			}
			set
			{
				this.m_server.b(value);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00031D4C File Offset: 0x00030D4C
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x00031D59 File Offset: 0x00030D59
		public string AccountDomain
		{
			get
			{
				return this.m_server.z();
			}
			set
			{
				this.m_server.f(value);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00031D67 File Offset: 0x00030D67
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00031D74 File Offset: 0x00030D74
		public string AccountName
		{
			get
			{
				return this.m_server.q();
			}
			set
			{
				this.m_server.c(value);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00031D82 File Offset: 0x00030D82
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00031D8F File Offset: 0x00030D8F
		public string Password
		{
			get
			{
				return this.m_server.aa();
			}
			set
			{
				this.m_server.d(value);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00031D9D File Offset: 0x00030D9D
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x00031DAA File Offset: 0x00030DAA
		public SslStartupMode SslMode
		{
			get
			{
				return this.m_server.ac();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00031DB8 File Offset: 0x00030DB8
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00031DC5 File Offset: 0x00030DC5
		public SecurityProtocol SslProtocol
		{
			get
			{
				return this.m_server.af();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00031DD3 File Offset: 0x00030DD3
		public ClientServerCertificates SslCertificates
		{
			get
			{
				return this.m_server.p();
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00031DE0 File Offset: 0x00030DE0
		public ProxyServer Proxy
		{
			get
			{
				return this.m_server.y();
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00031DED File Offset: 0x00030DED
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00031DFA File Offset: 0x00030DFA
		public int Priority
		{
			get
			{
				return this.m_server.j();
			}
			set
			{
				this.m_server.d(value);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00031E08 File Offset: 0x00030E08
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00031E15 File Offset: 0x00030E15
		public bool AllowRefusedRecipients
		{
			get
			{
				return this.m_server.d();
			}
			set
			{
				this.m_server.c(value);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00031E23 File Offset: 0x00030E23
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00031E30 File Offset: 0x00030E30
		public string HelloDomain
		{
			get
			{
				return this.m_server.h();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00031E3E File Offset: 0x00030E3E
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00031E4B File Offset: 0x00030E4B
		public bool IgnoreLoginFailure
		{
			get
			{
				return this.m_server.l();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00031E59 File Offset: 0x00030E59
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00031E66 File Offset: 0x00030E66
		public ExtendedSmtpOptions SmtpOptions
		{
			get
			{
				return this.m_server.b();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00031E74 File Offset: 0x00030E74
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x00031E81 File Offset: 0x00030E81
		public bool AuthPopBeforeSmtp
		{
			get
			{
				return this.m_server.c();
			}
			set
			{
				this.m_server.b(value);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00031E8F File Offset: 0x00030E8F
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x00031E9C File Offset: 0x00030E9C
		public int MaxConnectionCount
		{
			get
			{
				return this.m_server.f();
			}
			set
			{
				this.m_server.b(value);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00031EAA File Offset: 0x00030EAA
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x00031EB7 File Offset: 0x00030EB7
		public int MaxSendPerSessionCount
		{
			get
			{
				return this.m_server.i();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00031EC5 File Offset: 0x00030EC5
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00031ED2 File Offset: 0x00030ED2
		public int PauseInterval
		{
			get
			{
				return this.m_server.k();
			}
			set
			{
				this.m_server.c(value);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00031EE0 File Offset: 0x00030EE0
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x00031EED File Offset: 0x00030EED
		public EndPoint LocalEndPoint
		{
			get
			{
				return this.m_server.s();
			}
			set
			{
				this.m_server.a(value);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00031EFB File Offset: 0x00030EFB
		internal global::a.d.d Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x040008A8 RID: 2216
		private global::a.d.d m_server;
	}
}
