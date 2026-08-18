using System;

namespace System.Net.Cache
{
	// Token: 0x0200056B RID: 1387
	public enum RequestCacheLevel
	{
		// Token: 0x04002919 RID: 10521
		Default,
		// Token: 0x0400291A RID: 10522
		BypassCache,
		// Token: 0x0400291B RID: 10523
		CacheOnly,
		// Token: 0x0400291C RID: 10524
		CacheIfAvailable,
		// Token: 0x0400291D RID: 10525
		Revalidate,
		// Token: 0x0400291E RID: 10526
		Reload,
		// Token: 0x0400291F RID: 10527
		NoCacheNoStore
	}
}
