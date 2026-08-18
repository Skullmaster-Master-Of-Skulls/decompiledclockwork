using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009D RID: 157
	internal sealed class NdcPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F2CB File Offset: 0x0000D4CB
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.LookupProperty("NDC"));
		}
	}
}
