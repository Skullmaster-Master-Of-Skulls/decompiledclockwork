using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200002D RID: 45
	internal class TransactionStatePromotedAborted : TransactionStatePromotedEnded
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0002F164 File Offset: 0x0002E564
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			if (tx.phase1Volatiles.VolatileDemux != null)
			{
				tx.phase1Volatiles.VolatileDemux.BroadcastRollback(ref tx.phase1Volatiles);
			}
			if (tx.phase0Volatiles.VolatileDemux != null)
			{
				tx.phase0Volatiles.VolatileDemux.BroadcastRollback(ref tx.phase0Volatiles);
			}
			tx.FireCompletion();
			if (DiagnosticTrace.Warning)
			{
				TransactionAbortedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0002F1E4 File Offset: 0x0002E5E4
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Aborted;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0002F1F4 File Offset: 0x0002E5F4
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0002F204 File Offset: 0x0002E604
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionAbortedException.Create(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0002F234 File Offset: 0x0002E634
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			throw TransactionAbortedException.Create(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0002F264 File Offset: 0x0002E664
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			throw TransactionAbortedException.Create(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0002F294 File Offset: 0x0002E694
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0002F2A4 File Offset: 0x0002E6A4
		internal override void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0002F2B4 File Offset: 0x0002E6B4
		internal override void Phase1VolatilePrepareDone(InternalTransaction tx)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0002F2C4 File Offset: 0x0002E6C4
		internal override void ChangeStatePromotedPhase0(InternalTransaction tx)
		{
			throw new TransactionAbortedException(tx.innerException);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0002F2E4 File Offset: 0x0002E6E4
		internal override void ChangeStatePromotedPhase1(InternalTransaction tx)
		{
			throw new TransactionAbortedException(tx.innerException);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0002F304 File Offset: 0x0002E704
		internal override void ChangeStatePromotedAborted(InternalTransaction tx)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0002F314 File Offset: 0x0002E714
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0002F324 File Offset: 0x0002E724
		protected override void PromotedTransactionOutcome(InternalTransaction tx)
		{
			if (tx.innerException == null && tx.PromotedTransaction != null)
			{
				tx.innerException = tx.PromotedTransaction.InnerException;
			}
			throw TransactionAbortedException.Create(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0002F374 File Offset: 0x0002E774
		internal override void CheckForFinishedTransaction(InternalTransaction tx)
		{
			throw new TransactionAbortedException(tx.innerException);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0002F394 File Offset: 0x0002E794
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			throw TransactionAbortedException.Create(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0002F3C4 File Offset: 0x0002E7C4
		internal override void InDoubtFromDtc(InternalTransaction tx)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0002F3D4 File Offset: 0x0002E7D4
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
		}
	}
}
