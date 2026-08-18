using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008BC RID: 2236
	internal abstract class QueuedObjectPool<T>
	{
		// Token: 0x06005548 RID: 21832 RVA: 0x00139347 File Offset: 0x00137547
		protected void Initialize(int batchAllocCount, int maxFreeCount)
		{
			if (batchAllocCount <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("batchAllocCount"));
			}
			this.batchAllocCount = batchAllocCount;
			this.maxFreeCount = maxFreeCount;
			this.objectQueue = new Queue<T>(batchAllocCount);
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06005549 RID: 21833 RVA: 0x0013937C File Offset: 0x0013757C
		private object ThisLock
		{
			get
			{
				return this.objectQueue;
			}
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00139384 File Offset: 0x00137584
		public virtual bool Return(T value)
		{
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.objectQueue.Count < this.maxFreeCount && !this.isClosed)
				{
					this.objectQueue.Enqueue(value);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x001393EC File Offset: 0x001375EC
		public T Take()
		{
			object thisLock = this.ThisLock;
			T result;
			lock (thisLock)
			{
				if (this.objectQueue.Count == 0)
				{
					this.AllocObjects();
				}
				result = this.objectQueue.Dequeue();
			}
			return result;
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00139448 File Offset: 0x00137648
		public void Close()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				foreach (T t in this.objectQueue)
				{
					if (t != null)
					{
						this.CleanupItem(t);
					}
				}
				this.objectQueue.Clear();
				this.isClosed = true;
			}
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x001394E0 File Offset: 0x001376E0
		protected virtual void CleanupItem(T item)
		{
		}

		// Token: 0x0600554E RID: 21838
		protected abstract T Create();

		// Token: 0x0600554F RID: 21839 RVA: 0x001394E4 File Offset: 0x001376E4
		private void AllocObjects()
		{
			for (int i = 0; i < this.batchAllocCount; i++)
			{
				this.objectQueue.Enqueue(this.Create());
			}
		}

		// Token: 0x04003370 RID: 13168
		private Queue<T> objectQueue;

		// Token: 0x04003371 RID: 13169
		private bool isClosed;

		// Token: 0x04003372 RID: 13170
		private int batchAllocCount;

		// Token: 0x04003373 RID: 13171
		private int maxFreeCount;
	}
}
