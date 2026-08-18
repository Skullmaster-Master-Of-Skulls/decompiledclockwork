using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000386 RID: 902
	internal sealed class DerefExpr : Node
	{
		// Token: 0x06003268 RID: 12904 RVA: 0x000C521C File Offset: 0x000C341C
		internal DerefExpr(Node derefArgExpr)
		{
			this._argExpr = derefArgExpr;
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000C522B File Offset: 0x000C342B
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x0400164A RID: 5706
		private Node _argExpr;
	}
}
