using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000383 RID: 899
	internal sealed class ApplyClauseItem : Node
	{
		// Token: 0x0600325F RID: 12895 RVA: 0x000C51AA File Offset: 0x000C33AA
		internal ApplyClauseItem(FromClauseItem applyLeft, FromClauseItem applyRight, ApplyKind applyKind)
		{
			this._applyLeft = applyLeft;
			this._applyRight = applyRight;
			this._applyKind = applyKind;
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000C51C7 File Offset: 0x000C33C7
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._applyLeft;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000C51CF File Offset: 0x000C33CF
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._applyRight;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000C51D7 File Offset: 0x000C33D7
		internal ApplyKind ApplyKind
		{
			get
			{
				return this._applyKind;
			}
		}

		// Token: 0x04001644 RID: 5700
		private readonly FromClauseItem _applyLeft;

		// Token: 0x04001645 RID: 5701
		private readonly FromClauseItem _applyRight;

		// Token: 0x04001646 RID: 5702
		private readonly ApplyKind _applyKind;
	}
}
