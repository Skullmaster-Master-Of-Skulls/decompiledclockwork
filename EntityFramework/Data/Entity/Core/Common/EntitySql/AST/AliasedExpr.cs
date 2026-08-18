using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000211 RID: 529
	internal sealed class AliasedExpr : Node
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x000502B0 File Offset: 0x0004E4B0
		internal AliasedExpr(Node expr, Identifier alias)
		{
			if (string.IsNullOrEmpty(alias.Name))
			{
				ErrorContext errCtx = alias.ErrCtx;
				string invalidEmptyIdentifier = Strings.InvalidEmptyIdentifier;
				throw EntitySqlException.Create(errCtx, invalidEmptyIdentifier, null);
			}
			this._expr = expr;
			this._alias = alias;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000502F4 File Offset: 0x0004E4F4
		internal AliasedExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00050303 File Offset: 0x0004E503
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0005030B File Offset: 0x0004E50B
		internal Identifier Alias
		{
			get
			{
				return this._alias;
			}
		}

		// Token: 0x0400059E RID: 1438
		private readonly Node _expr;

		// Token: 0x0400059F RID: 1439
		private readonly Identifier _alias;
	}
}
