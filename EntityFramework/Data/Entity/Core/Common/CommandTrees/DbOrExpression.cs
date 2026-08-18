using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FE RID: 254
	public class DbOrExpression : DbBinaryExpression
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00025CF1 File Offset: 0x00023EF1
		internal DbOrExpression()
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00025CF9 File Offset: 0x00023EF9
		internal DbOrExpression(TypeUsage booleanResultType, DbExpression left, DbExpression right) : base(DbExpressionKind.Or, booleanResultType, left, right)
		{
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00025D06 File Offset: 0x00023F06
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00025D1B File Offset: 0x00023F1B
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
