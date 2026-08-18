using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000382 RID: 898
	internal sealed class JoinClauseItem : Node
	{
		// Token: 0x06003258 RID: 12888 RVA: 0x000C5150 File Offset: 0x000C3350
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind) : this(joinLeft, joinRight, joinKind, null)
		{
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000C515C File Offset: 0x000C335C
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind, Node onExpr)
		{
			this._joinLeft = joinLeft;
			this._joinRight = joinRight;
			this._joinKind = joinKind;
			this._onExpr = onExpr;
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000C5181 File Offset: 0x000C3381
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._joinLeft;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000C5189 File Offset: 0x000C3389
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._joinRight;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000C5191 File Offset: 0x000C3391
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x000C5199 File Offset: 0x000C3399
		internal JoinKind JoinKind
		{
			get
			{
				return this._joinKind;
			}
			set
			{
				this._joinKind = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000C51A2 File Offset: 0x000C33A2
		internal Node OnExpr
		{
			get
			{
				return this._onExpr;
			}
		}

		// Token: 0x04001640 RID: 5696
		private readonly FromClauseItem _joinLeft;

		// Token: 0x04001641 RID: 5697
		private readonly FromClauseItem _joinRight;

		// Token: 0x04001642 RID: 5698
		private JoinKind _joinKind;

		// Token: 0x04001643 RID: 5699
		private readonly Node _onExpr;
	}
}
