using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000047 RID: 71
	internal class DurableEnlistmentAborting : DurableEnlistmentState
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000317C4 File Offset: 0x00030BC4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
			Monitor.Exit(enlistment.Transaction);
			try
			{
				if (DiagnosticTrace.Verbose)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.EnlistmentTraceId, NotificationCall.Rollback);
				}
				if (enlistment.SinglePhaseNotification != null)
				{
					enlistment.SinglePhaseNotification.Rollback(enlistment.SinglePhaseEnlistment);
				}
				else
				{
					enlistment.PromotableSinglePhaseNotification.Rollback(enlistment.SinglePhaseEnlistment);
				}
			}
			finally
			{
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00031864 File Offset: 0x00030C64
		internal override void Aborted(InternalEnlistment enlistment, Exception e)
		{
			if (enlistment.Transaction.innerException == null)
			{
				enlistment.Transaction.innerException = e;
			}
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000318A4 File Offset: 0x00030CA4
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
		}
	}
}
