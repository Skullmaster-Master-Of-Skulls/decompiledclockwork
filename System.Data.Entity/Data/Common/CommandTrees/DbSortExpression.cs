using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000415 RID: 1045
	public sealed class DbSortExpression : DbExpression
	{
		// Token: 0x060036D9 RID: 14041 RVA: 0x000D1401 File Offset: 0x000CF601
		internal DbSortExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder) : base(DbExpressionKind.Sort, resultType)
		{
			this._input = input;
			this._keys = sortOrder;
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000D141A File Offset: 0x000CF61A
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000D1422 File Offset: 0x000CF622
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000D142A File Offset: 0x000CF62A
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000D1441 File Offset: 0x000CF641
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001822 RID: 6178
		private readonly DbExpressionBinding _input;

		// Token: 0x04001823 RID: 6179
		private readonly ReadOnlyCollection<DbSortClause> _keys;
	}
}
