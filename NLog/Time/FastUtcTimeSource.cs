using System;

namespace NLog.Time
{
	// Token: 0x0200018E RID: 398
	[TimeSource("FastUTC")]
	public class FastUtcTimeSource : CachedTimeSource
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00023A47 File Offset: 0x00021C47
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00023A4E File Offset: 0x00021C4E
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToUniversalTime();
		}
	}
}
