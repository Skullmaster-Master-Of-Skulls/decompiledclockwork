using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017FB RID: 6139
	internal class DoubleConverter2 : TypeConverter
	{
		// Token: 0x0600EEB5 RID: 61109 RVA: 0x00365642 File Offset: 0x00363842
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EEB6 RID: 61110 RVA: 0x00365660 File Offset: 0x00363860
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				double num;
				try
				{
					num = double.Parse(text, culture);
				}
				catch
				{
					num = double.NaN;
				}
				return num;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600EEB7 RID: 61111 RVA: 0x003656B0 File Offset: 0x003638B0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (context == null)
			{
				if (!(value is double))
				{
					return string.Empty;
				}
				double d = (double)value;
				if (double.IsNaN(d))
				{
					return string.Empty;
				}
				return d.ToString(culture);
			}
			else
			{
				if (context.Instance == null)
				{
					return "5,4,3,2";
				}
				double num = (double)value;
				if (num.Equals(double.NaN))
				{
					return string.Empty;
				}
				return num.ToString();
			}
		}
	}
}
