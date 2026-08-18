using System;
using System.Text;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x020000B2 RID: 178
	internal static class StringBuilderExt
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x0000C344 File Offset: 0x0000A544
		public static void Append(this StringBuilder builder, object o, LogEventInfo logEvent, LoggingConfiguration configuration)
		{
			IFormatProvider formatProvider = logEvent.FormatProvider;
			if (formatProvider == null && configuration != null)
			{
				formatProvider = configuration.DefaultCultureInfo;
			}
			builder.Append(Convert.ToString(o, formatProvider));
		}
	}
}
