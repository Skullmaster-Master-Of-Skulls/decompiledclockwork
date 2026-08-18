using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200090C RID: 2316
	internal abstract class DeliveryStrategy<ItemType> : IDisposable where ItemType : class, IDisposable
	{
		// Token: 0x0600585F RID: 22623 RVA: 0x00144CF2 File Offset: 0x00142EF2
		public DeliveryStrategy(InputQueueChannel<ItemType> channel, int quota)
		{
			if (quota <= 0)
			{
				throw Fx.AssertAndThrow("Argument quota must be positive.");
			}
			this.channel = channel;
			this.quota = quota;
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06005860 RID: 22624 RVA: 0x00144D17 File Offset: 0x00142F17
		protected InputQueueChannel<ItemType> Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06005861 RID: 22625 RVA: 0x00144D1F File Offset: 0x00142F1F
		// (set) Token: 0x06005862 RID: 22626 RVA: 0x00144D27 File Offset: 0x00142F27
		public Action DequeueCallback
		{
			get
			{
				return this.dequeueCallback;
			}
			set
			{
				this.dequeueCallback = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06005863 RID: 22627
		public abstract int EnqueuedCount { get; }

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06005864 RID: 22628 RVA: 0x00144D30 File Offset: 0x00142F30
		protected int Quota
		{
			get
			{
				return this.quota;
			}
		}

		// Token: 0x06005865 RID: 22629
		public abstract bool CanEnqueue(long sequenceNumber);

		// Token: 0x06005866 RID: 22630 RVA: 0x00144D38 File Offset: 0x00142F38
		public virtual void Dispose()
		{
		}

		// Token: 0x06005867 RID: 22631
		public abstract bool Enqueue(ItemType item, long sequenceNumber);

		// Token: 0x0400362E RID: 13870
		private InputQueueChannel<ItemType> channel;

		// Token: 0x0400362F RID: 13871
		private Action dequeueCallback;

		// Token: 0x04003630 RID: 13872
		private int quota;
	}
}
