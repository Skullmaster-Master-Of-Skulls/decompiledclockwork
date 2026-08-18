using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000229 RID: 553
	internal sealed class JoinClauseItem : Node
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x00050812 File Offset: 0x0004EA12
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind) : this(joinLeft, joinRight, joinKind, null)
		{
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0005081E File Offset: 0x0004EA1E
		internal JoinClauseItem(FromClauseItem joinLeft, FromClauseItem joinRight, JoinKind joinKind, Node onExpr)
		{
			this._joinLeft = joinLeft;
			this._joinRight = joinRight;
			this.JoinKind = joinKind;
			this._onExpr = onExpr;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00050843 File Offset: 0x0004EA43
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._joinLeft;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0005084B File Offset: 0x0004EA4B
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._joinRight;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00050853 File Offset: 0x0004EA53
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0005085B File Offset: 0x0004EA5B
		internal JoinKind JoinKind { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00050864 File Offset: 0x0004EA64
		internal Node OnExpr
		{
			get
			{
				return this._onExpr;
			}
		}

		// Token: 0x040005FB RID: 1531
		private readonly FromClauseItem _joinLeft;

		// Token: 0x040005FC RID: 1532
		private readonly FromClauseItem _joinRight;

		// Token: 0x040005FD RID: 1533
		private readonly Node _onExpr;
	}
}
