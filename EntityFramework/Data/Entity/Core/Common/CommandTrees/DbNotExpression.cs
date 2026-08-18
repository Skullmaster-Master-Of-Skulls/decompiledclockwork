using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FB RID: 251
	public sealed class DbNotExpression : DbUnaryExpression
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00025C40 File Offset: 0x00023E40
		internal DbNotExpression(TypeUsage booleanResultType, DbExpression argument) : base(DbExpressionKind.Not, booleanResultType, argument)
		{
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00025C4C File Offset: 0x00023E4C
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00025C61 File Offset: 0x00023E61
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
