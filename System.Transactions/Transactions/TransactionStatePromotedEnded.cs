using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x0200002C RID: 44
	internal abstract class TransactionStatePromotedEnded : TransactionStateEnded
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0002EF14 File Offset: 0x0002E314
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			base.CommonEnterState(tx);
			if (!ThreadPool.QueueUserWorkItem(TransactionStatePromotedEnded.SignalMethod, tx))
			{
				throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("UnexpectedFailureOfThreadPool"), null);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0002EF64 File Offset: 0x0002E364
		internal override void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			if (transactionCompletedDelegate != null)
			{
				TransactionEventArgs transactionEventArgs = new TransactionEventArgs();
				transactionEventArgs.transaction = tx.outcomeSource.InternalClone();
				transactionCompletedDelegate(transactionEventArgs.transaction, transactionEventArgs);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0002EFA4 File Offset: 0x0002E3A4
		internal override void EndCommit(InternalTransaction tx)
		{
			this.PromotedTransactionOutcome(tx);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0002EFC4 File Offset: 0x0002E3C4
		internal override void CompleteBlockingClone(InternalTransaction tx)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0002EFD4 File Offset: 0x0002E3D4
		internal override void CompleteAbortingClone(InternalTransaction tx)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0002EFE4 File Offset: 0x0002E3E4
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0002F014 File Offset: 0x0002E414
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0002F044 File Offset: 0x0002E444
		internal override Guid get_Identifier(InternalTransaction tx)
		{
			return tx.PromotedTransaction.Identifier;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0002F064 File Offset: 0x0002E464
		internal override void Promote(InternalTransaction tx)
		{
		}

		// Token: 0x06000162 RID: 354
		protected abstract void PromotedTransactionOutcome(InternalTransaction tx);

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0002F074 File Offset: 0x0002E474
		private static WaitCallback SignalMethod
		{
			get
			{
				if (TransactionStatePromotedEnded.signalMethod == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionStatePromotedEnded.signalMethod == null)
						{
							TransactionStatePromotedEnded.signalMethod = new WaitCallback(TransactionStatePromotedEnded.SignalCallback);
						}
					}
				}
				return TransactionStatePromotedEnded.signalMethod;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0002F0E4 File Offset: 0x0002E4E4
		private static void SignalCallback(object state)
		{
			InternalTransaction internalTransaction = (InternalTransaction)state;
			lock (internalTransaction)
			{
				internalTransaction.SignalAsyncCompletion();
				TransactionManager.TransactionTable.Remove(internalTransaction);
			}
		}

		// Token: 0x040000E2 RID: 226
		private static WaitCallback signalMethod;
	}
}
