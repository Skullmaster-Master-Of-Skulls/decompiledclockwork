using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000385 RID: 901
	internal sealed class RefExpr : Node
	{
		// Token: 0x06003266 RID: 12902 RVA: 0x000C5205 File Offset: 0x000C3405
		internal RefExpr(Node refArgExpr)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000C5214 File Offset: 0x000C3414
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04001649 RID: 5705
		private readonly Node _argExpr;
	}
}
