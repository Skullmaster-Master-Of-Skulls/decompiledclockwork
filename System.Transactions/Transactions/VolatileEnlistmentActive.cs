using System;

namespace System.Transactions
{
	// Token: 0x0200004C RID: 76
	internal class VolatileEnlistmentActive : VolatileEnlistmentState
	{
		// Token: 0x06000241 RID: 577 RVA: 0x000320E4 File Offset: 0x000314E4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00032104 File Offset: 0x00031504
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentDone.EnterState(enlistment);
			enlistment.FinishEnlistment();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00032124 File Offset: 0x00031524
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentPreparing.EnterState(enlistment);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00032144 File Offset: 0x00031544
		internal override void ChangeStateSinglePhaseCommit(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentSPC.EnterState(enlistment);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00032164 File Offset: 0x00031564
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentAborting.EnterState(enlistment);
		}
	}
}
