using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000058 RID: 88
	internal class Phase1VolatileDemultiplexer : VolatileDemultiplexer
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00033004 File Offset: 0x00032404
		public Phase1VolatileDemultiplexer(InternalTransaction transaction) : base(transaction)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00033024 File Offset: 0x00032424
		protected override void InternalPrepare()
		{
			try
			{
				this.transaction.State.ChangeStatePromotedPhase1(this.transaction);
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

		// Token: 0x06000295 RID: 661 RVA: 0x000330D4 File Offset: 0x000324D4
		protected override void InternalCommit()
		{
			this.oletxEnlistment.EnlistmentDone();
			this.transaction.State.ChangeStatePromotedCommitted(this.transaction);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00033104 File Offset: 0x00032504
		protected override void InternalRollback()
		{
			this.oletxEnlistment.EnlistmentDone();
			this.transaction.State.ChangeStatePromotedAborted(this.transaction);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00033134 File Offset: 0x00032534
		protected override void InternalInDoubt()
		{
			this.transaction.State.InDoubtFromDtc(this.transaction);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00033164 File Offset: 0x00032564
		public override void Prepare(IPromotedEnlistment en)
		{
			this.preparingEnlistment = en;
			VolatileDemultiplexer.PoolablePrepare(this);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00033184 File Offset: 0x00032584
		public override void Commit(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableCommit(this);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000331A4 File Offset: 0x000325A4
		public override void Rollback(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableRollback(this);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000331C4 File Offset: 0x000325C4
		public override void InDoubt(IPromotedEnlistment en)
		{
			this.oletxEnlistment = en;
			VolatileDemultiplexer.PoolableInDoubt(this);
		}
	}
}
