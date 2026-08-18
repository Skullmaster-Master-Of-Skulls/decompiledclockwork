using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F3 RID: 243
	public sealed class DbIsEmptyExpression : DbUnaryExpression
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0002598F File Offset: 0x00023B8F
		internal DbIsEmptyExpression(TypeUsage booleanResultType, DbExpression argument) : base(DbExpressionKind.IsEmpty, booleanResultType, argument)
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002599B File Offset: 0x00023B9B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000259B0 File Offset: 0x00023BB0
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
