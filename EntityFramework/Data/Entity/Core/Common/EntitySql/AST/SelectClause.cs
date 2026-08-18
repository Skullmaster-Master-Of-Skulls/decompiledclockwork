using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200023C RID: 572
	internal sealed class SelectClause : Node
	{
		// Token: 0x060013E3 RID: 5091 RVA: 0x00051675 File Offset: 0x0004F875
		internal SelectClause(NodeList<AliasedExpr> items, SelectKind selectKind, DistinctKind distinctKind, Node topExpr, uint methodCallCount)
		{
			this._selectKind = selectKind;
			this._selectClauseItems = items;
			this._distinctKind = distinctKind;
			this._topExpr = topExpr;
			this._methodCallCount = methodCallCount;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000516A2 File Offset: 0x0004F8A2
		internal NodeList<AliasedExpr> Items
		{
			get
			{
				return this._selectClauseItems;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x000516AA File Offset: 0x0004F8AA
		internal SelectKind SelectKind
		{
			get
			{
				return this._selectKind;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000516B2 File Offset: 0x0004F8B2
		internal DistinctKind DistinctKind
		{
			get
			{
				return this._distinctKind;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000516BA File Offset: 0x0004F8BA
		internal Node TopExpr
		{
			get
			{
				return this._topExpr;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000516C2 File Offset: 0x0004F8C2
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x0400063C RID: 1596
		private readonly NodeList<AliasedExpr> _selectClauseItems;

		// Token: 0x0400063D RID: 1597
		private readonly SelectKind _selectKind;

		// Token: 0x0400063E RID: 1598
		private readonly DistinctKind _distinctKind;

		// Token: 0x0400063F RID: 1599
		private readonly Node _topExpr;

		// Token: 0x04000640 RID: 1600
		private readonly uint _methodCallCount;
	}
}
