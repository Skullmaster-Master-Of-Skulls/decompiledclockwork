using System;

namespace System.Net
{
	// Token: 0x0200017F RID: 383
	[__DynamicallyInvokable]
	public enum WebExceptionStatus
	{
		// Token: 0x04001235 RID: 4661
		[__DynamicallyInvokable]
		Success,
		// Token: 0x04001236 RID: 4662
		NameResolutionFailure,
		// Token: 0x04001237 RID: 4663
		[__DynamicallyInvokable]
		ConnectFailure,
		// Token: 0x04001238 RID: 4664
		ReceiveFailure,
		// Token: 0x04001239 RID: 4665
		[__DynamicallyInvokable]
		SendFailure,
		// Token: 0x0400123A RID: 4666
		PipelineFailure,
		// Token: 0x0400123B RID: 4667
		[__DynamicallyInvokable]
		RequestCanceled,
		// Token: 0x0400123C RID: 4668
		ProtocolError,
		// Token: 0x0400123D RID: 4669
		ConnectionClosed,
		// Token: 0x0400123E RID: 4670
		TrustFailure,
		// Token: 0x0400123F RID: 4671
		SecureChannelFailure,
		// Token: 0x04001240 RID: 4672
		ServerProtocolViolation,
		// Token: 0x04001241 RID: 4673
		KeepAliveFailure,
		// Token: 0x04001242 RID: 4674
		[__DynamicallyInvokable]
		Pending,
		// Token: 0x04001243 RID: 4675
		Timeout,
		// Token: 0x04001244 RID: 4676
		ProxyNameResolutionFailure,
		// Token: 0x04001245 RID: 4677
		[__DynamicallyInvokable]
		UnknownError,
		// Token: 0x04001246 RID: 4678
		[__DynamicallyInvokable]
		MessageLengthLimitExceeded,
		// Token: 0x04001247 RID: 4679
		CacheEntryNotFound,
		// Token: 0x04001248 RID: 4680
		RequestProhibitedByCachePolicy,
		// Token: 0x04001249 RID: 4681
		RequestProhibitedByProxy
	}
}
