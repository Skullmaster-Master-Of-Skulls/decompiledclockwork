using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x02000044 RID: 68
	internal class EnlistmentStatePromoted : EnlistmentState
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000311F4 File Offset: 0x000305F4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00031214 File Offset: 0x00030614
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.EnlistmentDone();
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00031264 File Offset: 0x00030664
		internal override void Prepared(InternalEnlistment enlistment)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.Prepared();
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000312B4 File Offset: 0x000306B4
		internal override void ForceRollback(InternalEnlistment enlistment, Exception e)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.ForceRollback(e);
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00031304 File Offset: 0x00030704
		internal override void Committed(InternalEnlistment enlistment)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.Committed();
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00031354 File Offset: 0x00030754
		internal override void Aborted(InternalEnlistment enlistment, Exception e)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.Aborted(e);
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000313A4 File Offset: 0x000307A4
		internal override void InDoubt(InternalEnlistment enlistment, Exception e)
		{
			Monitor.Exit(enlistment.SyncRoot);
			try
			{
				enlistment.PromotedEnlistment.InDoubt(e);
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000313F4 File Offset: 0x000307F4
		internal override byte[] RecoveryInformation(InternalEnlistment enlistment)
		{
			Monitor.Exit(enlistment.SyncRoot);
			byte[] recoveryInformation;
			try
			{
				recoveryInformation = enlistment.PromotedEnlistment.GetRecoveryInformation();
			}
			finally
			{
				Monitor.Enter(enlistment.SyncRoot);
			}
			return recoveryInformation;
		}
	}
}
