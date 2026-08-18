using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200035B RID: 859
	internal sealed class ParenExpr : Node
	{
		// Token: 0x060031CC RID: 12748 RVA: 0x000C3E24 File Offset: 0x000C2024
		internal ParenExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000C3E33 File Offset: 0x000C2033
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x040015A3 RID: 5539
		private readonly Node _expr;
	}
}
