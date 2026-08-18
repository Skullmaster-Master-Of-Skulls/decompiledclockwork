using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000EB RID: 235
	public sealed class DbEntityRefExpression : DbUnaryExpression
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0002578D File Offset: 0x0002398D
		internal DbEntityRefExpression(TypeUsage refResultType, DbExpression entity) : base(DbExpressionKind.EntityRef, refResultType, entity)
		{
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00025799 File Offset: 0x00023999
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000257AE File Offset: 0x000239AE
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
