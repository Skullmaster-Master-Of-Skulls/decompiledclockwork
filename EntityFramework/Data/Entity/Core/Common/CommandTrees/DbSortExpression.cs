using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000108 RID: 264
	public sealed class DbSortExpression : DbExpression
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x000260FB File Offset: 0x000242FB
		internal DbSortExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder) : base(DbExpressionKind.Sort, resultType, true)
		{
			this._input = input;
			this._keys = sortOrder;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00026115 File Offset: 0x00024315
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0002611D File Offset: 0x0002431D
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00026125 File Offset: 0x00024325
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002613A File Offset: 0x0002433A
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001FE RID: 510
		private readonly DbExpressionBinding _input;

		// Token: 0x040001FF RID: 511
		private readonly ReadOnlyCollection<DbSortClause> _keys;
	}
}
