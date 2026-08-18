using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000035 RID: 53
	internal class TransactionStateDelegatedCommitting : TransactionStatePromotedCommitting
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0002FC74 File Offset: 0x0002F074
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			Monitor.Exit(tx);
			if (DiagnosticTrace.Verbose)
			{
				EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.durableEnlistment.EnlistmentTraceId, NotificationCall.SinglePhaseCommit);
			}
			try
			{
				tx.durableEnlistment.PromotableSinglePhaseNotification.SinglePhaseCommit(tx.durableEnlistment.SinglePhaseEnlistment);
			}
			finally
			{
				Monitor.Enter(tx);
			}
		}
	}
}
