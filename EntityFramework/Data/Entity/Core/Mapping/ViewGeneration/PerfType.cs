using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000434 RID: 1076
	internal enum PerfType
	{
		// Token: 0x04000EE4 RID: 3812
		InitialSetup,
		// Token: 0x04000EE5 RID: 3813
		CellCreation,
		// Token: 0x04000EE6 RID: 3814
		KeyConstraint,
		// Token: 0x04000EE7 RID: 3815
		ViewgenContext,
		// Token: 0x04000EE8 RID: 3816
		UpdateViews,
		// Token: 0x04000EE9 RID: 3817
		DisjointConstraint,
		// Token: 0x04000EEA RID: 3818
		PartitionConstraint,
		// Token: 0x04000EEB RID: 3819
		DomainConstraint,
		// Token: 0x04000EEC RID: 3820
		ForeignConstraint,
		// Token: 0x04000EED RID: 3821
		QueryViews,
		// Token: 0x04000EEE RID: 3822
		BoolResolution,
		// Token: 0x04000EEF RID: 3823
		Unsatisfiability,
		// Token: 0x04000EF0 RID: 3824
		ViewParsing
	}
}
