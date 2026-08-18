using System;
using System.Text;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000E8 RID: 232
	internal class EncodingConverter : IConvertFrom
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x000152FC File Offset: 0x000134FC
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00015310 File Offset: 0x00013510
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return Encoding.GetEncoding(text);
			}
			throw ConversionNotSupportedException.Create(typeof(Encoding), source);
		}
	}
}
