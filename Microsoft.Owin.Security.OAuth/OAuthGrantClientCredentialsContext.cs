using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002D RID: 45
	public class OAuthGrantClientCredentialsContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00007425 File Offset: 0x00005625
		public OAuthGrantClientCredentialsContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId, IList<string> scope) : base(context, options, null)
		{
			this.ClientId = clientId;
			this.Scope = scope;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000743F File Offset: 0x0000563F
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007447 File Offset: 0x00005647
		public string ClientId { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007450 File Offset: 0x00005650
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007458 File Offset: 0x00005658
		public IList<string> Scope { get; private set; }
	}
}
