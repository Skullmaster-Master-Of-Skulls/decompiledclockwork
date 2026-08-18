using System;

namespace System.Transactions
{
	// Token: 0x0200001C RID: 28
	internal class TransactionStateVolatileSPC : ActiveStates
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0002D3D4 File Offset: 0x0002C7D4
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			tx.phase1Volatiles.volatileEnlistments[0].twoPhaseState.ChangeStateSinglePhaseCommit(tx.phase1Volatiles.volatileEnlistments[0]);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0002D414 File Offset: 0x0002C814
		internal override void ChangeStateTransactionCommitted(InternalTransaction tx)
		{
			TransactionState._TransactionStateCommitted.EnterState(tx);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0002D434 File Offset: 0x0002C834
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
			TransactionState._TransactionStateInDoubt.EnterState(tx);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0002D454 File Offset: 0x0002C854
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
