using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000406 RID: 1030
	public sealed class DbRefKeyExpression : DbUnaryExpression
	{
		// Token: 0x06003694 RID: 13972 RVA: 0x000D0F47 File Offset: 0x000CF147
		internal DbRefKeyExpression(TypeUsage rowResultType, DbExpression reference) : base(DbExpressionKind.RefKey, rowResultType, reference)
		{
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000D0F53 File Offset: 0x000CF153
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000D0F6A File Offset: 0x000CF16A
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
