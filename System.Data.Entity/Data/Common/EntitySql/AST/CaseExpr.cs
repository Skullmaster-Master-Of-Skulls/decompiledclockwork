using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000366 RID: 870
	internal sealed class CaseExpr : Node
	{
		// Token: 0x06003201 RID: 12801 RVA: 0x000C4B3C File Offset: 0x000C2D3C
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr) : this(whenThenExpr, null)
		{
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000C4B46 File Offset: 0x000C2D46
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr, Node elseExpr)
		{
			this._whenThenExpr = whenThenExpr;
			this._elseExpr = elseExpr;
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000C4B5C File Offset: 0x000C2D5C
		internal NodeList<WhenThenExpr> WhenThenExprList
		{
			get
			{
				return this._whenThenExpr;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000C4B64 File Offset: 0x000C2D64
		internal Node ElseExpr
		{
			get
			{
				return this._elseExpr;
			}
		}

		// Token: 0x040015F0 RID: 5616
		private readonly NodeList<WhenThenExpr> _whenThenExpr;

		// Token: 0x040015F1 RID: 5617
		private readonly Node _elseExpr;
	}
}
