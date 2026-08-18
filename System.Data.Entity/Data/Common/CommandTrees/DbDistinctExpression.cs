using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000408 RID: 1032
	public sealed class DbDistinctExpression : DbUnaryExpression
	{
		// Token: 0x0600369C RID: 13980 RVA: 0x000D0FD8 File Offset: 0x000CF1D8
		internal DbDistinctExpression(TypeUsage resultType, DbExpression argument) : base(DbExpressionKind.Distinct, resultType, argument)
		{
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000D0FE4 File Offset: 0x000CF1E4
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000D0FFB File Offset: 0x000CF1FB
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
