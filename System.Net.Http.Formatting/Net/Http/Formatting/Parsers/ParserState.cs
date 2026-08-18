using System;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000072 RID: 114
	internal enum ParserState
	{
		// Token: 0x0400018B RID: 395
		NeedMoreData,
		// Token: 0x0400018C RID: 396
		Done,
		// Token: 0x0400018D RID: 397
		Invalid,
		// Token: 0x0400018E RID: 398
		DataTooBig
	}
}
