using System;

namespace System.Web.Razor
{
	// Token: 0x02000050 RID: 80
	[Flags]
	public enum PartialParseResult
	{
		// Token: 0x040000FE RID: 254
		Rejected = 1,
		// Token: 0x040000FF RID: 255
		Accepted = 2,
		// Token: 0x04000100 RID: 256
		Provisional = 4,
		// Token: 0x04000101 RID: 257
		SpanContextChanged = 8,
		// Token: 0x04000102 RID: 258
		AutoCompleteBlock = 16
	}
}
