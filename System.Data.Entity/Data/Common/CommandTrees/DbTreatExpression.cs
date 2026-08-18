using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000403 RID: 1027
	public sealed class DbTreatExpression : DbUnaryExpression
	{
		// Token: 0x06003688 RID: 13960 RVA: 0x000D0E6C File Offset: 0x000CF06C
		internal DbTreatExpression(TypeUsage asType, DbExpression argument) : base(DbExpressionKind.Treat, asType, argument)
		{
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000D0E78 File Offset: 0x000CF078
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000D0E8F File Offset: 0x000CF08F
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
