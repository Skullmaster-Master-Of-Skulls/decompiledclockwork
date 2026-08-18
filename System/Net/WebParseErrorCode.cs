using System;

namespace System.Net
{
	// Token: 0x020004C4 RID: 1220
	internal enum WebParseErrorCode
	{
		// Token: 0x04002574 RID: 9588
		Generic,
		// Token: 0x04002575 RID: 9589
		InvalidHeaderName,
		// Token: 0x04002576 RID: 9590
		InvalidContentLength,
		// Token: 0x04002577 RID: 9591
		IncompleteHeaderLine,
		// Token: 0x04002578 RID: 9592
		CrLfError,
		// Token: 0x04002579 RID: 9593
		InvalidChunkFormat,
		// Token: 0x0400257A RID: 9594
		UnexpectedServerResponse
	}
}
