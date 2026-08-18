using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000021 RID: 33
	internal class TransactionStateInDoubt : TransactionStateEnded
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0002D9D4 File Offset: 0x0002CDD4
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			base.CommonEnterState(tx);
			for (int i = 0; i < tx.phase0Volatiles.volatileEnlistmentCount; i++)
			{
				tx.phase0Volatiles.volatileEnlistments[i].twoPhaseState.InternalIndoubt(tx.phase0Volatiles.volatileEnlistments[i]);
			}
			for (int j = 0; j < tx.phase1Volatiles.volatileEnlistmentCount; j++)
			{
				tx.phase1Volatiles.volatileEnlistments[j].twoPhaseState.InternalIndoubt(tx.phase1Volatiles.volatileEnlistments[j]);
			}
			TransactionManager.TransactionTable.Remove(tx);
			if (DiagnosticTrace.Warning)
			{
				TransactionInDoubtTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
			tx.FireCompletion();
			if (tx.asyncCommit)
			{
				tx.SignalAsyncCompletion();
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0002DAA4 File Offset: 0x0002CEA4
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.InDoubt;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0002DAB4 File Offset: 0x0002CEB4
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0002DAE4 File Offset: 0x0002CEE4
		internal override void EndCommit(InternalTransaction tx)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0002DB14 File Offset: 0x0002CF14
		internal override void CheckForFinishedTransaction(InternalTransaction tx)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0002DB44 File Offset: 0x0002CF44
		internal override void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			throw TransactionInDoubtException.Create(SR.GetString("TraceSourceBase"), tx.innerException);
		}
	}
}
