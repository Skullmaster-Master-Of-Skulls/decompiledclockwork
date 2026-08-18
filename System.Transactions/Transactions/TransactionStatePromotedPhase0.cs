using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x02000027 RID: 39
	internal class TransactionStatePromotedPhase0 : TransactionStatePromotedCommitting
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0002E934 File Offset: 0x0002DD34
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
			this.Phase0VolatilePrepareDone(tx);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0002E9C4 File Offset: 0x0002DDC4
		internal override void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
			Monitor.Exit(tx);
			try
			{
				tx.phase0Volatiles.VolatileDemux.oletxEnlistment.Prepared();
			}
			finally
			{
				Monitor.Enter(tx);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0002EA14 File Offset: 0x0002DE14
		internal override bool ContinuePhase0Prepares()
		{
			return true;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0002EA24 File Offset: 0x0002DE24
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStatePromotedP0Aborting.EnterState(tx);
		}
	}
}
