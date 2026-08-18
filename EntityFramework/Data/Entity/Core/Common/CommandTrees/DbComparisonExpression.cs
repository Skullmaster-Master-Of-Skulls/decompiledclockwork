using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E5 RID: 229
	public sealed class DbComparisonExpression : DbBinaryExpression
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x0002559E File Offset: 0x0002379E
		internal DbComparisonExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(kind, booleanResultType, left, right)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000255AB File Offset: 0x000237AB
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000255C0 File Offset: 0x000237C0
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
