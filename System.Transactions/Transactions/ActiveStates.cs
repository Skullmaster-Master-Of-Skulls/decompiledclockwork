using System;

namespace System.Transactions
{
	// Token: 0x02000016 RID: 22
	internal abstract class ActiveStates : TransactionState
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0002C7B4 File Offset: 0x0002BBB4
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Active;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0002C7C4 File Offset: 0x0002BBC4
		internal override void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			tx.transactionCompletedDelegate = (TransactionCompletedEventHandler)Delegate.Combine(tx.transactionCompletedDelegate, transactionCompletedDelegate);
		}
	}
}
