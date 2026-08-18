using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000ED RID: 237
	internal class TypeConverter : IConvertFrom
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00015522 File Offset: 0x00013722
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00015534 File Offset: 0x00013734
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return SystemInfo.GetTypeFromString(text, true, true);
			}
			throw ConversionNotSupportedException.Create(typeof(Type), source);
		}
	}
}
