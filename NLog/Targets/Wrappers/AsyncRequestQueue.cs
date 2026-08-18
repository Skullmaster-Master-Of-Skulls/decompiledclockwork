using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000173 RID: 371
	internal class AsyncRequestQueue
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00021E92 File Offset: 0x00020092
		public AsyncRequestQueue(int requestLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			this.RequestLimit = requestLimit;
			this.OnOverflow = overflowAction;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00021EB3 File Offset: 0x000200B3
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00021EBB File Offset: 0x000200BB
		public int RequestLimit { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00021EC4 File Offset: 0x000200C4
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00021ECC File Offset: 0x000200CC
		public AsyncTargetWrapperOverflowAction OnOverflow { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00021ED5 File Offset: 0x000200D5
		public int RequestCount
		{
			get
			{
				return this.logEventInfoQueue.Count;
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00021EE4 File Offset: 0x000200E4
		public void Enqueue(AsyncLogEventInfo logEventInfo)
		{
			lock (this)
			{
				if (this.logEventInfoQueue.Count >= this.RequestLimit)
				{
					InternalLogger.Debug("Async queue is full");
					switch (this.OnOverflow)
					{
					case AsyncTargetWrapperOverflowAction.Grow:
						InternalLogger.Debug("The overflow action is Grow, adding element anyway");
						break;
					case AsyncTargetWrapperOverflowAction.Discard:
						InternalLogger.Debug("Discarding one element from queue");
						this.logEventInfoQueue.Dequeue();
						break;
					case AsyncTargetWrapperOverflowAction.Block:
						while (this.logEventInfoQueue.Count >= this.RequestLimit)
						{
							InternalLogger.Debug("Blocking because the overflow action is Block...");
							Monitor.Wait(this);
							InternalLogger.Trace("Entered critical section.");
						}
						InternalLogger.Trace("Limit ok.");
						break;
					}
				}
				this.logEventInfoQueue.Enqueue(logEventInfo);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00021FBC File Offset: 0x000201BC
		public AsyncLogEventInfo[] DequeueBatch(int count)
		{
			List<AsyncLogEventInfo> list = new List<AsyncLogEventInfo>();
			lock (this)
			{
				int num = 0;
				while (num < count && this.logEventInfoQueue.Count > 0)
				{
					list.Add(this.logEventInfoQueue.Dequeue());
					num++;
				}
				if (this.OnOverflow == AsyncTargetWrapperOverflowAction.Block)
				{
					Monitor.PulseAll(this);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00022038 File Offset: 0x00020238
		public void Clear()
		{
			lock (this)
			{
				this.logEventInfoQueue.Clear();
			}
		}

		// Token: 0x040003EB RID: 1003
		private readonly Queue<AsyncLogEventInfo> logEventInfoQueue = new Queue<AsyncLogEventInfo>();
	}
}
