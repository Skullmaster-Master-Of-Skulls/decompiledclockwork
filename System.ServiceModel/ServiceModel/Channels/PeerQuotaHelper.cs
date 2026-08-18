using System;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FE RID: 2558
	internal class PeerQuotaHelper
	{
		// Token: 0x06006587 RID: 25991 RVA: 0x0017AB50 File Offset: 0x00178D50
		public PeerQuotaHelper(int limit)
		{
			this.quota = limit;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x0017AB74 File Offset: 0x00178D74
		public void ReadyToEnqueueItem()
		{
			int num = Interlocked.Increment(ref this.enqueuedCount);
			if (num > this.quota)
			{
				this.waiter.WaitOne();
			}
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x0017ABA4 File Offset: 0x00178DA4
		public void ItemDequeued()
		{
			int num = Interlocked.Decrement(ref this.enqueuedCount);
			if (num >= this.quota)
			{
				this.waiter.Set();
			}
		}

		// Token: 0x04003A32 RID: 14898
		private int enqueuedCount;

		// Token: 0x04003A33 RID: 14899
		private int quota = 64;

		// Token: 0x04003A34 RID: 14900
		private AutoResetEvent waiter = new AutoResetEvent(false);
	}
}
