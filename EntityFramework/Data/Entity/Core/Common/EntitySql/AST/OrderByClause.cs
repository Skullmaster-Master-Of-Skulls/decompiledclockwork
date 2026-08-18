using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000230 RID: 560
	internal sealed class OrderByClause : Node
	{
		// Token: 0x060013C1 RID: 5057 RVA: 0x00051444 File Offset: 0x0004F644
		internal OrderByClause(NodeList<OrderByClauseItem> orderByClauseItem, Node skipExpr, Node limitExpr, uint methodCallCount)
		{
			this._orderByClauseItem = orderByClauseItem;
			this._skipExpr = skipExpr;
			this._limitExpr = limitExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00051469 File Offset: 0x0004F669
		internal NodeList<OrderByClauseItem> OrderByClauseItem
		{
			get
			{
				return this._orderByClauseItem;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00051471 File Offset: 0x0004F671
		internal Node SkipSubClause
		{
			get
			{
				return this._skipExpr;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00051479 File Offset: 0x0004F679
		internal Node LimitSubClause
		{
			get
			{
				return this._limitExpr;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00051481 File Offset: 0x0004F681
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04000623 RID: 1571
		private readonly NodeList<OrderByClauseItem> _orderByClauseItem;

		// Token: 0x04000624 RID: 1572
		private readonly Node _skipExpr;

		// Token: 0x04000625 RID: 1573
		private readonly Node _limitExpr;

		// Token: 0x04000626 RID: 1574
		private readonly uint _methodCallCount;
	}
}
