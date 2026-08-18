using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000414 RID: 1044
	public sealed class DbSkipExpression : DbExpression
	{
		// Token: 0x060036D3 RID: 14035 RVA: 0x000D139A File Offset: 0x000CF59A
		internal DbSkipExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder, DbExpression count) : base(DbExpressionKind.Skip, resultType)
		{
			this._input = input;
			this._keys = sortOrder;
			this._count = count;
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000D13BB File Offset: 0x000CF5BB
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000D13C3 File Offset: 0x000CF5C3
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000D13CB File Offset: 0x000CF5CB
		public DbExpression Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000D13D3 File Offset: 0x000CF5D3
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000D13EA File Offset: 0x000CF5EA
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400181F RID: 6175
		private readonly DbExpressionBinding _input;

		// Token: 0x04001820 RID: 6176
		private readonly ReadOnlyCollection<DbSortClause> _keys;

		// Token: 0x04001821 RID: 6177
		private readonly DbExpression _count;
	}
}
