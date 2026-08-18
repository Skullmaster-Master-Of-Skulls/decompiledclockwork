using System;

namespace System.Transactions
{
	// Token: 0x02000054 RID: 84
	internal class VolatileEnlistmentEnded : VolatileEnlistmentState
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00032864 File Offset: 0x00031C64
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00032884 File Offset: 0x00031C84
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00032894 File Offset: 0x00031C94
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000328A4 File Offset: 0x00031CA4
		internal override void InternalCommitted(InternalEnlistment enlistment)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000328B4 File Offset: 0x00031CB4
		internal override void InternalIndoubt(InternalEnlistment enlistment)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000328C4 File Offset: 0x00031CC4
		internal override void InDoubt(InternalEnlistment enlistment, Exception e)
		{
		}
	}
}
