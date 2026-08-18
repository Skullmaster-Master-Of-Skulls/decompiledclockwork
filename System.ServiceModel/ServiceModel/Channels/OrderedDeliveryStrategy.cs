using System;
using System.Collections.Generic;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200090D RID: 2317
	internal class OrderedDeliveryStrategy<ItemType> : DeliveryStrategy<ItemType> where ItemType : class, IDisposable
	{
		// Token: 0x06005868 RID: 22632 RVA: 0x00144D3A File Offset: 0x00142F3A
		public OrderedDeliveryStrategy(InputQueueChannel<ItemType> channel, int quota, bool isEnqueueInOrder) : base(channel, quota)
		{
			this.isEnqueueInOrder = isEnqueueInOrder;
			this.items = new Dictionary<long, ItemType>();
			this.windowStart = 1L;
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06005869 RID: 22633 RVA: 0x00144D5E File Offset: 0x00142F5E
		public override int EnqueuedCount
		{
			get
			{
				return base.Channel.InternalPendingItems + this.items.Count;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x0600586A RID: 22634 RVA: 0x00144D77 File Offset: 0x00142F77
		private Action<object> OnDispatchCallback
		{
			get
			{
				if (this.onDispatchCallback == null)
				{
					this.onDispatchCallback = new Action<object>(this.OnDispatch);
				}
				return this.onDispatchCallback;
			}
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x00144D9C File Offset: 0x00142F9C
		public override bool CanEnqueue(long sequenceNumber)
		{
			return this.EnqueuedCount < base.Quota && (!this.isEnqueueInOrder || sequenceNumber <= this.windowStart) && (long)base.Channel.InternalPendingItems + sequenceNumber - this.windowStart < (long)base.Quota;
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x00144DEC File Offset: 0x00142FEC
		public override bool Enqueue(ItemType item, long sequenceNumber)
		{
			if (sequenceNumber > this.windowStart)
			{
				this.items.Add(sequenceNumber, item);
				return false;
			}
			this.windowStart += 1L;
			while (this.items.ContainsKey(this.windowStart))
			{
				if (base.Channel.EnqueueWithoutDispatch(item, base.DequeueCallback))
				{
					ActionItem.Schedule(this.OnDispatchCallback, null);
				}
				item = this.items[this.windowStart];
				this.items.Remove(this.windowStart);
				this.windowStart += 1L;
			}
			return base.Channel.EnqueueWithoutDispatch(item, base.DequeueCallback);
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x00144E9C File Offset: 0x0014309C
		private static void DisposeItems(Dictionary<long, ItemType>.Enumerator items)
		{
			if (items.MoveNext())
			{
				KeyValuePair<long, ItemType> keyValuePair = items.Current;
				using (keyValuePair.Value)
				{
					OrderedDeliveryStrategy<ItemType>.DisposeItems(items);
				}
			}
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x00144EF0 File Offset: 0x001430F0
		public override void Dispose()
		{
			OrderedDeliveryStrategy<ItemType>.DisposeItems(this.items.GetEnumerator());
			this.items.Clear();
			base.Dispose();
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x00144F13 File Offset: 0x00143113
		private void OnDispatch(object state)
		{
			base.Channel.Dispatch();
		}

		// Token: 0x04003631 RID: 13873
		private bool isEnqueueInOrder;

		// Token: 0x04003632 RID: 13874
		private Dictionary<long, ItemType> items;

		// Token: 0x04003633 RID: 13875
		private Action<object> onDispatchCallback;

		// Token: 0x04003634 RID: 13876
		private long windowStart;
	}
}
