using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000416 RID: 1046
	public sealed class DbUnionAllExpression : DbBinaryExpression
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x000D1458 File Offset: 0x000CF658
		internal DbUnionAllExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.UnionAll, resultType, left, right)
		{
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000D1465 File Offset: 0x000CF665
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000D147C File Offset: 0x000CF67C
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}
	}
}
