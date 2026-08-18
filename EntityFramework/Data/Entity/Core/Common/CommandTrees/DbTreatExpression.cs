using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000109 RID: 265
	public sealed class DbTreatExpression : DbUnaryExpression
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0002614F File Offset: 0x0002434F
		internal DbTreatExpression(TypeUsage asType, DbExpression argument) : base(DbExpressionKind.Treat, asType, argument)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002615B File Offset: 0x0002435B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00026170 File Offset: 0x00024370
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
