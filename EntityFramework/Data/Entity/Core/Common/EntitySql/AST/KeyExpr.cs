using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200021B RID: 539
	internal class KeyExpr : Node
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x000504F7 File Offset: 0x0004E6F7
		internal KeyExpr(Node argExpr)
		{
			this._argExpr = argExpr;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00050506 File Offset: 0x0004E706
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x040005DA RID: 1498
		private readonly Node _argExpr;
	}
}
