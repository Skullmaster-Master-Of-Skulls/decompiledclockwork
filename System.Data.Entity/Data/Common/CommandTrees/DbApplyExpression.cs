using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000407 RID: 1031
	public sealed class DbApplyExpression : DbExpression
	{
		// Token: 0x06003697 RID: 13975 RVA: 0x000D0F81 File Offset: 0x000CF181
		internal DbApplyExpression(DbExpressionKind applyKind, TypeUsage resultRowCollectionTypeUsage, DbExpressionBinding input, DbExpressionBinding apply) : base(applyKind, resultRowCollectionTypeUsage)
		{
			this._input = input;
			this._apply = apply;
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000D0F9A File Offset: 0x000CF19A
		public DbExpressionBinding Apply
		{
			get
			{
				return this._apply;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000D0FA2 File Offset: 0x000CF1A2
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000D0FAA File Offset: 0x000CF1AA
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000D0FC1 File Offset: 0x000CF1C1
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001809 RID: 6153
		private readonly DbExpressionBinding _input;

		// Token: 0x0400180A RID: 6154
		private readonly DbExpressionBinding _apply;
	}
}
