using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public sealed class DependentTransaction : Transaction
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0002A874 File Offset: 0x00029C74
		internal DependentTransaction(IsolationLevel isoLevel, InternalTransaction internalTransaction, bool blocking) : base(isoLevel, internalTransaction)
		{
			this.blocking = blocking;
			lock (this.internalTransaction)
			{
				if (blocking)
				{
					this.internalTransaction.State.CreateBlockingClone(this.internalTransaction);
				}
				else
				{
					this.internalTransaction.State.CreateAbortingClone(this.internalTransaction);
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0002A8F4 File Offset: 0x00029CF4
		public void Complete()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "DependentTransaction.Complete");
			}
			lock (this.internalTransaction)
			{
				if (base.Disposed)
				{
					throw new ObjectDisposedException("Transaction");
				}
				if (this.complete)
				{
					throw TransactionException.CreateTransactionCompletedException(SR.GetString("TraceSourceLtm"));
				}
				this.complete = true;
				if (this.blocking)
				{
					this.internalTransaction.State.CompleteBlockingClone(this.internalTransaction);
				}
				else
				{
					this.internalTransaction.State.CompleteAbortingClone(this.internalTransaction);
				}
			}
			if (DiagnosticTrace.Information)
			{
				DependentCloneCompleteTraceRecord.Trace(SR.GetString("TraceSourceLtm"), base.TransactionTraceId);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "DependentTransaction.Complete");
			}
		}

		// Token: 0x040000A0 RID: 160
		private bool blocking;
	}
}
