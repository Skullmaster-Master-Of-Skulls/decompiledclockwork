using System;

namespace System.Transactions
{
	// Token: 0x0200001D RID: 29
	internal class TransactionStateSPC : ActiveStates
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0002D4A4 File Offset: 0x0002C8A4
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			if (tx.durableEnlistment != null)
			{
				tx.durableEnlistment.State.ChangeStateCommitting(tx.durableEnlistment);
				return;
			}
			TransactionState._TransactionStateCommitted.EnterState(tx);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0002D4E4 File Offset: 0x0002C8E4
		internal override void ChangeStateTransactionCommitted(InternalTransaction tx)
		{
			TransactionState._TransactionStateCommitted.EnterState(tx);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0002D504 File Offset: 0x0002C904
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
			TransactionState._TransactionStateInDoubt.EnterState(tx);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0002D524 File Offset: 0x0002C924
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStateAborted.EnterState(tx);
		}
	}
}
