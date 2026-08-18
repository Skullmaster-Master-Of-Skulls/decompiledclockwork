using System;

namespace System.Net.Cache
{
	// Token: 0x02000311 RID: 785
	public enum RequestCacheLevel
	{
		// Token: 0x04001B58 RID: 7000
		Default,
		// Token: 0x04001B59 RID: 7001
		BypassCache,
		// Token: 0x04001B5A RID: 7002
		CacheOnly,
		// Token: 0x04001B5B RID: 7003
		CacheIfAvailable,
		// Token: 0x04001B5C RID: 7004
		Revalidate,
		// Token: 0x04001B5D RID: 7005
		Reload,
		// Token: 0x04001B5E RID: 7006
		NoCacheNoStore
	}
}
