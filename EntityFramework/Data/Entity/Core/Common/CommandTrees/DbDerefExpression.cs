using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E8 RID: 232
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deref")]
	public sealed class DbDerefExpression : DbUnaryExpression
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x000256CA File Offset: 0x000238CA
		internal DbDerefExpression(TypeUsage entityResultType, DbExpression refExpr) : base(DbExpressionKind.Deref, entityResultType, refExpr)
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000256D5 File Offset: 0x000238D5
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000256EA File Offset: 0x000238EA
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
