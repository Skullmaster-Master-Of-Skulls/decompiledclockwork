using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020002D1 RID: 721
	internal struct XsdDuration
	{
		// Token: 0x06002B1A RID: 11034 RVA: 0x000E0F5C File Offset: 0x000DF15C
		public XsdDuration(bool isNegative, int years, int months, int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (years < 0)
			{
				throw new ArgumentOutOfRangeException("years");
			}
			if (months < 0)
			{
				throw new ArgumentOutOfRangeException("months");
			}
			if (days < 0)
			{
				throw new ArgumentOutOfRangeException("days");
			}
			if (hours < 0)
			{
				throw new ArgumentOutOfRangeException("hours");
			}
			if (minutes < 0)
			{
				throw new ArgumentOutOfRangeException("minutes");
			}
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds");
			}
			if (nanoseconds < 0 || nanoseconds > 999999999)
			{
				throw new ArgumentOutOfRangeException("nanoseconds");
			}
			this.years = years;
			this.months = months;
			this.days = days;
			this.hours = hours;
			this.minutes = minutes;
			this.seconds = seconds;
			this.nanoseconds = (uint)nanoseconds;
			if (isNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000E102B File Offset: 0x000DF22B
		public XsdDuration(TimeSpan timeSpan)
		{
			this = new XsdDuration(timeSpan, XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000E1038 File Offset: 0x000DF238
		public XsdDuration(TimeSpan timeSpan, XsdDuration.DurationType durationType)
		{
			long ticks = timeSpan.Ticks;
			bool flag;
			ulong num;
			if (ticks < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)ticks);
			}
			else
			{
				flag = false;
				num = (ulong)ticks;
			}
			if (durationType == XsdDuration.DurationType.YearMonthDuration)
			{
				int num2 = (int)(num / 315360000000000UL);
				int num3 = (int)(num % 315360000000000UL / 25920000000000UL);
				if (num3 == 12)
				{
					num2++;
					num3 = 0;
				}
				this = new XsdDuration(flag, num2, num3, 0, 0, 0, 0, 0);
				return;
			}
			this.nanoseconds = (uint)(num % 10000000UL) * 100U;
			if (flag)
			{
				this.nanoseconds |= 2147483648U;
			}
			this.years = 0;
			this.months = 0;
			this.days = (int)(num / 864000000000UL);
			this.hours = (int)(num / 36000000000UL % 24UL);
			this.minutes = (int)(num / 600000000UL % 60UL);
			this.seconds = (int)(num / 10000000UL % 60UL);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000E112B File Offset: 0x000DF32B
		public XsdDuration(string s)
		{
			this = new XsdDuration(s, XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000E1138 File Offset: 0x000DF338
		public XsdDuration(string s, XsdDuration.DurationType durationType)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, durationType, out xsdDuration);
			if (ex != null)
			{
				throw ex;
			}
			this.years = xsdDuration.Years;
			this.months = xsdDuration.Months;
			this.days = xsdDuration.Days;
			this.hours = xsdDuration.Hours;
			this.minutes = xsdDuration.Minutes;
			this.seconds = xsdDuration.Seconds;
			this.nanoseconds = (uint)xsdDuration.Nanoseconds;
			if (xsdDuration.IsNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000E11CA File Offset: 0x000DF3CA
		public bool IsNegative
		{
			get
			{
				return (this.nanoseconds & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000E11DB File Offset: 0x000DF3DB
		public int Years
		{
			get
			{
				return this.years;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000E11E3 File Offset: 0x000DF3E3
		public int Months
		{
			get
			{
				return this.months;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000E11EB File Offset: 0x000DF3EB
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000E11F3 File Offset: 0x000DF3F3
		public int Hours
		{
			get
			{
				return this.hours;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000E11FB File Offset: 0x000DF3FB
		public int Minutes
		{
			get
			{
				return this.minutes;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000E1203 File Offset: 0x000DF403
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000E120B File Offset: 0x000DF40B
		public int Nanoseconds
		{
			get
			{
				return (int)(this.nanoseconds & 2147483647U);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000E1219 File Offset: 0x000DF419
		public int Microseconds
		{
			get
			{
				return this.Nanoseconds / 1000;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000E1227 File Offset: 0x000DF427
		public int Milliseconds
		{
			get
			{
				return this.Nanoseconds / 1000000;
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000E1238 File Offset: 0x000DF438
		public XsdDuration Normalize()
		{
			int num = this.Years;
			int num2 = this.Months;
			int num3 = this.Days;
			int num4 = this.Hours;
			int num5 = this.Minutes;
			int num6 = this.Seconds;
			checked
			{
				try
				{
					if (num2 >= 12)
					{
						num += num2 / 12;
						num2 %= 12;
					}
					if (num6 >= 60)
					{
						num5 += num6 / 60;
						num6 %= 60;
					}
					if (num5 >= 60)
					{
						num4 += num5 / 60;
						num5 %= 60;
					}
					if (num4 >= 24)
					{
						num3 += num4 / 24;
						num4 %= 24;
					}
				}
				catch (OverflowException)
				{
					throw new OverflowException(Res.GetString("XmlConvert_Overflow", new object[]
					{
						this.ToString(),
						"Duration"
					}));
				}
				return new XsdDuration(this.IsNegative, num, num2, num3, num4, num5, num6, this.Nanoseconds);
			}
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000E1318 File Offset: 0x000DF518
		public TimeSpan ToTimeSpan()
		{
			return this.ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000E1324 File Offset: 0x000DF524
		public TimeSpan ToTimeSpan(XsdDuration.DurationType durationType)
		{
			TimeSpan result;
			Exception ex = this.TryToTimeSpan(durationType, out result);
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000E1341 File Offset: 0x000DF541
		internal Exception TryToTimeSpan(out TimeSpan result)
		{
			return this.TryToTimeSpan(XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000E134C File Offset: 0x000DF54C
		internal Exception TryToTimeSpan(XsdDuration.DurationType durationType, out TimeSpan result)
		{
			Exception result2 = null;
			ulong num = 0UL;
			checked
			{
				try
				{
					if (durationType != XsdDuration.DurationType.DayTimeDuration)
					{
						num += ((ulong)this.years + (ulong)this.months / 12UL) * 365UL;
						num += (ulong)this.months % 12UL * 30UL;
					}
					if (durationType != XsdDuration.DurationType.YearMonthDuration)
					{
						num += (ulong)this.days;
						num *= 24UL;
						num += (ulong)this.hours;
						num *= 60UL;
						num += (ulong)this.minutes;
						num *= 60UL;
						num += (ulong)this.seconds;
						num *= 10000000UL;
						num += (ulong)this.Nanoseconds / 100UL;
					}
					else
					{
						num *= 864000000000UL;
					}
					if (this.IsNegative)
					{
						if (num == 9223372036854775808UL)
						{
							result = new TimeSpan(long.MinValue);
						}
						else
						{
							result = new TimeSpan(0L - (long)num);
						}
					}
					else
					{
						result = new TimeSpan((long)num);
					}
					return null;
				}
				catch (OverflowException)
				{
					result = TimeSpan.MinValue;
					result2 = new OverflowException(Res.GetString("XmlConvert_Overflow", new object[]
					{
						durationType,
						"TimeSpan"
					}));
				}
				return result2;
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000E148C File Offset: 0x000DF68C
		public override string ToString()
		{
			return this.ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000E1498 File Offset: 0x000DF698
		internal string ToString(XsdDuration.DurationType durationType)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.IsNegative)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append('P');
			if (durationType != XsdDuration.DurationType.DayTimeDuration)
			{
				if (this.years != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.years));
					stringBuilder.Append('Y');
				}
				if (this.months != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.months));
					stringBuilder.Append('M');
				}
			}
			if (durationType != XsdDuration.DurationType.YearMonthDuration)
			{
				if (this.days != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.days));
					stringBuilder.Append('D');
				}
				if (this.hours != 0 || this.minutes != 0 || this.seconds != 0 || this.Nanoseconds != 0)
				{
					stringBuilder.Append('T');
					if (this.hours != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.hours));
						stringBuilder.Append('H');
					}
					if (this.minutes != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.minutes));
						stringBuilder.Append('M');
					}
					int num = this.Nanoseconds;
					if (this.seconds != 0 || num != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.seconds));
						if (num != 0)
						{
							stringBuilder.Append('.');
							int length = stringBuilder.Length;
							stringBuilder.Length += 9;
							int num2 = stringBuilder.Length - 1;
							for (int i = num2; i >= length; i--)
							{
								int num3 = num % 10;
								stringBuilder[i] = (char)(num3 + 48);
								if (num2 == i && num3 == 0)
								{
									num2--;
								}
								num /= 10;
							}
							stringBuilder.Length = num2 + 1;
						}
						stringBuilder.Append('S');
					}
				}
				if (stringBuilder[stringBuilder.Length - 1] == 'P')
				{
					stringBuilder.Append("T0S");
				}
			}
			else if (stringBuilder[stringBuilder.Length - 1] == 'P')
			{
				stringBuilder.Append("0M");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000E168A File Offset: 0x000DF88A
		internal static Exception TryParse(string s, out XsdDuration result)
		{
			return XsdDuration.TryParse(s, XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000E1694 File Offset: 0x000DF894
		internal static Exception TryParse(string s, XsdDuration.DurationType durationType, out XsdDuration result)
		{
			XsdDuration.Parts parts = XsdDuration.Parts.HasNone;
			result = default(XsdDuration);
			s = s.Trim();
			int length = s.Length;
			int num = 0;
			int i = 0;
			if (num < length)
			{
				if (s[num] == '-')
				{
					num++;
					result.nanoseconds = 2147483648U;
				}
				else
				{
					result.nanoseconds = 0U;
				}
				if (num < length && s[num++] == 'P')
				{
					int num2;
					if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) == null)
					{
						if (num >= length)
						{
							goto IL_2D5;
						}
						if (s[num] == 'Y')
						{
							if (i == 0)
							{
								goto IL_2D5;
							}
							parts |= XsdDuration.Parts.HasYears;
							result.years = num2;
							if (++num == length)
							{
								goto IL_2B8;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2F8;
							}
							if (num >= length)
							{
								goto IL_2D5;
							}
						}
						if (s[num] == 'M')
						{
							if (i == 0)
							{
								goto IL_2D5;
							}
							parts |= XsdDuration.Parts.HasMonths;
							result.months = num2;
							if (++num == length)
							{
								goto IL_2B8;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2F8;
							}
							if (num >= length)
							{
								goto IL_2D5;
							}
						}
						if (s[num] == 'D')
						{
							if (i == 0)
							{
								goto IL_2D5;
							}
							parts |= XsdDuration.Parts.HasDays;
							result.days = num2;
							if (++num == length)
							{
								goto IL_2B8;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2F8;
							}
							if (num >= length)
							{
								goto IL_2D5;
							}
						}
						if (s[num] == 'T')
						{
							if (i != 0)
							{
								goto IL_2D5;
							}
							num++;
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2F8;
							}
							if (num >= length)
							{
								goto IL_2D5;
							}
							if (s[num] == 'H')
							{
								if (i == 0)
								{
									goto IL_2D5;
								}
								parts |= XsdDuration.Parts.HasHours;
								result.hours = num2;
								if (++num == length)
								{
									goto IL_2B8;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_2F8;
								}
								if (num >= length)
								{
									goto IL_2D5;
								}
							}
							if (s[num] == 'M')
							{
								if (i == 0)
								{
									goto IL_2D5;
								}
								parts |= XsdDuration.Parts.HasMinutes;
								result.minutes = num2;
								if (++num == length)
								{
									goto IL_2B8;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_2F8;
								}
								if (num >= length)
								{
									goto IL_2D5;
								}
							}
							if (s[num] == '.')
							{
								num++;
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (XsdDuration.TryParseDigits(s, ref num, true, out num2, out i) != null)
								{
									goto IL_2F8;
								}
								if (i == 0)
								{
									num2 = 0;
								}
								while (i > 9)
								{
									num2 /= 10;
									i--;
								}
								while (i < 9)
								{
									num2 *= 10;
									i++;
								}
								result.nanoseconds |= (uint)num2;
								if (num >= length || s[num] != 'S')
								{
									goto IL_2D5;
								}
								if (++num == length)
								{
									goto IL_2B8;
								}
							}
							else if (s[num] == 'S')
							{
								if (i == 0)
								{
									goto IL_2D5;
								}
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (++num == length)
								{
									goto IL_2B8;
								}
							}
						}
						if (i != 0 || num != length)
						{
							goto IL_2D5;
						}
						IL_2B8:
						if (parts != XsdDuration.Parts.HasNone)
						{
							if (durationType == XsdDuration.DurationType.DayTimeDuration)
							{
								if ((parts & (XsdDuration.Parts)3) != XsdDuration.Parts.HasNone)
								{
									goto IL_2D5;
								}
							}
							else if (durationType == XsdDuration.DurationType.YearMonthDuration && (parts & (XsdDuration.Parts)(-4)) != XsdDuration.Parts.HasNone)
							{
								goto IL_2D5;
							}
							return null;
						}
						goto IL_2D5;
					}
					IL_2F8:
					return new OverflowException(Res.GetString("XmlConvert_Overflow", new object[]
					{
						s,
						durationType
					}));
				}
			}
			IL_2D5:
			return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
			{
				s,
				durationType
			}));
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000E19BC File Offset: 0x000DFBBC
		private static string TryParseDigits(string s, ref int offset, bool eatDigits, out int result, out int numDigits)
		{
			int num = offset;
			int length = s.Length;
			result = 0;
			numDigits = 0;
			while (offset < length && s[offset] >= '0' && s[offset] <= '9')
			{
				int num2 = (int)(s[offset] - '0');
				if (result > (2147483647 - num2) / 10)
				{
					if (!eatDigits)
					{
						return "XmlConvert_Overflow";
					}
					numDigits = offset - num;
					while (offset < length && s[offset] >= '0' && s[offset] <= '9')
					{
						offset++;
					}
					return null;
				}
				else
				{
					result = result * 10 + num2;
					offset++;
				}
			}
			numDigits = offset - num;
			return null;
		}

		// Token: 0x040012C1 RID: 4801
		private int years;

		// Token: 0x040012C2 RID: 4802
		private int months;

		// Token: 0x040012C3 RID: 4803
		private int days;

		// Token: 0x040012C4 RID: 4804
		private int hours;

		// Token: 0x040012C5 RID: 4805
		private int minutes;

		// Token: 0x040012C6 RID: 4806
		private int seconds;

		// Token: 0x040012C7 RID: 4807
		private uint nanoseconds;

		// Token: 0x040012C8 RID: 4808
		private const uint NegativeBit = 2147483648U;

		// Token: 0x020004B7 RID: 1207
		private enum Parts
		{
			// Token: 0x04001F75 RID: 8053
			HasNone,
			// Token: 0x04001F76 RID: 8054
			HasYears,
			// Token: 0x04001F77 RID: 8055
			HasMonths,
			// Token: 0x04001F78 RID: 8056
			HasDays = 4,
			// Token: 0x04001F79 RID: 8057
			HasHours = 8,
			// Token: 0x04001F7A RID: 8058
			HasMinutes = 16,
			// Token: 0x04001F7B RID: 8059
			HasSeconds = 32
		}

		// Token: 0x020004B8 RID: 1208
		public enum DurationType
		{
			// Token: 0x04001F7D RID: 8061
			Duration,
			// Token: 0x04001F7E RID: 8062
			YearMonthDuration,
			// Token: 0x04001F7F RID: 8063
			DayTimeDuration
		}
	}
}
