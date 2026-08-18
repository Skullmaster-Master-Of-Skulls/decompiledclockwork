using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000217 RID: 535
	internal sealed class CaseExpr : Node
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x00050473 File Offset: 0x0004E673
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr) : this(whenThenExpr, null)
		{
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0005047D File Offset: 0x0004E67D
		internal CaseExpr(NodeList<WhenThenExpr> whenThenExpr, Node elseExpr)
		{
			this._whenThenExpr = whenThenExpr;
			this._elseExpr = elseExpr;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x00050493 File Offset: 0x0004E693
		internal NodeList<WhenThenExpr> WhenThenExprList
		{
			get
			{
				return this._whenThenExpr;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0005049B File Offset: 0x0004E69B
		internal Node ElseExpr
		{
			get
			{
				return this._elseExpr;
			}
		}

		// Token: 0x040005D4 RID: 1492
		private readonly NodeList<WhenThenExpr> _whenThenExpr;

		// Token: 0x040005D5 RID: 1493
		private readonly Node _elseExpr;
	}
}
