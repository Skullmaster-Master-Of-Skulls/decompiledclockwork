using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200090E RID: 2318
	internal class UnorderedDeliveryStrategy<ItemType> : DeliveryStrategy<ItemType> where ItemType : class, IDisposable
	{
		// Token: 0x06005870 RID: 22640 RVA: 0x00144F20 File Offset: 0x00143120
		public UnorderedDeliveryStrategy(InputQueueChannel<ItemType> channel, int quota) : base(channel, quota)
		{
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x00144F2A File Offset: 0x0014312A
		public override int EnqueuedCount
		{
			get
			{
				return base.Channel.InternalPendingItems;
			}
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x00144F37 File Offset: 0x00143137
		public override bool CanEnqueue(long sequenceNumber)
		{
			return this.EnqueuedCount < base.Quota;
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x00144F47 File Offset: 0x00143147
		public override bool Enqueue(ItemType item, long sequenceNumber)
		{
			return base.Channel.EnqueueWithoutDispatch(item, base.DequeueCallback);
		}
	}
}
