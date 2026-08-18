using System;
using log4net.Layout;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000EB RID: 235
	internal class PatternLayoutConverter : IConvertFrom
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001544F File Offset: 0x0001364F
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00015464 File Offset: 0x00013664
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return new PatternLayout(text);
			}
			throw ConversionNotSupportedException.Create(typeof(PatternLayout), source);
		}
	}
}
