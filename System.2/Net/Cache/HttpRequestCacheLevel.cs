using System;

namespace System.Net.Cache
{
	// Token: 0x02000313 RID: 787
	public enum HttpRequestCacheLevel
	{
		// Token: 0x04001B61 RID: 7009
		Default,
		// Token: 0x04001B62 RID: 7010
		BypassCache,
		// Token: 0x04001B63 RID: 7011
		CacheOnly,
		// Token: 0x04001B64 RID: 7012
		CacheIfAvailable,
		// Token: 0x04001B65 RID: 7013
		Revalidate,
		// Token: 0x04001B66 RID: 7014
		Reload,
		// Token: 0x04001B67 RID: 7015
		NoCacheNoStore,
		// Token: 0x04001B68 RID: 7016
		CacheOrNextCacheOnly,
		// Token: 0x04001B69 RID: 7017
		Refresh
	}
}
