using System;
using System.IO;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000E4 RID: 228
	internal class UtcDatePatternConverter : DatePatternConverter
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x00014EC0 File Offset: 0x000130C0
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				this.m_dateFormatter.FormatDate(DateTime.UtcNow, writer);
			}
			catch (Exception exception)
			{
				LogLog.Error(UtcDatePatternConverter.declaringType, "Error occurred while converting date.", exception);
			}
		}

		// Token: 0x04000295 RID: 661
		private static readonly Type declaringType = typeof(UtcDatePatternConverter);
	}
}
