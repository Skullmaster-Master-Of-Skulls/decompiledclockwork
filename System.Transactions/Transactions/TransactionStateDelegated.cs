using System;

namespace System.Transactions
{
	// Token: 0x02000031 RID: 49
	internal class TransactionStateDelegated : TransactionStateDelegatedBase
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0002F8B4 File Offset: 0x0002ECB4
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			tx.asyncCommit = asyncCommit;
			tx.asyncCallback = asyncCallback;
			tx.asyncState = asyncState;
			TransactionState._TransactionStateDelegatedCommitting.EnterState(tx);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0002F8E4 File Offset: 0x0002ECE4
		internal override bool PromoteDurable(InternalTransaction tx)
		{
			tx.durableEnlistment.State.ChangeStateDelegated(tx.durableEnlistment);
			return true;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0002F914 File Offset: 0x0002ED14
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
			TransactionState._TransactionStateDelegatedP0Wave.EnterState(tx);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0002F934 File Offset: 0x0002ED34
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStateDelegatedAborting.EnterState(tx);
		}
	}
}
