using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000217 RID: 535
	internal class ReadWriteObjectLock
	{
		// Token: 0x060019DC RID: 6620 RVA: 0x000030B5 File Offset: 0x000012B5
		internal ReadWriteObjectLock()
		{
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00050CE4 File Offset: 0x0004EEE4
		internal virtual void AcquireRead()
		{
			lock (this)
			{
				while (this._lock == -1)
				{
					try
					{
						Monitor.Wait(this);
					}
					catch (ThreadInterruptedException)
					{
					}
				}
				this._lock++;
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00050D4C File Offset: 0x0004EF4C
		internal virtual void ReleaseRead()
		{
			lock (this)
			{
				this._lock--;
				if (this._lock == 0)
				{
					Monitor.PulseAll(this);
				}
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00050DA0 File Offset: 0x0004EFA0
		internal virtual void AcquireWrite()
		{
			lock (this)
			{
				while (this._lock != 0)
				{
					try
					{
						Monitor.Wait(this);
					}
					catch (ThreadInterruptedException)
					{
					}
				}
				this._lock = -1;
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00050E00 File Offset: 0x0004F000
		internal virtual void ReleaseWrite()
		{
			lock (this)
			{
				this._lock = 0;
				Monitor.PulseAll(this);
			}
		}

		// Token: 0x040017F0 RID: 6128
		private int _lock;
	}
}
