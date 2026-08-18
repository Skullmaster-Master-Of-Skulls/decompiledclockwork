using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DC RID: 220
	[LayoutRenderer("longdate")]
	[ThreadAgnostic]
	public class LongDateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x0600066A RID: 1642 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			LongDateLayoutRenderer.Append4DigitsZeroPadded(builder, dateTime.Year);
			builder.Append('-');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Month);
			builder.Append('-');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Day);
			builder.Append(' ');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Hour);
			builder.Append(':');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Minute);
			builder.Append(':');
			LongDateLayoutRenderer.Append2DigitsZeroPadded(builder, dateTime.Second);
			builder.Append('.');
			LongDateLayoutRenderer.Append4DigitsZeroPadded(builder, (int)(dateTime.Ticks % 10000000L) / 1000);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000E89B File Offset: 0x0000CA9B
		private static void Append2DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 10 + 48));
			builder.Append((char)(number % 10 + 48));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000E8BC File Offset: 0x0000CABC
		private static void Append4DigitsZeroPadded(StringBuilder builder, int number)
		{
			builder.Append((char)(number / 1000 % 10 + 48));
			builder.Append((char)(number / 100 % 10 + 48));
			builder.Append((char)(number / 10 % 10 + 48));
			builder.Append((char)(number / 1 % 10 + 48));
		}
	}
}
