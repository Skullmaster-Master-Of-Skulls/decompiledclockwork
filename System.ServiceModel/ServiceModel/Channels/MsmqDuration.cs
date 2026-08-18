using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000902 RID: 2306
	internal static class MsmqDuration
	{
		// Token: 0x060057EF RID: 22511 RVA: 0x001435CC File Offset: 0x001417CC
		public static int FromTimeSpan(TimeSpan timeSpan)
		{
			long num = (long)timeSpan.TotalSeconds;
			if (num > 2147483647L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTimeSpanTooLarge")));
			}
			return (int)num;
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x00143607 File Offset: 0x00141807
		public static TimeSpan ToTimeSpan(int seconds)
		{
			return TimeSpan.FromSeconds((double)seconds);
		}
	}
}
