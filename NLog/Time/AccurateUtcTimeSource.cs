using System;

namespace NLog.Time
{
	// Token: 0x0200018B RID: 395
	[TimeSource("AccurateUTC")]
	public class AccurateUtcTimeSource : TimeSource
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000239C3 File Offset: 0x00021BC3
		public override DateTime Time
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000239CA File Offset: 0x00021BCA
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToUniversalTime();
		}
	}
}
