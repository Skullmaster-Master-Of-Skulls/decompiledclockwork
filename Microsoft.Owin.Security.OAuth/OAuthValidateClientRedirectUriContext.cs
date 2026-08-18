using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000030 RID: 48
	public class OAuthValidateClientRedirectUriContext : BaseValidatingClientContext
	{
		// Token: 0x0600014A RID: 330 RVA: 0x000074F1 File Offset: 0x000056F1
		public OAuthValidateClientRedirectUriContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId, string redirectUri) : base(context, options, clientId)
		{
			this.RedirectUri = redirectUri;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007504 File Offset: 0x00005704
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000750C File Offset: 0x0000570C
		public string RedirectUri { get; private set; }

		// Token: 0x0600014D RID: 333 RVA: 0x00007515 File Offset: 0x00005715
		public override bool Validated()
		{
			return !string.IsNullOrEmpty(this.RedirectUri) && base.Validated();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000752C File Offset: 0x0000572C
		public bool Validated(string redirectUri)
		{
			if (redirectUri == null)
			{
				throw new ArgumentNullException("redirectUri");
			}
			if (!string.IsNullOrEmpty(this.RedirectUri) && !string.Equals(this.RedirectUri, redirectUri, StringComparison.Ordinal))
			{
				return false;
			}
			this.RedirectUri = redirectUri;
			return this.Validated();
		}
	}
}
