using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000226 RID: 550
	internal sealed class GroupPartitionExpr : GroupAggregateExpr
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0005075A File Offset: 0x0004E95A
		internal GroupPartitionExpr(DistinctKind distinctKind, Node refArgExpr) : base(distinctKind)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0005076A File Offset: 0x0004E96A
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x040005F6 RID: 1526
		private readonly Node _argExpr;
	}
}
