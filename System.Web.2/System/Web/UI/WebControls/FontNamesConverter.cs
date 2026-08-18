using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F9 RID: 1017
	public class FontNamesConverter : TypeConverter
	{
		// Token: 0x0600310E RID: 12558 RVA: 0x0009FC5A File Offset: 0x0009DE5A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x0009FC74 File Offset: 0x0009DE74
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				throw base.GetConvertFromException(value);
			}
			if (((string)value).Length == 0)
			{
				return new string[0];
			}
			string[] array = ((string)value).Split(new char[]
			{
				culture.TextInfo.ListSeparator[0]
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			return array;
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0009FCE5 File Offset: 0x0009DEE5
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				throw base.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			return string.Join(culture.TextInfo.ListSeparator, (string[])value);
		}
	}
}
