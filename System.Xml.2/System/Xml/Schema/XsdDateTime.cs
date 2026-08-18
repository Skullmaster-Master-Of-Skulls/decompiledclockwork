using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020002D0 RID: 720
	internal struct XsdDateTime
	{
		// Token: 0x06002AF8 RID: 11000 RVA: 0x000E01C2 File Offset: 0x000DE3C2
		public XsdDateTime(string text)
		{
			this = new XsdDateTime(text, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000E01D0 File Offset: 0x000DE3D0
		public XsdDateTime(string text, XsdDateTimeFlags kinds)
		{
			this = default(XsdDateTime);
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				throw new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					text,
					kinds
				}));
			}
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000E0221 File Offset: 0x000DE421
		private XsdDateTime(XsdDateTime.Parser parser)
		{
			this = default(XsdDateTime);
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000E0234 File Offset: 0x000DE434
		private void InitiateXsdDateTime(XsdDateTime.Parser parser)
		{
			this.dt = new DateTime(parser.year, parser.month, parser.day, parser.hour, parser.minute, parser.second);
			if (parser.fraction != 0)
			{
				this.dt = this.dt.AddTicks((long)parser.fraction);
			}
			this.extra = (uint)((int)parser.typeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)parser.kind << 16) | (XsdDateTime.DateTimeTypeCode)(parser.zoneHour << 8) | (XsdDateTime.DateTimeTypeCode)parser.zoneMinute);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000E02BC File Offset: 0x000DE4BC
		internal static bool TryParse(string text, XsdDateTimeFlags kinds, out XsdDateTime result)
		{
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				result = default(XsdDateTime);
				return false;
			}
			result = new XsdDateTime(parser);
			return true;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000E02F4 File Offset: 0x000DE4F4
		public XsdDateTime(DateTime dateTime, XsdDateTimeFlags kinds)
		{
			this.dt = dateTime;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			int num = 0;
			int num2 = 0;
			DateTimeKind kind = dateTime.Kind;
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (kind != DateTimeKind.Unspecified)
			{
				if (kind != DateTimeKind.Utc)
				{
					TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					if (utcOffset.Ticks < 0L)
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
						num = -utcOffset.Hours;
						num2 = -utcOffset.Minutes;
					}
					else
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
						num = utcOffset.Hours;
						num2 = utcOffset.Minutes;
					}
				}
				else
				{
					xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
				}
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Unspecified;
			}
			this.extra = (uint)((int)dateTimeTypeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(num << 8) | (XsdDateTime.DateTimeTypeCode)num2);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000E0386 File Offset: 0x000DE586
		public XsdDateTime(DateTimeOffset dateTimeOffset)
		{
			this = new XsdDateTime(dateTimeOffset, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000E0390 File Offset: 0x000DE590
		public XsdDateTime(DateTimeOffset dateTimeOffset, XsdDateTimeFlags kinds)
		{
			this.dt = dateTimeOffset.DateTime;
			TimeSpan timeSpan = dateTimeOffset.Offset;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (timeSpan.TotalMinutes < 0.0)
			{
				timeSpan = timeSpan.Negate();
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
			}
			else if (timeSpan.TotalMinutes > 0.0)
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
			}
			this.extra = (uint)((int)dateTimeTypeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(timeSpan.Hours << 8) | (XsdDateTime.DateTimeTypeCode)timeSpan.Minutes);
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000E0412 File Offset: 0x000DE612
		private XsdDateTime.DateTimeTypeCode InternalTypeCode
		{
			get
			{
				return (XsdDateTime.DateTimeTypeCode)((this.extra & 4278190080U) >> 24);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000E0423 File Offset: 0x000DE623
		private XsdDateTime.XsdDateTimeKind InternalKind
		{
			get
			{
				return (XsdDateTime.XsdDateTimeKind)((this.extra & 16711680U) >> 16);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000E0434 File Offset: 0x000DE634
		public XmlTypeCode TypeCode
		{
			get
			{
				return XsdDateTime.typeCodes[(int)this.InternalTypeCode];
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000E0444 File Offset: 0x000DE644
		public DateTimeKind Kind
		{
			get
			{
				XsdDateTime.XsdDateTimeKind internalKind = this.InternalKind;
				if (internalKind == XsdDateTime.XsdDateTimeKind.Unspecified)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != XsdDateTime.XsdDateTimeKind.Zulu)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000E0466 File Offset: 0x000DE666
		public int Year
		{
			get
			{
				return this.dt.Year;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000E0473 File Offset: 0x000DE673
		public int Month
		{
			get
			{
				return this.dt.Month;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000E0480 File Offset: 0x000DE680
		public int Day
		{
			get
			{
				return this.dt.Day;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000E048D File Offset: 0x000DE68D
		public int Hour
		{
			get
			{
				return this.dt.Hour;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000E049A File Offset: 0x000DE69A
		public int Minute
		{
			get
			{
				return this.dt.Minute;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000E04A7 File Offset: 0x000DE6A7
		public int Second
		{
			get
			{
				return this.dt.Second;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000E04B4 File Offset: 0x000DE6B4
		public int Fraction
		{
			get
			{
				return (int)(this.dt.Ticks - new DateTime(this.dt.Year, this.dt.Month, this.dt.Day, this.dt.Hour, this.dt.Minute, this.dt.Second).Ticks);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000E0520 File Offset: 0x000DE720
		public int ZoneHour
		{
			get
			{
				return (int)((this.extra & 65280U) >> 8);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000E0540 File Offset: 0x000DE740
		public int ZoneMinute
		{
			get
			{
				return (int)(this.extra & 255U);
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000E055C File Offset: 0x000DE75C
		public DateTime ToZulu()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return new DateTime(this.dt.Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return new DateTime(this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return new DateTime(this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			default:
				return this.dt;
			}
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000E05F8 File Offset: 0x000DE7F8
		public static implicit operator DateTime(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan value = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(value);
			}
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
				break;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				long num = dateTime.Ticks + new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num > DateTime.MaxValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num > DateTime.MaxValue.Ticks)
					{
						num = DateTime.MaxValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				long num = dateTime.Ticks - new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num < DateTime.MinValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num < DateTime.MinValue.Ticks)
					{
						num = DateTime.MinValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			}
			return dateTime;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000E07E4 File Offset: 0x000DE9E4
		public static implicit operator DateTimeOffset(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan value = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(value);
			}
			DateTimeOffset result;
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(0L));
				return result;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(-xdt.ZoneHour, -xdt.ZoneMinute, 0));
				return result;
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				return result;
			}
			result = new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime));
			return result;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000E0908 File Offset: 0x000DEB08
		public static int Compare(XsdDateTime left, XsdDateTime right)
		{
			if (left.extra == right.extra)
			{
				return DateTime.Compare(left.dt, right.dt);
			}
			if (left.InternalTypeCode != right.InternalTypeCode)
			{
				throw new ArgumentException(Res.GetString("Sch_XsdDateTimeCompare", new object[]
				{
					left.TypeCode,
					right.TypeCode
				}));
			}
			return DateTime.Compare(left.GetZuluDateTime(), right.GetZuluDateTime());
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000E098C File Offset: 0x000DEB8C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			return XsdDateTime.Compare(this, (XsdDateTime)value);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000E09A4 File Offset: 0x000DEBA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			switch (this.InternalTypeCode)
			{
			case XsdDateTime.DateTimeTypeCode.DateTime:
				this.PrintDate(stringBuilder);
				stringBuilder.Append('T');
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Time:
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Date:
				this.PrintDate(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.GYearMonth:
			{
				char[] array = new char[XsdDateTime.Lzyyyy_MM];
				this.IntToCharArray(array, 0, this.Year, 4);
				array[XsdDateTime.Lzyyyy] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GYear:
			{
				char[] array = new char[XsdDateTime.Lzyyyy];
				this.IntToCharArray(array, 0, this.Year, 4);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonthDay:
			{
				char[] array = new char[XsdDateTime.Lz__mm_dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__mm_, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GDay:
			{
				char[] array = new char[XsdDateTime.Lz___dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				array[XsdDateTime.Lz__] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz___, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonth:
			{
				char[] array = new char[XsdDateTime.Lz__mm__];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				array[XsdDateTime.Lz__mm_] = '-';
				stringBuilder.Append(array);
				break;
			}
			}
			this.PrintZone(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000E0B6C File Offset: 0x000DED6C
		private void PrintDate(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.Lzyyyy_MM_dd];
			this.IntToCharArray(array, 0, this.Year, 4);
			array[XsdDateTime.Lzyyyy] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
			array[XsdDateTime.Lzyyyy_MM] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_MM_, this.Day);
			sb.Append(array);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000E0BD4 File Offset: 0x000DEDD4
		private void PrintTime(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.LzHH_mm_ss];
			this.ShortToCharArray(array, 0, this.Hour);
			array[XsdDateTime.LzHH] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_, this.Minute);
			array[XsdDateTime.LzHH_mm] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_mm_, this.Second);
			sb.Append(array);
			int num = this.Fraction;
			if (num != 0)
			{
				int num2 = 7;
				while (num % 10 == 0)
				{
					num2--;
					num /= 10;
				}
				array = new char[num2 + 1];
				array[0] = '.';
				this.IntToCharArray(array, 1, num, num2);
				sb.Append(array);
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000E0C78 File Offset: 0x000DEE78
		private void PrintZone(StringBuilder sb)
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				sb.Append('Z');
				return;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '+';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000E0D36 File Offset: 0x000DEF36
		private void IntToCharArray(char[] text, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				text[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000E0D57 File Offset: 0x000DEF57
		private void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000E0D74 File Offset: 0x000DEF74
		private DateTime GetZuluDateTime()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return this.dt;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			default:
				return this.dt.ToUniversalTime();
			}
		}

		// Token: 0x040012A0 RID: 4768
		private DateTime dt;

		// Token: 0x040012A1 RID: 4769
		private uint extra;

		// Token: 0x040012A2 RID: 4770
		private const uint TypeMask = 4278190080U;

		// Token: 0x040012A3 RID: 4771
		private const uint KindMask = 16711680U;

		// Token: 0x040012A4 RID: 4772
		private const uint ZoneHourMask = 65280U;

		// Token: 0x040012A5 RID: 4773
		private const uint ZoneMinuteMask = 255U;

		// Token: 0x040012A6 RID: 4774
		private const int TypeShift = 24;

		// Token: 0x040012A7 RID: 4775
		private const int KindShift = 16;

		// Token: 0x040012A8 RID: 4776
		private const int ZoneHourShift = 8;

		// Token: 0x040012A9 RID: 4777
		private const short maxFractionDigits = 7;

		// Token: 0x040012AA RID: 4778
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040012AB RID: 4779
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040012AC RID: 4780
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040012AD RID: 4781
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040012AE RID: 4782
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040012AF RID: 4783
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040012B0 RID: 4784
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040012B1 RID: 4785
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040012B2 RID: 4786
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040012B3 RID: 4787
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040012B4 RID: 4788
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040012B5 RID: 4789
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040012B6 RID: 4790
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040012B7 RID: 4791
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x040012B8 RID: 4792
		private static readonly int Lz_zz_zz = "-zz:zz".Length;

		// Token: 0x040012B9 RID: 4793
		private static readonly int Lz__ = "--".Length;

		// Token: 0x040012BA RID: 4794
		private static readonly int Lz__mm = "--MM".Length;

		// Token: 0x040012BB RID: 4795
		private static readonly int Lz__mm_ = "--MM-".Length;

		// Token: 0x040012BC RID: 4796
		private static readonly int Lz__mm__ = "--MM--".Length;

		// Token: 0x040012BD RID: 4797
		private static readonly int Lz__mm_dd = "--MM-dd".Length;

		// Token: 0x040012BE RID: 4798
		private static readonly int Lz___ = "---".Length;

		// Token: 0x040012BF RID: 4799
		private static readonly int Lz___dd = "---dd".Length;

		// Token: 0x040012C0 RID: 4800
		private static readonly XmlTypeCode[] typeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.DateTime,
			XmlTypeCode.Time,
			XmlTypeCode.Date,
			XmlTypeCode.GYearMonth,
			XmlTypeCode.GYear,
			XmlTypeCode.GMonthDay,
			XmlTypeCode.GDay,
			XmlTypeCode.GMonth
		};

		// Token: 0x020004B4 RID: 1204
		private enum DateTimeTypeCode
		{
			// Token: 0x04001F55 RID: 8021
			DateTime,
			// Token: 0x04001F56 RID: 8022
			Time,
			// Token: 0x04001F57 RID: 8023
			Date,
			// Token: 0x04001F58 RID: 8024
			GYearMonth,
			// Token: 0x04001F59 RID: 8025
			GYear,
			// Token: 0x04001F5A RID: 8026
			GMonthDay,
			// Token: 0x04001F5B RID: 8027
			GDay,
			// Token: 0x04001F5C RID: 8028
			GMonth,
			// Token: 0x04001F5D RID: 8029
			XdrDateTime
		}

		// Token: 0x020004B5 RID: 1205
		private enum XsdDateTimeKind
		{
			// Token: 0x04001F5F RID: 8031
			Unspecified,
			// Token: 0x04001F60 RID: 8032
			Zulu,
			// Token: 0x04001F61 RID: 8033
			LocalWestOfZulu,
			// Token: 0x04001F62 RID: 8034
			LocalEastOfZulu
		}

		// Token: 0x020004B6 RID: 1206
		private struct Parser
		{
			// Token: 0x06003196 RID: 12694 RVA: 0x001203B4 File Offset: 0x0011E5B4
			public bool Parse(string text, XsdDateTimeFlags kinds)
			{
				this.text = text;
				this.length = text.Length;
				int num = 0;
				while (num < this.length && char.IsWhiteSpace(text[num]))
				{
					num++;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Date | XsdDateTimeFlags.XdrDateTimeNoTz | XsdDateTimeFlags.XdrDateTime) && this.ParseDate(num))
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime) && this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.DateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Date) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.Date;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTime) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd) || (this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTimeNoTz))
					{
						if (!this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T'))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
						if (this.ParseTimeAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Time) && this.ParseTimeAndZoneAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrTimeNoTz) && this.ParseTimeAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear) && this.Parse4Dig(num, ref this.year) && 1 <= this.year)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth) && this.ParseChar(num + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(num + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM))
					{
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYearMonth;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYear) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy))
					{
						this.month = 1;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYear;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GMonth) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.Parse2Dig(num + XsdDateTime.Lz__, ref this.month) && 1 <= this.month && this.month <= 12)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay) && this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.Parse2Dig(num + XsdDateTime.Lz__mm_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, this.month) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm_dd))
					{
						this.year = 1904;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonthDay;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonth) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm) || (this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.ParseChar(num + XsdDateTime.Lz__mm_, '-') && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm__))))
					{
						this.year = 1904;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonth;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GDay) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.ParseChar(num + XsdDateTime.Lz__, '-') && this.Parse2Dig(num + XsdDateTime.Lz___, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, 1) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz___dd))
				{
					this.year = 1904;
					this.month = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.GDay;
					return true;
				}
				return false;
			}

			// Token: 0x06003197 RID: 12695 RVA: 0x001207E4 File Offset: 0x0011E9E4
			private bool ParseDate(int start)
			{
				return this.Parse4Dig(start, ref this.year) && 1 <= this.year && this.ParseChar(start + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseChar(start + XsdDateTime.Lzyyyy_MM, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_MM_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(this.year, this.month);
			}

			// Token: 0x06003198 RID: 12696 RVA: 0x00120895 File Offset: 0x0011EA95
			private bool ParseTimeAndZoneAndWhitespace(int start)
			{
				return this.ParseTime(ref start) && this.ParseZoneAndWhitespace(start);
			}

			// Token: 0x06003199 RID: 12697 RVA: 0x001208AD File Offset: 0x0011EAAD
			private bool ParseTimeAndWhitespace(int start)
			{
				if (this.ParseTime(ref start))
				{
					while (start < this.length)
					{
						start++;
					}
					return start == this.length;
				}
				return false;
			}

			// Token: 0x0600319A RID: 12698 RVA: 0x001208D4 File Offset: 0x0011EAD4
			private bool ParseTime(ref int start)
			{
				if (this.Parse2Dig(start, ref this.hour) && this.hour < 24 && this.ParseChar(start + XsdDateTime.LzHH, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_, ref this.minute) && this.minute < 60 && this.ParseChar(start + XsdDateTime.LzHH_mm, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_mm_, ref this.second) && this.second < 60)
				{
					start += XsdDateTime.LzHH_mm_ss;
					if (this.ParseChar(start, '.'))
					{
						this.fraction = 0;
						int num = 0;
						int num2 = 0;
						for (;;)
						{
							int num3 = start + 1;
							start = num3;
							if (num3 >= this.length)
							{
								break;
							}
							int num4 = (int)(this.text[start] - '0');
							if (9 < num4)
							{
								break;
							}
							if (num < 7)
							{
								this.fraction = this.fraction * 10 + num4;
							}
							else if (num == 7)
							{
								if (5 < num4)
								{
									num2 = 1;
								}
								else if (num4 == 5)
								{
									num2 = -1;
								}
							}
							else if (num2 < 0 && num4 != 0)
							{
								num2 = 1;
							}
							num++;
						}
						if (num < 7)
						{
							if (num == 0)
							{
								return false;
							}
							this.fraction *= XsdDateTime.Parser.Power10[7 - num];
						}
						else
						{
							if (num2 < 0)
							{
								num2 = (this.fraction & 1);
							}
							this.fraction += num2;
						}
					}
					return true;
				}
				this.hour = 0;
				return false;
			}

			// Token: 0x0600319B RID: 12699 RVA: 0x00120A44 File Offset: 0x0011EC44
			private bool ParseZoneAndWhitespace(int start)
			{
				if (start < this.length)
				{
					char c = this.text[start];
					if (c == 'Z' || c == 'z')
					{
						this.kind = XsdDateTime.XsdDateTimeKind.Zulu;
						start++;
					}
					else if (start + 5 < this.length && this.Parse2Dig(start + XsdDateTime.Lz_, ref this.zoneHour) && this.zoneHour <= 99 && this.ParseChar(start + XsdDateTime.Lz_zz, ':') && this.Parse2Dig(start + XsdDateTime.Lz_zz_, ref this.zoneMinute) && this.zoneMinute <= 99)
					{
						if (c == '-')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
						else if (c == '+')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
					}
				}
				while (start < this.length && char.IsWhiteSpace(this.text[start]))
				{
					start++;
				}
				return start == this.length;
			}

			// Token: 0x0600319C RID: 12700 RVA: 0x00120B3C File Offset: 0x0011ED3C
			private bool Parse4Dig(int start, ref int num)
			{
				if (start + 3 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					int num4 = (int)(this.text[start + 2] - '0');
					int num5 = (int)(this.text[start + 3] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
					{
						num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600319D RID: 12701 RVA: 0x00120BD4 File Offset: 0x0011EDD4
			private bool Parse2Dig(int start, ref int num)
			{
				if (start + 1 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
					{
						num = num2 * 10 + num3;
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600319E RID: 12702 RVA: 0x00120C2B File Offset: 0x0011EE2B
			private bool ParseChar(int start, char ch)
			{
				return start < this.length && this.text[start] == ch;
			}

			// Token: 0x0600319F RID: 12703 RVA: 0x00120C47 File Offset: 0x0011EE47
			private static bool Test(XsdDateTimeFlags left, XsdDateTimeFlags right)
			{
				return (left & right) > (XsdDateTimeFlags)0;
			}

			// Token: 0x04001F63 RID: 8035
			private const int leapYear = 1904;

			// Token: 0x04001F64 RID: 8036
			private const int firstMonth = 1;

			// Token: 0x04001F65 RID: 8037
			private const int firstDay = 1;

			// Token: 0x04001F66 RID: 8038
			public XsdDateTime.DateTimeTypeCode typeCode;

			// Token: 0x04001F67 RID: 8039
			public int year;

			// Token: 0x04001F68 RID: 8040
			public int month;

			// Token: 0x04001F69 RID: 8041
			public int day;

			// Token: 0x04001F6A RID: 8042
			public int hour;

			// Token: 0x04001F6B RID: 8043
			public int minute;

			// Token: 0x04001F6C RID: 8044
			public int second;

			// Token: 0x04001F6D RID: 8045
			public int fraction;

			// Token: 0x04001F6E RID: 8046
			public XsdDateTime.XsdDateTimeKind kind;

			// Token: 0x04001F6F RID: 8047
			public int zoneHour;

			// Token: 0x04001F70 RID: 8048
			public int zoneMinute;

			// Token: 0x04001F71 RID: 8049
			private string text;

			// Token: 0x04001F72 RID: 8050
			private int length;

			// Token: 0x04001F73 RID: 8051
			private static int[] Power10 = new int[]
			{
				-1,
				10,
				100,
				1000,
				10000,
				100000,
				1000000
			};
		}
	}
}
