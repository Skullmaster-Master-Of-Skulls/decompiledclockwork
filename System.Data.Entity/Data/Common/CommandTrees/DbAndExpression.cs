using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F8 RID: 1016
	public sealed class DbAndExpression : DbBinaryExpression
	{
		// Token: 0x06003661 RID: 13921 RVA: 0x000D0B96 File Offset: 0x000CED96
		internal DbAndExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(DbExpressionKind.And, booleanResultType, left, right)
		{
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000D0BA2 File Offset: 0x000CEDA2
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000D0BB9 File Offset: 0x000CEDB9
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
