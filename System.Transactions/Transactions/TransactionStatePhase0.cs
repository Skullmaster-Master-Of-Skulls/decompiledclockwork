using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200001A RID: 26
	internal class TransactionStatePhase0 : EnlistableStates
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0002CE24 File Offset: 0x0002C224
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			int volatileEnlistmentCount = tx.phase0Volatiles.volatileEnlistmentCount;
			int dependentClones = tx.phase0Volatiles.dependentClones;
			tx.phase0VolatileWaveCount = volatileEnlistmentCount;
			if (tx.phase0Volatiles.preparedVolatileEnlistments < volatileEnlistmentCount + dependentClones)
			{
				for (int i = 0; i < volatileEnlistmentCount; i++)
				{
					tx.phase0Volatiles.volatileEnlistments[i].twoPhaseState.ChangeStatePreparing(tx.phase0Volatiles.volatileEnlistments[i]);
					if (!tx.State.ContinuePhase0Prepares())
					{
						return;
					}
				}
				return;
			}
			TransactionState._TransactionStateVolatilePhase1.EnterState(tx);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0002CEB4 File Offset: 0x0002C2B4
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Enlistment result = base.EnlistDurable(tx, resourceManagerIdentifier, enlistmentNotification, enlistmentOptions, atomicTransaction);
			tx.State.RestartCommitIfNeeded(tx);
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0002CEE4 File Offset: 0x0002C2E4
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Enlistment result = base.EnlistDurable(tx, resourceManagerIdentifier, enlistmentNotification, enlistmentOptions, atomicTransaction);
			tx.State.RestartCommitIfNeeded(tx);
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0002CF14 File Offset: 0x0002C314
		internal override Enlistment EnlistVolatile(InternalTransaction tx, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Enlistment enlistment = new Enlistment(tx, enlistmentNotification, null, atomicTransaction, enlistmentOptions);
			if ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
			{
				base.AddVolatileEnlistment(ref tx.phase0Volatiles, enlistment);
			}
			else
			{
				base.AddVolatileEnlistment(ref tx.phase1Volatiles, enlistment);
			}
			if (DiagnosticTrace.Information)
			{
				EnlistmentTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.InternalEnlistment.EnlistmentTraceId, EnlistmentType.Volatile, enlistmentOptions);
			}
			return enlistment;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0002CF74 File Offset: 0x0002C374
		internal override Enlistment EnlistVolatile(InternalTransaction tx, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			Enlistment enlistment = new Enlistment(tx, enlistmentNotification, enlistmentNotification, atomicTransaction, enlistmentOptions);
			if ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
			{
				base.AddVolatileEnlistment(ref tx.phase0Volatiles, enlistment);
			}
			else
			{
				base.AddVolatileEnlistment(ref tx.phase1Volatiles, enlistment);
			}
			if (DiagnosticTrace.Information)
			{
				EnlistmentTraceRecord.Trace(SR.GetString("TraceSourceLtm"), enlistment.InternalEnlistment.EnlistmentTraceId, EnlistmentType.Volatile, enlistmentOptions);
			}
			return enlistment;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0002CFD4 File Offset: 0x0002C3D4
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			this.ChangeStateTransactionAborted(tx, e);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0002CFF4 File Offset: 0x0002C3F4
		internal override bool EnlistPromotableSinglePhase(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Transaction atomicTransaction)
		{
			if (tx.durableEnlistment != null)
			{
				return false;
			}
			TransactionState._TransactionStatePSPEOperation.Phase0PSPEInitialize(tx, promotableSinglePhaseNotification);
			Enlistment enlistment = new Enlistment(tx, promotableSinglePhaseNotification, atomicTransaction);
			tx.durableEnlistment = enlistment.InternalEnlistment;
			if (DiagnosticTrace.Information)
			{
				EnlistmentTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.durableEnlistment.EnlistmentTraceId, EnlistmentType.PromotableSinglePhase, EnlistmentOptions.None);
			}
			tx.promoter = promotableSinglePhaseNotification;
			tx.promoteState = TransactionState._TransactionStateDelegated;
			DurableEnlistmentState._DurableEnlistmentActive.EnterState(tx.durableEnlistment);
			return true;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0002D074 File Offset: 0x0002C474
		internal override void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
			int volatileEnlistmentCount = tx.phase0Volatiles.volatileEnlistmentCount;
			int dependentClones = tx.phase0Volatiles.dependentClones;
			tx.phase0VolatileWaveCount = volatileEnlistmentCount;
			if (tx.phase0Volatiles.preparedVolatileEnlistments < volatileEnlistmentCount + dependentClones)
			{
				for (int i = 0; i < volatileEnlistmentCount; i++)
				{
					tx.phase0Volatiles.volatileEnlistments[i].twoPhaseState.ChangeStatePreparing(tx.phase0Volatiles.volatileEnlistments[i]);
					if (!tx.State.ContinuePhase0Prepares())
					{
						return;
					}
				}
				return;
			}
			TransactionState._TransactionStateVolatilePhase1.EnterState(tx);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0002D104 File Offset: 0x0002C504
		internal override void Phase1VolatilePrepareDone(InternalTransaction tx)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0002D114 File Offset: 0x0002C514
		internal override void RestartCommitIfNeeded(InternalTransaction tx)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0002D124 File Offset: 0x0002C524
		internal override bool ContinuePhase0Prepares()
		{
			return true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0002D134 File Offset: 0x0002C534
		internal override void Promote(InternalTransaction tx)
		{
			tx.promoteState.EnterState(tx);
			tx.State.CheckForFinishedTransaction(tx);
			tx.State.RestartCommitIfNeeded(tx);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0002D174 File Offset: 0x0002C574
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStateAborted.EnterState(tx);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0002D1A4 File Offset: 0x0002C5A4
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			tx.promoteState.EnterState(tx);
			tx.State.GetObjectData(tx, serializationInfo, context);
			tx.State.RestartCommitIfNeeded(tx);
		}
	}
}
