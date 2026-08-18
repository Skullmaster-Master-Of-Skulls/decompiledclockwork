using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200021C RID: 540
	internal sealed class MultisetConstructorExpr : Node
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x0005050E File Offset: 0x0004E70E
		internal MultisetConstructorExpr(NodeList<Node> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0005051D File Offset: 0x0004E71D
		internal NodeList<Node> ExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x040005DB RID: 1499
		private readonly NodeList<Node> _exprList;
	}
}
