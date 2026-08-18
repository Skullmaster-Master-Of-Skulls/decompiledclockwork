using System;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000026 RID: 38
	internal class TransactionStatePromotedCommitting : TransactionStatePromotedBase
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0002E874 File Offset: 0x0002DC74
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			OletxCommittableTransaction oletxCommittableTransaction = (OletxCommittableTransaction)tx.PromotedTransaction;
			oletxCommittableTransaction.BeginCommit(tx);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0002E8A4 File Offset: 0x0002DCA4
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0002E8D4 File Offset: 0x0002DCD4
		internal override void ChangeStatePromotedPhase0(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedPhase0.EnterState(tx);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0002E8F4 File Offset: 0x0002DCF4
		internal override void ChangeStatePromotedPhase1(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedPhase1.EnterState(tx);
		}
	}
}
