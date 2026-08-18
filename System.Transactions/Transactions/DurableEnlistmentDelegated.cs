using System;

namespace System.Transactions
{
	// Token: 0x02000049 RID: 73
	internal class DurableEnlistmentDelegated : DurableEnlistmentState
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00031A94 File Offset: 0x00030E94
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00031AB4 File Offset: 0x00030EB4
		internal override void Committed(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
			enlistment.Transaction.State.ChangeStatePromotedCommitted(enlistment.Transaction);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00031AE4 File Offset: 0x00030EE4
		internal override void Aborted(InternalEnlistment enlistment, Exception e)
		{
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
			if (enlistment.Transaction.innerException == null)
			{
				enlistment.Transaction.innerException = e;
			}
			enlistment.Transaction.State.ChangeStatePromotedAborted(enlistment.Transaction);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00031B34 File Offset: 0x00030F34
		internal override void InDoubt(InternalEnlistment enlistment, Exception e)
		{
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
			if (enlistment.Transaction.innerException == null)
			{
				enlistment.Transaction.innerException = e;
			}
			enlistment.Transaction.State.InDoubtFromEnlistment(enlistment.Transaction);
		}
	}
}
