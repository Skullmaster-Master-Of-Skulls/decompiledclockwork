using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000025 RID: 37
	public class OAuthBearerAuthenticationProvider : IOAuthBearerAuthenticationProvider
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0000707C File Offset: 0x0000527C
		public OAuthBearerAuthenticationProvider()
		{
			this.OnRequestToken = ((OAuthRequestTokenContext context) => Task.FromResult<object>(null));
			this.OnValidateIdentity = ((OAuthValidateIdentityContext context) => Task.FromResult<object>(null));
			this.OnApplyChallenge = delegate(OAuthChallengeContext context)
			{
				context.OwinContext.Response.Headers.AppendValues("WWW-Authenticate", new string[]
				{
					context.Challenge
				});
				return Task.FromResult<int>(0);
			};
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000070F8 File Offset: 0x000052F8
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00007100 File Offset: 0x00005300
		public Func<OAuthRequestTokenContext, Task> OnRequestToken { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00007109 File Offset: 0x00005309
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00007111 File Offset: 0x00005311
		public Func<OAuthValidateIdentityContext, Task> OnValidateIdentity { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000711A File Offset: 0x0000531A
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00007122 File Offset: 0x00005322
		public Func<OAuthChallengeContext, Task> OnApplyChallenge { get; set; }

		// Token: 0x0600010F RID: 271 RVA: 0x0000712B File Offset: 0x0000532B
		public virtual Task RequestToken(OAuthRequestTokenContext context)
		{
			return this.OnRequestToken(context);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007139 File Offset: 0x00005339
		public virtual Task ValidateIdentity(OAuthValidateIdentityContext context)
		{
			return this.OnValidateIdentity(context);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007147 File Offset: 0x00005347
		public Task ApplyChallenge(OAuthChallengeContext context)
		{
			return this.OnApplyChallenge(context);
		}
	}
}
