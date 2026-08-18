using System;

namespace Org.BouncyCastle.Utilities.Date
{
	// Token: 0x02000468 RID: 1128
	public class DateTimeUtilities
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x000E8384 File Offset: 0x000E7384
		private DateTimeUtilities()
		{
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000E838C File Offset: 0x000E738C
		public static long DateTimeToUnixMs(DateTime dateTime)
		{
			if (dateTime.CompareTo(DateTimeUtilities.UnixEpoch) < 0)
			{
				throw new ArgumentException("DateTime value may not be before the epoch", "dateTime");
			}
			return (dateTime.Ticks - DateTimeUtilities.UnixEpoch.Ticks) / 10000L;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000E83D4 File Offset: 0x000E73D4
		public static DateTime UnixMsToDateTime(long unixMs)
		{
			return new DateTime(unixMs * 10000L + DateTimeUtilities.UnixEpoch.Ticks);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000E83FC File Offset: 0x000E73FC
		public static long CurrentUnixMs()
		{
			return DateTimeUtilities.DateTimeToUnixMs(DateTime.UtcNow);
		}

		// Token: 0x04001AA5 RID: 6821
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
