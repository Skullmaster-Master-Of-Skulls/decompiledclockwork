using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200001D RID: 29
	public abstract class BaseValidatingClientContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000069F3 File Offset: 0x00004BF3
		protected BaseValidatingClientContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId) : base(context, options)
		{
			this.ClientId = clientId;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006A04 File Offset: 0x00004C04
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00006A0C File Offset: 0x00004C0C
		public string ClientId { get; protected set; }
	}
}
