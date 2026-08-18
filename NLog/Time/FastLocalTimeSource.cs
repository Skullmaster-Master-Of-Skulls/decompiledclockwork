using System;

namespace NLog.Time
{
	// Token: 0x0200018D RID: 397
	[TimeSource("FastLocal")]
	public class FastLocalTimeSource : CachedTimeSource
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00023A2F File Offset: 0x00021C2F
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00023A36 File Offset: 0x00021C36
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToLocalTime();
		}
	}
}
