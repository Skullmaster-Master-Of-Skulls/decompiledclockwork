using System;
using System.Collections;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000030 RID: 48
	internal abstract class TransactionStateDelegatedBase : TransactionStatePromoted
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0002F704 File Offset: 0x0002EB04
		internal override void EnterState(InternalTransaction tx)
		{
			if (tx.outcomeSource.isoLevel == IsolationLevel.Snapshot)
			{
				throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("CannotPromoteSnapshot"), null);
			}
			base.CommonEnterState(tx);
			OletxTransaction oletxTransaction = null;
			try
			{
				if (DiagnosticTrace.Verbose && tx.durableEnlistment != null)
				{
					EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.durableEnlistment.EnlistmentTraceId, NotificationCall.Promote);
				}
				oletxTransaction = TransactionState._TransactionStatePSPEOperation.PSPEPromote(tx);
			}
			catch (TransactionPromotionException ex)
			{
				tx.innerException = ex;
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex);
				}
			}
			finally
			{
				if (oletxTransaction == null)
				{
					tx.State.ChangeStateAbortedDuringPromotion(tx);
				}
			}
			if (oletxTransaction == null)
			{
				return;
			}
			tx.PromotedTransaction = oletxTransaction;
			Hashtable promotedTransactionTable = TransactionManager.PromotedTransactionTable;
			lock (promotedTransactionTable)
			{
				tx.finalizedObject = new FinalizedObject(tx, tx.PromotedTransaction.Identifier);
				WeakReference value = new WeakReference(tx.outcomeSource, false);
				promotedTransactionTable[tx.PromotedTransaction.Identifier] = value;
			}
			TransactionManager.FireDistributedTransactionStarted(tx.outcomeSource);
			if (DiagnosticTrace.Information)
			{
				TransactionPromotedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId, oletxTransaction.TransactionTraceId);
			}
			this.PromoteEnlistmentsAndOutcome(tx);
		}
	}
}
