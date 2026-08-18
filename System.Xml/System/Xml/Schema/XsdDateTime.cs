using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020002A2 RID: 674
	internal struct XsdDateTime
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x000938A3 File Offset: 0x000928A3
		public XsdDateTime(string text)
		{
			this = new XsdDateTime(text, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000938B4 File Offset: 0x000928B4
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

		// Token: 0x0600204D RID: 8269 RVA: 0x00093907 File Offset: 0x00092907
		private XsdDateTime(XsdDateTime.Parser parser)
		{
			this = default(XsdDateTime);
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00093918 File Offset: 0x00092918
		private void InitiateXsdDateTime(XsdDateTime.Parser parser)
		{
			this.dt = new DateTime(parser.year, parser.month, parser.day, parser.hour, parser.minute, parser.second);
			if (parser.fraction != 0)
			{
				this.dt = this.dt.AddTicks((long)parser.fraction);
			}
			this.extra = (uint)((int)parser.typeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)parser.kind << 16) | (XsdDateTime.DateTimeTypeCode)(parser.zoneHour << 8) | (XsdDateTime.DateTimeTypeCode)parser.zoneMinute);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000939AC File Offset: 0x000929AC
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

		// Token: 0x06002050 RID: 8272 RVA: 0x000939E4 File Offset: 0x000929E4
		public XsdDateTime(DateTime dateTime, XsdDateTimeFlags kinds)
		{
			this.dt = dateTime;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			int num = 0;
			int num2 = 0;
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			switch (dateTime.Kind)
			{
			case DateTimeKind.Unspecified:
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Unspecified;
				break;
			case DateTimeKind.Utc:
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
				break;
			default:
			{
				TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
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
				break;
			}
			}
			this.extra = (uint)((int)dateTimeTypeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(num << 8) | (XsdDateTime.DateTimeTypeCode)num2);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00093A7C File Offset: 0x00092A7C
		public XsdDateTime(DateTimeOffset dateTimeOffset)
		{
			this = new XsdDateTime(dateTimeOffset, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00093A88 File Offset: 0x00092A88
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00093B0A File Offset: 0x00092B0A
		private XsdDateTime.DateTimeTypeCode InternalTypeCode
		{
			get
			{
				return (XsdDateTime.DateTimeTypeCode)((this.extra & 4278190080U) >> 24);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00093B1B File Offset: 0x00092B1B
		private XsdDateTime.XsdDateTimeKind InternalKind
		{
			get
			{
				return (XsdDateTime.XsdDateTimeKind)((this.extra & 16711680U) >> 16);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00093B2C File Offset: 0x00092B2C
		public XmlTypeCode TypeCode
		{
			get
			{
				return XsdDateTime.typeCodes[(int)this.InternalTypeCode];
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00093B3C File Offset: 0x00092B3C
		public DateTimeKind Kind
		{
			get
			{
				switch (this.InternalKind)
				{
				case XsdDateTime.XsdDateTimeKind.Unspecified:
					return DateTimeKind.Unspecified;
				case XsdDateTime.XsdDateTimeKind.Zulu:
					return DateTimeKind.Utc;
				default:
					return DateTimeKind.Local;
				}
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00093B65 File Offset: 0x00092B65
		public int Year
		{
			get
			{
				return this.dt.Year;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00093B72 File Offset: 0x00092B72
		public int Month
		{
			get
			{
				return this.dt.Month;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00093B7F File Offset: 0x00092B7F
		public int Day
		{
			get
			{
				return this.dt.Day;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00093B8C File Offset: 0x00092B8C
		public int Hour
		{
			get
			{
				return this.dt.Hour;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00093B99 File Offset: 0x00092B99
		public int Minute
		{
			get
			{
				return this.dt.Minute;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00093BA6 File Offset: 0x00092BA6
		public int Second
		{
			get
			{
				return this.dt.Second;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00093BB4 File Offset: 0x00092BB4
		public int Fraction
		{
			get
			{
				return (int)(this.dt.Ticks - new DateTime(this.dt.Year, this.dt.Month, this.dt.Day, this.dt.Hour, this.dt.Minute, this.dt.Second).Ticks);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00093C20 File Offset: 0x00092C20
		public int ZoneHour
		{
			get
			{
				return (int)((this.extra & 65280U) >> 8);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00093C40 File Offset: 0x00092C40
		public int ZoneMinute
		{
			get
			{
				return (int)(this.extra & 255U);
			}
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00093C5C File Offset: 0x00092C5C
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

		// Token: 0x06002061 RID: 8289 RVA: 0x00093CF8 File Offset: 0x00092CF8
		public static implicit operator DateTime(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime result;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				switch (internalTypeCode)
				{
				case XsdDateTime.DateTimeTypeCode.GDay:
				case XsdDateTime.DateTimeTypeCode.GMonth:
					result = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
					break;
				default:
					result = xdt.dt;
					break;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan value = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				result = xdt.dt.Add(value);
			}
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				result = new DateTime(result.Ticks, DateTimeKind.Utc);
				break;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				try
				{
					result = result.Add(new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				}
				catch (ArgumentOutOfRangeException)
				{
					return new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Local);
				}
				result = new DateTime(result.Ticks, DateTimeKind.Utc).ToLocalTime();
				break;
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				try
				{
					result = result.Subtract(new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				}
				catch (ArgumentOutOfRangeException)
				{
					return new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Local);
				}
				result = new DateTime(result.Ticks, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			return result;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00093E98 File Offset: 0x00092E98
		public static implicit operator DateTimeOffset(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				switch (internalTypeCode)
				{
				case XsdDateTime.DateTimeTypeCode.GDay:
				case XsdDateTime.DateTimeTypeCode.GMonth:
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
					break;
				default:
					dateTime = xdt.dt;
					break;
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
			result = new DateTimeOffset(dateTime, TimeZone.CurrentTimeZone.GetUtcOffset(dateTime));
			return result;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00093FC8 File Offset: 0x00092FC8
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

		// Token: 0x06002064 RID: 8292 RVA: 0x00094052 File Offset: 0x00093052
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			return XsdDateTime.Compare(this, (XsdDateTime)value);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0009406C File Offset: 0x0009306C
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

		// Token: 0x06002066 RID: 8294 RVA: 0x00094234 File Offset: 0x00093234
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

		// Token: 0x06002067 RID: 8295 RVA: 0x0009429C File Offset: 0x0009329C
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

		// Token: 0x06002068 RID: 8296 RVA: 0x00094340 File Offset: 0x00093340
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

		// Token: 0x06002069 RID: 8297 RVA: 0x000943FE File Offset: 0x000933FE
		private void IntToCharArray(char[] text, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				text[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0009441F File Offset: 0x0009341F
		private void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0009443C File Offset: 0x0009343C
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

		// Token: 0x0400136A RID: 4970
		private const uint TypeMask = 4278190080U;

		// Token: 0x0400136B RID: 4971
		private const uint KindMask = 16711680U;

		// Token: 0x0400136C RID: 4972
		private const uint ZoneHourMask = 65280U;

		// Token: 0x0400136D RID: 4973
		private const uint ZoneMinuteMask = 255U;

		// Token: 0x0400136E RID: 4974
		private const int TypeShift = 24;

		// Token: 0x0400136F RID: 4975
		private const int KindShift = 16;

		// Token: 0x04001370 RID: 4976
		private const int ZoneHourShift = 8;

		// Token: 0x04001371 RID: 4977
		private const short maxFractionDigits = 7;

		// Token: 0x04001372 RID: 4978
		private DateTime dt;

		// Token: 0x04001373 RID: 4979
		private uint extra;

		// Token: 0x04001374 RID: 4980
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x04001375 RID: 4981
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04001376 RID: 4982
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04001377 RID: 4983
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x04001378 RID: 4984
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x04001379 RID: 4985
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x0400137A RID: 4986
		private static readonly int LzHH = "HH".Length;

		// Token: 0x0400137B RID: 4987
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x0400137C RID: 4988
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x0400137D RID: 4989
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x0400137E RID: 4990
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x0400137F RID: 4991
		private static readonly int Lz_ = "-".Length;

		// Token: 0x04001380 RID: 4992
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x04001381 RID: 4993
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x04001382 RID: 4994
		private static readonly int Lz_zz_zz = "-zz:zz".Length;

		// Token: 0x04001383 RID: 4995
		private static readonly int Lz__ = "--".Length;

		// Token: 0x04001384 RID: 4996
		private static readonly int Lz__mm = "--MM".Length;

		// Token: 0x04001385 RID: 4997
		private static readonly int Lz__mm_ = "--MM-".Length;

		// Token: 0x04001386 RID: 4998
		private static readonly int Lz__mm__ = "--MM--".Length;

		// Token: 0x04001387 RID: 4999
		private static readonly int Lz__mm_dd = "--MM-dd".Length;

		// Token: 0x04001388 RID: 5000
		private static readonly int Lz___ = "---".Length;

		// Token: 0x04001389 RID: 5001
		private static readonly int Lz___dd = "---dd".Length;

		// Token: 0x0400138A RID: 5002
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

		// Token: 0x020002A3 RID: 675
		private enum DateTimeTypeCode
		{
			// Token: 0x0400138C RID: 5004
			DateTime,
			// Token: 0x0400138D RID: 5005
			Time,
			// Token: 0x0400138E RID: 5006
			Date,
			// Token: 0x0400138F RID: 5007
			GYearMonth,
			// Token: 0x04001390 RID: 5008
			GYear,
			// Token: 0x04001391 RID: 5009
			GMonthDay,
			// Token: 0x04001392 RID: 5010
			GDay,
			// Token: 0x04001393 RID: 5011
			GMonth,
			// Token: 0x04001394 RID: 5012
			XdrDateTime
		}

		// Token: 0x020002A4 RID: 676
		private enum XsdDateTimeKind
		{
			// Token: 0x04001396 RID: 5014
			Unspecified,
			// Token: 0x04001397 RID: 5015
			Zulu,
			// Token: 0x04001398 RID: 5016
			LocalWestOfZulu,
			// Token: 0x04001399 RID: 5017
			LocalEastOfZulu
		}

		// Token: 0x020002A5 RID: 677
		private struct Parser
		{
			// Token: 0x0600206D RID: 8301 RVA: 0x00094640 File Offset: 0x00093640
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

			// Token: 0x0600206E RID: 8302 RVA: 0x00094A70 File Offset: 0x00093A70
			private bool ParseDate(int start)
			{
				return this.Parse4Dig(start, ref this.year) && 1 <= this.year && this.ParseChar(start + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseChar(start + XsdDateTime.Lzyyyy_MM, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_MM_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(this.year, this.month);
			}

			// Token: 0x0600206F RID: 8303 RVA: 0x00094B21 File Offset: 0x00093B21
			private bool ParseTimeAndZoneAndWhitespace(int start)
			{
				return this.ParseTime(ref start) && this.ParseZoneAndWhitespace(start);
			}

			// Token: 0x06002070 RID: 8304 RVA: 0x00094B39 File Offset: 0x00093B39
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

			// Token: 0x06002071 RID: 8305 RVA: 0x00094B60 File Offset: 0x00093B60
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
						while (++start < this.length)
						{
							int num3 = (int)(this.text[start] - '0');
							if (9 < num3)
							{
								break;
							}
							if (num < 7)
							{
								this.fraction = this.fraction * 10 + num3;
							}
							else if (num == 7)
							{
								if (5 < num3)
								{
									num2 = 1;
								}
								else if (num3 == 5)
								{
									num2 = -1;
								}
							}
							else if (num2 < 0 && num3 != 0)
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

			// Token: 0x06002072 RID: 8306 RVA: 0x00094CD0 File Offset: 0x00093CD0
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

			// Token: 0x06002073 RID: 8307 RVA: 0x00094DC8 File Offset: 0x00093DC8
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

			// Token: 0x06002074 RID: 8308 RVA: 0x00094E60 File Offset: 0x00093E60
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

			// Token: 0x06002075 RID: 8309 RVA: 0x00094EB7 File Offset: 0x00093EB7
			private bool ParseChar(int start, char ch)
			{
				return start < this.length && this.text[start] == ch;
			}

			// Token: 0x06002076 RID: 8310 RVA: 0x00094ED3 File Offset: 0x00093ED3
			private static bool Test(XsdDateTimeFlags left, XsdDateTimeFlags right)
			{
				return (left & right) != (XsdDateTimeFlags)0;
			}

			// Token: 0x0400139A RID: 5018
			private const int leapYear = 1904;

			// Token: 0x0400139B RID: 5019
			private const int firstMonth = 1;

			// Token: 0x0400139C RID: 5020
			private const int firstDay = 1;

			// Token: 0x0400139D RID: 5021
			public XsdDateTime.DateTimeTypeCode typeCode;

			// Token: 0x0400139E RID: 5022
			public int year;

			// Token: 0x0400139F RID: 5023
			public int month;

			// Token: 0x040013A0 RID: 5024
			public int day;

			// Token: 0x040013A1 RID: 5025
			public int hour;

			// Token: 0x040013A2 RID: 5026
			public int minute;

			// Token: 0x040013A3 RID: 5027
			public int second;

			// Token: 0x040013A4 RID: 5028
			public int fraction;

			// Token: 0x040013A5 RID: 5029
			public XsdDateTime.XsdDateTimeKind kind;

			// Token: 0x040013A6 RID: 5030
			public int zoneHour;

			// Token: 0x040013A7 RID: 5031
			public int zoneMinute;

			// Token: 0x040013A8 RID: 5032
			private string text;

			// Token: 0x040013A9 RID: 5033
			private int length;

			// Token: 0x040013AA RID: 5034
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
