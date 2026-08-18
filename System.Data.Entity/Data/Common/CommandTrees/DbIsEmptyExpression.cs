using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FF RID: 1023
	public sealed class DbIsEmptyExpression : DbUnaryExpression
	{
		// Token: 0x0600367A RID: 13946 RVA: 0x000D0D66 File Offset: 0x000CEF66
		internal DbIsEmptyExpression(TypeUsage booleanResultType, DbExpression argument) : base(DbExpressionKind.IsEmpty, booleanResultType, argument)
		{
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000D0D72 File Offset: 0x000CEF72
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000D0D89 File Offset: 0x000CEF89
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
