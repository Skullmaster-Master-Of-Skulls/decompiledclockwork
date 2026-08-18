using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000036 RID: 54
	internal class TransactionStateDelegatedAborting : TransactionStatePromotedAborted
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0002FD14 File Offset: 0x0002F114
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			Monitor.Exit(tx);
			try
			{
				if (DiagnosticTrace.Verbose)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.durableEnlistment.EnlistmentTraceId, NotificationCall.Rollback);
				}
				tx.durableEnlistment.PromotableSinglePhaseNotification.Rollback(tx.durableEnlistment.SinglePhaseEnlistment);
			}
			finally
			{
				Monitor.Enter(tx);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0002FD94 File Offset: 0x0002F194
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0002FDC4 File Offset: 0x0002F1C4
		internal override void ChangeStatePromotedAborted(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedAborted.EnterState(tx);
		}
	}
}
