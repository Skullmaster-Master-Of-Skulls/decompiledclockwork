using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020001B6 RID: 438
	internal static class HttpDateParse
	{
		// Token: 0x06001146 RID: 4422 RVA: 0x0005E060 File Offset: 0x0005C260
		private static char MAKE_UPPER(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0005E070 File Offset: 0x0005C270
		private static int MapDayMonthToDword(char[] lpszDay, int index)
		{
			switch (HttpDateParse.MAKE_UPPER(lpszDay[index]))
			{
			case 'A':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'P')
				{
					return 4;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 8;
			}
			case 'D':
				return 12;
			case 'F':
			{
				char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c2 == 'E')
				{
					return 2;
				}
				if (c2 == 'R')
				{
					return 5;
				}
				return -999;
			}
			case 'G':
				return -1000;
			case 'J':
			{
				char c3 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c3 != 'A')
				{
					if (c3 == 'U')
					{
						char c4 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
						if (c4 == 'L')
						{
							return 7;
						}
						if (c4 == 'N')
						{
							return 6;
						}
					}
					return -999;
				}
				return 1;
			}
			case 'M':
			{
				char c5 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c5 != 'A')
				{
					if (c5 == 'O')
					{
						return 1;
					}
				}
				else
				{
					char c6 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
					if (c6 == 'R')
					{
						return 3;
					}
					if (c6 == 'Y')
					{
						return 5;
					}
				}
				return -999;
			}
			case 'N':
				return 11;
			case 'O':
				return 10;
			case 'S':
			{
				char c7 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c7 == 'A')
				{
					return 6;
				}
				if (c7 == 'E')
				{
					return 9;
				}
				if (c7 != 'U')
				{
					return -999;
				}
				return 0;
			}
			case 'T':
			{
				char c8 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c8 == 'H')
				{
					return 4;
				}
				if (c8 == 'U')
				{
					return 2;
				}
				return -999;
			}
			case 'U':
				return -1000;
			case 'W':
				return 3;
			}
			return -999;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0005E214 File Offset: 0x0005C414
		public static bool ParseHttpDate(string DateString, out DateTime dtOut)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			int[] array = new int[8];
			bool result = true;
			char[] array2 = DateString.ToCharArray();
			dtOut = default(DateTime);
			while (num < DateString.Length && num2 < 8)
			{
				if (array2[num] >= '0' && array2[num] <= '9')
				{
					array[num2] = 0;
					do
					{
						array[num2] *= 10;
						array[num2] += (int)(array2[num] - '0');
						num++;
					}
					while (num < DateString.Length && array2[num] >= '0' && array2[num] <= '9');
					num2++;
				}
				else if ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z'))
				{
					array[num2] = HttpDateParse.MapDayMonthToDword(array2, num);
					num3 = num2;
					if (array[num2] == -999 && (!flag || num2 != 6))
					{
						result = false;
						return result;
					}
					if (num2 == 1)
					{
						flag = true;
					}
					do
					{
						num++;
					}
					while (num < DateString.Length && ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z')));
					num2++;
				}
				else
				{
					num++;
				}
			}
			int millisecond = 0;
			int num4;
			int month;
			int num5;
			int num6;
			int num7;
			int num8;
			if (flag)
			{
				num4 = array[2];
				month = array[1];
				num5 = array[3];
				num6 = array[4];
				num7 = array[5];
				if (num3 != 6)
				{
					num8 = array[6];
				}
				else
				{
					num8 = array[7];
				}
			}
			else
			{
				num4 = array[1];
				month = array[2];
				num8 = array[3];
				num5 = array[4];
				num6 = array[5];
				num7 = array[6];
			}
			if (num8 < 100)
			{
				num8 += ((num8 < 80) ? 2000 : 1900);
			}
			if (num2 < 4 || num4 > 31 || num5 > 23 || num6 > 59 || num7 > 59)
			{
				return false;
			}
			dtOut = new DateTime(num8, month, num4, num5, num6, num7, millisecond);
			if (num3 == 6)
			{
				dtOut = dtOut.ToUniversalTime();
			}
			if (num2 > 7 && array[7] != -1000)
			{
				double value = (double)array[7];
				dtOut.AddHours(value);
			}
			dtOut = dtOut.ToLocalTime();
			return result;
		}

		// Token: 0x04001420 RID: 5152
		private const int BASE_DEC = 10;

		// Token: 0x04001421 RID: 5153
		private const int DATE_INDEX_DAY_OF_WEEK = 0;

		// Token: 0x04001422 RID: 5154
		private const int DATE_1123_INDEX_DAY = 1;

		// Token: 0x04001423 RID: 5155
		private const int DATE_1123_INDEX_MONTH = 2;

		// Token: 0x04001424 RID: 5156
		private const int DATE_1123_INDEX_YEAR = 3;

		// Token: 0x04001425 RID: 5157
		private const int DATE_1123_INDEX_HRS = 4;

		// Token: 0x04001426 RID: 5158
		private const int DATE_1123_INDEX_MINS = 5;

		// Token: 0x04001427 RID: 5159
		private const int DATE_1123_INDEX_SECS = 6;

		// Token: 0x04001428 RID: 5160
		private const int DATE_ANSI_INDEX_MONTH = 1;

		// Token: 0x04001429 RID: 5161
		private const int DATE_ANSI_INDEX_DAY = 2;

		// Token: 0x0400142A RID: 5162
		private const int DATE_ANSI_INDEX_HRS = 3;

		// Token: 0x0400142B RID: 5163
		private const int DATE_ANSI_INDEX_MINS = 4;

		// Token: 0x0400142C RID: 5164
		private const int DATE_ANSI_INDEX_SECS = 5;

		// Token: 0x0400142D RID: 5165
		private const int DATE_ANSI_INDEX_YEAR = 6;

		// Token: 0x0400142E RID: 5166
		private const int DATE_INDEX_TZ = 7;

		// Token: 0x0400142F RID: 5167
		private const int DATE_INDEX_LAST = 7;

		// Token: 0x04001430 RID: 5168
		private const int MAX_FIELD_DATE_ENTRIES = 8;

		// Token: 0x04001431 RID: 5169
		private const int DATE_TOKEN_JANUARY = 1;

		// Token: 0x04001432 RID: 5170
		private const int DATE_TOKEN_FEBRUARY = 2;

		// Token: 0x04001433 RID: 5171
		private const int DATE_TOKEN_MARCH = 3;

		// Token: 0x04001434 RID: 5172
		private const int DATE_TOKEN_APRIL = 4;

		// Token: 0x04001435 RID: 5173
		private const int DATE_TOKEN_MAY = 5;

		// Token: 0x04001436 RID: 5174
		private const int DATE_TOKEN_JUNE = 6;

		// Token: 0x04001437 RID: 5175
		private const int DATE_TOKEN_JULY = 7;

		// Token: 0x04001438 RID: 5176
		private const int DATE_TOKEN_AUGUST = 8;

		// Token: 0x04001439 RID: 5177
		private const int DATE_TOKEN_SEPTEMBER = 9;

		// Token: 0x0400143A RID: 5178
		private const int DATE_TOKEN_OCTOBER = 10;

		// Token: 0x0400143B RID: 5179
		private const int DATE_TOKEN_NOVEMBER = 11;

		// Token: 0x0400143C RID: 5180
		private const int DATE_TOKEN_DECEMBER = 12;

		// Token: 0x0400143D RID: 5181
		private const int DATE_TOKEN_LAST_MONTH = 13;

		// Token: 0x0400143E RID: 5182
		private const int DATE_TOKEN_SUNDAY = 0;

		// Token: 0x0400143F RID: 5183
		private const int DATE_TOKEN_MONDAY = 1;

		// Token: 0x04001440 RID: 5184
		private const int DATE_TOKEN_TUESDAY = 2;

		// Token: 0x04001441 RID: 5185
		private const int DATE_TOKEN_WEDNESDAY = 3;

		// Token: 0x04001442 RID: 5186
		private const int DATE_TOKEN_THURSDAY = 4;

		// Token: 0x04001443 RID: 5187
		private const int DATE_TOKEN_FRIDAY = 5;

		// Token: 0x04001444 RID: 5188
		private const int DATE_TOKEN_SATURDAY = 6;

		// Token: 0x04001445 RID: 5189
		private const int DATE_TOKEN_LAST_DAY = 7;

		// Token: 0x04001446 RID: 5190
		private const int DATE_TOKEN_GMT = -1000;

		// Token: 0x04001447 RID: 5191
		private const int DATE_TOKEN_LAST = -1000;

		// Token: 0x04001448 RID: 5192
		private const int DATE_TOKEN_ERROR = -999;
	}
}
