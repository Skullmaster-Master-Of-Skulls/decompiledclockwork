using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000233 RID: 563
	internal sealed class ParenExpr : Node
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x000514CC File Offset: 0x0004F6CC
		internal ParenExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x000514DB File Offset: 0x0004F6DB
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x0400062E RID: 1582
		private readonly Node _expr;
	}
}
