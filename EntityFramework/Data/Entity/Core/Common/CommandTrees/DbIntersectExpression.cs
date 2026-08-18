using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F2 RID: 242
	public sealed class DbIntersectExpression : DbBinaryExpression
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00025958 File Offset: 0x00023B58
		internal DbIntersectExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Intersect, resultType, left, right)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00025965 File Offset: 0x00023B65
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002597A File Offset: 0x00023B7A
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
