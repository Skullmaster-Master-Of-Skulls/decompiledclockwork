using System;

namespace System.Transactions
{
	// Token: 0x02000019 RID: 25
	internal class TransactionStateSubordinateActive : TransactionStateActive
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0002CC74 File Offset: 0x0002C074
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0002CC94 File Offset: 0x0002C094
		internal override void Rollback(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			((ISimpleTransactionSuperior)tx.promoter).Rollback();
			TransactionState._TransactionStateAborted.EnterState(tx);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0002CCD4 File Offset: 0x0002C0D4
		internal override Enlistment EnlistVolatile(InternalTransaction tx, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			tx.promoteState.EnterState(tx);
			return tx.State.EnlistVolatile(tx, enlistmentNotification, enlistmentOptions, atomicTransaction);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0002CD04 File Offset: 0x0002C104
		internal override Enlistment EnlistVolatile(InternalTransaction tx, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			tx.promoteState.EnterState(tx);
			return tx.State.EnlistVolatile(tx, enlistmentNotification, enlistmentOptions, atomicTransaction);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0002CD34 File Offset: 0x0002C134
		internal override TransactionStatus get_Status(InternalTransaction tx)
		{
			tx.promoteState.EnterState(tx);
			return tx.State.get_Status(tx);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0002CD64 File Offset: 0x0002C164
		internal override void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			tx.promoteState.EnterState(tx);
			tx.State.AddOutcomeRegistrant(tx, transactionCompletedDelegate);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0002CD94 File Offset: 0x0002C194
		internal override bool EnlistPromotableSinglePhase(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Transaction atomicTransaction)
		{
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0002CDA4 File Offset: 0x0002C1A4
		internal override void CreateBlockingClone(InternalTransaction tx)
		{
			tx.promoteState.EnterState(tx);
			tx.State.CreateBlockingClone(tx);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0002CDD4 File Offset: 0x0002C1D4
		internal override void CreateAbortingClone(InternalTransaction tx)
		{
			tx.promoteState.EnterState(tx);
			tx.State.CreateAbortingClone(tx);
		}
	}
}
