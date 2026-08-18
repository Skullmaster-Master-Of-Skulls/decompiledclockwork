using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005C RID: 92
	internal enum PlanCompilerPhase
	{
		// Token: 0x040007D7 RID: 2007
		PreProcessor,
		// Token: 0x040007D8 RID: 2008
		AggregatePushdown,
		// Token: 0x040007D9 RID: 2009
		Normalization,
		// Token: 0x040007DA RID: 2010
		NTE,
		// Token: 0x040007DB RID: 2011
		ProjectionPruning,
		// Token: 0x040007DC RID: 2012
		NestPullup,
		// Token: 0x040007DD RID: 2013
		Transformations,
		// Token: 0x040007DE RID: 2014
		JoinElimination,
		// Token: 0x040007DF RID: 2015
		CodeGen,
		// Token: 0x040007E0 RID: 2016
		PostCodeGen,
		// Token: 0x040007E1 RID: 2017
		MaxMarker
	}
}
