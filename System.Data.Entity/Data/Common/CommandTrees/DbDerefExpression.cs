using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000422 RID: 1058
	public sealed class DbDerefExpression : DbUnaryExpression
	{
		// Token: 0x06003718 RID: 14104 RVA: 0x000D1985 File Offset: 0x000CFB85
		internal DbDerefExpression(TypeUsage entityResultType, DbExpression refExpr) : base(DbExpressionKind.Deref, entityResultType, refExpr)
		{
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D1990 File Offset: 0x000CFB90
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D19A7 File Offset: 0x000CFBA7
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
