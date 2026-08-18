using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000183 RID: 387
	public class DataGridPreferredColumnWidthTypeConverter : TypeConverter
	{
		// Token: 0x060016D4 RID: 5844 RVA: 0x00051773 File Offset: 0x0004F973
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(int);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005179C File Offset: 0x0004F99C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (!(value.GetType() == typeof(int)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			int num = (int)value;
			if (num == -1)
			{
				return "AutoColumnResize (-1)";
			}
			return num.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00051808 File Offset: 0x0004FA08
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				string text = value.ToString();
				if (text.Equals("AutoColumnResize (-1)"))
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.CurrentCulture);
			}
			else
			{
				if (value.GetType() == typeof(int))
				{
					return (int)value;
				}
				throw base.GetConvertFromException(value);
			}
		}
	}
}
