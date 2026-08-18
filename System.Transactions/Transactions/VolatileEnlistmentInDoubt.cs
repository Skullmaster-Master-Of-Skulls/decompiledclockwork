using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000053 RID: 83
	internal class VolatileEnlistmentInDoubt : VolatileEnlistmentState
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000327A4 File Offset: 0x00031BA4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
			Monitor.Exit(enlistment.Transaction);
			try
			{
				if (DiagnosticTrace.Verbose)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.EnlistmentTraceId, NotificationCall.InDoubt);
				}
				enlistment.EnlistmentNotification.InDoubt(enlistment.PreparingEnlistment);
			}
			finally
			{
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00032824 File Offset: 0x00031C24
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
		}
	}
}
