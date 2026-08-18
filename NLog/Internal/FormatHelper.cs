using System;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x02000091 RID: 145
	internal static class FormatHelper
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000A5AC File Offset: 0x000087AC
		public static string ToStringWithOptionalFormat(this object value, string format, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (format == null)
			{
				return Convert.ToString(value, formatProvider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return Convert.ToString(value, formatProvider);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000A5E8 File Offset: 0x000087E8
		internal static string ConvertToString(object o, IFormatProvider formatProvider)
		{
			if (formatProvider == null && !(o is string))
			{
				LoggingConfiguration configuration = LogManager.Configuration;
				if (configuration != null)
				{
					formatProvider = configuration.DefaultCultureInfo;
				}
			}
			return Convert.ToString(o, formatProvider);
		}
	}
}
