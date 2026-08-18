using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x0200001E RID: 30
	internal abstract class TransactionStateEnded : TransactionState
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0002D574 File Offset: 0x0002C974
		internal override void EnterState(InternalTransaction tx)
		{
			if (tx.needPulse)
			{
				Monitor.Pulse(tx);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0002D594 File Offset: 0x0002C994
		internal override void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			if (transactionCompletedDelegate != null)
			{
				TransactionEventArgs transactionEventArgs = new TransactionEventArgs();
				transactionEventArgs.transaction = tx.outcomeSource.InternalClone();
				transactionCompletedDelegate(transactionEventArgs.transaction, transactionEventArgs);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0002D5D4 File Offset: 0x0002C9D4
		internal override bool IsCompleted(InternalTransaction tx)
		{
			return true;
		}
	}
}
