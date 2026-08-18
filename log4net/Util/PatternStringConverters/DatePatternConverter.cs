using System;
using System.IO;
using log4net.Core;
using log4net.DateFormatter;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DA RID: 218
	internal class DatePatternConverter : PatternConverter, IOptionHandler
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00014858 File Offset: 0x00012A58
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

		// Token: 0x06000668 RID: 1640 RVA: 0x00014908 File Offset: 0x00012B08
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				this.m_dateFormatter.FormatDate(DateTime.Now, writer);
			}
			catch (Exception exception)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Error occurred while converting date.", exception);
			}
		}

		// Token: 0x0400028B RID: 651
		protected IDateFormatter m_dateFormatter;

		// Token: 0x0400028C RID: 652
		private static readonly Type declaringType = typeof(DatePatternConverter);
	}
}
