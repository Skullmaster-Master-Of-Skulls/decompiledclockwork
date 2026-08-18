using System;

namespace System.Web.Hosting
{
	// Token: 0x02000789 RID: 1929
	public class RecycleLimitObserver : IObserver<RecycleLimitInfo>
	{
		// Token: 0x06005C66 RID: 23654 RVA: 0x00006164 File Offset: 0x00004364
		public void OnCompleted()
		{
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x00006164 File Offset: 0x00004364
		public void OnError(Exception error)
		{
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x0013FB94 File Offset: 0x0013DD94
		public void OnNext(RecycleLimitInfo recycleLimitInfo)
		{
			if (recycleLimitInfo.TrimFrequency == RecycleLimitNotificationFrequency.High)
			{
				this._lastTrimPercent = Math.Min(50, this._lastTrimPercent + 10);
			}
			else if (this._lastTrimPercent > 10 && recycleLimitInfo.TrimFrequency == RecycleLimitNotificationFrequency.Low)
			{
				this._lastTrimPercent = Math.Max(10, this._lastTrimPercent - 10);
			}
			long num = HostingEnvironment.TrimCache(this._lastTrimPercent);
			recycleLimitInfo.RequestGC = (num > 0L);
		}

		// Token: 0x040030AF RID: 12463
		private int _lastTrimPercent = 10;
	}
}
