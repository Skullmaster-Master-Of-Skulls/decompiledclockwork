using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F9 RID: 249
	public sealed class DbLimitExpression : DbExpression
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00025B64 File Offset: 0x00023D64
		internal DbLimitExpression(TypeUsage resultType, DbExpression argument, DbExpression limit, bool withTies) : base(DbExpressionKind.Limit, resultType, true)
		{
			this._argument = argument;
			this._limit = limit;
			this._withTies = withTies;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00025B86 File Offset: 0x00023D86
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00025B8E File Offset: 0x00023D8E
		public DbExpression Limit
		{
			get
			{
				return this._limit;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00025B96 File Offset: 0x00023D96
		public bool WithTies
		{
			get
			{
				return this._withTies;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00025B9E File Offset: 0x00023D9E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00025BB3 File Offset: 0x00023DB3
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001E3 RID: 483
		private readonly DbExpression _argument;

		// Token: 0x040001E4 RID: 484
		private readonly DbExpression _limit;

		// Token: 0x040001E5 RID: 485
		private readonly bool _withTies;
	}
}
