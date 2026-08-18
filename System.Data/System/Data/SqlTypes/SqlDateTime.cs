using System;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000348 RID: 840
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002C41 RID: 11329 RVA: 0x002C78D8 File Offset: 0x002C6CD8
		private SqlDateTime(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_day = 0;
			this.m_time = 0;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x002C7908 File Offset: 0x002C6D08
		public SqlDateTime(DateTime value)
		{
			this = SqlDateTime.FromDateTime(value);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x002C7928 File Offset: 0x002C6D28
		public SqlDateTime(int year, int month, int day)
		{
			this = new SqlDateTime(year, month, day, 0, 0, 0, 0.0);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x002C7958 File Offset: 0x002C6D58
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, 0.0);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x002C7988 File Offset: 0x002C6D88
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (year >= SqlDateTime.MinYear && year <= SqlDateTime.MaxYear && month >= 1 && month <= 12)
			{
				int[] array = SqlDateTime.IsLeapYear(year) ? SqlDateTime.DaysToMonth366 : SqlDateTime.DaysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					num2 -= SqlDateTime.DayBase;
					if (num2 >= SqlDateTime.MinDay && num2 <= SqlDateTime.MaxDay && hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60 && millisecond >= 0.0 && millisecond < 1000.0)
					{
						double num3 = millisecond * SqlDateTime.SQLTicksPerMillisecond + 0.5;
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

		// Token: 0x06002C46 RID: 11334 RVA: 0x002C7AD8 File Offset: 0x002C6ED8
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, int bilisecond)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, (double)bilisecond / 1000.0);
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x002C7B08 File Offset: 0x002C6F08
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			if (dayTicks < SqlDateTime.MinDay || dayTicks > SqlDateTime.MaxDay || timeTicks < SqlDateTime.MinTime || timeTicks > SqlDateTime.MaxTime)
			{
				this.m_fNotNull = false;
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			this.m_day = dayTicks;
			this.m_time = timeTicks;
			this.m_fNotNull = true;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x002C7B68 File Offset: 0x002C6F68
		internal SqlDateTime(double dblVal)
		{
			if (dblVal < (double)SqlDateTime.MinDay || dblVal >= (double)(SqlDateTime.MaxDay + 1))
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

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x002C7BD8 File Offset: 0x002C6FD8
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x002C7BF8 File Offset: 0x002C6FF8
		private static TimeSpan ToTimeSpan(SqlDateTime value)
		{
			long num = (long)((double)value.m_time / SqlDateTime.SQLTicksPerMillisecond + 0.5);
			return new TimeSpan((long)value.m_day * 864000000000L + num * 10000L);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x002C7C48 File Offset: 0x002C7048
		private static DateTime ToDateTime(SqlDateTime value)
		{
			return SqlDateTime.SQLBaseDate.Add(SqlDateTime.ToTimeSpan(value));
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x002C7C68 File Offset: 0x002C7068
		internal static DateTime ToDateTime(int daypart, int timepart)
		{
			if (daypart < SqlDateTime.MinDay || daypart > SqlDateTime.MaxDay || timepart < SqlDateTime.MinTime || timepart > SqlDateTime.MaxTime)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / SqlDateTime.SQLTicksPerMillisecond + 0.5) * 10000L;
			DateTime result = new DateTime(SqlDateTime.SQLBaseDateTicks + num + num2);
			return result;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x002C7CE8 File Offset: 0x002C70E8
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
			int num3 = (int)((double)num2 / 10000.0 * SqlDateTime.SQLTicksPerMillisecond + 0.5);
			if (num3 > SqlDateTime.MaxTime)
			{
				num3 = 0;
				num++;
			}
			return new SqlDateTime(num, num3);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x002C7D88 File Offset: 0x002C7188
		private static SqlDateTime FromDateTime(DateTime value)
		{
			if (value == DateTime.MaxValue)
			{
				return SqlDateTime.MaxValue;
			}
			return SqlDateTime.FromTimeSpan(value.Subtract(SqlDateTime.SQLBaseDate));
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x002C7DC8 File Offset: 0x002C71C8
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

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x002C7DF8 File Offset: 0x002C71F8
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

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x002C7E28 File Offset: 0x002C7228
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

		// Token: 0x06002C52 RID: 11346 RVA: 0x002C7E58 File Offset: 0x002C7258
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x002C7E78 File Offset: 0x002C7278
		public static explicit operator DateTime(SqlDateTime x)
		{
			return SqlDateTime.ToDateTime(x);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x002C7E98 File Offset: 0x002C7298
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return SqlDateTime.ToDateTime(this).ToString(null);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x002C7EC8 File Offset: 0x002C72C8
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

		// Token: 0x06002C56 RID: 11350 RVA: 0x002C7F58 File Offset: 0x002C7358
		public static SqlDateTime operator +(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) + t);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x002C7F88 File Offset: 0x002C7388
		public static SqlDateTime operator -(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) - t);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x002C7FB8 File Offset: 0x002C73B8
		public static SqlDateTime Add(SqlDateTime x, TimeSpan t)
		{
			return x + t;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x002C7FD8 File Offset: 0x002C73D8
		public static SqlDateTime Subtract(SqlDateTime x, TimeSpan t)
		{
			return x - t;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x002C7FF8 File Offset: 0x002C73F8
		public static explicit operator SqlDateTime(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.Parse(x.Value);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x002C8028 File Offset: 0x002C7428
		private static bool IsLeapYear(int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x002C8058 File Offset: 0x002C7458
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day == y.m_day && x.m_time == y.m_time);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x002C80A8 File Offset: 0x002C74A8
		public static SqlBoolean operator !=(SqlDateTime x, SqlDateTime y)
		{
			return !(x == y);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x002C80C8 File Offset: 0x002C74C8
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time < y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x002C8128 File Offset: 0x002C7528
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time > y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x002C8188 File Offset: 0x002C7588
		public static SqlBoolean operator <=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time <= y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x002C81F8 File Offset: 0x002C75F8
		public static SqlBoolean operator >=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time >= y.m_time));
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x002C8268 File Offset: 0x002C7668
		public static SqlBoolean Equals(SqlDateTime x, SqlDateTime y)
		{
			return x == y;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x002C8288 File Offset: 0x002C7688
		public static SqlBoolean NotEquals(SqlDateTime x, SqlDateTime y)
		{
			return x != y;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x002C82A8 File Offset: 0x002C76A8
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x002C82C8 File Offset: 0x002C76C8
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x002C82E8 File Offset: 0x002C76E8
		public static SqlBoolean LessThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x <= y;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x002C8308 File Offset: 0x002C7708
		public static SqlBoolean GreaterThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x >= y;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x002C8328 File Offset: 0x002C7728
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x002C8348 File Offset: 0x002C7748
		public int CompareTo(object value)
		{
			if (value is SqlDateTime)
			{
				SqlDateTime value2 = (SqlDateTime)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDateTime));
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x002C8388 File Offset: 0x002C7788
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

		// Token: 0x06002C6B RID: 11371 RVA: 0x002C83E8 File Offset: 0x002C77E8
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

		// Token: 0x06002C6C RID: 11372 RVA: 0x002C8448 File Offset: 0x002C7848
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x002C8478 File Offset: 0x002C7878
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x002C8488 File Offset: 0x002C7888
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
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

		// Token: 0x06002C6F RID: 11375 RVA: 0x002C8508 File Offset: 0x002C7908
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.Value, SqlDateTime.x_ISO8601_DateTimeFormat));
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x002C8558 File Offset: 0x002C7958
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001C89 RID: 7305
		private const DateTimeStyles x_DateTimeStyle = DateTimeStyles.AllowWhiteSpaces;

		// Token: 0x04001C8A RID: 7306
		private bool m_fNotNull;

		// Token: 0x04001C8B RID: 7307
		private int m_day;

		// Token: 0x04001C8C RID: 7308
		private int m_time;

		// Token: 0x04001C8D RID: 7309
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x04001C8E RID: 7310
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x04001C8F RID: 7311
		public static readonly int SQLTicksPerMinute = SqlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x04001C90 RID: 7312
		public static readonly int SQLTicksPerHour = SqlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x04001C91 RID: 7313
		private static readonly int SQLTicksPerDay = SqlDateTime.SQLTicksPerHour * 24;

		// Token: 0x04001C92 RID: 7314
		private static readonly long TicksPerSecond = 10000000L;

		// Token: 0x04001C93 RID: 7315
		private static readonly DateTime SQLBaseDate = new DateTime(1900, 1, 1);

		// Token: 0x04001C94 RID: 7316
		private static readonly long SQLBaseDateTicks = SqlDateTime.SQLBaseDate.Ticks;

		// Token: 0x04001C95 RID: 7317
		private static readonly int MinYear = 1753;

		// Token: 0x04001C96 RID: 7318
		private static readonly int MaxYear = 9999;

		// Token: 0x04001C97 RID: 7319
		private static readonly int MinDay = -53690;

		// Token: 0x04001C98 RID: 7320
		private static readonly int MaxDay = 2958463;

		// Token: 0x04001C99 RID: 7321
		private static readonly int MinTime = 0;

		// Token: 0x04001C9A RID: 7322
		private static readonly int MaxTime = SqlDateTime.SQLTicksPerDay - 1;

		// Token: 0x04001C9B RID: 7323
		private static readonly int DayBase = 693595;

		// Token: 0x04001C9C RID: 7324
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

		// Token: 0x04001C9D RID: 7325
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

		// Token: 0x04001C9E RID: 7326
		private static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		// Token: 0x04001C9F RID: 7327
		private static readonly DateTime MaxDateTime = DateTime.MaxValue;

		// Token: 0x04001CA0 RID: 7328
		private static readonly TimeSpan MinTimeSpan = SqlDateTime.MinDateTime.Subtract(SqlDateTime.SQLBaseDate);

		// Token: 0x04001CA1 RID: 7329
		private static readonly TimeSpan MaxTimeSpan = SqlDateTime.MaxDateTime.Subtract(SqlDateTime.SQLBaseDate);

		// Token: 0x04001CA2 RID: 7330
		private static readonly string x_ISO8601_DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x04001CA3 RID: 7331
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

		// Token: 0x04001CA4 RID: 7332
		public static readonly SqlDateTime MinValue = new SqlDateTime(SqlDateTime.MinDay, 0);

		// Token: 0x04001CA5 RID: 7333
		public static readonly SqlDateTime MaxValue = new SqlDateTime(SqlDateTime.MaxDay, SqlDateTime.MaxTime);

		// Token: 0x04001CA6 RID: 7334
		public static readonly SqlDateTime Null = new SqlDateTime(true);
	}
}
