using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004DC RID: 1244
	public class StringArrayConverter : TypeConverter
	{
		// Token: 0x06003E02 RID: 15874 RVA: 0x0009FC5A File Offset: 0x0009DE5A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000C7D10 File Offset: 0x000C5F10
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
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			return array;
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000C7D72 File Offset: 0x000C5F72
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
			return string.Join(",", (string[])value);
		}
	}
}
