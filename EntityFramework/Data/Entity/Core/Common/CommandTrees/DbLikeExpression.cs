using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F8 RID: 248
	public sealed class DbLikeExpression : DbExpression
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00025B00 File Offset: 0x00023D00
		internal DbLikeExpression(TypeUsage booleanResultType, DbExpression input, DbExpression pattern, DbExpression escape) : base(DbExpressionKind.Like, booleanResultType, true)
		{
			this._argument = input;
			this._pattern = pattern;
			this._escape = escape;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00025B22 File Offset: 0x00023D22
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00025B2A File Offset: 0x00023D2A
		public DbExpression Pattern
		{
			get
			{
				return this._pattern;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00025B32 File Offset: 0x00023D32
		public DbExpression Escape
		{
			get
			{
				return this._escape;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00025B3A File Offset: 0x00023D3A
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00025B4F File Offset: 0x00023D4F
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001E0 RID: 480
		private readonly DbExpression _argument;

		// Token: 0x040001E1 RID: 481
		private readonly DbExpression _pattern;

		// Token: 0x040001E2 RID: 482
		private readonly DbExpression _escape;
	}
}
