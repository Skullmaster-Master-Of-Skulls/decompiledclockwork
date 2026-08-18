using System;

namespace System.ServiceModel
{
	// Token: 0x02000117 RID: 279
	internal static class TimeSpanHelper
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		public static TimeSpan FromMinutes(int minutes, string text)
		{
			return TimeSpan.FromTicks(600000000L * (long)minutes);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		public static TimeSpan FromSeconds(int seconds, string text)
		{
			return TimeSpan.FromTicks(10000000L * (long)seconds);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001E500 File Offset: 0x0001C700
		public static TimeSpan FromMilliseconds(int ms, string text)
		{
			return TimeSpan.FromTicks(10000L * (long)ms);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001E520 File Offset: 0x0001C720
		public static TimeSpan FromDays(int days, string text)
		{
			return TimeSpan.FromTicks(864000000000L * (long)days);
		}
	}
}
