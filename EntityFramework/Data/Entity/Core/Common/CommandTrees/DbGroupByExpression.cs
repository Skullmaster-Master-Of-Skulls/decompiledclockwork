using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F1 RID: 241
	public sealed class DbGroupByExpression : DbExpression
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x000258F4 File Offset: 0x00023AF4
		internal DbGroupByExpression(TypeUsage collectionOfRowResultType, DbGroupExpressionBinding input, DbExpressionList groupKeys, ReadOnlyCollection<DbAggregate> aggregates) : base(DbExpressionKind.GroupBy, collectionOfRowResultType, true)
		{
			this._input = input;
			this._keys = groupKeys;
			this._aggregates = aggregates;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00025916 File Offset: 0x00023B16
		public DbGroupExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0002591E File Offset: 0x00023B1E
		public IList<DbExpression> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00025926 File Offset: 0x00023B26
		public IList<DbAggregate> Aggregates
		{
			get
			{
				return this._aggregates;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002592E File Offset: 0x00023B2E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00025943 File Offset: 0x00023B43
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001D7 RID: 471
		private readonly DbGroupExpressionBinding _input;

		// Token: 0x040001D8 RID: 472
		private readonly DbExpressionList _keys;

		// Token: 0x040001D9 RID: 473
		private readonly ReadOnlyCollection<DbAggregate> _aggregates;
	}
}
