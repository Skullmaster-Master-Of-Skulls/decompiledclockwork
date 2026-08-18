using System;
using System.Threading;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FD RID: 253
	public class SemaphoreLight
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00024C96 File Offset: 0x00022E96
		public SemaphoreLight(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", "The value cannot be negative.");
			}
			this._currentCount = initialCount;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00024CC4 File Offset: 0x00022EC4
		public int CurrentCount
		{
			get
			{
				return this._currentCount;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00024CCC File Offset: 0x00022ECC
		public int Release()
		{
			return this.Release(1);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00024CD8 File Offset: 0x00022ED8
		public int Release(int releaseCount)
		{
			int currentCount = this._currentCount;
			object @lock = this._lock;
			lock (@lock)
			{
				this._currentCount += releaseCount;
				Monitor.Pulse(this._lock);
			}
			return currentCount;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00024D34 File Offset: 0x00022F34
		public void Wait()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				while (this._currentCount < 1)
				{
					Monitor.Wait(this._lock);
				}
				this._currentCount--;
				Monitor.Pulse(this._lock);
			}
		}

		// Token: 0x04000409 RID: 1033
		private readonly object _lock = new object();

		// Token: 0x0400040A RID: 1034
		private int _currentCount;
	}
}
