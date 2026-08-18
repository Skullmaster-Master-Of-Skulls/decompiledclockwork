using System;

namespace System.Web.Hosting
{
	// Token: 0x0200078B RID: 1931
	public class LowPhysicalMemoryObserver : IObserver<LowPhysicalMemoryInfo>
	{
		// Token: 0x06005C74 RID: 23668 RVA: 0x00006164 File Offset: 0x00004364
		public void OnCompleted()
		{
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00006164 File Offset: 0x00004364
		public void OnError(Exception error)
		{
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x0013FF50 File Offset: 0x0013E150
		public void OnNext(LowPhysicalMemoryInfo lowMemoryInfo)
		{
			int num = 0;
			int num2 = GC.CollectionCount(2);
			DateTime utcNow = DateTime.UtcNow;
			if (num2 != this._lastTrimGen2Count)
			{
				long ticks = utcNow.Subtract(this._lastTrimTime).Ticks;
				if (ticks > 0L)
				{
					num = Math.Min(50, (int)((long)this._lastTrimPercent * (long)((ulong)-1294967296) / ticks));
					num = Math.Max(10, num);
				}
				HostingEnvironment.TrimCache(num);
				this._lastTrimGen2Count = num2;
				this._lastTrimTime = utcNow;
				this._lastTrimPercent = num;
			}
		}

		// Token: 0x040030B6 RID: 12470
		private const int MIN_TOTAL_MEMORY_TRIM_PERCENT = 10;

		// Token: 0x040030B7 RID: 12471
		private const long TARGET_TOTAL_MEMORY_TRIM_INTERVAL_TICKS = 3000000000L;

		// Token: 0x040030B8 RID: 12472
		private int _lastTrimPercent = 10;

		// Token: 0x040030B9 RID: 12473
		private int _lastTrimGen2Count = -1;

		// Token: 0x040030BA RID: 12474
		private DateTime _lastTrimTime = DateTime.MinValue;
	}
}
