using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009B RID: 155
	internal sealed class MessagePatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F29F File Offset: 0x0000D49F
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			loggingEvent.WriteRenderedMessage(writer);
		}
	}
}
