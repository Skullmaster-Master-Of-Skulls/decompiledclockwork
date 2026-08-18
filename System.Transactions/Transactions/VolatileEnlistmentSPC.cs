using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200004E RID: 78
	internal class VolatileEnlistmentSPC : VolatileEnlistmentState
	{
		// Token: 0x0600024E RID: 590 RVA: 0x000322F4 File Offset: 0x000316F4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			bool flag = false;
			enlistment.State = this;
			if (DiagnosticTrace.Verbose)
			{
				EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.EnlistmentTraceId, NotificationCall.SinglePhaseCommit);
			}
			Monitor.Exit(enlistment.Transaction);
			try
			{
				enlistment.SinglePhaseNotification.SinglePhaseCommit(enlistment.SinglePhaseEnlistment);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					enlistment.SinglePhaseEnlistment.InDoubt();
				}
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00032384 File Offset: 0x00031784
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			enlistment.Transaction.State.ChangeStateTransactionCommitted(enlistment.Transaction);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000323B4 File Offset: 0x000317B4
		internal override void Committed(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			enlistment.Transaction.State.ChangeStateTransactionCommitted(enlistment.Transaction);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000323E4 File Offset: 0x000317E4
		internal override void Aborted(InternalEnlistment enlistment, Exception e)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			enlistment.Transaction.State.ChangeStateTransactionAborted(enlistment.Transaction, e);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00032414 File Offset: 0x00031814
		internal override void InDoubt(InternalEnlistment enlistment, Exception e)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			if (enlistment.Transaction.innerException == null)
			{
				enlistment.Transaction.innerException = e;
			}
			enlistment.Transaction.State.InDoubtFromEnlistment(enlistment.Transaction);
		}
	}
}
