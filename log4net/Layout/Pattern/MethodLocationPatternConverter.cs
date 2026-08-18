using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009C RID: 156
	internal sealed class MethodLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.MethodName);
		}
	}
}
