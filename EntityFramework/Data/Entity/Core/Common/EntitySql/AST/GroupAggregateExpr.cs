using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000224 RID: 548
	internal abstract class GroupAggregateExpr : Node
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x00050734 File Offset: 0x0004E934
		internal GroupAggregateExpr(DistinctKind distinctKind)
		{
			this.DistinctKind = distinctKind;
		}

		// Token: 0x040005F3 RID: 1523
		internal readonly DistinctKind DistinctKind;

		// Token: 0x040005F4 RID: 1524
		internal GroupAggregateInfo AggregateInfo;
	}
}
