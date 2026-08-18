using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F4 RID: 244
	public class DbIsNullExpression : DbUnaryExpression
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x000259C5 File Offset: 0x00023BC5
		internal DbIsNullExpression()
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000259CD File Offset: 0x00023BCD
		internal DbIsNullExpression(TypeUsage booleanResultType, DbExpression arg) : base(DbExpressionKind.IsNull, booleanResultType, arg)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000259D9 File Offset: 0x00023BD9
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000259EE File Offset: 0x00023BEE
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
