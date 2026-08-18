using System;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000011 RID: 17
	public class OAuthBearerAuthenticationOptions : AuthenticationOptions
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000067C9 File Offset: 0x000049C9
		public OAuthBearerAuthenticationOptions() : base("Bearer")
		{
			this.SystemClock = new SystemClock();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000067E1 File Offset: 0x000049E1
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000067E9 File Offset: 0x000049E9
		public string Realm { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000067F2 File Offset: 0x000049F2
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000067FA File Offset: 0x000049FA
		public string Challenge { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00006803 File Offset: 0x00004A03
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000680B File Offset: 0x00004A0B
		public IOAuthBearerAuthenticationProvider Provider { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00006814 File Offset: 0x00004A14
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000681C File Offset: 0x00004A1C
		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006825 File Offset: 0x00004A25
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000682D File Offset: 0x00004A2D
		public IAuthenticationTokenProvider AccessTokenProvider { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00006836 File Offset: 0x00004A36
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000683E File Offset: 0x00004A3E
		public ISystemClock SystemClock { get; set; }
	}
}
