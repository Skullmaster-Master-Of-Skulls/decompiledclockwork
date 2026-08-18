using System;

namespace System.Net
{
	// Token: 0x020004C1 RID: 1217
	internal enum DataParseStatus
	{
		// Token: 0x04002564 RID: 9572
		NeedMoreData,
		// Token: 0x04002565 RID: 9573
		ContinueParsing,
		// Token: 0x04002566 RID: 9574
		Done,
		// Token: 0x04002567 RID: 9575
		Invalid,
		// Token: 0x04002568 RID: 9576
		DataTooBig
	}
}
