using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000212 RID: 530
	internal sealed class ApplyClauseItem : Node
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x00050313 File Offset: 0x0004E513
		internal ApplyClauseItem(FromClauseItem applyLeft, FromClauseItem applyRight, ApplyKind applyKind)
		{
			this._applyLeft = applyLeft;
			this._applyRight = applyRight;
			this._applyKind = applyKind;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00050330 File Offset: 0x0004E530
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._applyLeft;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x00050338 File Offset: 0x0004E538
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._applyRight;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00050340 File Offset: 0x0004E540
		internal ApplyKind ApplyKind
		{
			get
			{
				return this._applyKind;
			}
		}

		// Token: 0x040005A0 RID: 1440
		private readonly FromClauseItem _applyLeft;

		// Token: 0x040005A1 RID: 1441
		private readonly FromClauseItem _applyRight;

		// Token: 0x040005A2 RID: 1442
		private readonly ApplyKind _applyKind;
	}
}
