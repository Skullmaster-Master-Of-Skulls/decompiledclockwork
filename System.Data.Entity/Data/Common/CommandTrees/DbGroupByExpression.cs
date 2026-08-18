using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040C RID: 1036
	public sealed class DbGroupByExpression : DbExpression
	{
		// Token: 0x060036AC RID: 13996 RVA: 0x000D1100 File Offset: 0x000CF300
		internal DbGroupByExpression(TypeUsage collectionOfRowResultType, DbGroupExpressionBinding input, DbExpressionList groupKeys, ReadOnlyCollection<DbAggregate> aggregates) : base(DbExpressionKind.GroupBy, collectionOfRowResultType)
		{
			this._input = input;
			this._keys = groupKeys;
			this._aggregates = aggregates;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x000D1121 File Offset: 0x000CF321
		public DbGroupExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000D1129 File Offset: 0x000CF329
		public IList<DbExpression> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000D1131 File Offset: 0x000CF331
		public IList<DbAggregate> Aggregates
		{
			get
			{
				return this._aggregates;
			}
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000D1139 File Offset: 0x000CF339
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000D1150 File Offset: 0x000CF350
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400180E RID: 6158
		private readonly DbGroupExpressionBinding _input;

		// Token: 0x0400180F RID: 6159
		private readonly DbExpressionList _keys;

		// Token: 0x04001810 RID: 6160
		private readonly ReadOnlyCollection<DbAggregate> _aggregates;
	}
}
