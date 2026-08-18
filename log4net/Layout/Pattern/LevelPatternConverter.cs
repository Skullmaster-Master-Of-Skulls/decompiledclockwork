using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000097 RID: 151
	internal sealed class LevelPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F106 File Offset: 0x0000D306
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Level.DisplayName);
		}
	}
}
