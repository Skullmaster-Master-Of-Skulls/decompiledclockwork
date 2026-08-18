using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public sealed class CommittableTransaction : Transaction, IAsyncResult
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0002A9F4 File Offset: 0x00029DF4
		public CommittableTransaction() : this(TransactionManager.DefaultIsolationLevel, TransactionManager.DefaultTimeout)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0002AA14 File Offset: 0x00029E14
		public CommittableTransaction(TimeSpan timeout) : this(TransactionManager.DefaultIsolationLevel, timeout)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0002AA34 File Offset: 0x00029E34
		public CommittableTransaction(TransactionOptions options) : this(options.IsolationLevel, options.Timeout)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0002AA64 File Offset: 0x00029E64
		internal CommittableTransaction(IsolationLevel isoLevel, TimeSpan timeout) : base(isoLevel, null)
		{
			this.internalTransaction = new InternalTransaction(timeout, this);
			this.internalTransaction.cloneCount = 1;
			this.cloneId = 1;
			if (DiagnosticTrace.Information)
			{
				TransactionCreatedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), base.TransactionTraceId);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0002AAC4 File Offset: 0x00029EC4
		public IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.BeginCommit");
				TransactionCommitCalledTraceRecord.Trace(SR.GetString("TraceSourceLtm"), base.TransactionTraceId);
			}
			if (base.Disposed)
			{
				throw new ObjectDisposedException("Transaction");
			}
			lock (this.internalTransaction)
			{
				if (this.complete)
				{
					throw TransactionException.CreateTransactionCompletedException(SR.GetString("TraceSourceLtm"));
				}
				this.internalTransaction.State.BeginCommit(this.internalTransaction, true, asyncCallback, asyncState);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.BeginCommit");
			}
			return this;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0002AB94 File Offset: 0x00029F94
		public void Commit()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.Commit");
				TransactionCommitCalledTraceRecord.Trace(SR.GetString("TraceSourceLtm"), base.TransactionTraceId);
			}
			if (base.Disposed)
			{
				throw new ObjectDisposedException("Transaction");
			}
			lock (this.internalTransaction)
			{
				if (this.complete)
				{
					throw TransactionException.CreateTransactionCompletedException(SR.GetString("TraceSourceLtm"));
				}
				this.internalTransaction.State.BeginCommit(this.internalTransaction, false, null, null);
				while (!this.internalTransaction.State.IsCompleted(this.internalTransaction) && Monitor.Wait(this.internalTransaction))
				{
				}
				this.internalTransaction.State.EndCommit(this.internalTransaction);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.Commit");
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0002ACA4 File Offset: 0x0002A0A4
		internal override void InternalDispose()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "IDisposable.Dispose");
			}
			if (Interlocked.Exchange(ref this.disposed, 1) == 1)
			{
				return;
			}
			if (this.internalTransaction.State.get_Status(this.internalTransaction) == TransactionStatus.Active)
			{
				lock (this.internalTransaction)
				{
					this.internalTransaction.State.DisposeRoot(this.internalTransaction);
				}
			}
			long num = (long)Interlocked.Decrement(ref this.internalTransaction.cloneCount);
			if (num == 0L)
			{
				this.internalTransaction.Dispose();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "IDisposable.Dispose");
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0002AD84 File Offset: 0x0002A184
		public void EndCommit(IAsyncResult asyncResult)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.EndCommit");
			}
			if (asyncResult != this)
			{
				throw new ArgumentException(SR.GetString("BadAsyncResult"), "asyncResult");
			}
			lock (this.internalTransaction)
			{
				while (!this.internalTransaction.State.IsCompleted(this.internalTransaction) && Monitor.Wait(this.internalTransaction))
				{
				}
				this.internalTransaction.State.EndCommit(this.internalTransaction);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "CommittableTransaction.EndCommit");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0002AE54 File Offset: 0x0002A254
		object IAsyncResult.AsyncState
		{
			get
			{
				return this.internalTransaction.asyncState;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0002AE74 File Offset: 0x0002A274
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0002AE94 File Offset: 0x0002A294
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				if (this.internalTransaction.asyncResultEvent == null)
				{
					lock (this.internalTransaction)
					{
						if (this.internalTransaction.asyncResultEvent == null)
						{
							ManualResetEvent asyncResultEvent = new ManualResetEvent(this.internalTransaction.State.get_Status(this.internalTransaction) != TransactionStatus.Active);
							Thread.MemoryBarrier();
							this.internalTransaction.asyncResultEvent = asyncResultEvent;
						}
					}
				}
				return this.internalTransaction.asyncResultEvent;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0002AF34 File Offset: 0x0002A334
		bool IAsyncResult.IsCompleted
		{
			get
			{
				bool result;
				lock (this.internalTransaction)
				{
					result = (this.internalTransaction.State.get_Status(this.internalTransaction) != TransactionStatus.Active);
				}
				return result;
			}
		}

		// Token: 0x040000A1 RID: 161
		internal bool completedSynchronously;
	}
}
