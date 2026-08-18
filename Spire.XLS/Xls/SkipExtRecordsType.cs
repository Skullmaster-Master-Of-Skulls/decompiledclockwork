using System;

namespace Spire.Xls
{
	// Token: 0x0200007D RID: 125
	[Flags]
	public enum SkipExtRecordsType
	{
		// Token: 0x040005A7 RID: 1447
		None = 0,
		// Token: 0x040005A8 RID: 1448
		Macros = 1,
		// Token: 0x040005A9 RID: 1449
		Drawings = 2,
		// Token: 0x040005AA RID: 1450
		SummaryInfo = 4,
		// Token: 0x040005AB RID: 1451
		CopySubstreams = 16,
		// Token: 0x040005AC RID: 1452
		All = 23
	}
}
