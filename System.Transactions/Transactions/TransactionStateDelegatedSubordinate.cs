using System;

namespace System.Transactions
{
	// Token: 0x02000032 RID: 50
	internal class TransactionStateDelegatedSubordinate : TransactionStateDelegatedBase
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0002F984 File Offset: 0x0002ED84
		internal override bool PromoteDurable(InternalTransaction tx)
		{
			return true;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0002F994 File Offset: 0x0002ED94
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			tx.PromotedTransaction.Rollback();
			TransactionState._TransactionStatePromotedAborted.EnterState(tx);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0002F9D4 File Offset: 0x0002EDD4
		internal override void ChangeStatePromotedPhase0(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedPhase0.EnterState(tx);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0002F9F4 File Offset: 0x0002EDF4
		internal override void ChangeStatePromotedPhase1(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedPhase1.EnterState(tx);
		}
	}
}
