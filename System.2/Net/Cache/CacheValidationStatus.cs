using System;

namespace System.Net.Cache
{
	// Token: 0x02000317 RID: 791
	internal enum CacheValidationStatus
	{
		// Token: 0x04001B7C RID: 7036
		DoNotUseCache,
		// Token: 0x04001B7D RID: 7037
		Fail,
		// Token: 0x04001B7E RID: 7038
		DoNotTakeFromCache,
		// Token: 0x04001B7F RID: 7039
		RetryResponseFromCache,
		// Token: 0x04001B80 RID: 7040
		RetryResponseFromServer,
		// Token: 0x04001B81 RID: 7041
		ReturnCachedResponse,
		// Token: 0x04001B82 RID: 7042
		CombineCachedAndServerResponse,
		// Token: 0x04001B83 RID: 7043
		CacheResponse,
		// Token: 0x04001B84 RID: 7044
		UpdateResponseInformation,
		// Token: 0x04001B85 RID: 7045
		RemoveFromCache,
		// Token: 0x04001B86 RID: 7046
		DoNotUpdateCache,
		// Token: 0x04001B87 RID: 7047
		Continue
	}
}
