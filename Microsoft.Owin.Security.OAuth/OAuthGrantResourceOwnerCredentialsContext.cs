using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000032 RID: 50
	public class OAuthGrantResourceOwnerCredentialsContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00007572 File Offset: 0x00005772
		public OAuthGrantResourceOwnerCredentialsContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId, string userName, string password, IList<string> scope) : base(context, options, null)
		{
			this.ClientId = clientId;
			this.UserName = userName;
			this.Password = password;
			this.Scope = scope;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000759C File Offset: 0x0000579C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000075A4 File Offset: 0x000057A4
		public string ClientId { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000075AD File Offset: 0x000057AD
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000075B5 File Offset: 0x000057B5
		public string UserName { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000075BE File Offset: 0x000057BE
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000075C6 File Offset: 0x000057C6
		public string Password { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000075CF File Offset: 0x000057CF
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000075D7 File Offset: 0x000057D7
		public IList<string> Scope { get; private set; }
	}
}
