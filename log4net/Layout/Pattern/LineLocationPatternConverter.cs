using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000098 RID: 152
	internal sealed class LineLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000F121 File Offset: 0x0000D321
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.LineNumber);
		}
	}
}
