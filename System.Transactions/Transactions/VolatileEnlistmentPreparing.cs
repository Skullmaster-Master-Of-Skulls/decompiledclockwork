using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200004D RID: 77
	internal class VolatileEnlistmentPreparing : VolatileEnlistmentState
	{
		// Token: 0x06000247 RID: 583 RVA: 0x000321A4 File Offset: 0x000315A4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
			Monitor.Exit(enlistment.Transaction);
			try
			{
				if (DiagnosticTrace.Verbose)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.EnlistmentTraceId, NotificationCall.Prepare);
				}
				enlistment.EnlistmentNotification.Prepare(enlistment.PreparingEnlistment);
			}
			finally
			{
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00032224 File Offset: 0x00031624
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentDone.EnterState(enlistment);
			enlistment.FinishEnlistment();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00032244 File Offset: 0x00031644
		internal override void Prepared(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentPrepared.EnterState(enlistment);
			enlistment.FinishEnlistment();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00032264 File Offset: 0x00031664
		internal override void ForceRollback(InternalEnlistment enlistment, Exception e)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
			enlistment.Transaction.State.ChangeStateTransactionAborted(enlistment.Transaction, e);
			enlistment.FinishEnlistment();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000322A4 File Offset: 0x000316A4
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000322B4 File Offset: 0x000316B4
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentPreparingAborting.EnterState(enlistment);
		}
	}
}
