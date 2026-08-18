using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000E5 RID: 229
	internal class BooleanConverter : IConvertFrom
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x00014F1D File Offset: 0x0001311D
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00014F30 File Offset: 0x00013130
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return bool.Parse(text);
			}
			throw ConversionNotSupportedException.Create(typeof(bool), source);
		}
	}
}
