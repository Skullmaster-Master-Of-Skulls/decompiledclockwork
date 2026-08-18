using System;

namespace System.Xml.Schema
{
	// Token: 0x020002A1 RID: 673
	[Flags]
	internal enum XsdDateTimeFlags
	{
		// Token: 0x0400135E RID: 4958
		DateTime = 1,
		// Token: 0x0400135F RID: 4959
		Time = 2,
		// Token: 0x04001360 RID: 4960
		Date = 4,
		// Token: 0x04001361 RID: 4961
		GYearMonth = 8,
		// Token: 0x04001362 RID: 4962
		GYear = 16,
		// Token: 0x04001363 RID: 4963
		GMonthDay = 32,
		// Token: 0x04001364 RID: 4964
		GDay = 64,
		// Token: 0x04001365 RID: 4965
		GMonth = 128,
		// Token: 0x04001366 RID: 4966
		XdrDateTimeNoTz = 256,
		// Token: 0x04001367 RID: 4967
		XdrDateTime = 512,
		// Token: 0x04001368 RID: 4968
		XdrTimeNoTz = 1024,
		// Token: 0x04001369 RID: 4969
		AllXsd = 255
	}
}
