using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053C RID: 1340
	internal sealed class SharedTransactedBatchContext
	{
		// Token: 0x060032AD RID: 12973 RVA: 0x000C3850 File Offset: 0x000C1A50
		internal SharedTransactedBatchContext(ChannelHandler handler, ChannelDispatcher dispatcher, int maxConcurrentBatches)
		{
			this.handler = handler;
			this.maxBatchSize = dispatcher.MaxTransactedBatchSize;
			this.maxConcurrentBatches = maxConcurrentBatches;
			this.currentBatchSize = dispatcher.MaxTransactedBatchSize;
			this.currentConcurrentBatches = 0;
			this.currentConcurrentDispatches = 0;
			this.successfullCommits = 0;
			this.isBatching = true;
			this.isolationLevel = dispatcher.TransactionIsolationLevel;
			this.txTimeout = TransactionBehavior.NormalizeTimeout(dispatcher.TransactionTimeout);
			this.BatchingStateChanged(this.isBatching);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000C38E4 File Offset: 0x000C1AE4
		internal TransactedBatchContext CreateTransactedBatchContext()
		{
			object obj = this.thisLock;
			TransactedBatchContext result;
			lock (obj)
			{
				TransactedBatchContext transactedBatchContext = new TransactedBatchContext(this);
				this.currentConcurrentBatches++;
				result = transactedBatchContext;
			}
			return result;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000C3938 File Offset: 0x000C1B38
		internal void DispatchStarted()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.currentConcurrentDispatches++;
				if (this.currentConcurrentDispatches == this.currentConcurrentBatches && this.currentConcurrentBatches < this.maxConcurrentBatches)
				{
					TransactedBatchContext context = new TransactedBatchContext(this);
					this.currentConcurrentBatches++;
					ChannelHandler channelHandler = new ChannelHandler(this.handler, context);
					ChannelHandler.Register(channelHandler);
				}
			}
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000C39C4 File Offset: 0x000C1BC4
		internal void DispatchEnded()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.currentConcurrentDispatches--;
				int num = this.currentConcurrentDispatches;
			}
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000C3A18 File Offset: 0x000C1C18
		internal void BatchDone()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.currentConcurrentBatches--;
				int num = this.currentConcurrentBatches;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000C3A6C File Offset: 0x000C1C6C
		internal int CurrentBatchSize
		{
			get
			{
				object obj = this.thisLock;
				int result;
				lock (obj)
				{
					result = this.currentBatchSize;
				}
				return result;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000C3AB0 File Offset: 0x000C1CB0
		internal IsolationLevel IsolationLevel
		{
			get
			{
				return this.isolationLevel;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000C3AB8 File Offset: 0x000C1CB8
		internal TimeSpan TransactionTimeout
		{
			get
			{
				return this.txTimeout;
			}
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000C3AC0 File Offset: 0x000C1CC0
		internal void ReportAbort()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.isBatching)
				{
					this.successfullCommits = 0;
					this.currentBatchSize = 1;
					this.isBatching = false;
					this.BatchingStateChanged(this.isBatching);
				}
			}
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000C3B24 File Offset: 0x000C1D24
		internal void ReportCommit()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				int num = this.successfullCommits + 1;
				this.successfullCommits = num;
				if (num >= this.maxBatchSize * 2)
				{
					this.successfullCommits = 0;
					if (!this.isBatching)
					{
						this.currentBatchSize = this.maxBatchSize;
						this.isBatching = true;
						this.BatchingStateChanged(this.isBatching);
					}
				}
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000C3BA8 File Offset: 0x000C1DA8
		private void BatchingStateChanged(bool batchingNow)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, batchingNow ? 262231 : 262234, batchingNow ? SR.GetString("TraceCodeMsmqEnteredBatch") : SR.GetString("TraceCodeMsmqLeftBatch"), null, null, null);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000C3BE3 File Offset: 0x000C1DE3
		internal object ReceiveLock
		{
			get
			{
				return this.receiveLock;
			}
		}

		// Token: 0x0400272B RID: 10027
		private readonly int maxBatchSize;

		// Token: 0x0400272C RID: 10028
		private readonly int maxConcurrentBatches;

		// Token: 0x0400272D RID: 10029
		private readonly IsolationLevel isolationLevel;

		// Token: 0x0400272E RID: 10030
		private readonly TimeSpan txTimeout;

		// Token: 0x0400272F RID: 10031
		private int currentBatchSize;

		// Token: 0x04002730 RID: 10032
		private int currentConcurrentBatches;

		// Token: 0x04002731 RID: 10033
		private int currentConcurrentDispatches;

		// Token: 0x04002732 RID: 10034
		private int successfullCommits;

		// Token: 0x04002733 RID: 10035
		private object receiveLock = new object();

		// Token: 0x04002734 RID: 10036
		private object thisLock = new object();

		// Token: 0x04002735 RID: 10037
		private bool isBatching;

		// Token: 0x04002736 RID: 10038
		private ChannelHandler handler;
	}
}
