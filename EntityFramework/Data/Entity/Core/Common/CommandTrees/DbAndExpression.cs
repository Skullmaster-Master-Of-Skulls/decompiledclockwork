using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000DE RID: 222
	public sealed class DbAndExpression : DbBinaryExpression
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x0002540E File Offset: 0x0002360E
		internal DbAndExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(DbExpressionKind.And, booleanResultType, left, right)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0002541A File Offset: 0x0002361A
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002542F File Offset: 0x0002362F
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
