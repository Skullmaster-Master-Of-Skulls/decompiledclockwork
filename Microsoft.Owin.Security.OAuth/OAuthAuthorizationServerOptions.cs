using System;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200000C RID: 12
	public class OAuthAuthorizationServerOptions : AuthenticationOptions
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00005E35 File Offset: 0x00004035
		public OAuthAuthorizationServerOptions() : base("Bearer")
		{
			this.AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(5.0);
			this.AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20.0);
			this.SystemClock = new SystemClock();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00005E75 File Offset: 0x00004075
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00005E7D File Offset: 0x0000407D
		public PathString AuthorizeEndpointPath { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00005E86 File Offset: 0x00004086
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00005E8E File Offset: 0x0000408E
		public PathString TokenEndpointPath { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00005E97 File Offset: 0x00004097
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00005E9F File Offset: 0x0000409F
		public IOAuthAuthorizationServerProvider Provider { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00005EA8 File Offset: 0x000040A8
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00005EB0 File Offset: 0x000040B0
		public ISecureDataFormat<AuthenticationTicket> AuthorizationCodeFormat { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00005EB9 File Offset: 0x000040B9
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00005EC1 File Offset: 0x000040C1
		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00005ECA File Offset: 0x000040CA
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00005ED2 File Offset: 0x000040D2
		public ISecureDataFormat<AuthenticationTicket> RefreshTokenFormat { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005EDB File Offset: 0x000040DB
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00005EE3 File Offset: 0x000040E3
		public TimeSpan AuthorizationCodeExpireTimeSpan { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005EEC File Offset: 0x000040EC
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00005EF4 File Offset: 0x000040F4
		public TimeSpan AccessTokenExpireTimeSpan { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00005EFD File Offset: 0x000040FD
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00005F05 File Offset: 0x00004105
		public IAuthenticationTokenProvider AuthorizationCodeProvider { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00005F0E File Offset: 0x0000410E
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00005F16 File Offset: 0x00004116
		public IAuthenticationTokenProvider AccessTokenProvider { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00005F1F File Offset: 0x0000411F
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00005F27 File Offset: 0x00004127
		public IAuthenticationTokenProvider RefreshTokenProvider { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00005F30 File Offset: 0x00004130
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00005F38 File Offset: 0x00004138
		public bool ApplicationCanDisplayErrors { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00005F41 File Offset: 0x00004141
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00005F49 File Offset: 0x00004149
		public ISystemClock SystemClock { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00005F52 File Offset: 0x00004152
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00005F5A File Offset: 0x0000415A
		public bool AllowInsecureHttp { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00005F63 File Offset: 0x00004163
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00005F6B File Offset: 0x0000416B
		public PathString FormPostEndpoint { get; set; }
	}
}
