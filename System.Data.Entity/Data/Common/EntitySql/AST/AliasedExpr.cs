using System;
using System.Data.Entity;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200035F RID: 863
	internal sealed class AliasedExpr : Node
	{
		// Token: 0x060031EA RID: 12778 RVA: 0x000C495A File Offset: 0x000C2B5A
		internal AliasedExpr(Node expr, Identifier alias)
		{
			if (string.IsNullOrEmpty(alias.Name))
			{
				throw EntityUtil.EntitySqlError(alias.ErrCtx, Strings.InvalidEmptyIdentifier);
			}
			this._expr = expr;
			this._alias = alias;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000C498E File Offset: 0x000C2B8E
		internal AliasedExpr(Node expr)
		{
			this._expr = expr;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000C499D File Offset: 0x000C2B9D
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000C49A5 File Offset: 0x000C2BA5
		internal Identifier Alias
		{
			get
			{
				return this._alias;
			}
		}

		// Token: 0x040015BC RID: 5564
		private readonly Node _expr;

		// Token: 0x040015BD RID: 5565
		private readonly Identifier _alias;
	}
}
