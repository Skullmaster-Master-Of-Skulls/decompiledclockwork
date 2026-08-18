using System;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000266 RID: 614
	internal enum PerfType
	{
		// Token: 0x0400115F RID: 4447
		InitialSetup,
		// Token: 0x04001160 RID: 4448
		CellCreation,
		// Token: 0x04001161 RID: 4449
		KeyConstraint,
		// Token: 0x04001162 RID: 4450
		ViewgenContext,
		// Token: 0x04001163 RID: 4451
		UpdateViews,
		// Token: 0x04001164 RID: 4452
		DisjointConstraint,
		// Token: 0x04001165 RID: 4453
		PartitionConstraint,
		// Token: 0x04001166 RID: 4454
		DomainConstraint,
		// Token: 0x04001167 RID: 4455
		ForeignConstraint,
		// Token: 0x04001168 RID: 4456
		QueryViews,
		// Token: 0x04001169 RID: 4457
		BoolResolution,
		// Token: 0x0400116A RID: 4458
		Unsatisfiability,
		// Token: 0x0400116B RID: 4459
		ViewParsing
	}
}
