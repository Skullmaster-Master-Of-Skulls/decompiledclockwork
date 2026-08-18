using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040D RID: 1037
	public sealed class DbIntersectExpression : DbBinaryExpression
	{
		// Token: 0x060036B2 RID: 14002 RVA: 0x000D1167 File Offset: 0x000CF367
		internal DbIntersectExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Intersect, resultType, left, right)
		{
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000D1174 File Offset: 0x000CF374
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000D118B File Offset: 0x000CF38B
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
