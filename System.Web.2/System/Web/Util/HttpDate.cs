using System;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000213 RID: 531
	internal static class HttpDate
	{
		// Token: 0x060019B6 RID: 6582 RVA: 0x000502B0 File Offset: 0x0004E4B0
		private static int atoi2(string s, int startIndex)
		{
			int result;
			try
			{
				int num = (int)(s[startIndex] - '0');
				int num2 = (int)(s[1 + startIndex] - '0');
				result = HttpDate.s_tensDigit[num] + num2;
			}
			catch
			{
				throw new FormatException(SR.GetString("Atio2BadString", new object[]
				{
					s,
					startIndex
				}));
			}
			return result;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00050318 File Offset: 0x0004E518
		private static int make_month(string s, int startIndex)
		{
			int num = (int)(s[2 + startIndex] - '@' & '?');
			sbyte b = HttpDate.s_monthIndexTable[num];
			if (b >= 13)
			{
				if (b == 78)
				{
					if (HttpDate.s_monthIndexTable[(int)(s[1 + startIndex] - '@' & '?')] == 65)
					{
						b = 1;
					}
					else
					{
						b = 6;
					}
				}
				else
				{
					if (b != 82)
					{
						throw new FormatException(SR.GetString("MakeMonthBadString", new object[]
						{
							s,
							startIndex
						}));
					}
					if (HttpDate.s_monthIndexTable[(int)(s[1 + startIndex] - '@' & '?')] == 65)
					{
						b = 3;
					}
					else
					{
						b = 4;
					}
				}
			}
			string text = HttpDate.s_months[(int)(b - 1)];
			if (s[startIndex] == text[0] && s[1 + startIndex] == text[1] && s[2 + startIndex] == text[2])
			{
				return (int)b;
			}
			if (char.ToUpper(s[startIndex], CultureInfo.InvariantCulture) == text[0] && char.ToLower(s[1 + startIndex], CultureInfo.InvariantCulture) == text[1] && char.ToLower(s[2 + startIndex], CultureInfo.InvariantCulture) == text[2])
			{
				return (int)b;
			}
			throw new FormatException(SR.GetString("MakeMonthBadString", new object[]
			{
				s,
				startIndex
			}));
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00050468 File Offset: 0x0004E668
		internal static DateTime UtcParse(string time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			int num;
			int day;
			int month;
			int num3;
			int hour;
			int minute;
			int second;
			if ((num = time.IndexOf(',')) != -1)
			{
				int num2 = time.Length - num;
				while (--num2 > 0 && time[++num] == ' ')
				{
				}
				if (time[num + 2] == '-')
				{
					if (num2 < 18)
					{
						throw new FormatException(SR.GetString("UtilParseDateTimeBad", new object[]
						{
							time
						}));
					}
					day = HttpDate.atoi2(time, num);
					month = HttpDate.make_month(time, num + 3);
					num3 = HttpDate.atoi2(time, num + 7);
					if (num3 < 50)
					{
						num3 += 2000;
					}
					else
					{
						num3 += 1900;
					}
					hour = HttpDate.atoi2(time, num + 10);
					minute = HttpDate.atoi2(time, num + 13);
					second = HttpDate.atoi2(time, num + 16);
				}
				else
				{
					if (num2 < 20)
					{
						throw new FormatException(SR.GetString("UtilParseDateTimeBad", new object[]
						{
							time
						}));
					}
					day = HttpDate.atoi2(time, num);
					month = HttpDate.make_month(time, num + 3);
					num3 = HttpDate.atoi2(time, num + 7) * 100 + HttpDate.atoi2(time, num + 9);
					hour = HttpDate.atoi2(time, num + 12);
					minute = HttpDate.atoi2(time, num + 15);
					second = HttpDate.atoi2(time, num + 18);
				}
			}
			else
			{
				num = -1;
				int num4 = time.Length + 1;
				while (--num4 > 0 && time[++num] == ' ')
				{
				}
				if (num4 < 24)
				{
					throw new FormatException(SR.GetString("UtilParseDateTimeBad", new object[]
					{
						time
					}));
				}
				day = HttpDate.atoi2(time, num + 8);
				month = HttpDate.make_month(time, num + 4);
				num3 = HttpDate.atoi2(time, num + 20) * 100 + HttpDate.atoi2(time, num + 22);
				hour = HttpDate.atoi2(time, num + 11);
				minute = HttpDate.atoi2(time, num + 14);
				second = HttpDate.atoi2(time, num + 17);
			}
			return new DateTime(num3, month, day, hour, minute, second, DateTimeKind.Utc);
		}

		// Token: 0x040017E3 RID: 6115
		private static readonly int[] s_tensDigit = new int[]
		{
			0,
			10,
			20,
			30,
			40,
			50,
			60,
			70,
			80,
			90
		};

		// Token: 0x040017E4 RID: 6116
		private static readonly string[] s_days = new string[]
		{
			"Sun",
			"Mon",
			"Tue",
			"Wed",
			"Thu",
			"Fri",
			"Sat"
		};

		// Token: 0x040017E5 RID: 6117
		private static readonly string[] s_months = new string[]
		{
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"May",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec"
		};

		// Token: 0x040017E6 RID: 6118
		private static readonly sbyte[] s_monthIndexTable = new sbyte[]
		{
			-1,
			65,
			2,
			12,
			-1,
			-1,
			-1,
			8,
			-1,
			-1,
			-1,
			-1,
			7,
			-1,
			78,
			-1,
			9,
			-1,
			82,
			-1,
			10,
			-1,
			11,
			-1,
			-1,
			5,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			65,
			2,
			12,
			-1,
			-1,
			-1,
			8,
			-1,
			-1,
			-1,
			-1,
			7,
			-1,
			78,
			-1,
			9,
			-1,
			82,
			-1,
			10,
			-1,
			11,
			-1,
			-1,
			5,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		};
	}
}
