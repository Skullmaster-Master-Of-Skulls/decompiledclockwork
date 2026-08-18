using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E3 RID: 227
	public class DbCastExpression : DbUnaryExpression
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00025561 File Offset: 0x00023761
		internal DbCastExpression()
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00025569 File Offset: 0x00023769
		internal DbCastExpression(TypeUsage type, DbExpression argument) : base(DbExpressionKind.Cast, type, argument)
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00025574 File Offset: 0x00023774
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00025589 File Offset: 0x00023789
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
