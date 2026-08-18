using System;

namespace System.Transactions
{
	// Token: 0x02000029 RID: 41
	internal abstract class TransactionStatePromotedAborting : TransactionStatePromotedBase
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0002ECB4 File Offset: 0x0002E0B4
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0002ECD4 File Offset: 0x0002E0D4
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Aborted;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0002ECE4 File Offset: 0x0002E0E4
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0002ED14 File Offset: 0x0002E114
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0002ED44 File Offset: 0x0002E144
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0002ED74 File Offset: 0x0002E174
		internal override void ChangeStatePromotedAborted(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedAborted.EnterState(tx);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0002ED94 File Offset: 0x0002E194
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0002EDA4 File Offset: 0x0002E1A4
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
		}
	}
}
