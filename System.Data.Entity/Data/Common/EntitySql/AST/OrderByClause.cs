using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000380 RID: 896
	internal sealed class OrderByClause : Node
	{
		// Token: 0x0600324E RID: 12878 RVA: 0x000C50C8 File Offset: 0x000C32C8
		internal OrderByClause(NodeList<OrderByClauseItem> orderByClauseItem, Node skipExpr, Node limitExpr, uint methodCallCount)
		{
			this._orderByClauseItem = orderByClauseItem;
			this._skipExpr = skipExpr;
			this._limitExpr = limitExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000C50ED File Offset: 0x000C32ED
		internal NodeList<OrderByClauseItem> OrderByClauseItem
		{
			get
			{
				return this._orderByClauseItem;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000C50F5 File Offset: 0x000C32F5
		internal Node SkipSubClause
		{
			get
			{
				return this._skipExpr;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000C50FD File Offset: 0x000C32FD
		internal Node LimitSubClause
		{
			get
			{
				return this._limitExpr;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000C5105 File Offset: 0x000C3305
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04001639 RID: 5689
		private readonly NodeList<OrderByClauseItem> _orderByClauseItem;

		// Token: 0x0400163A RID: 5690
		private readonly Node _skipExpr;

		// Token: 0x0400163B RID: 5691
		private readonly Node _limitExpr;

		// Token: 0x0400163C RID: 5692
		private readonly uint _methodCallCount;
	}
}
