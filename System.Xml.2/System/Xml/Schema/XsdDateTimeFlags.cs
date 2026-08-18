using System;

namespace System.Xml.Schema
{
	// Token: 0x020002CF RID: 719
	[Flags]
	internal enum XsdDateTimeFlags
	{
		// Token: 0x04001294 RID: 4756
		DateTime = 1,
		// Token: 0x04001295 RID: 4757
		Time = 2,
		// Token: 0x04001296 RID: 4758
		Date = 4,
		// Token: 0x04001297 RID: 4759
		GYearMonth = 8,
		// Token: 0x04001298 RID: 4760
		GYear = 16,
		// Token: 0x04001299 RID: 4761
		GMonthDay = 32,
		// Token: 0x0400129A RID: 4762
		GDay = 64,
		// Token: 0x0400129B RID: 4763
		GMonth = 128,
		// Token: 0x0400129C RID: 4764
		XdrDateTimeNoTz = 256,
		// Token: 0x0400129D RID: 4765
		XdrDateTime = 512,
		// Token: 0x0400129E RID: 4766
		XdrTimeNoTz = 1024,
		// Token: 0x0400129F RID: 4767
		AllXsd = 255
	}
}
