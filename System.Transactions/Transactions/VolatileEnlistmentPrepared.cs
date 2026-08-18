using System;

namespace System.Transactions
{
	// Token: 0x0200004F RID: 79
	internal class VolatileEnlistmentPrepared : VolatileEnlistmentState
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00032484 File Offset: 0x00031884
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000324A4 File Offset: 0x000318A4
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentAborting.EnterState(enlistment);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000324C4 File Offset: 0x000318C4
		internal override void InternalCommitted(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentCommitting.EnterState(enlistment);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000324E4 File Offset: 0x000318E4
		internal override void InternalIndoubt(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentInDoubt.EnterState(enlistment);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00032504 File Offset: 0x00031904
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
		}
	}
}
