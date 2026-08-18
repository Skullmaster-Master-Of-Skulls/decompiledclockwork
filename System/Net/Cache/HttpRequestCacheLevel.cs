using System;

namespace System.Net.Cache
{
	// Token: 0x0200056D RID: 1389
	public enum HttpRequestCacheLevel
	{
		// Token: 0x04002922 RID: 10530
		Default,
		// Token: 0x04002923 RID: 10531
		BypassCache,
		// Token: 0x04002924 RID: 10532
		CacheOnly,
		// Token: 0x04002925 RID: 10533
		CacheIfAvailable,
		// Token: 0x04002926 RID: 10534
		Revalidate,
		// Token: 0x04002927 RID: 10535
		Reload,
		// Token: 0x04002928 RID: 10536
		NoCacheNoStore,
		// Token: 0x04002929 RID: 10537
		CacheOrNextCacheOnly,
		// Token: 0x0400292A RID: 10538
		Refresh
	}
}
