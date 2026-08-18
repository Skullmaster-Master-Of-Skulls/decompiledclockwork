using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x020000A2 RID: 162
	internal sealed class ThreadPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000F589 File Offset: 0x0000D789
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.ThreadName);
		}
	}
}
