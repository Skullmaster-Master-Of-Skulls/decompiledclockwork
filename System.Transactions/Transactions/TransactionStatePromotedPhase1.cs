using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x02000028 RID: 40
	internal class TransactionStatePromotedPhase1 : TransactionStatePromotedCommitting
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0002EA74 File Offset: 0x0002DE74
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
			if (tx.committableTransaction != null)
			{
				tx.committableTransaction.complete = true;
			}
			if (tx.phase1Volatiles.dependentClones != 0)
			{
				tx.State.ChangeStateTransactionAborted(tx, null);
				return;
			}
			int volatileEnlistmentCount = tx.phase1Volatiles.volatileEnlistmentCount;
			if (tx.phase1Volatiles.preparedVolatileEnlistments < volatileEnlistmentCount)
			{
				for (int i = 0; i < volatileEnlistmentCount; i++)
				{
					tx.phase1Volatiles.volatileEnlistments[i].twoPhaseState.ChangeStatePreparing(tx.phase1Volatiles.volatileEnlistments[i]);
					if (!tx.State.ContinuePhase1Prepares())
					{
						return;
					}
				}
				return;
			}
			this.Phase1VolatilePrepareDone(tx);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0002EB24 File Offset: 0x0002DF24
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0002EB54 File Offset: 0x0002DF54
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0002EB84 File Offset: 0x0002DF84
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStatePromotedP1Aborting.EnterState(tx);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0002EBB4 File Offset: 0x0002DFB4
		internal override void Phase1VolatilePrepareDone(InternalTransaction tx)
		{
			Monitor.Exit(tx);
			try
			{
				tx.phase1Volatiles.VolatileDemux.oletxEnlistment.Prepared();
			}
			finally
			{
				Monitor.Enter(tx);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0002EC04 File Offset: 0x0002E004
		internal override bool ContinuePhase1Prepares()
		{
			return true;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0002EC14 File Offset: 0x0002E014
		internal override Enlistment EnlistVolatile(InternalTransaction tx, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw new TransactionException(SR.GetString("TooLate"));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0002EC34 File Offset: 0x0002E034
		internal override Enlistment EnlistVolatile(InternalTransaction tx, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw new TransactionException(SR.GetString("TooLate"));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0002EC54 File Offset: 0x0002E054
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw new TransactionException(SR.GetString("TooLate"));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0002EC74 File Offset: 0x0002E074
		internal override Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw new TransactionException(SR.GetString("TooLate"));
		}
	}
}
