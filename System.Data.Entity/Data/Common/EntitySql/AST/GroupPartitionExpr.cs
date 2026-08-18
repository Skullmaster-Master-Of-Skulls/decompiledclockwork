using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000363 RID: 867
	internal sealed class GroupPartitionExpr : GroupAggregateExpr
	{
		// Token: 0x060031FA RID: 12794 RVA: 0x000C4A79 File Offset: 0x000C2C79
		internal GroupPartitionExpr(DistinctKind distinctKind, Node refArgExpr) : base(distinctKind)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000C4A89 File Offset: 0x000C2C89
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x040015C2 RID: 5570
		private readonly Node _argExpr;
	}
}
