using System;

namespace System.Transactions
{
	// Token: 0x02000050 RID: 80
	internal class VolatileEnlistmentPreparingAborting : VolatileEnlistmentState
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00032534 File Offset: 0x00031934
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00032554 File Offset: 0x00031954
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00032574 File Offset: 0x00031974
		internal override void Prepared(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentAborting.EnterState(enlistment);
			enlistment.FinishEnlistment();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00032594 File Offset: 0x00031994
		internal override void ForceRollback(InternalEnlistment enlistment, Exception e)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			if (enlistment.Transaction.innerException == null)
			{
				enlistment.Transaction.innerException = e;
			}
			enlistment.FinishEnlistment();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000325D4 File Offset: 0x000319D4
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
		}
	}
}
