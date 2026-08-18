using System;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	internal class OletxCommittableTransaction : OletxTransaction
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00037E44 File Offset: 0x00037244
		internal OletxCommittableTransaction(RealOletxTransaction realOletxTransaction) : base(realOletxTransaction)
		{
			realOletxTransaction.committableTransaction = this;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00037E64 File Offset: 0x00037264
		internal bool CommitCalled
		{
			get
			{
				return this.commitCalled;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00037E84 File Offset: 0x00037284
		internal void BeginCommit(InternalTransaction internalTransaction)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "CommittableTransaction.BeginCommit");
				TransactionCommitCalledTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.TransactionTraceId);
			}
			this.realOletxTransaction.InternalTransaction = internalTransaction;
			this.commitCalled = true;
			this.realOletxTransaction.Commit();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "CommittableTransaction.BeginCommit");
			}
		}

		// Token: 0x040001D5 RID: 469
		private bool commitCalled;
	}
}
