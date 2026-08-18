using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022A RID: 554
	internal enum JoinKind
	{
		// Token: 0x04000600 RID: 1536
		Cross,
		// Token: 0x04000601 RID: 1537
		Inner,
		// Token: 0x04000602 RID: 1538
		LeftOuter,
		// Token: 0x04000603 RID: 1539
		FullOuter,
		// Token: 0x04000604 RID: 1540
		RightOuter
	}
}
