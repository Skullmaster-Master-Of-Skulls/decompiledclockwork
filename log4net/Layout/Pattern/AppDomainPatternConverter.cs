using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200008C RID: 140
	internal sealed class AppDomainPatternConverter : PatternLayoutConverter
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Domain);
		}
	}
}
