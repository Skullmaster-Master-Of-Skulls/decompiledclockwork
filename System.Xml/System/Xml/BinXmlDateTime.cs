using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000F7 RID: 247
	internal abstract class BinXmlDateTime
	{
		// Token: 0x06000EEE RID: 3822 RVA: 0x000418F5 File Offset: 0x000408F5
		private static void Write2Dig(StringBuilder sb, int val)
		{
			sb.Append((char)(48 + val / 10));
			sb.Append((char)(48 + val % 10));
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00041915 File Offset: 0x00040915
		private static void Write4DigNeg(StringBuilder sb, int val)
		{
			if (val < 0)
			{
				val = -val;
				sb.Append('-');
			}
			BinXmlDateTime.Write2Dig(sb, val / 100);
			BinXmlDateTime.Write2Dig(sb, val % 100);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0004193C File Offset: 0x0004093C
		private static void Write3Dec(StringBuilder sb, int val)
		{
			int num = val % 10;
			val /= 10;
			int num2 = val % 10;
			val /= 10;
			int num3 = val;
			sb.Append('.');
			sb.Append((char)(48 + num3));
			sb.Append((char)(48 + num2));
			sb.Append((char)(48 + num));
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0004198E File Offset: 0x0004098E
		private static void WriteDate(StringBuilder sb, int yr, int mnth, int day)
		{
			BinXmlDateTime.Write4DigNeg(sb, yr);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, mnth);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, day);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000419B7 File Offset: 0x000409B7
		private static void WriteTime(StringBuilder sb, int hr, int min, int sec, int ms)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (ms != 0)
			{
				BinXmlDateTime.Write3Dec(sb, ms);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000419EC File Offset: 0x000409EC
		private static void WriteTimeFullPrecision(StringBuilder sb, int hr, int min, int sec, int fraction)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (fraction != 0)
			{
				int i = 7;
				while (fraction % 10 == 0)
				{
					i--;
					fraction /= 10;
				}
				char[] array = new char[i];
				while (i > 0)
				{
					i--;
					array[i] = (char)(fraction % 10 + 48);
					fraction /= 10;
				}
				sb.Append('.');
				sb.Append(array);
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00041A70 File Offset: 0x00040A70
		private static void WriteTimeZone(StringBuilder sb, TimeSpan zone)
		{
			bool negTimeZone = true;
			if (zone.Ticks < 0L)
			{
				negTimeZone = false;
				zone = zone.Negate();
			}
			BinXmlDateTime.WriteTimeZone(sb, negTimeZone, zone.Hours, zone.Minutes);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00041AAA File Offset: 0x00040AAA
		private static void WriteTimeZone(StringBuilder sb, bool negTimeZone, int hr, int min)
		{
			if (hr == 0 && min == 0)
			{
				sb.Append('Z');
				return;
			}
			sb.Append(negTimeZone ? '+' : '-');
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00041AE4 File Offset: 0x00040AE4
		private static void BreakDownXsdDateTime(long val, out int yr, out int mnth, out int day, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				long num = val / 4L;
				ms = (int)(num % 1000L);
				num /= 1000L;
				sec = (int)(num % 60L);
				num /= 60L;
				min = (int)(num % 60L);
				num /= 60L;
				hr = (int)(num % 24L);
				num /= 24L;
				day = (int)(num % 31L) + 1;
				num /= 31L;
				mnth = (int)(num % 12L) + 1;
				num /= 12L;
				yr = (int)(num - 9999L);
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("SqlTypes_ArithOverflow", null);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00041B8C File Offset: 0x00040B8C
		private static void BreakDownXsdDate(long val, out int yr, out int mnth, out int day, out bool negTimeZone, out int hr, out int min)
		{
			if (val >= 0L)
			{
				val /= 4L;
				int num = (int)(val % 1740L) - 840;
				long num2 = val / 1740L;
				if (negTimeZone = (num < 0))
				{
					num = -num;
				}
				min = num % 60;
				hr = num / 60;
				day = (int)(num2 % 31L) + 1;
				num2 /= 31L;
				mnth = (int)(num2 % 12L) + 1;
				yr = (int)(num2 / 12L) - 9999;
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("SqlTypes_ArithOverflow", null);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00041C20 File Offset: 0x00040C20
		private static void BreakDownXsdTime(long val, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				val /= 4L;
				ms = (int)(val % 1000L);
				val /= 1000L;
				sec = (int)(val % 60L);
				val /= 60L;
				min = (int)(val % 60L);
				hr = (int)(val / 60L);
				if (0 <= hr && hr <= 23)
				{
					return;
				}
			}
			throw new XmlException("SqlTypes_ArithOverflow", null);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00041C84 File Offset: 0x00040C84
		public static string XsdDateTimeToString(long val)
		{
			int yr;
			int mnth;
			int day;
			int hr;
			int min;
			int sec;
			int ms;
			BinXmlDateTime.BreakDownXsdDateTime(val, out yr, out mnth, out day, out hr, out min, out sec, out ms);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, yr, mnth, day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTime(stringBuilder, hr, min, sec, ms);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00041CE4 File Offset: 0x00040CE4
		public static DateTime XsdDateTimeToDateTime(long val)
		{
			int year;
			int month;
			int day;
			int hour;
			int minute;
			int second;
			int millisecond;
			BinXmlDateTime.BreakDownXsdDateTime(val, out year, out month, out day, out hour, out minute, out second, out millisecond);
			return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00041D18 File Offset: 0x00040D18
		public static string XsdDateToString(long val)
		{
			int yr;
			int mnth;
			int day;
			bool negTimeZone;
			int hr;
			int min;
			BinXmlDateTime.BreakDownXsdDate(val, out yr, out mnth, out day, out negTimeZone, out hr, out min);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, yr, mnth, day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, negTimeZone, hr, min);
			return stringBuilder.ToString();
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00041D60 File Offset: 0x00040D60
		public static DateTime XsdDateToDateTime(long val)
		{
			int year;
			int month;
			int day;
			bool flag;
			int num;
			int num2;
			BinXmlDateTime.BreakDownXsdDate(val, out year, out month, out day, out flag, out num, out num2);
			DateTime dateTime = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
			int num3 = (flag ? -1 : 1) * (num * 60 + num2);
			return TimeZone.CurrentTimeZone.ToLocalTime(dateTime.AddMinutes((double)num3));
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00041DB4 File Offset: 0x00040DB4
		public static string XsdTimeToString(long val)
		{
			int hr;
			int min;
			int sec;
			int ms;
			BinXmlDateTime.BreakDownXsdTime(val, out hr, out min, out sec, out ms);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTime(stringBuilder, hr, min, sec, ms);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00041DF4 File Offset: 0x00040DF4
		public static DateTime XsdTimeToDateTime(long val)
		{
			int hour;
			int minute;
			int second;
			int millisecond;
			BinXmlDateTime.BreakDownXsdTime(val, out hour, out minute, out second, out millisecond);
			return new DateTime(1, 1, 1, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00041E1C File Offset: 0x00040E1C
		public static string SqlDateTimeToString(int dateticks, uint timeticks)
		{
			DateTime dateTime = BinXmlDateTime.SqlDateTimeToDateTime(dateticks, timeticks);
			string format = (dateTime.Millisecond != 0) ? "yyyy/MM/dd\\THH:mm:ss.ffff" : "yyyy/MM/dd\\THH:mm:ss";
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00041E54 File Offset: 0x00040E54
		public static DateTime SqlDateTimeToDateTime(int dateticks, uint timeticks)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			long num = (long)(timeticks / BinXmlDateTime.SQLTicksPerMillisecond + 0.5);
			return dateTime.Add(new TimeSpan((long)dateticks * 864000000000L + num * 10000L));
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00041EA8 File Offset: 0x00040EA8
		public static string SqlSmallDateTimeToString(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(dateticks, timeticks).ToString("yyyy/MM/dd\\THH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00041ECE File Offset: 0x00040ECE
		public static DateTime SqlSmallDateTimeToDateTime(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlDateTimeToDateTime((int)dateticks, (uint)((int)timeticks * BinXmlDateTime.SQLTicksPerMinute));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00041EE0 File Offset: 0x00040EE0
		public static DateTime XsdKatmaiDateToDateTime(byte[] data, int offset)
		{
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			DateTime result = new DateTime(katmaiDateTicks);
			return result;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00041F00 File Offset: 0x00040F00
		public static DateTime XsdKatmaiDateTimeToDateTime(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			DateTime result = new DateTime(katmaiDateTicks + katmaiTimeTicks);
			return result;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00041F2A File Offset: 0x00040F2A
		public static DateTime XsdKatmaiTimeToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00041F34 File Offset: 0x00040F34
		public static DateTime XsdKatmaiDateOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00041F50 File Offset: 0x00040F50
		public static DateTime XsdKatmaiDateTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00041F6C File Offset: 0x00040F6C
		public static DateTime XsdKatmaiTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00041F88 File Offset: 0x00040F88
		public static DateTimeOffset XsdKatmaiDateToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00041F96 File Offset: 0x00040F96
		public static DateTimeOffset XsdKatmaiDateTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00041FA4 File Offset: 0x00040FA4
		public static DateTimeOffset XsdKatmaiTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00041FB2 File Offset: 0x00040FB2
		public static DateTimeOffset XsdKatmaiDateOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00041FBC File Offset: 0x00040FBC
		public static DateTimeOffset XsdKatmaiDateTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			long katmaiTimeZoneTicks = BinXmlDateTime.GetKatmaiTimeZoneTicks(data, offset);
			DateTimeOffset result = new DateTimeOffset(katmaiDateTicks + katmaiTimeTicks + katmaiTimeZoneTicks, new TimeSpan(katmaiTimeZoneTicks));
			return result;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00041FF6 File Offset: 0x00040FF6
		public static DateTimeOffset XsdKatmaiTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00042000 File Offset: 0x00041000
		public static string XsdKatmaiDateToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(10);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00042040 File Offset: 0x00041040
		public static string XsdKatmaiDateTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(33);
			BinXmlDateTime.WriteDate(stringBuilder, dt.Year, dt.Month, dt.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000420A8 File Offset: 0x000410A8
		public static string XsdKatmaiTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000420EC File Offset: 0x000410EC
		public static string XsdKatmaiDateOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00042138 File Offset: 0x00041138
		public static string XsdKatmaiDateTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dt = BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(39);
			BinXmlDateTime.WriteDate(stringBuilder, dt.Year, dt.Month, dt.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dt.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000421B0 File Offset: 0x000411B0
		public static string XsdKatmaiTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dt = BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(22);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dt.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00042204 File Offset: 0x00041204
		private static long GetKatmaiDateTicks(byte[] data, ref int pos)
		{
			int num = pos;
			pos = num + 3;
			return (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16) * 864000000000L;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00042238 File Offset: 0x00041238
		private static long GetKatmaiTimeTicks(byte[] data, ref int pos)
		{
			int num = pos;
			byte b = data[num];
			num++;
			long num2;
			if (b <= 2)
			{
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				pos = num + 3;
			}
			else if (b <= 4)
			{
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				num2 |= (long)((long)((ulong)data[num + 3]) << 24);
				pos = num + 4;
			}
			else
			{
				if (b > 7)
				{
					throw new XmlException("SqlTypes_ArithOverflow", null);
				}
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				num2 |= (long)((ulong)data[num + 3] << 24 | (ulong)data[num + 4] << 32);
				pos = num + 5;
			}
			return num2 * (long)BinXmlDateTime.KatmaiTimeScaleMultiplicator[(int)b];
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000422EB File Offset: 0x000412EB
		private static long GetKatmaiTimeZoneTicks(byte[] data, int pos)
		{
			return (long)((short)((int)data[pos] | (int)data[pos + 1] << 8)) * 600000000L;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00042304 File Offset: 0x00041304
		private static int GetFractions(DateTime dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00042354 File Offset: 0x00041354
		private static int GetFractions(DateTimeOffset dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x04000A09 RID: 2569
		private const int MaxFractionDigits = 7;

		// Token: 0x04000A0A RID: 2570
		internal static int[] KatmaiTimeScaleMultiplicator = new int[]
		{
			10000000,
			1000000,
			100000,
			10000,
			1000,
			100,
			10,
			1
		};

		// Token: 0x04000A0B RID: 2571
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x04000A0C RID: 2572
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x04000A0D RID: 2573
		public static readonly int SQLTicksPerMinute = BinXmlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x04000A0E RID: 2574
		public static readonly int SQLTicksPerHour = BinXmlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x04000A0F RID: 2575
		private static readonly int SQLTicksPerDay = BinXmlDateTime.SQLTicksPerHour * 24;
	}
}
