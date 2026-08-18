using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000126 RID: 294
	internal abstract class BinXmlDateTime
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x00054C55 File Offset: 0x00052E55
		private static void Write2Dig(StringBuilder sb, int val)
		{
			sb.Append((char)(48 + val / 10));
			sb.Append((char)(48 + val % 10));
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00054C75 File Offset: 0x00052E75
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

		// Token: 0x06001485 RID: 5253 RVA: 0x00054C9C File Offset: 0x00052E9C
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

		// Token: 0x06001486 RID: 5254 RVA: 0x00054CEE File Offset: 0x00052EEE
		private static void WriteDate(StringBuilder sb, int yr, int mnth, int day)
		{
			BinXmlDateTime.Write4DigNeg(sb, yr);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, mnth);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, day);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00054D17 File Offset: 0x00052F17
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

		// Token: 0x06001488 RID: 5256 RVA: 0x00054D4C File Offset: 0x00052F4C
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

		// Token: 0x06001489 RID: 5257 RVA: 0x00054DD0 File Offset: 0x00052FD0
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

		// Token: 0x0600148A RID: 5258 RVA: 0x00054E0A File Offset: 0x0005300A
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

		// Token: 0x0600148B RID: 5259 RVA: 0x00054E44 File Offset: 0x00053044
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

		// Token: 0x0600148C RID: 5260 RVA: 0x00054EEC File Offset: 0x000530EC
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

		// Token: 0x0600148D RID: 5261 RVA: 0x00054F80 File Offset: 0x00053180
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

		// Token: 0x0600148E RID: 5262 RVA: 0x00054FE4 File Offset: 0x000531E4
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

		// Token: 0x0600148F RID: 5263 RVA: 0x00055044 File Offset: 0x00053244
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

		// Token: 0x06001490 RID: 5264 RVA: 0x00055078 File Offset: 0x00053278
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

		// Token: 0x06001491 RID: 5265 RVA: 0x000550C0 File Offset: 0x000532C0
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

		// Token: 0x06001492 RID: 5266 RVA: 0x00055114 File Offset: 0x00053314
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

		// Token: 0x06001493 RID: 5267 RVA: 0x00055154 File Offset: 0x00053354
		public static DateTime XsdTimeToDateTime(long val)
		{
			int hour;
			int minute;
			int second;
			int millisecond;
			BinXmlDateTime.BreakDownXsdTime(val, out hour, out minute, out second, out millisecond);
			return new DateTime(1, 1, 1, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005517C File Offset: 0x0005337C
		public static string SqlDateTimeToString(int dateticks, uint timeticks)
		{
			DateTime dateTime = BinXmlDateTime.SqlDateTimeToDateTime(dateticks, timeticks);
			string format = (dateTime.Millisecond != 0) ? "yyyy/MM/dd\\THH:mm:ss.ffff" : "yyyy/MM/dd\\THH:mm:ss";
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000551B4 File Offset: 0x000533B4
		public static DateTime SqlDateTimeToDateTime(int dateticks, uint timeticks)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			long num = (long)(timeticks / BinXmlDateTime.SQLTicksPerMillisecond + 0.5);
			return dateTime.Add(new TimeSpan((long)dateticks * 864000000000L + num * 10000L));
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00055208 File Offset: 0x00053408
		public static string SqlSmallDateTimeToString(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(dateticks, timeticks).ToString("yyyy/MM/dd\\THH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005522E File Offset: 0x0005342E
		public static DateTime SqlSmallDateTimeToDateTime(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlDateTimeToDateTime((int)dateticks, (uint)((int)timeticks * BinXmlDateTime.SQLTicksPerMinute));
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00055240 File Offset: 0x00053440
		public static DateTime XsdKatmaiDateToDateTime(byte[] data, int offset)
		{
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			DateTime result = new DateTime(katmaiDateTicks);
			return result;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00055260 File Offset: 0x00053460
		public static DateTime XsdKatmaiDateTimeToDateTime(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			DateTime result = new DateTime(katmaiDateTicks + katmaiTimeTicks);
			return result;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005528A File Offset: 0x0005348A
		public static DateTime XsdKatmaiTimeToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00055294 File Offset: 0x00053494
		public static DateTime XsdKatmaiDateOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000552B0 File Offset: 0x000534B0
		public static DateTime XsdKatmaiDateTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000552CC File Offset: 0x000534CC
		public static DateTime XsdKatmaiTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000552E8 File Offset: 0x000534E8
		public static DateTimeOffset XsdKatmaiDateToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000552F6 File Offset: 0x000534F6
		public static DateTimeOffset XsdKatmaiDateTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00055304 File Offset: 0x00053504
		public static DateTimeOffset XsdKatmaiTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00055312 File Offset: 0x00053512
		public static DateTimeOffset XsdKatmaiDateOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005531C File Offset: 0x0005351C
		public static DateTimeOffset XsdKatmaiDateTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			long katmaiTimeZoneTicks = BinXmlDateTime.GetKatmaiTimeZoneTicks(data, offset);
			DateTimeOffset result = new DateTimeOffset(katmaiDateTicks + katmaiTimeTicks + katmaiTimeZoneTicks, new TimeSpan(katmaiTimeZoneTicks));
			return result;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00055356 File Offset: 0x00053556
		public static DateTimeOffset XsdKatmaiTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00055360 File Offset: 0x00053560
		public static string XsdKatmaiDateToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(10);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			return stringBuilder.ToString();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000553A0 File Offset: 0x000535A0
		public static string XsdKatmaiDateTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(33);
			BinXmlDateTime.WriteDate(stringBuilder, dt.Year, dt.Month, dt.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00055408 File Offset: 0x00053608
		public static string XsdKatmaiTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005544C File Offset: 0x0005364C
		public static string XsdKatmaiDateOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00055498 File Offset: 0x00053698
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

		// Token: 0x060014A9 RID: 5289 RVA: 0x00055510 File Offset: 0x00053710
		public static string XsdKatmaiTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dt = BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(22);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dt.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00055564 File Offset: 0x00053764
		private static long GetKatmaiDateTicks(byte[] data, ref int pos)
		{
			int num = pos;
			pos = num + 3;
			return (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16) * 864000000000L;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00055598 File Offset: 0x00053798
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

		// Token: 0x060014AC RID: 5292 RVA: 0x0005564B File Offset: 0x0005384B
		private static long GetKatmaiTimeZoneTicks(byte[] data, int pos)
		{
			return (long)((short)((int)data[pos] | (int)data[pos + 1] << 8)) * 600000000L;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00055664 File Offset: 0x00053864
		private static int GetFractions(DateTime dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000556B4 File Offset: 0x000538B4
		private static int GetFractions(DateTimeOffset dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x040005E9 RID: 1513
		private const int MaxFractionDigits = 7;

		// Token: 0x040005EA RID: 1514
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

		// Token: 0x040005EB RID: 1515
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x040005EC RID: 1516
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x040005ED RID: 1517
		public static readonly int SQLTicksPerMinute = BinXmlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x040005EE RID: 1518
		public static readonly int SQLTicksPerHour = BinXmlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x040005EF RID: 1519
		private static readonly int SQLTicksPerDay = BinXmlDateTime.SQLTicksPerHour * 24;
	}
}
