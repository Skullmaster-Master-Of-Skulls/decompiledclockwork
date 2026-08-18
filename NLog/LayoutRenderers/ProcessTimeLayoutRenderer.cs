using System;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E9 RID: 233
	[LayoutRenderer("processtime")]
	[ThreadAgnostic]
	public class ProcessTimeLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0000EECC File Offset: 0x0000D0CC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			TimeSpan ts = logEvent.TimeStamp.ToUniversalTime() - LogEventInfo.ZeroDate;
			CultureInfo culture = base.GetCulture(logEvent, null);
			ProcessTimeLayoutRenderer.WritetTimestamp(builder, ts, culture);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000EF04 File Offset: 0x0000D104
		internal static void WritetTimestamp(StringBuilder builder, TimeSpan ts, CultureInfo culture)
		{
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
			if (ts.Hours < 10)
			{
				builder.Append('0');
			}
			builder.Append(ts.Hours);
			builder.Append(value);
			if (ts.Minutes < 10)
			{
				builder.Append('0');
			}
			builder.Append(ts.Minutes);
			builder.Append(value);
			if (ts.Seconds < 10)
			{
				builder.Append('0');
			}
			builder.Append(ts.Seconds);
			builder.Append(value2);
			if (ts.Milliseconds < 100)
			{
				builder.Append('0');
				if (ts.Milliseconds < 10)
				{
					builder.Append('0');
					if (ts.Milliseconds < 0)
					{
						builder.Append('0');
						return;
					}
				}
			}
			builder.Append(ts.Milliseconds);
		}
	}
}
