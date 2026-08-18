using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FC RID: 252
	public sealed class DbNullExpression : DbExpression
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x00025C76 File Offset: 0x00023E76
		internal DbNullExpression(TypeUsage type) : base(DbExpressionKind.Null, type, true)
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00025C82 File Offset: 0x00023E82
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00025C97 File Offset: 0x00023E97
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
