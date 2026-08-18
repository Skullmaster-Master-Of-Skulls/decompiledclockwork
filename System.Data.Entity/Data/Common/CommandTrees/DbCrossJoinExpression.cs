using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040E RID: 1038
	public sealed class DbCrossJoinExpression : DbExpression
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x000D11A2 File Offset: 0x000CF3A2
		internal DbCrossJoinExpression(TypeUsage collectionOfRowResultType, ReadOnlyCollection<DbExpressionBinding> inputs) : base(DbExpressionKind.CrossJoin, collectionOfRowResultType)
		{
			this._inputs = inputs;
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000D11B3 File Offset: 0x000CF3B3
		public IList<DbExpressionBinding> Inputs
		{
			get
			{
				return this._inputs;
			}
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000D11BB File Offset: 0x000CF3BB
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000D11D2 File Offset: 0x000CF3D2
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001811 RID: 6161
		private readonly ReadOnlyCollection<DbExpressionBinding> _inputs;
	}
}
