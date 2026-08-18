using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036D RID: 877
	internal class KeyExpr : Node
	{
		// Token: 0x06003215 RID: 12821 RVA: 0x000C4C2E File Offset: 0x000C2E2E
		internal KeyExpr(Node argExpr)
		{
			this._argExpr = argExpr;
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000C4C3D File Offset: 0x000C2E3D
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x040015FB RID: 5627
		private readonly Node _argExpr;
	}
}
