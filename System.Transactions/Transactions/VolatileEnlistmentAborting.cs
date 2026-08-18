using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000051 RID: 81
	internal class VolatileEnlistmentAborting : VolatileEnlistmentState
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00032604 File Offset: 0x00031A04
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
				enlistment.EnlistmentNotification.Rollback(enlistment.SinglePhaseEnlistment);
			}
			finally
			{
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00032684 File Offset: 0x00031A84
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00032694 File Offset: 0x00031A94
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000326B4 File Offset: 0x00031AB4
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
		}
	}
}
