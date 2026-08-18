using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x020000A4 RID: 164
	internal sealed class UserNamePatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.UserName);
		}
	}
}
