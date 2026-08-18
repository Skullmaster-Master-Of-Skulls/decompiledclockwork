using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000057 RID: 87
	internal class Phase0VolatileDemultiplexer : VolatileDemultiplexer
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00032E24 File Offset: 0x00032224
		public Phase0VolatileDemultiplexer(InternalTransaction transaction) : base(transaction)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00032E44 File Offset: 0x00032244
		protected override void InternalPrepare()
		{
			try
			{
				this.transaction.State.ChangeStatePromotedPhase0(this.transaction);
			}
			catch (TransactionAbortedException ex)
			{
				this.oletxEnlistment.ForceRollback(ex);
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex);
				}
			}
			catch (TransactionInDoubtException exception)
			{
				this.oletxEnlistment.EnlistmentDone();
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), exception);
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00032EF4 File Offset: 0x000322F4
		protected override void InternalCommit()
		{
			this.oletxEnlistment.EnlistmentDone();
			this.transaction.State.ChangeStatePromotedCommitted(this.transaction);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00032F24 File Offset: 0x00032324
		protected override void InternalRollback()
		{
			this.oletxEnlistment.EnlistmentDone();
			this.transaction.State.ChangeStatePromotedAborted(this.transaction);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00032F54 File Offset: 0x00032354
		protected override void InternalInDoubt()
		{
			this.transaction.State.InDoubtFromDtc(this.transaction);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00032F84 File Offset: 0x00032384
		public override void Prepare(IPromotedEnlistment en)
		{
			this.preparingEnlistment = en;
			VolatileDemultiplexer.PoolablePrepare(this);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00032FA4 File Offset: 0x000323A4
		public override void Commit(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableCommit(this);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00032FC4 File Offset: 0x000323C4
		public override void Rollback(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableRollback(this);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00032FE4 File Offset: 0x000323E4
		public override void InDoubt(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableInDoubt(this);
		}
	}
}
