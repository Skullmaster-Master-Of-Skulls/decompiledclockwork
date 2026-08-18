using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068C RID: 1676
	internal enum PlanCompilerPhase
	{
		// Token: 0x0400187F RID: 6271
		PreProcessor,
		// Token: 0x04001880 RID: 6272
		AggregatePushdown,
		// Token: 0x04001881 RID: 6273
		Normalization,
		// Token: 0x04001882 RID: 6274
		NTE,
		// Token: 0x04001883 RID: 6275
		ProjectionPruning,
		// Token: 0x04001884 RID: 6276
		NestPullup,
		// Token: 0x04001885 RID: 6277
		Transformations,
		// Token: 0x04001886 RID: 6278
		JoinElimination,
		// Token: 0x04001887 RID: 6279
		NullSemantics,
		// Token: 0x04001888 RID: 6280
		CodeGen,
		// Token: 0x04001889 RID: 6281
		PostCodeGen,
		// Token: 0x0400188A RID: 6282
		MaxMarker
	}
}
