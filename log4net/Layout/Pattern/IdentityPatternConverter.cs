using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000096 RID: 150
	internal sealed class IdentityPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Identity);
		}
	}
}
