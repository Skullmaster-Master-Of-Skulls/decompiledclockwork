using System;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037D RID: 893
	public interface ILogonTokenCacheManager
	{
		// Token: 0x0600211E RID: 8478
		bool RemoveCachedLogonToken(string username);

		// Token: 0x0600211F RID: 8479
		void FlushLogonTokenCache();
	}
}
