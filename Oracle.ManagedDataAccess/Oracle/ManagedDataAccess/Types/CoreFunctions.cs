using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200025C RID: 604
	internal static class CoreFunctions
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x00102398 File Offset: 0x00100598
		internal static bool IsLeapYear(int y)
		{
			if (y % 4 != 0)
			{
				return false;
			}
			if (y > 1582)
			{
				return y % 100 != 0 || y % 400 == 0;
			}
			return y != -4712;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x001023C8 File Offset: 0x001005C8
		internal static int Cal2Days(int y, int m, int d)
		{
			return CoreFunctions.ldidom[m] + d + ((m >= 3 && CoreFunctions.IsLeapYear(y)) ? 1 : 0);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x001023E4 File Offset: 0x001005E4
		internal static int DateToJulianDays(int year, int month, int day)
		{
			int num;
			if (year == -4712)
			{
				num = 0;
			}
			else
			{
				num = year + 4712;
				num = 365 * num + (num - 1) / 4;
			}
			if (year >= 1583)
			{
				num = num - 10 - (year - 1501) / 100 + (year - 1201) / 400;
			}
			num += CoreFunctions.Cal2Days(year, month, day);
			if (year == 1582 && ((month == 10 && day >= 15) || month >= 11))
			{
				num -= 10;
			}
			return num;
		}

		// Token: 0x04001AB0 RID: 6832
		internal static int[] ldidom = new int[]
		{
			0,
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};
	}
}
