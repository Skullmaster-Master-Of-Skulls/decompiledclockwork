using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000405 RID: 1029
	public sealed class DbEntityRefExpression : DbUnaryExpression
	{
		// Token: 0x06003691 RID: 13969 RVA: 0x000D0F0D File Offset: 0x000CF10D
		internal DbEntityRefExpression(TypeUsage refResultType, DbExpression entity) : base(DbExpressionKind.EntityRef, refResultType, entity)
		{
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000D0F19 File Offset: 0x000CF119
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000D0F30 File Offset: 0x000CF130
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
