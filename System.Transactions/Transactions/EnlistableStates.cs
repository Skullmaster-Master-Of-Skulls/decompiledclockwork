using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000017 RID: 23
	internal abstract class EnlistableStates : ActiveStates
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0002C814 File Offset: 0x0002BC14
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			tx.promoteState.EnterState(tx);
			return tx.State.EnlistDurable(tx, resourceManagerIdentifier, enlistmentNotification, enlistmentOptions, atomicTransaction);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0002C844 File Offset: 0x0002BC44
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			if (tx.durableEnlistment != null || (enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
			{
				tx.promoteState.EnterState(tx);
				return tx.State.EnlistDurable(tx, resourceManagerIdentifier, enlistmentNotification, enlistmentOptions, atomicTransaction);
			}
			Enlistment enlistment = new Enlistment(resourceManagerIdentifier, tx, enlistmentNotification, enlistmentNotification, atomicTransaction);
			tx.durableEnlistment = enlistment.InternalEnlistment;
			DurableEnlistmentState._DurableEnlistmentActive.EnterState(tx.durableEnlistment);
			if (DiagnosticTrace.Information)
			{
				EnlistmentTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.durableEnlistment.EnlistmentTraceId, EnlistmentType.Durable, EnlistmentOptions.None);
			}
			return enlistment;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0002C8D4 File Offset: 0x0002BCD4
		internal override void Timeout(InternalTransaction tx)
		{
			if (DiagnosticTrace.Warning)
			{
				TransactionTimeoutTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
			TimeoutException e = new TimeoutException(SR.GetString("TraceTransactionTimeout"));
			this.Rollback(tx, e);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0002C924 File Offset: 0x0002BD24
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			tx.promoteState.EnterState(tx);
			tx.State.GetObjectData(tx, serializationInfo, context);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0002C954 File Offset: 0x0002BD54
		internal override void CompleteBlockingClone(InternalTransaction tx)
		{
			tx.phase0Volatiles.dependentClones = tx.phase0Volatiles.dependentClones - 1;
			if (tx.phase0Volatiles.preparedVolatileEnlistments == tx.phase0VolatileWaveCount + tx.phase0Volatiles.dependentClones)
			{
				tx.State.Phase0VolatilePrepareDone(tx);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0002C9A4 File Offset: 0x0002BDA4
		internal override void CompleteAbortingClone(InternalTransaction tx)
		{
			tx.phase1Volatiles.dependentClones = tx.phase1Volatiles.dependentClones - 1;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0002C9C4 File Offset: 0x0002BDC4
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			tx.phase0Volatiles.dependentClones = tx.phase0Volatiles.dependentClones + 1;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0002C9E4 File Offset: 0x0002BDE4
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			tx.phase1Volatiles.dependentClones = tx.phase1Volatiles.dependentClones + 1;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0002CA04 File Offset: 0x0002BE04
		internal override void Promote(InternalTransaction tx)
		{
			tx.promoteState.EnterState(tx);
			tx.State.CheckForFinishedTransaction(tx);
		}
	}
}
