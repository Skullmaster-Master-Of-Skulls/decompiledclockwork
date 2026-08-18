using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036A RID: 874
	internal sealed class RowConstructorExpr : Node
	{
		// Token: 0x0600320C RID: 12812 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
		internal RowConstructorExpr(NodeList<AliasedExpr> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000C4BCF File Offset: 0x000C2DCF
		internal NodeList<AliasedExpr> AliasedExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x040015F6 RID: 5622
		private readonly NodeList<AliasedExpr> _exprList;
	}
}
