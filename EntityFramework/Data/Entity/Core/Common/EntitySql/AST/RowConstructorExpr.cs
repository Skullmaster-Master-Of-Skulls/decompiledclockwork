using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200023A RID: 570
	internal sealed class RowConstructorExpr : Node
	{
		// Token: 0x060013DF RID: 5087 RVA: 0x00051647 File Offset: 0x0004F847
		internal RowConstructorExpr(NodeList<AliasedExpr> exprList)
		{
			this._exprList = exprList;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00051656 File Offset: 0x0004F856
		internal NodeList<AliasedExpr> AliasedExprList
		{
			get
			{
				return this._exprList;
			}
		}

		// Token: 0x0400063A RID: 1594
		private readonly NodeList<AliasedExpr> _exprList;
	}
}
