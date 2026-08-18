using System;

namespace System.Net
{
	// Token: 0x020004A0 RID: 1184
	public enum WebExceptionStatus
	{
		// Token: 0x0400247C RID: 9340
		Success,
		// Token: 0x0400247D RID: 9341
		NameResolutionFailure,
		// Token: 0x0400247E RID: 9342
		ConnectFailure,
		// Token: 0x0400247F RID: 9343
		ReceiveFailure,
		// Token: 0x04002480 RID: 9344
		SendFailure,
		// Token: 0x04002481 RID: 9345
		PipelineFailure,
		// Token: 0x04002482 RID: 9346
		RequestCanceled,
		// Token: 0x04002483 RID: 9347
		ProtocolError,
		// Token: 0x04002484 RID: 9348
		ConnectionClosed,
		// Token: 0x04002485 RID: 9349
		TrustFailure,
		// Token: 0x04002486 RID: 9350
		SecureChannelFailure,
		// Token: 0x04002487 RID: 9351
		ServerProtocolViolation,
		// Token: 0x04002488 RID: 9352
		KeepAliveFailure,
		// Token: 0x04002489 RID: 9353
		Pending,
		// Token: 0x0400248A RID: 9354
		Timeout,
		// Token: 0x0400248B RID: 9355
		ProxyNameResolutionFailure,
		// Token: 0x0400248C RID: 9356
		UnknownError,
		// Token: 0x0400248D RID: 9357
		MessageLengthLimitExceeded,
		// Token: 0x0400248E RID: 9358
		CacheEntryNotFound,
		// Token: 0x0400248F RID: 9359
		RequestProhibitedByCachePolicy,
		// Token: 0x04002490 RID: 9360
		RequestProhibitedByProxy
	}
}
