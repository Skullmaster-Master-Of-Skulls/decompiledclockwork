using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200002F RID: 47
	internal class TransactionStatePromotedIndoubt : TransactionStatePromotedEnded
	{
		// Token: 0x06000180 RID: 384 RVA: 0x0002F4F4 File Offset: 0x0002E8F4
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			if (tx.phase1Volatiles.VolatileDemux != null)
			{
				tx.phase1Volatiles.VolatileDemux.BroadcastInDoubt(ref tx.phase1Volatiles);
			}
			if (tx.phase0Volatiles.VolatileDemux != null)
			{
				tx.phase0Volatiles.VolatileDemux.BroadcastInDoubt(ref tx.phase0Volatiles);
			}
			tx.FireCompletion();
			if (DiagnosticTrace.Warning)
			{
				TransactionInDoubtTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0002F574 File Offset: 0x0002E974
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.InDoubt;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0002F584 File Offset: 0x0002E984
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0002F594 File Offset: 0x0002E994
		internal override void ChangeStatePromotedPhase0(InternalTransaction tx)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0002F5C4 File Offset: 0x0002E9C4
		internal override void ChangeStatePromotedPhase1(InternalTransaction tx)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0002F5F4 File Offset: 0x0002E9F4
		internal override void InDoubtFromDtc(InternalTransaction tx)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0002F604 File Offset: 0x0002EA04
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0002F614 File Offset: 0x0002EA14
		protected override void PromotedTransactionOutcome(InternalTransaction tx)
		{
			if (tx.innerException == null && tx.PromotedTransaction != null)
			{
				tx.innerException = tx.PromotedTransaction.InnerException;
			}
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0002F664 File Offset: 0x0002EA64
		internal override void CheckForFinishedTransaction(InternalTransaction tx)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0002F694 File Offset: 0x0002EA94
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0002F6C4 File Offset: 0x0002EAC4
		internal override void ChangeStatePromotedAborted(InternalTransaction tx)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0002F6D4 File Offset: 0x0002EAD4
		internal override void ChangeStatePromotedCommitted(InternalTransaction tx)
		{
		}
	}
}
