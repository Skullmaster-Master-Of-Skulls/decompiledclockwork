using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036B RID: 875
	internal sealed class MultisetConstructorExpr : Node
	{
		// Token: 0x0600320E RID: 12814 RVA: 0x000C4BD7 File Offset: 0x000C2DD7
		internal MultisetConstructorExpr(NodeList<Node> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000C4BE6 File Offset: 0x000C2DE6
		internal NodeList<Node> ExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x040015F7 RID: 5623
		private readonly NodeList<Node> _exprList;
	}
}
