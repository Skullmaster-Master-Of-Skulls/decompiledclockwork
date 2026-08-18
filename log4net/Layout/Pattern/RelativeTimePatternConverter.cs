using System;
using System.Globalization;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009F RID: 159
	internal sealed class RelativeTimePatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0000F32C File Offset: 0x0000D52C
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(RelativeTimePatternConverter.TimeDifferenceInMillis(LoggingEvent.StartTimeUtc, loggingEvent.TimeStampUtc).ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F35C File Offset: 0x0000D55C
		private static long TimeDifferenceInMillis(DateTime start, DateTime end)
		{
			return (long)(end.ToUniversalTime() - start.ToUniversalTime()).TotalMilliseconds;
		}
	}
}
