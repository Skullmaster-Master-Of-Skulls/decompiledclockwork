using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002E RID: 46
	public class OAuthGrantCustomExtensionContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00007461 File Offset: 0x00005661
		public OAuthGrantCustomExtensionContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId, string grantType, IReadableStringCollection parameters) : base(context, options, null)
		{
			this.ClientId = clientId;
			this.GrantType = grantType;
			this.Parameters = parameters;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007483 File Offset: 0x00005683
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000748B File Offset: 0x0000568B
		public string ClientId { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007494 File Offset: 0x00005694
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000749C File Offset: 0x0000569C
		public string GrantType { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000074A5 File Offset: 0x000056A5
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000074AD File Offset: 0x000056AD
		public IReadableStringCollection Parameters { get; private set; }
	}
}
