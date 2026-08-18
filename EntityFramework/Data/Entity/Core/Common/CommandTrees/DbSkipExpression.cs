using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000106 RID: 262
	public sealed class DbSkipExpression : DbExpression
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00026062 File Offset: 0x00024262
		internal DbSkipExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder, DbExpression count) : base(DbExpressionKind.Skip, resultType, true)
		{
			this._input = input;
			this._keys = sortOrder;
			this._count = count;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00026084 File Offset: 0x00024284
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002608C File Offset: 0x0002428C
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00026094 File Offset: 0x00024294
		public DbExpression Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002609C File Offset: 0x0002429C
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000260B1 File Offset: 0x000242B1
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001F8 RID: 504
		private readonly DbExpressionBinding _input;

		// Token: 0x040001F9 RID: 505
		private readonly ReadOnlyCollection<DbSortClause> _keys;

		// Token: 0x040001FA RID: 506
		private readonly DbExpression _count;
	}
}
