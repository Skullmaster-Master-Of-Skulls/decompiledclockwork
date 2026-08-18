using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FE RID: 1022
	public sealed class DbComparisonExpression : DbBinaryExpression
	{
		// Token: 0x06003677 RID: 13943 RVA: 0x000D0D2B File Offset: 0x000CEF2B
		internal DbComparisonExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(kind, booleanResultType, left, right)
		{
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000D0D38 File Offset: 0x000CEF38
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000D0D4F File Offset: 0x000CEF4F
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
