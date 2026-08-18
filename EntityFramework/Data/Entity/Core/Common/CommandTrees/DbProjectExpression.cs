using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000100 RID: 256
	public sealed class DbProjectExpression : DbExpression
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00025D7D File Offset: 0x00023F7D
		internal DbProjectExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression projection) : base(DbExpressionKind.Project, resultType, true)
		{
			this._input = input;
			this._projection = projection;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00025D97 File Offset: 0x00023F97
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00025D9F File Offset: 0x00023F9F
		public DbExpression Projection
		{
			get
			{
				return this._projection;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00025DA7 File Offset: 0x00023FA7
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00025DBC File Offset: 0x00023FBC
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001EA RID: 490
		private readonly DbExpressionBinding _input;

		// Token: 0x040001EB RID: 491
		private readonly DbExpression _projection;
	}
}
