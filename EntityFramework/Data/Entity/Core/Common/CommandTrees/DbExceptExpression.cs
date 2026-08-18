using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000EC RID: 236
	public sealed class DbExceptExpression : DbBinaryExpression
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x000257C3 File Offset: 0x000239C3
		internal DbExceptExpression(TypeUsage resultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Except, resultType, left, right)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000257D0 File Offset: 0x000239D0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000257E5 File Offset: 0x000239E5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
