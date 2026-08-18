using System;
using System.Globalization;
using System.Threading;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x02000726 RID: 1830
	internal static class UtcNowGenerator
	{
		// Token: 0x06004B51 RID: 19281 RVA: 0x00161584 File Offset: 0x0015F784
		public static DateTime UtcNow()
		{
			DateTime dateTime = DateTime.UtcNow;
			DateTime value = UtcNowGenerator._lastNow.Value;
			if (dateTime <= value || dateTime.ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture).Equals(value.ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture), StringComparison.Ordinal))
			{
				dateTime = value.AddMilliseconds(100.0);
			}
			UtcNowGenerator._lastNow.Value = dateTime;
			return dateTime;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x001615F4 File Offset: 0x0015F7F4
		public static string UtcNowAsMigrationIdTimestamp()
		{
			return UtcNowGenerator.UtcNow().ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture);
		}

		// Token: 0x04001B67 RID: 7015
		public const string MigrationIdFormat = "yyyyMMddHHmmssf";

		// Token: 0x04001B68 RID: 7016
		private static readonly ThreadLocal<DateTime> _lastNow = new ThreadLocal<DateTime>(() => DateTime.UtcNow);
	}
}
