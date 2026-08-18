using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000094 RID: 148
	internal sealed class FileLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F0BA File Offset: 0x0000D2BA
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.FileName);
		}
	}
}
