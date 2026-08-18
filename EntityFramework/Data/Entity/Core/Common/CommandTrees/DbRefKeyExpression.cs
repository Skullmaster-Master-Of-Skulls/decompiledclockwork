using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000136 RID: 310
	public sealed class DbRefKeyExpression : DbUnaryExpression
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x00035E45 File Offset: 0x00034045
		internal DbRefKeyExpression(TypeUsage rowResultType, DbExpression reference) : base(DbExpressionKind.RefKey, rowResultType, reference)
		{
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00035E51 File Offset: 0x00034051
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00035E66 File Offset: 0x00034066
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
