using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000022 RID: 34
	internal abstract class TransactionStatePromotedBase : TransactionState
	{
		// Token: 0x0600010C RID: 268 RVA: 0x0002DB94 File Offset: 0x0002CF94
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Active;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0002DBA4 File Offset: 0x0002CFA4
		internal override Enlistment EnlistVolatile(InternalTransaction tx, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Monitor.Exit(tx);
			Enlistment result;
			try
			{
				Enlistment enlistment = new Enlistment(enlistmentNotification, tx, atomicTransaction);
				EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment.InternalEnlistment);
				enlistment.InternalEnlistment.PromotedEnlistment = tx.PromotedTransaction.EnlistVolatile(enlistment.InternalEnlistment, enlistmentOptions);
				result = enlistment;
			}
			finally
			{
				Monitor.Enter(tx);
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0002DC24 File Offset: 0x0002D024
		internal override Enlistment EnlistVolatile(InternalTransaction tx, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Monitor.Exit(tx);
			Enlistment result;
			try
			{
				Enlistment enlistment = new Enlistment(enlistmentNotification, tx, atomicTransaction);
				EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment.InternalEnlistment);
				enlistment.InternalEnlistment.PromotedEnlistment = tx.PromotedTransaction.EnlistVolatile(enlistment.InternalEnlistment, enlistmentOptions);
				result = enlistment;
			}
			finally
			{
				Monitor.Enter(tx);
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0002DCA4 File Offset: 0x0002D0A4
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Monitor.Exit(tx);
			Enlistment result;
			try
			{
				Enlistment enlistment = new Enlistment(resourceManagerIdentifier, tx, enlistmentNotification, null, atomicTransaction);
				EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment.InternalEnlistment);
				enlistment.InternalEnlistment.PromotedEnlistment = tx.PromotedTransaction.EnlistDurable(resourceManagerIdentifier, (DurableInternalEnlistment)enlistment.InternalEnlistment, false, enlistmentOptions);
				result = enlistment;
			}
			finally
			{
				Monitor.Enter(tx);
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0002DD24 File Offset: 0x0002D124
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Monitor.Exit(tx);
			Enlistment result;
			try
			{
				Enlistment enlistment = new Enlistment(resourceManagerIdentifier, tx, enlistmentNotification, enlistmentNotification, atomicTransaction);
				EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment.InternalEnlistment);
				enlistment.InternalEnlistment.PromotedEnlistment = tx.PromotedTransaction.EnlistDurable(resourceManagerIdentifier, (DurableInternalEnlistment)enlistment.InternalEnlistment, true, enlistmentOptions);
				result = enlistment;
			}
			finally
			{
				Monitor.Enter(tx);
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0002DDA4 File Offset: 0x0002D1A4
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			Monitor.Exit(tx);
			try
			{
				tx.PromotedTransaction.Rollback();
			}
			finally
			{
				Monitor.Enter(tx);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0002DE04 File Offset: 0x0002D204
		internal override Guid get_Identifier(InternalTransaction tx)
		{
			return tx.PromotedTransaction.Identifier;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0002DE24 File Offset: 0x0002D224
		internal override void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			tx.transactionCompletedDelegate = (TransactionCompletedEventHandler)Delegate.Combine(tx.transactionCompletedDelegate, transactionCompletedDelegate);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0002DE54 File Offset: 0x0002D254
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			tx.asyncCommit = asyncCommit;
			tx.asyncCallback = asyncCallback;
			tx.asyncState = asyncState;
			TransactionState._TransactionStatePromotedCommitting.EnterState(tx);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0002DE84 File Offset: 0x0002D284
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedP0Wave.EnterState(tx);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0002DEA4 File Offset: 0x0002D2A4
		internal override bool EnlistPromotableSinglePhase(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Transaction atomicTransaction)
		{
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0002DEB4 File Offset: 0x0002D2B4
		internal override void CompleteBlockingClone(InternalTransaction tx)
		{
			if (tx.phase0Volatiles.dependentClones > 0)
			{
				tx.phase0Volatiles.dependentClones = tx.phase0Volatiles.dependentClones - 1;
				if (tx.phase0Volatiles.preparedVolatileEnlistments == tx.phase0VolatileWaveCount + tx.phase0Volatiles.dependentClones)
				{
					tx.State.Phase0VolatilePrepareDone(tx);
					return;
				}
			}
			else
			{
				tx.phase0WaveDependentCloneCount--;
				if (tx.phase0WaveDependentCloneCount == 0)
				{
					OletxDependentTransaction phase0WaveDependentClone = tx.phase0WaveDependentClone;
					tx.phase0WaveDependentClone = null;
					Monitor.Exit(tx);
					try
					{
						try
						{
							phase0WaveDependentClone.Complete();
						}
						finally
						{
							phase0WaveDependentClone.Dispose();
						}
					}
					finally
					{
						Monitor.Enter(tx);
					}
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0002DF84 File Offset: 0x0002D384
		internal override void CompleteAbortingClone(InternalTransaction tx)
		{
			if (tx.phase1Volatiles.VolatileDemux != null)
			{
				tx.phase1Volatiles.dependentClones = tx.phase1Volatiles.dependentClones - 1;
				return;
			}
			tx.abortingDependentCloneCount--;
			if (tx.abortingDependentCloneCount == 0)
			{
				OletxDependentTransaction abortingDependentClone = tx.abortingDependentClone;
				tx.abortingDependentClone = null;
				Monitor.Exit(tx);
				try
				{
					try
					{
						abortingDependentClone.Complete();
					}
					finally
					{
						abortingDependentClone.Dispose();
					}
				}
				finally
				{
					Monitor.Enter(tx);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0002E034 File Offset: 0x0002D434
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			if (tx.phase0WaveDependentClone == null)
			{
				tx.phase0WaveDependentClone = tx.PromotedTransaction.DependentClone(true);
			}
			tx.phase0WaveDependentCloneCount++;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0002E074 File Offset: 0x0002D474
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			if (tx.phase1Volatiles.VolatileDemux != null)
			{
				tx.phase1Volatiles.dependentClones = tx.phase1Volatiles.dependentClones + 1;
				return;
			}
			if (tx.abortingDependentClone == null)
			{
				tx.abortingDependentClone = tx.PromotedTransaction.DependentClone(false);
			}
			tx.abortingDependentCloneCount++;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0002E0D4 File Offset: 0x0002D4D4
		internal override bool ContinuePhase0Prepares()
		{
			return true;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0002E0E4 File Offset: 0x0002D4E4
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			ISerializable promotedTransaction = tx.PromotedTransaction;
			if (promotedTransaction == null)
			{
				throw new NotSupportedException();
			}
			serializationInfo.FullTypeName = tx.PromotedTransaction.GetType().FullName;
			promotedTransaction.GetObjectData(serializationInfo, context);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0002E124 File Offset: 0x0002D524
		internal override void ChangeStatePromotedAborted(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedAborted.EnterState(tx);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0002E144 File Offset: 0x0002D544
		internal override void ChangeStatePromotedCommitted(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedCommitted.EnterState(tx);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0002E164 File Offset: 0x0002D564
		internal override void InDoubtFromDtc(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedIndoubt.EnterState(tx);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0002E184 File Offset: 0x0002D584
		internal override void InDoubtFromEnlistment(InternalTransaction tx)
		{
			TransactionState._TransactionStatePromotedIndoubt.EnterState(tx);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0002E1A4 File Offset: 0x0002D5A4
		internal override void ChangeStateAbortedDuringPromotion(InternalTransaction tx)
		{
			TransactionState._TransactionStateAborted.EnterState(tx);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0002E1C4 File Offset: 0x0002D5C4
		internal override void Timeout(InternalTransaction tx)
		{
			try
			{
				if (tx.innerException == null)
				{
					tx.innerException = new TimeoutException(SR.GetString("TraceTransactionTimeout"));
				}
				tx.PromotedTransaction.Rollback();
				if (DiagnosticTrace.Warning)
				{
					TransactionTimeoutTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
				}
			}
			catch (TransactionException exception)
			{
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), exception);
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0002E254 File Offset: 0x0002D654
		internal override void Promote(InternalTransaction tx)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0002E264 File Offset: 0x0002D664
		internal override void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0002E274 File Offset: 0x0002D674
		internal override void Phase1VolatilePrepareDone(InternalTransaction tx)
		{
		}
	}
}
