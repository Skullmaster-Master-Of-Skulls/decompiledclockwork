using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F9 RID: 1017
	public sealed class DbOrExpression : DbBinaryExpression
	{
		// Token: 0x06003664 RID: 13924 RVA: 0x000D0BD0 File Offset: 0x000CEDD0
		internal DbOrExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Or, booleanResultType, left, right)
		{
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000D0BDD File Offset: 0x000CEDDD
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000D0BF4 File Offset: 0x000CEDF4
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
