using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000542 RID: 1346
	internal class ConcurrencyInstanceContextFacet
	{
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000C4F48 File Offset: 0x000C3148
		internal bool HasWaiters
		{
			get
			{
				return (this.calloutMessageQueue != null && this.calloutMessageQueue.Count > 0) || (this.newMessageQueue != null && this.newMessageQueue.Count > 0);
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000C4F7C File Offset: 0x000C317C
		private ConcurrencyBehavior.IWaiter DequeueFrom(Queue<ConcurrencyBehavior.IWaiter> queue)
		{
			ConcurrencyBehavior.IWaiter result = queue.Dequeue();
			if (queue.Count == 0)
			{
				queue.TrimExcess();
			}
			return result;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000C4F9F File Offset: 0x000C319F
		internal ConcurrencyBehavior.IWaiter DequeueWaiter()
		{
			if (this.calloutMessageQueue != null && this.calloutMessageQueue.Count > 0)
			{
				return this.DequeueFrom(this.calloutMessageQueue);
			}
			return this.DequeueFrom(this.newMessageQueue);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000C4FD0 File Offset: 0x000C31D0
		internal void EnqueueNewMessage(ConcurrencyBehavior.IWaiter waiter)
		{
			if (this.newMessageQueue == null)
			{
				this.newMessageQueue = new Queue<ConcurrencyBehavior.IWaiter>();
			}
			this.newMessageQueue.Enqueue(waiter);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000C4FF1 File Offset: 0x000C31F1
		internal void EnqueueCalloutMessage(ConcurrencyBehavior.IWaiter waiter)
		{
			if (this.calloutMessageQueue == null)
			{
				this.calloutMessageQueue = new Queue<ConcurrencyBehavior.IWaiter>();
			}
			this.calloutMessageQueue.Enqueue(waiter);
		}

		// Token: 0x0400274F RID: 10063
		internal bool Locked;

		// Token: 0x04002750 RID: 10064
		private Queue<ConcurrencyBehavior.IWaiter> calloutMessageQueue;

		// Token: 0x04002751 RID: 10065
		private Queue<ConcurrencyBehavior.IWaiter> newMessageQueue;
	}
}
