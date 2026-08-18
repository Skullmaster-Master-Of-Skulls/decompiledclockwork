using System;
using System.Globalization;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000241 RID: 577
	internal static class DateTimeStringUtilities
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x000E1C10 File Offset: 0x000DFE10
		internal static string ToString(int year, int month, int day, int hours, int minutes, int seconds, int nanos = 0, string region = null)
		{
			string text = string.Empty;
			if (year < 0)
			{
				year *= -1;
				text = "-";
			}
			text += string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2} {3}:{4}:{5}", new object[]
			{
				month.ToString("D2"),
				day.ToString("D2"),
				year.ToString("D4"),
				hours.ToString("D2"),
				minutes.ToString("D2"),
				seconds.ToString("D2")
			});
			if (nanos > 0)
			{
				text += ((double)nanos / 1000000000.0).ToString("F9", CultureInfo.InvariantCulture).Trim(new char[]
				{
					'0'
				});
			}
			if (!string.IsNullOrWhiteSpace(region))
			{
				text = text + " " + region;
			}
			return text;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000E1D00 File Offset: 0x000DFF00
		internal static void FromString(string stringRep, out int year, out int month, out int day, out int hours, out int minutes, out int seconds, out int nanos, out string region, bool expectNoRegion = false, bool expectNoNanos = false)
		{
			region = null;
			year = (month = (day = (hours = (minutes = (seconds = (nanos = 0))))));
			if (stringRep == null)
			{
				throw new ArgumentNullException();
			}
			string text = stringRep.Trim();
			if (text.Length == 0)
			{
				throw new FormatException();
			}
			bool flag = false;
			if (text[0] == '-')
			{
				flag = true;
			}
			if (flag || text[0] == '+')
			{
				text = text.Substring(1).TrimStart(new char[0]);
			}
			string[] array = text.Split(DateTimeStringUtilities.space, 3, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim();
				switch (i)
				{
				case 0:
				{
					string[] array2 = text2.Split(DateTimeStringUtilities.slash, StringSplitOptions.RemoveEmptyEntries);
					if (array2.Length != 3)
					{
						throw new FormatException();
					}
					month = int.Parse(array2[0]);
					if (month < 1 || month > 12)
					{
						throw new FormatException();
					}
					day = int.Parse(array2[1]);
					if (day < 1 || day > 31)
					{
						throw new FormatException();
					}
					year = int.Parse(array2[2]);
					if (flag)
					{
						year *= -1;
					}
					if (year < -4712 || year > 9999)
					{
						throw new FormatException();
					}
					break;
				}
				case 1:
				{
					string[] array3 = text2.Split(DateTimeStringUtilities.colon, StringSplitOptions.RemoveEmptyEntries);
					if (array3.Length != 3)
					{
						throw new FormatException();
					}
					hours = int.Parse(array3[0]);
					if (hours < 0 || hours > 23)
					{
						throw new FormatException();
					}
					minutes = int.Parse(array3[1]);
					if (minutes < 0 || minutes > 59)
					{
						throw new FormatException();
					}
					string[] array4 = array3[2].Split(DateTimeStringUtilities.dot, StringSplitOptions.RemoveEmptyEntries);
					seconds = int.Parse(array4[0]);
					if (seconds < 0 || seconds > 59)
					{
						throw new FormatException();
					}
					if (array4.Length == 2)
					{
						if (expectNoNanos)
						{
							throw new FormatException();
						}
						string s = "." + array4[1];
						nanos = (int)(1000000000.0 * double.Parse(s, CultureInfo.InvariantCulture));
						if (nanos < 0 || nanos > 999999999)
						{
							throw new FormatException();
						}
					}
					break;
				}
				case 2:
					if (!string.IsNullOrWhiteSpace(text2))
					{
						if (expectNoRegion)
						{
							throw new FormatException();
						}
						region = text2;
					}
					break;
				default:
					throw new FormatException();
				}
			}
		}

		// Token: 0x04001985 RID: 6533
		private const char ZERO = '0';

		// Token: 0x04001986 RID: 6534
		private const int DATE_TOKEN = 0;

		// Token: 0x04001987 RID: 6535
		private const int TIME_TOKEN = 1;

		// Token: 0x04001988 RID: 6536
		private const int REGION_TOKEN = 2;

		// Token: 0x04001989 RID: 6537
		private const int TOKEN_COUNT = 3;

		// Token: 0x0400198A RID: 6538
		private static char[] space = new char[]
		{
			' '
		};

		// Token: 0x0400198B RID: 6539
		private static char[] slash = new char[]
		{
			'/'
		};

		// Token: 0x0400198C RID: 6540
		private static char[] colon = new char[]
		{
			':'
		};

		// Token: 0x0400198D RID: 6541
		private static char[] dot = new char[]
		{
			'.'
		};
	}
}
