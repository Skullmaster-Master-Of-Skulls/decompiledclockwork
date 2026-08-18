using System;

namespace NLog.Time
{
	// Token: 0x0200018A RID: 394
	[TimeSource("AccurateLocal")]
	public class AccurateLocalTimeSource : TimeSource
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x000239AB File Offset: 0x00021BAB
		public override DateTime Time
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000239B2 File Offset: 0x00021BB2
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToLocalTime();
		}
	}
}
