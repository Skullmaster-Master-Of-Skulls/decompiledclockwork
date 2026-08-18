using System;

namespace System.Net
{
	// Token: 0x020004C0 RID: 1216
	internal enum ReadState
	{
		// Token: 0x0400255F RID: 9567
		Start,
		// Token: 0x04002560 RID: 9568
		StatusLine,
		// Token: 0x04002561 RID: 9569
		Headers,
		// Token: 0x04002562 RID: 9570
		Data
	}
}
