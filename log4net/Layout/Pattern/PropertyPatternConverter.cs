using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200009E RID: 158
	internal sealed class PropertyPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.LookupProperty(this.Option));
				return;
			}
			PatternConverter.WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
		}
	}
}
