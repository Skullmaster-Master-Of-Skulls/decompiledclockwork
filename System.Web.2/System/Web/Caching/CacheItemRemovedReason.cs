using System;

namespace System.Web.Caching
{
	// Token: 0x0200087A RID: 2170
	public enum CacheItemRemovedReason
	{
		// Token: 0x0400349A RID: 13466
		Removed = 1,
		// Token: 0x0400349B RID: 13467
		Expired,
		// Token: 0x0400349C RID: 13468
		Underused,
		// Token: 0x0400349D RID: 13469
		DependencyChanged
	}
}
