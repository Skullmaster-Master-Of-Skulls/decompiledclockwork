using System;
using System.Collections;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000024 RID: 36
	internal class TransactionStatePromoted : TransactionStatePromotedBase
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0002E2F4 File Offset: 0x0002D6F4
		internal override void EnterState(InternalTransaction tx)
		{
			if (tx.outcomeSource.isoLevel == IsolationLevel.Snapshot)
			{
				throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("CannotPromoteSnapshot"), null);
			}
			base.CommonEnterState(tx);
			OletxCommittableTransaction oletxCommittableTransaction = null;
			try
			{
				TimeSpan timeSpan;
				if (tx.AbsoluteTimeout == 9223372036854775807L)
				{
					timeSpan = TimeSpan.Zero;
				}
				else
				{
					timeSpan = TransactionManager.TransactionTable.RecalcTimeout(tx);
					if (timeSpan <= TimeSpan.Zero)
					{
						return;
					}
				}
				TransactionOptions properties = default(TransactionOptions);
				properties.IsolationLevel = tx.outcomeSource.isoLevel;
				properties.Timeout = timeSpan;
				oletxCommittableTransaction = TransactionManager.DistributedTransactionManager.CreateTransaction(properties);
				oletxCommittableTransaction.savedLtmPromotedTransaction = tx.outcomeSource;
				if (DiagnosticTrace.Information)
				{
					TransactionPromotedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId, oletxCommittableTransaction.TransactionTraceId);
				}
			}
			catch (TransactionException ex)
			{
				tx.innerException = ex;
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex);
				}
				return;
			}
			finally
			{
				if (oletxCommittableTransaction == null)
				{
					tx.State.ChangeStateAbortedDuringPromotion(tx);
				}
			}
			tx.PromotedTransaction = oletxCommittableTransaction;
			Hashtable promotedTransactionTable = TransactionManager.PromotedTransactionTable;
			lock (promotedTransactionTable)
			{
				tx.finalizedObject = new FinalizedObject(tx, oletxCommittableTransaction.Identifier);
				WeakReference value = new WeakReference(tx.outcomeSource, false);
				promotedTransactionTable[oletxCommittableTransaction.Identifier] = value;
			}
			TransactionManager.FireDistributedTransactionStarted(tx.outcomeSource);
			this.PromoteEnlistmentsAndOutcome(tx);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0002E4B4 File Offset: 0x0002D8B4
		protected bool PromotePhaseVolatiles(InternalTransaction tx, ref VolatileEnlistmentSet volatiles, bool phase0)
		{
			if (volatiles.volatileEnlistmentCount + volatiles.dependentClones > 0)
			{
				if (phase0)
				{
					volatiles.VolatileDemux = new Phase0VolatileDemultiplexer(tx);
				}
				else
				{
					volatiles.VolatileDemux = new Phase1VolatileDemultiplexer(tx);
				}
				volatiles.VolatileDemux.oletxEnlistment = tx.PromotedTransaction.EnlistVolatile(volatiles.VolatileDemux, phase0 ? EnlistmentOptions.EnlistDuringPrepareRequired : EnlistmentOptions.None);
			}
			return true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0002E514 File Offset: 0x0002D914
		internal virtual bool PromoteDurable(InternalTransaction tx)
		{
			if (tx.durableEnlistment != null)
			{
				InternalEnlistment durableEnlistment = tx.durableEnlistment;
				IPromotedEnlistment promotedEnlistment = tx.PromotedTransaction.EnlistDurable(durableEnlistment.ResourceManagerIdentifier, (DurableInternalEnlistment)durableEnlistment, durableEnlistment.SinglePhaseNotification != null, EnlistmentOptions.None);
				tx.durableEnlistment.State.ChangeStatePromoted(tx.durableEnlistment, promotedEnlistment);
			}
			return true;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0002E574 File Offset: 0x0002D974
		internal virtual void PromoteEnlistmentsAndOutcome(InternalTransaction tx)
		{
			bool flag = false;
			tx.PromotedTransaction.RealTransaction.InternalTransaction = tx;
			try
			{
				flag = this.PromotePhaseVolatiles(tx, ref tx.phase0Volatiles, true);
			}
			catch (TransactionException ex)
			{
				tx.innerException = ex;
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex);
				}
				return;
			}
			finally
			{
				if (!flag)
				{
					tx.PromotedTransaction.Rollback();
					tx.State.ChangeStateAbortedDuringPromotion(tx);
				}
			}
			flag = false;
			try
			{
				flag = this.PromotePhaseVolatiles(tx, ref tx.phase1Volatiles, false);
			}
			catch (TransactionException ex2)
			{
				tx.innerException = ex2;
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex2);
				}
				return;
			}
			finally
			{
				if (!flag)
				{
					tx.PromotedTransaction.Rollback();
					tx.State.ChangeStateAbortedDuringPromotion(tx);
				}
			}
			flag = false;
			try
			{
				flag = this.PromoteDurable(tx);
			}
			catch (TransactionException ex3)
			{
				tx.innerException = ex3;
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex3);
				}
			}
			finally
			{
				if (!flag)
				{
					tx.PromotedTransaction.Rollback();
					tx.State.ChangeStateAbortedDuringPromotion(tx);
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0002E724 File Offset: 0x0002DB24
		internal override void DisposeRoot(InternalTransaction tx)
		{
			tx.State.Rollback(tx, null);
		}
	}
}
