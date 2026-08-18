using System;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009A RID: 154
	internal sealed class LoggerPatternConverter : NamedPatternConverter
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F28F File Offset: 0x0000D48F
		protected override string GetFullyQualifiedName(LoggingEvent loggingEvent)
		{
			return loggingEvent.LoggerName;
		}
	}
}
