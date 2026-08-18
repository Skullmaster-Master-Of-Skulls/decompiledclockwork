using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	internal class OletxDependentTransaction : OletxTransaction
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00037F04 File Offset: 0x00037304
		internal OletxDependentTransaction(RealOletxTransaction realTransaction, bool delayCommit) : base(realTransaction)
		{
			if (realTransaction == null)
			{
				throw new ArgumentNullException("realTransaction");
			}
			this.volatileEnlistmentContainer = this.realOletxTransaction.AddDependentClone(delayCommit);
			if (DiagnosticTrace.Information)
			{
				DependentCloneCreatedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.TransactionTraceId, delayCommit ? DependentCloneOption.BlockCommitUntilComplete : DependentCloneOption.RollbackIfNotComplete);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00037F64 File Offset: 0x00037364
		public void Complete()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "DependentTransaction.Complete");
			}
			int num = Interlocked.CompareExchange(ref this.completed, 1, 0);
			if (1 == num)
			{
				throw TransactionException.CreateTransactionCompletedException(SR.GetString("TraceSourceOletx"));
			}
			if (DiagnosticTrace.Information)
			{
				DependentCloneCompleteTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.TransactionTraceId);
			}
			this.volatileEnlistmentContainer.DependentCloneCompleted();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "DependentTransaction.Complete");
			}
		}

		// Token: 0x040001D6 RID: 470
		private OletxVolatileEnlistmentContainer volatileEnlistmentContainer;

		// Token: 0x040001D7 RID: 471
		private int completed;
	}
}
