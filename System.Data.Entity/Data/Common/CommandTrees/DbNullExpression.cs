using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000418 RID: 1048
	public sealed class DbNullExpression : DbExpression
	{
		// Token: 0x060036E6 RID: 14054 RVA: 0x000D153D File Offset: 0x000CF73D
		internal DbNullExpression(TypeUsage type) : base(DbExpressionKind.Null, type)
		{
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000D1548 File Offset: 0x000CF748
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000D155F File Offset: 0x000CF75F
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
