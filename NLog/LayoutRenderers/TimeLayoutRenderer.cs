using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F6 RID: 246
	[ThreadAgnostic]
	[LayoutRenderer("time")]
	public class TimeLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000F95B File Offset: 0x0000DB5B
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0000F963 File Offset: 0x0000DB63
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060006FA RID: 1786 RVA: 0x0000F96C File Offset: 0x0000DB6C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			CultureInfo culture = base.GetCulture(logEvent, null);
			string value;
			string value2;
			if (culture != null)
			{
				value = culture.DateTimeFormat.TimeSeparator;
				value2 = culture.NumberFormat.NumberDecimalSeparator;
			}
			else
			{
				value = ":";
				value2 = ".";
			}
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Hour);
			builder.Append(value);
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Minute);
			builder.Append(value);
			TimeLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Second);
			builder.Append(value2);
			TimeLayoutRenderer.Append4DigitsZeroPadded(builder, (int)(dateTime.Ticks % 10000000L) / 1000);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		private static void Append2DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 10 + 48));
			builder.Append((char)(number % 10 + 48));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		private static void Append4DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 1000 % 10 + 48));
			builder.Append((char)(number / 100 % 10 + 48));
			builder.Append((char)(number / 10 % 10 + 48));
			builder.Append((char)(number / 1 % 10 + 48));
		}
	}
}
