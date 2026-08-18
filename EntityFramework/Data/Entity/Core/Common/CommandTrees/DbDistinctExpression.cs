using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E9 RID: 233
	public sealed class DbDistinctExpression : DbUnaryExpression
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x000256FF File Offset: 0x000238FF
		internal DbDistinctExpression(TypeUsage resultType, DbExpression argument) : base(DbExpressionKind.Distinct, resultType, argument)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002570B File Offset: 0x0002390B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00025720 File Offset: 0x00023920
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
