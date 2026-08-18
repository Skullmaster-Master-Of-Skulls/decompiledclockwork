using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200023F RID: 575
	internal class WhenThenExpr : Node
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x000516F3 File Offset: 0x0004F8F3
		internal WhenThenExpr(Node whenExpr, Node thenExpr)
		{
			this._whenExpr = whenExpr;
			this._thenExpr = thenExpr;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00051709 File Offset: 0x0004F909
		internal Node WhenExpr
		{
			get
			{
				return this._whenExpr;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00051711 File Offset: 0x0004F911
		internal Node ThenExpr
		{
			get
			{
				return this._thenExpr;
			}
		}

		// Token: 0x04000646 RID: 1606
		private readonly Node _whenExpr;

		// Token: 0x04000647 RID: 1607
		private readonly Node _thenExpr;
	}
}
