using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000EC RID: 236
	internal class PatternStringConverter : IConvertTo, IConvertFrom
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001549A File Offset: 0x0001369A
		public bool CanConvertTo(Type targetType)
		{
			return typeof(string).IsAssignableFrom(targetType);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000154AC File Offset: 0x000136AC
		public object ConvertTo(object source, Type targetType)
		{
			PatternString patternString = source as PatternString;
			if (patternString != null && this.CanConvertTo(targetType))
			{
				return patternString.Format();
			}
			throw ConversionNotSupportedException.Create(targetType, source);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000154DA File Offset: 0x000136DA
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000154EC File Offset: 0x000136EC
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return new PatternString(text);
			}
			throw ConversionNotSupportedException.Create(typeof(PatternString), source);
		}
	}
}
