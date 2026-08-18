using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000137 RID: 311
	public sealed class DbUnionAllExpression : DbBinaryExpression
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00035E7B File Offset: 0x0003407B
		internal DbUnionAllExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.UnionAll, resultType, left, right)
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00035E88 File Offset: 0x00034088
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00035E9D File Offset: 0x0003409D
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
