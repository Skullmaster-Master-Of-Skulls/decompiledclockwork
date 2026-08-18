using System;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000159 RID: 345
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0009DEE0 File Offset: 0x0009D2E0
		private SqlDateTime(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_day = 0;
			this.m_time = 0;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0009DF04 File Offset: 0x0009D304
		public SqlDateTime(DateTime value)
		{
			this = SqlDateTime.FromDateTime(value);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0009DF20 File Offset: 0x0009D320
		public SqlDateTime(int year, int month, int day)
		{
			this = new SqlDateTime(year, month, day, 0, 0, 0, 0.0);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0009DF44 File Offset: 0x0009D344
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, 0.0);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0009DF6C File Offset: 0x0009D36C
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (year >= 1753 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = SqlDateTime.IsLeapYear(year) ? SqlDateTime.DaysToMonth366 : SqlDateTime.DaysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					num2 -= 693595;
					if (num2 >= -53690 && num2 <= 2958463 && hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60 && millisecond >= 0.0 && millisecond < 1000.0)
					{
						double num3 = millisecond * 0.3 + 0.5;
						int num4 = hour * SqlDateTime.SQLTicksPerHour + minute * SqlDateTime.SQLTicksPerMinute + second * SqlDateTime.SQLTicksPerSecond + (int)num3;
						if (num4 > SqlDateTime.MaxTime)
						{
							num4 = 0;
							num2++;
						}
						this = new SqlDateTime(num2, num4);
						return;
					}
				}
			}
			throw new SqlTypeException(SQLResource.InvalidDateTimeMessage);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0009E0B4 File Offset: 0x0009D4B4
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, int bilisecond)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, (double)bilisecond / 1000.0);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0009E0E0 File Offset: 0x0009D4E0
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			if (dayTicks < -53690 || dayTicks > 2958463 || timeTicks < 0 || timeTicks > SqlDateTime.MaxTime)
			{
				this.m_fNotNull = false;
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			this.m_day = dayTicks;
			this.m_time = timeTicks;
			this.m_fNotNull = true;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0009E130 File Offset: 0x0009D530
		internal SqlDateTime(double dblVal)
		{
			if (dblVal < -53690.0 || dblVal >= 2958464.0)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			int num = (int)dblVal;
			int num2 = (int)((dblVal - (double)num) * (double)SqlDateTime.SQLTicksPerDay);
			if (num2 < 0)
			{
				num--;
				num2 += SqlDateTime.SQLTicksPerDay;
			}
			else if (num2 >= SqlDateTime.SQLTicksPerDay)
			{
				num++;
				num2 -= SqlDateTime.SQLTicksPerDay;
			}
			this = new SqlDateTime(num, num2);
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0009E1A4 File Offset: 0x0009D5A4
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0009E1BC File Offset: 0x0009D5BC
		private static TimeSpan ToTimeSpan(SqlDateTime value)
		{
			long num = (long)((double)value.m_time / 0.3 + 0.5);
			return new TimeSpan((long)value.m_day * 864000000000L + num * 10000L);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0009E208 File Offset: 0x0009D608
		private static DateTime ToDateTime(SqlDateTime value)
		{
			return SqlDateTime.SQLBaseDate.Add(SqlDateTime.ToTimeSpan(value));
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0009E228 File Offset: 0x0009D628
		internal static DateTime ToDateTime(int daypart, int timepart)
		{
			if (daypart < -53690 || daypart > 2958463 || timepart < 0 || timepart > SqlDateTime.MaxTime)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / 0.3 + 0.5) * 10000L;
			DateTime result = new DateTime(SqlDateTime.SQLBaseDateTicks + num + num2);
			return result;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0009E29C File Offset: 0x0009D69C
		private static SqlDateTime FromTimeSpan(TimeSpan value)
		{
			if (value < SqlDateTime.MinTimeSpan || value > SqlDateTime.MaxTimeSpan)
			{
				throw new SqlTypeException(SQLResource.DateTimeOverflowMessage);
			}
			int num = value.Days;
			long num2 = value.Ticks - (long)num * 864000000000L;
			if (num2 < 0L)
			{
				num--;
				num2 += 864000000000L;
			}
			int num3 = (int)((double)num2 / 10000.0 * 0.3 + 0.5);
			if (num3 > SqlDateTime.MaxTime)
			{
				num3 = 0;
				num++;
			}
			return new SqlDateTime(num, num3);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0009E338 File Offset: 0x0009D738
		private static SqlDateTime FromDateTime(DateTime value)
		{
			if (value == DateTime.MaxValue)
			{
				return SqlDateTime.MaxValue;
			}
			return SqlDateTime.FromTimeSpan(value.Subtract(SqlDateTime.SQLBaseDate));
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0009E36C File Offset: 0x0009D76C
		public DateTime Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return SqlDateTime.ToDateTime(this);
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0009E394 File Offset: 0x0009D794
		public int DayTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_day;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0009E3B8 File Offset: 0x0009D7B8
		public int TimeTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_time;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0009E3DC File Offset: 0x0009D7DC
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0009E3F0 File Offset: 0x0009D7F0
		public static explicit operator DateTime(SqlDateTime x)
		{
			return SqlDateTime.ToDateTime(x);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0009E404 File Offset: 0x0009D804
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return SqlDateTime.ToDateTime(this).ToString(null);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0009E434 File Offset: 0x0009D834
		public static SqlDateTime Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDateTime.Null;
			}
			DateTime value;
			try
			{
				value = DateTime.Parse(s, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				DateTimeFormatInfo provider = (DateTimeFormatInfo)Thread.CurrentThread.CurrentCulture.GetFormat(typeof(DateTimeFormatInfo));
				value = DateTime.ParseExact(s, SqlDateTime.x_DateTimeFormats, provider, DateTimeStyles.AllowWhiteSpaces);
			}
			return new SqlDateTime(value);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0009E4B8 File Offset: 0x0009D8B8
		public static SqlDateTime operator +(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) + t);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0009E4E8 File Offset: 0x0009D8E8
		public static SqlDateTime operator -(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) - t);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0009E518 File Offset: 0x0009D918
		public static SqlDateTime Add(SqlDateTime x, TimeSpan t)
		{
			return x + t;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0009E52C File Offset: 0x0009D92C
		public static SqlDateTime Subtract(SqlDateTime x, TimeSpan t)
		{
			return x - t;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0009E540 File Offset: 0x0009D940
		public static explicit operator SqlDateTime(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.Parse(x.Value);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0009E568 File Offset: 0x0009D968
		private static bool IsLeapYear(int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0009E590 File Offset: 0x0009D990
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day == y.m_day && x.m_time == y.m_time);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0009E5DC File Offset: 0x0009D9DC
		public static SqlBoolean operator !=(SqlDateTime x, SqlDateTime y)
		{
			return !(x == y);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0009E5F8 File Offset: 0x0009D9F8
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time < y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0009E654 File Offset: 0x0009DA54
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time > y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0009E6B0 File Offset: 0x0009DAB0
		public static SqlBoolean operator <=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time <= y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0009E710 File Offset: 0x0009DB10
		public static SqlBoolean operator >=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time >= y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0009E770 File Offset: 0x0009DB70
		public static SqlBoolean Equals(SqlDateTime x, SqlDateTime y)
		{
			return x == y;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0009E784 File Offset: 0x0009DB84
		public static SqlBoolean NotEquals(SqlDateTime x, SqlDateTime y)
		{
			return x != y;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0009E798 File Offset: 0x0009DB98
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0009E7AC File Offset: 0x0009DBAC
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0009E7C0 File Offset: 0x0009DBC0
		public static SqlBoolean LessThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x <= y;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0009E7D4 File Offset: 0x0009DBD4
		public static SqlBoolean GreaterThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x >= y;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0009E7E8 File Offset: 0x0009DBE8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0009E800 File Offset: 0x0009DC00
		public int CompareTo(object value)
		{
			if (value is SqlDateTime)
			{
				SqlDateTime value2 = (SqlDateTime)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDateTime));
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0009E83C File Offset: 0x0009DC3C
		public int CompareTo(SqlDateTime value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0009E894 File Offset: 0x0009DC94
		public override bool Equals(object value)
		{
			if (!(value is SqlDateTime))
			{
				return false;
			}
			SqlDateTime y = (SqlDateTime)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0009E8EC File Offset: 0x0009DCEC
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0009E914 File Offset: 0x0009DD14
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0009E924 File Offset: 0x0009DD24
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			DateTime value = XmlConvert.ToDateTime(reader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
			if (value.Kind != DateTimeKind.Unspecified)
			{
				throw new SqlTypeException(SQLResource.TimeZoneSpecifiedMessage);
			}
			SqlDateTime sqlDateTime = SqlDateTime.FromDateTime(value);
			this.m_day = sqlDateTime.DayTicks;
			this.m_time = sqlDateTime.TimeTicks;
			this.m_fNotNull = true;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0009E9A8 File Offset: 0x0009DDA8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.Value, "yyyy-MM-ddTHH:mm:ss.fff"));
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0009E9F0 File Offset: 0x0009DDF0
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000D72 RID: 3442
		private bool m_fNotNull;

		// Token: 0x04000D73 RID: 3443
		private int m_day;

		// Token: 0x04000D74 RID: 3444
		private int m_time;

		// Token: 0x04000D75 RID: 3445
		private const double SQLTicksPerMillisecond = 0.3;

		// Token: 0x04000D76 RID: 3446
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x04000D77 RID: 3447
		public static readonly int SQLTicksPerMinute = SqlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x04000D78 RID: 3448
		public static readonly int SQLTicksPerHour = SqlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x04000D79 RID: 3449
		private static readonly int SQLTicksPerDay = SqlDateTime.SQLTicksPerHour * 24;

		// Token: 0x04000D7A RID: 3450
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04000D7B RID: 3451
		private static readonly DateTime SQLBaseDate = new DateTime(1900, 1, 1);

		// Token: 0x04000D7C RID: 3452
		private static readonly long SQLBaseDateTicks = SqlDateTime.SQLBaseDate.Ticks;

		// Token: 0x04000D7D RID: 3453
		private const int MinYear = 1753;

		// Token: 0x04000D7E RID: 3454
		private const int MaxYear = 9999;

		// Token: 0x04000D7F RID: 3455
		private const int MinDay = -53690;

		// Token: 0x04000D80 RID: 3456
		private const int MaxDay = 2958463;

		// Token: 0x04000D81 RID: 3457
		private const int MinTime = 0;

		// Token: 0x04000D82 RID: 3458
		private static readonly int MaxTime = SqlDateTime.SQLTicksPerDay - 1;

		// Token: 0x04000D83 RID: 3459
		private const int DayBase = 693595;

		// Token: 0x04000D84 RID: 3460
		private static readonly int[] DaysToMonth365 = new int[]
		{
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

		// Token: 0x04000D85 RID: 3461
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};

		// Token: 0x04000D86 RID: 3462
		private static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		// Token: 0x04000D87 RID: 3463
		private static readonly DateTime MaxDateTime = DateTime.MaxValue;

		// Token: 0x04000D88 RID: 3464
		private static readonly TimeSpan MinTimeSpan = SqlDateTime.MinDateTime.Subtract(SqlDateTime.SQLBaseDate);

		// Token: 0x04000D89 RID: 3465
		private static readonly TimeSpan MaxTimeSpan = SqlDateTime.MaxDateTime.Subtract(SqlDateTime.SQLBaseDate);

		// Token: 0x04000D8A RID: 3466
		private const string x_ISO8601_DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x04000D8B RID: 3467
		private static readonly string[] x_DateTimeFormats = new string[]
		{
			"MMM d yyyy hh:mm:ss:ffftt",
			"MMM d yyyy hh:mm:ss:fff",
			"d MMM yyyy hh:mm:ss:ffftt",
			"d MMM yyyy hh:mm:ss:fff",
			"hh:mm:ss:ffftt",
			"hh:mm:ss:fff",
			"yyMMdd",
			"yyyyMMdd"
		};

		// Token: 0x04000D8C RID: 3468
		private const DateTimeStyles x_DateTimeStyle = DateTimeStyles.AllowWhiteSpaces;

		// Token: 0x04000D8D RID: 3469
		public static readonly SqlDateTime MinValue = new SqlDateTime(-53690, 0);

		// Token: 0x04000D8E RID: 3470
		public static readonly SqlDateTime MaxValue = new SqlDateTime(2958463, SqlDateTime.MaxTime);

		// Token: 0x04000D8F RID: 3471
		public static readonly SqlDateTime Null = new SqlDateTime(true);
	}
}
