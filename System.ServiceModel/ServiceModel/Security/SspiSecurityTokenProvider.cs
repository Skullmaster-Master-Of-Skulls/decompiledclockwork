using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BD RID: 701
	public class SspiSecurityTokenProvider : SecurityTokenProvider
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x000545E1 File Offset: 0x000527E1
		public SspiSecurityTokenProvider(NetworkCredential credential, bool allowNtlm, TokenImpersonationLevel impersonationLevel)
		{
			this.token = new SspiSecurityToken(impersonationLevel, allowNtlm, credential);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000545F7 File Offset: 0x000527F7
		public SspiSecurityTokenProvider(NetworkCredential credential, bool extractGroupsForWindowsAccounts, bool allowUnauthenticatedCallers)
		{
			this.token = new SspiSecurityToken(credential, extractGroupsForWindowsAccounts, allowUnauthenticatedCallers);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0005460D File Offset: 0x0005280D
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return this.token;
		}

		// Token: 0x04001BB2 RID: 7090
		internal const bool DefaultAllowNtlm = true;

		// Token: 0x04001BB3 RID: 7091
		internal const bool DefaultExtractWindowsGroupClaims = true;

		// Token: 0x04001BB4 RID: 7092
		internal const bool DefaultAllowUnauthenticatedCallers = false;

		// Token: 0x04001BB5 RID: 7093
		private SspiSecurityToken token;
	}
}
