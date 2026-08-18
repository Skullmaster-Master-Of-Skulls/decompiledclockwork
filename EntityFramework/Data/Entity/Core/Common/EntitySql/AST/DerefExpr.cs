using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200021A RID: 538
	internal sealed class DerefExpr : Node
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x000504E0 File Offset: 0x0004E6E0
		internal DerefExpr(Node derefArgExpr)
		{
			this._argExpr = derefArgExpr;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000504EF File Offset: 0x0004E6EF
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x040005D9 RID: 1497
		private readonly Node _argExpr;
	}
}
