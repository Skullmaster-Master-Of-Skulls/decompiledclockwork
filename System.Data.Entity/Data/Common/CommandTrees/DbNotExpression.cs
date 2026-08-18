using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FA RID: 1018
	public sealed class DbNotExpression : DbUnaryExpression
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x000D0C0B File Offset: 0x000CEE0B
		internal DbNotExpression(TypeUsage booleanResultType, DbExpression argument) : base(DbExpressionKind.Not, booleanResultType, argument)
		{
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000D0C17 File Offset: 0x000CEE17
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000D0C2E File Offset: 0x000CEE2E
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}
	}
}
