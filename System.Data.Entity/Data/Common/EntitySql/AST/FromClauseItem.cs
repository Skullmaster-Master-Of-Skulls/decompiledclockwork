using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200037D RID: 893
	internal sealed class FromClauseItem : Node
	{
		// Token: 0x06003244 RID: 12868 RVA: 0x000C5036 File Offset: 0x000C3236
		internal FromClauseItem(AliasedExpr aliasExpr)
		{
			this._fromClauseItemExpr = aliasExpr;
			this._fromClauseItemKind = FromClauseItemKind.AliasedFromClause;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000C504C File Offset: 0x000C324C
		internal FromClauseItem(JoinClauseItem joinClauseItem)
		{
			this._fromClauseItemExpr = joinClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.JoinFromClause;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000C5062 File Offset: 0x000C3262
		internal FromClauseItem(ApplyClauseItem applyClauseItem)
		{
			this._fromClauseItemExpr = applyClauseItem;
			this._fromClauseItemKind = FromClauseItemKind.ApplyFromClause;
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000C5078 File Offset: 0x000C3278
		internal Node FromExpr
		{
			get
			{
				return this._fromClauseItemExpr;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000C5080 File Offset: 0x000C3280
		internal FromClauseItemKind FromClauseItemKind
		{
			get
			{
				return this._fromClauseItemKind;
			}
		}

		// Token: 0x04001634 RID: 5684
		private readonly Node _fromClauseItemExpr;

		// Token: 0x04001635 RID: 5685
		private readonly FromClauseItemKind _fromClauseItemKind;
	}
}
