using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FD RID: 1021
	public sealed class DbCastExpression : DbUnaryExpression
	{
		// Token: 0x06003674 RID: 13940 RVA: 0x000D0CF2 File Offset: 0x000CEEF2
		internal DbCastExpression(TypeUsage type, DbExpression argument) : base(DbExpressionKind.Cast, type, argument)
		{
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000D0CFD File Offset: 0x000CEEFD
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000D0D14 File Offset: 0x000CEF14
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
