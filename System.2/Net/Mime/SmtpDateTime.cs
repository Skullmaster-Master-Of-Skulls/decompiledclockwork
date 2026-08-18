using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Mime
{
	// Token: 0x02000251 RID: 593
	internal class SmtpDateTime
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x00074E9C File Offset: 0x0007309C
		internal static IDictionary<string, TimeSpan> InitializeShortHandLookups()
		{
			return new Dictionary<string, TimeSpan>
			{
				{
					"UT",
					TimeSpan.Zero
				},
				{
					"GMT",
					TimeSpan.Zero
				},
				{
					"EDT",
					new TimeSpan(-4, 0, 0)
				},
				{
					"EST",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CDT",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CST",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MDT",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MST",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PDT",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PST",
					new TimeSpan(-8, 0, 0)
				}
			};
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00074F70 File Offset: 0x00073170
		internal SmtpDateTime(DateTime value)
		{
			this.date = value;
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				this.unknownTimeZone = true;
				return;
			case DateTimeKind.Utc:
				this.timeZone = TimeSpan.Zero;
				return;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				this.timeZone = this.ValidateAndGetSanitizedTimeSpan(utcOffset);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00074FD4 File Offset: 0x000731D4
		internal SmtpDateTime(string value)
		{
			string timeZoneString;
			this.date = this.ParseValue(value, out timeZoneString);
			if (!this.TryParseTimeZoneString(timeZoneString, out this.timeZone))
			{
				this.unknownTimeZone = true;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0007500C File Offset: 0x0007320C
		internal DateTime Date
		{
			get
			{
				if (this.unknownTimeZone)
				{
					return DateTime.SpecifyKind(this.date, DateTimeKind.Unspecified);
				}
				DateTimeOffset dateTimeOffset = new DateTimeOffset(this.date, this.timeZone);
				return dateTimeOffset.LocalDateTime;
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00075048 File Offset: 0x00073248
		public override string ToString()
		{
			if (this.unknownTimeZone)
			{
				return string.Format("{0} {1}", this.FormatDate(this.date), "-0000");
			}
			return string.Format("{0} {1}", this.FormatDate(this.date), this.TimeSpanToOffset(this.timeZone));
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0007509C File Offset: 0x0007329C
		internal void ValidateAndGetTimeZoneOffsetValues(string offset, out bool positive, out int hours, out int minutes)
		{
			if (offset.Length != 5)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			positive = offset.StartsWith("+");
			if (!int.TryParse(offset.Substring(1, 2), NumberStyles.None, CultureInfo.InvariantCulture, out hours))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			if (!int.TryParse(offset.Substring(3, 2), NumberStyles.None, CultureInfo.InvariantCulture, out minutes))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			if (minutes > 59)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00075134 File Offset: 0x00073334
		internal void ValidateTimeZoneShortHandValue(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsLetter(value, i))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
				}
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0007516C File Offset: 0x0007336C
		internal string FormatDate(DateTime value)
		{
			return value.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0007518C File Offset: 0x0007338C
		internal DateTime ParseValue(string data, out string timeZone)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			int num = data.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
			}
			int num2 = data.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars, num);
			if (num2 == -1)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
			}
			string s = data.Substring(0, num2).Trim();
			DateTime result;
			if (!DateTime.TryParseExact(s, SmtpDateTime.validDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			string text = data.Substring(num2).Trim();
			int num3 = text.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars);
			if (num3 != -1)
			{
				text = text.Substring(0, num3);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			timeZone = text;
			return result;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00075270 File Offset: 0x00073470
		internal bool TryParseTimeZoneString(string timeZoneString, out TimeSpan timeZone)
		{
			timeZone = TimeSpan.Zero;
			if (timeZoneString == "-0000")
			{
				return false;
			}
			if (timeZoneString[0] == '+' || timeZoneString[0] == '-')
			{
				bool flag;
				int num;
				int num2;
				this.ValidateAndGetTimeZoneOffsetValues(timeZoneString, out flag, out num, out num2);
				if (!flag)
				{
					if (num != 0)
					{
						num *= -1;
					}
					else if (num2 != 0)
					{
						num2 *= -1;
					}
				}
				timeZone = new TimeSpan(num, num2, 0);
				return true;
			}
			this.ValidateTimeZoneShortHandValue(timeZoneString);
			if (SmtpDateTime.timeZoneOffsetLookup.ContainsKey(timeZoneString))
			{
				timeZone = SmtpDateTime.timeZoneOffsetLookup[timeZoneString];
				return true;
			}
			return false;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00075308 File Offset: 0x00073508
		internal TimeSpan ValidateAndGetSanitizedTimeSpan(TimeSpan span)
		{
			TimeSpan result = new TimeSpan(span.Days, span.Hours, span.Minutes, 0, 0);
			if (Math.Abs(result.Ticks) > SmtpDateTime.timeSpanMaxTicks)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			return result;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00075358 File Offset: 0x00073558
		internal string TimeSpanToOffset(TimeSpan span)
		{
			if (span.Ticks == 0L)
			{
				return "+0000";
			}
			uint num = (uint)Math.Abs(Math.Floor(span.TotalHours));
			uint num2 = (uint)Math.Abs(span.Minutes);
			string str = (span.Ticks > 0L) ? "+" : "-";
			if (num < 10U)
			{
				str += "0";
			}
			str += num.ToString();
			if (num2 < 10U)
			{
				str += "0";
			}
			return str + num2.ToString();
		}

		// Token: 0x04001753 RID: 5971
		internal const string unknownTimeZoneDefaultOffset = "-0000";

		// Token: 0x04001754 RID: 5972
		internal const string utcDefaultTimeZoneOffset = "+0000";

		// Token: 0x04001755 RID: 5973
		internal const int offsetLength = 5;

		// Token: 0x04001756 RID: 5974
		internal const int maxMinuteValue = 59;

		// Token: 0x04001757 RID: 5975
		internal const string dateFormatWithDayOfWeek = "ddd, dd MMM yyyy HH:mm:ss";

		// Token: 0x04001758 RID: 5976
		internal const string dateFormatWithoutDayOfWeek = "dd MMM yyyy HH:mm:ss";

		// Token: 0x04001759 RID: 5977
		internal const string dateFormatWithDayOfWeekAndNoSeconds = "ddd, dd MMM yyyy HH:mm";

		// Token: 0x0400175A RID: 5978
		internal const string dateFormatWithoutDayOfWeekAndNoSeconds = "dd MMM yyyy HH:mm";

		// Token: 0x0400175B RID: 5979
		internal static readonly string[] validDateTimeFormats = new string[]
		{
			"ddd, dd MMM yyyy HH:mm:ss",
			"dd MMM yyyy HH:mm:ss",
			"ddd, dd MMM yyyy HH:mm",
			"dd MMM yyyy HH:mm"
		};

		// Token: 0x0400175C RID: 5980
		internal static readonly char[] allowedWhiteSpaceChars = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x0400175D RID: 5981
		internal static readonly IDictionary<string, TimeSpan> timeZoneOffsetLookup = SmtpDateTime.InitializeShortHandLookups();

		// Token: 0x0400175E RID: 5982
		internal static readonly long timeSpanMaxTicks = 3599400000000L;

		// Token: 0x0400175F RID: 5983
		internal static readonly int offsetMaxValue = 9959;

		// Token: 0x04001760 RID: 5984
		private readonly DateTime date;

		// Token: 0x04001761 RID: 5985
		private readonly TimeSpan timeZone;

		// Token: 0x04001762 RID: 5986
		private readonly bool unknownTimeZone;
	}
}
