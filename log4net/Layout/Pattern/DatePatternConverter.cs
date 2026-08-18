using System;
using System.IO;
using log4net.Core;
using log4net.DateFormatter;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000092 RID: 146
	internal class DatePatternConverter : PatternLayoutConverter, IOptionHandler
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x0000EE84 File Offset: 0x0000D084
		public void ActivateOptions()
		{
			string text = this.Option;
			if (text == null)
			{
				text = "ISO8601";
			}
			if (SystemInfo.EqualsIgnoringCase(text, "ISO8601"))
			{
				this.m_dateFormatter = new Iso8601DateFormatter();
				return;
			}
			if (SystemInfo.EqualsIgnoringCase(text, "ABSOLUTE"))
			{
				this.m_dateFormatter = new AbsoluteTimeDateFormatter();
				return;
			}
			if (SystemInfo.EqualsIgnoringCase(text, "DATE"))
			{
				this.m_dateFormatter = new DateTimeDateFormatter();
				return;
			}
			try
			{
				this.m_dateFormatter = new SimpleDateFormatter(text);
			}
			catch (Exception exception)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Could not instantiate SimpleDateFormatter with [" + text + "]", exception);
				this.m_dateFormatter = new Iso8601DateFormatter();
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000EF34 File Offset: 0x0000D134
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			try
			{
				this.m_dateFormatter.FormatDate(loggingEvent.TimeStamp, writer);
			}
			catch (Exception exception)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Error occurred while converting date.", exception);
			}
		}

		// Token: 0x040001FE RID: 510
		protected IDateFormatter m_dateFormatter;

		// Token: 0x040001FF RID: 511
		private static readonly Type declaringType = typeof(DatePatternConverter);
	}
}
