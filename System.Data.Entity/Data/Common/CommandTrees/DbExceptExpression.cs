using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040A RID: 1034
	public sealed class DbExceptExpression : DbBinaryExpression
	{
		// Token: 0x060036A4 RID: 13988 RVA: 0x000D106E File Offset: 0x000CF26E
		internal DbExceptExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Except, resultType, left, right)
		{
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000D107B File Offset: 0x000CF27B
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000D1092 File Offset: 0x000CF292
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
