using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040F RID: 1039
	public sealed class DbJoinExpression : DbExpression
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x000D11E9 File Offset: 0x000CF3E9
		internal DbJoinExpression(DbExpressionKind joinKind, TypeUsage collectionOfRowResultType, DbExpressionBinding left, DbExpressionBinding right, DbExpression condition) : base(joinKind, collectionOfRowResultType)
		{
			this._left = left;
			this._right = right;
			this._condition = condition;
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000D120A File Offset: 0x000CF40A
		public DbExpressionBinding Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000D1212 File Offset: 0x000CF412
		public DbExpressionBinding Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000D121A File Offset: 0x000CF41A
		public DbExpression JoinCondition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000D1222 File Offset: 0x000CF422
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000D1239 File Offset: 0x000CF439
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001812 RID: 6162
		private readonly DbExpressionBinding _left;

		// Token: 0x04001813 RID: 6163
		private readonly DbExpressionBinding _right;

		// Token: 0x04001814 RID: 6164
		private readonly DbExpression _condition;
	}
}
