using System;

namespace System.Net.Cache
{
	// Token: 0x02000571 RID: 1393
	internal enum CacheValidationStatus
	{
		// Token: 0x0400293D RID: 10557
		DoNotUseCache,
		// Token: 0x0400293E RID: 10558
		Fail,
		// Token: 0x0400293F RID: 10559
		DoNotTakeFromCache,
		// Token: 0x04002940 RID: 10560
		RetryResponseFromCache,
		// Token: 0x04002941 RID: 10561
		RetryResponseFromServer,
		// Token: 0x04002942 RID: 10562
		ReturnCachedResponse,
		// Token: 0x04002943 RID: 10563
		CombineCachedAndServerResponse,
		// Token: 0x04002944 RID: 10564
		CacheResponse,
		// Token: 0x04002945 RID: 10565
		UpdateResponseInformation,
		// Token: 0x04002946 RID: 10566
		RemoveFromCache,
		// Token: 0x04002947 RID: 10567
		DoNotUpdateCache,
		// Token: 0x04002948 RID: 10568
		Continue
	}
}
