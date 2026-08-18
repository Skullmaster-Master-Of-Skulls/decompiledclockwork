using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200002E RID: 46
	internal class TransactionStatePromotedCommitted : TransactionStatePromotedEnded
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0002F404 File Offset: 0x0002E804
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			if (tx.phase1Volatiles.VolatileDemux != null)
			{
				tx.phase1Volatiles.VolatileDemux.BroadcastCommitted(ref tx.phase1Volatiles);
			}
			if (tx.phase0Volatiles.VolatileDemux != null)
			{
				tx.phase0Volatiles.VolatileDemux.BroadcastCommitted(ref tx.phase0Volatiles);
			}
			tx.FireCompletion();
			if (DiagnosticTrace.Verbose)
			{
				TransactionCommittedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0002F484 File Offset: 0x0002E884
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Committed;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0002F494 File Offset: 0x0002E894
		internal override void ChangeStatePromotedCommitted(InternalTransaction tx)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0002F4A4 File Offset: 0x0002E8A4
		protected override void PromotedTransactionOutcome(InternalTransaction tx)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0002F4B4 File Offset: 0x0002E8B4
		internal override void InDoubtFromDtc(InternalTransaction tx)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0002F4C4 File Offset: 0x0002E8C4
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
		}
	}
}
