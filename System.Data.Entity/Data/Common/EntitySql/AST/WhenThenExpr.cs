using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000367 RID: 871
	internal class WhenThenExpr : Node
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x000C4B6C File Offset: 0x000C2D6C
		internal WhenThenExpr(Node whenExpr, Node thenExpr)
		{
			this._whenExpr = whenExpr;
			this._thenExpr = thenExpr;
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000C4B82 File Offset: 0x000C2D82
		internal Node WhenExpr
		{
			get
			{
				return this._whenExpr;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000C4B8A File Offset: 0x000C2D8A
		internal Node ThenExpr
		{
			get
			{
				return this._thenExpr;
			}
		}

		// Token: 0x040015F2 RID: 5618
		private readonly Node _whenExpr;

		// Token: 0x040015F3 RID: 5619
		private readonly Node _thenExpr;
	}
}
