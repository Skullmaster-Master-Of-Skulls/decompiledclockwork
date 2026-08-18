using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200037A RID: 890
	internal sealed class SelectClause : Node
	{
		// Token: 0x0600323C RID: 12860 RVA: 0x000C4FC7 File Offset: 0x000C31C7
		internal SelectClause(NodeList<AliasedExpr> items, SelectKind selectKind, DistinctKind distinctKind, Node topExpr, uint methodCallCount)
		{
			this._selectKind = selectKind;
			this._selectClauseItems = items;
			this._distinctKind = distinctKind;
			this._topExpr = topExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000C4FF4 File Offset: 0x000C31F4
		internal NodeList<AliasedExpr> Items
		{
			get
			{
				return this._selectClauseItems;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000C4FFC File Offset: 0x000C31FC
		internal SelectKind SelectKind
		{
			get
			{
				return this._selectKind;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000C5004 File Offset: 0x000C3204
		internal DistinctKind DistinctKind
		{
			get
			{
				return this._distinctKind;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000C500C File Offset: 0x000C320C
		internal Node TopExpr
		{
			get
			{
				return this._topExpr;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000C5014 File Offset: 0x000C3214
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x0400162A RID: 5674
		private readonly NodeList<AliasedExpr> _selectClauseItems;

		// Token: 0x0400162B RID: 5675
		private readonly SelectKind _selectKind;

		// Token: 0x0400162C RID: 5676
		private readonly DistinctKind _distinctKind;

		// Token: 0x0400162D RID: 5677
		private readonly Node _topExpr;

		// Token: 0x0400162E RID: 5678
		private readonly uint _methodCallCount;
	}
}
