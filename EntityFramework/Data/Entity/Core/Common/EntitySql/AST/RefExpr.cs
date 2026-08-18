using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000238 RID: 568
	internal sealed class RefExpr : Node
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x00051619 File Offset: 0x0004F819
		internal RefExpr(Node refArgExpr)
		{
			this._argExpr = refArgExpr;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00051628 File Offset: 0x0004F828
		internal Node ArgExpr
		{
			get
			{
				return this._argExpr;
			}
		}

		// Token: 0x04000638 RID: 1592
		private readonly Node _argExpr;
	}
}
