using System;

namespace System.Net
{
	// Token: 0x0200019F RID: 415
	internal enum WebParseErrorCode
	{
		// Token: 0x0400132D RID: 4909
		Generic,
		// Token: 0x0400132E RID: 4910
		InvalidHeaderName,
		// Token: 0x0400132F RID: 4911
		InvalidContentLength,
		// Token: 0x04001330 RID: 4912
		IncompleteHeaderLine,
		// Token: 0x04001331 RID: 4913
		CrLfError,
		// Token: 0x04001332 RID: 4914
		InvalidChunkFormat,
		// Token: 0x04001333 RID: 4915
		UnexpectedServerResponse
	}
}
