using System;

namespace System.Net
{
	// Token: 0x0200019C RID: 412
	internal enum DataParseStatus
	{
		// Token: 0x0400131D RID: 4893
		NeedMoreData,
		// Token: 0x0400131E RID: 4894
		ContinueParsing,
		// Token: 0x0400131F RID: 4895
		Done,
		// Token: 0x04001320 RID: 4896
		Invalid,
		// Token: 0x04001321 RID: 4897
		DataTooBig
	}
}
