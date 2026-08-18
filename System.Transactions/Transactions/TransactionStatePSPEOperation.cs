using System;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000033 RID: 51
	internal class TransactionStatePSPEOperation : TransactionState
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0002FA34 File Offset: 0x0002EE34
		internal override void EnterState(InternalTransaction tx)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0002FA54 File Offset: 0x0002EE54
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0002FA84 File Offset: 0x0002EE84
		internal void PSPEInitialize(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification)
		{
			base.CommonEnterState(tx);
			try
			{
				promotableSinglePhaseNotification.Initialize();
			}
			finally
			{
				TransactionState._TransactionStateActive.CommonEnterState(tx);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0002FAD4 File Offset: 0x0002EED4
		internal void Phase0PSPEInitialize(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification)
		{
			base.CommonEnterState(tx);
			try
			{
				promotableSinglePhaseNotification.Initialize();
			}
			finally
			{
				TransactionState._TransactionStatePhase0.CommonEnterState(tx);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0002FB24 File Offset: 0x0002EF24
		internal OletxTransaction PSPEPromote(InternalTransaction tx)
		{
			TransactionState state = tx.State;
			base.CommonEnterState(tx);
			OletxTransaction oletxTransaction = null;
			try
			{
				byte[] array = tx.promoter.Promote();
				if (array == null)
				{
					throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("PromotedReturnedInvalidValue"), null);
				}
				try
				{
					oletxTransaction = TransactionInterop.GetOletxTransactionFromTransmitterPropigationToken(array);
				}
				catch (ArgumentException innerException)
				{
					throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("PromotedReturnedInvalidValue"), innerException);
				}
				if (TransactionManager.FindPromotedTransaction(oletxTransaction.Identifier) != null)
				{
					oletxTransaction.Dispose();
					throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("PromotedTransactionExists"), null);
				}
			}
			finally
			{
				state.CommonEnterState(tx);
			}
			return oletxTransaction;
		}
	}
}
