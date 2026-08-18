using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000052 RID: 82
	internal class VolatileEnlistmentCommitting : VolatileEnlistmentState
	{
		// Token: 0x06000265 RID: 613 RVA: 0x000326E4 File Offset: 0x00031AE4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
			Monitor.Exit(enlistment.Transaction);
			try
			{
				if (DiagnosticTrace.Verbose)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.EnlistmentTraceId, NotificationCall.Commit);
				}
				enlistment.EnlistmentNotification.Commit(enlistment.Enlistment);
			}
			finally
			{
				Monitor.Enter(enlistment.Transaction);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00032764 File Offset: 0x00031B64
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			VolatileEnlistmentState._VolatileEnlistmentEnded.EnterState(enlistment);
		}
	}
}
