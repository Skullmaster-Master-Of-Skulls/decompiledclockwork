using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000362 RID: 866
	internal abstract class GroupAggregateExpr : Node
	{
		// Token: 0x060031F9 RID: 12793 RVA: 0x000C4A6A File Offset: 0x000C2C6A
		internal GroupAggregateExpr(DistinctKind distinctKind)
		{
			this.DistinctKind = distinctKind;
		}

		// Token: 0x040015C0 RID: 5568
		internal readonly DistinctKind DistinctKind;

		// Token: 0x040015C1 RID: 5569
		internal GroupAggregateInfo AggregateInfo;
	}
}
