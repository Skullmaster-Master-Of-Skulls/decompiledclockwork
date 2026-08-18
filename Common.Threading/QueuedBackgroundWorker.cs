using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TechnoPro.Common.Threading
{
	// Token: 0x02000002 RID: 2
	public class QueuedBackgroundWorker
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		public void RunAsync<T, K>(Func<T, K> doWork, T inputArgument, QueuedBackgroundWorker.WorkerCompletedDelegate<K> workerCompleted)
		{
			BackgroundWorker backgroundWorker = this.GetBackgroundWorker<T, K>(doWork, workerCompleted);
			this.Queue.Enqueue(new QueueItem(backgroundWorker, inputArgument));
			object obj = this.lockingObject1;
			lock (obj)
			{
				if (this.Queue.Count == 1)
				{
					((QueueItem)this.Queue.Peek()).RunWorkerAsync();
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		public void RunAsync<T, K>(Func<T, K> doWork, T inputArgument)
		{
			this.RunAsync<T, K>(doWork, inputArgument, null);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		private BackgroundWorker GetBackgroundWorker<T, K>(Func<T, K> doWork, QueuedBackgroundWorker.WorkerCompletedDelegate<K> workerCompleted)
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.WorkerReportsProgress = false;
			backgroundWorker.WorkerSupportsCancellation = false;
			backgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs args)
			{
				if (doWork != null)
				{
					args.Result = doWork((T)((object)args.Argument));
				}
			};
			backgroundWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs args)
			{
				if (workerCompleted != null)
				{
					workerCompleted((K)((object)args.Result), args.Error);
				}
				this.Queue.Dequeue();
				object obj = this.lockingObject1;
				lock (obj)
				{
					if (this.Queue.Count > 0)
					{
						((QueueItem)this.Queue.Peek()).RunWorkerAsync();
					}
				}
			};
			return backgroundWorker;
		}

		// Token: 0x04000001 RID: 1
		private Queue<object> Queue = new Queue<object>();

		// Token: 0x04000002 RID: 2
		private object lockingObject1 = new object();

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x0600000C RID: 12
		public delegate void WorkerCompletedDelegate<K>(K result, Exception error);
	}
}
