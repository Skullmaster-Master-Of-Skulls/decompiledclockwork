using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000020 RID: 32
	internal class TransactionStateCommitted : TransactionStateEnded
	{
		// Token: 0x06000100 RID: 256 RVA: 0x0002D894 File Offset: 0x0002CC94
		internal override void EnterState(InternalTransaction tx)
		{
			base.EnterState(tx);
			base.CommonEnterState(tx);
			for (int i = 0; i < tx.phase0Volatiles.volatileEnlistmentCount; i++)
			{
				tx.phase0Volatiles.volatileEnlistments[i].twoPhaseState.InternalCommitted(tx.phase0Volatiles.volatileEnlistments[i]);
			}
			for (int j = 0; j < tx.phase1Volatiles.volatileEnlistmentCount; j++)
			{
				tx.phase1Volatiles.volatileEnlistments[j].twoPhaseState.InternalCommitted(tx.phase1Volatiles.volatileEnlistments[j]);
			}
			TransactionManager.TransactionTable.Remove(tx);
			if (DiagnosticTrace.Verbose)
			{
				TransactionCommittedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), tx.TransactionTraceId);
			}
			tx.FireCompletion();
			if (tx.asyncCommit)
			{
				tx.SignalAsyncCompletion();
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0002D964 File Offset: 0x0002CD64
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			return TransactionStatus.Committed;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0002D974 File Offset: 0x0002CD74
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0002D9A4 File Offset: 0x0002CDA4
		internal override void EndCommit(InternalTransaction tx)
		{
		}
	}
}
