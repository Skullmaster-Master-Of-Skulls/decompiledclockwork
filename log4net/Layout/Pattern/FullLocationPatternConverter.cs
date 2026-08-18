using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000095 RID: 149
	internal sealed class FullLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.FullInfo);
		}
	}
}
