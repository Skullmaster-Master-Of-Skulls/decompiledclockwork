using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000410 RID: 1040
	public sealed class DbLimitExpression : DbExpression
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000D1250 File Offset: 0x000CF450
		internal DbLimitExpression(TypeUsage resultType, DbExpression argument, DbExpression limit, bool withTies) : base(DbExpressionKind.Limit, resultType)
		{
			this._argument = argument;
			this._limit = limit;
			this._withTies = withTies;
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000D1271 File Offset: 0x000CF471
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000D1279 File Offset: 0x000CF479
		public DbExpression Limit
		{
			get
			{
				return this._limit;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000D1281 File Offset: 0x000CF481
		public bool WithTies
		{
			get
			{
				return this._withTies;
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000D1289 File Offset: 0x000CF489
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000D12A0 File Offset: 0x000CF4A0
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001815 RID: 6165
		private readonly DbExpression _argument;

		// Token: 0x04001816 RID: 6166
		private readonly DbExpression _limit;

		// Token: 0x04001817 RID: 6167
		private readonly bool _withTies;
	}
}
