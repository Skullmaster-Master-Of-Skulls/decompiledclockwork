using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000400 RID: 1024
	public sealed class DbIsNullExpression : DbUnaryExpression
	{
		// Token: 0x0600367D RID: 13949 RVA: 0x000D0DA0 File Offset: 0x000CEFA0
		internal DbIsNullExpression(TypeUsage booleanResultType, DbExpression arg, bool isRowTypeArgumentAllowed) : base(DbExpressionKind.IsNull, booleanResultType, arg)
		{
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000D0DAC File Offset: 0x000CEFAC
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000D0DC3 File Offset: 0x000CEFC3
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
