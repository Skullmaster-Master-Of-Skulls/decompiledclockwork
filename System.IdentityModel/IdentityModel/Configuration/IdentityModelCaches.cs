using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C8 RID: 456
	public sealed class IdentityModelCaches
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000430DA File Offset: 0x000412DA
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x000430E2 File Offset: 0x000412E2
		public TokenReplayCache TokenReplayCache
		{
			get
			{
				return this.tokenReplayCache;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.tokenReplayCache = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000430FE File Offset: 0x000412FE
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00043106 File Offset: 0x00041306
		public SessionSecurityTokenCache SessionSecurityTokenCache
		{
			get
			{
				return this.sessionSecurityTokenCache;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.sessionSecurityTokenCache = value;
			}
		}

		// Token: 0x04000D78 RID: 3448
		private TokenReplayCache tokenReplayCache = new DefaultTokenReplayCache();

		// Token: 0x04000D79 RID: 3449
		private SessionSecurityTokenCache sessionSecurityTokenCache = new MruSessionSecurityTokenCache();
	}
}
