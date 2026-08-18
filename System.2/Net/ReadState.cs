using System;

namespace System.Net
{
	// Token: 0x0200019B RID: 411
	internal enum ReadState
	{
		// Token: 0x04001318 RID: 4888
		Start,
		// Token: 0x04001319 RID: 4889
		StatusLine,
		// Token: 0x0400131A RID: 4890
		Headers,
		// Token: 0x0400131B RID: 4891
		Data
	}
}
