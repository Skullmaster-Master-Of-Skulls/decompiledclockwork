using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x020000A5 RID: 165
	internal class UtcDatePatternConverter : DatePatternConverter
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			try
			{
				this.m_dateFormatter.FormatDate(loggingEvent.TimeStampUtc, writer);
			}
			catch (Exception exception)
			{
				LogLog.Error(UtcDatePatternConverter.declaringType, "Error occurred while converting date.", exception);
			}
		}

		// Token: 0x04000206 RID: 518
		private static readonly Type declaringType = typeof(UtcDatePatternConverter);
	}
}
