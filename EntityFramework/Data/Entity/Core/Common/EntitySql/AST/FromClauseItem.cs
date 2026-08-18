using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000221 RID: 545
	internal sealed class FromClauseItem : Node
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x00050688 File Offset: 0x0004E888
		internal FromClauseItem(AliasedExpr aliasExpr)
		{
			this._fromClauseItemExpr = aliasExpr;
			this._fromClauseItemKind = FromClauseItemKind.AliasedFromClause;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0005069E File Offset: 0x0004E89E
		internal FromClauseItem(JoinClauseItem joinClauseItem)
		{
			this._fromClauseItemExpr = joinClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.JoinFromClause;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000506B4 File Offset: 0x0004E8B4
		internal FromClauseItem(ApplyClauseItem applyClauseItem)
		{
			this._fromClauseItemExpr = applyClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.ApplyFromClause;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x000506CA File Offset: 0x0004E8CA
		internal Node FromExpr
		{
			get
			{
				return this._fromClauseItemExpr;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x000506D2 File Offset: 0x0004E8D2
		internal FromClauseItemKind FromClauseItemKind
		{
			get
			{
				return this._fromClauseItemKind;
			}
		}

		// Token: 0x040005E8 RID: 1512
		private readonly Node _fromClauseItemExpr;

		// Token: 0x040005E9 RID: 1513
		private readonly FromClauseItemKind _fromClauseItemKind;
	}
}
