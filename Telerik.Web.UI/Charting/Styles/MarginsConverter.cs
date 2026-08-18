using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017FC RID: 6140
	public class MarginsConverter : TypeConverter
	{
		// Token: 0x0600EEB9 RID: 61113 RVA: 0x00365747 File Offset: 0x00363947
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EEBA RID: 61114 RVA: 0x00365768 File Offset: 0x00363968
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text2 = text.Trim();
			if (text2.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			char c = culture.TextInfo.ListSeparator[0];
			string[] array = text2.Split(new char[]
			{
				c
			});
			Unit[] array2 = new Unit[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Unit));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (Unit)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 1)
			{
				return new ChartMargins(array2[0]);
			}
			if (array2.Length != 4)
			{
				throw new ArgumentException("Input value is invalid");
			}
			return new ChartMargins(array2[0], array2[1], array2[2], array2[3]);
		}

		// Token: 0x0600EEBB RID: 61115 RVA: 0x0036584C File Offset: 0x00363A4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			ChartMargins chartMargins = value as ChartMargins;
			if (chartMargins != null && destinationType == typeof(string))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Unit));
				return string.Join(separator, new string[]
				{
					converter.ConvertToString(context, culture, chartMargins.Top),
					converter.ConvertToString(context, culture, chartMargins.Right),
					converter.ConvertToString(context, culture, chartMargins.Bottom),
					converter.ConvertToString(context, culture, chartMargins.Left)
				});
			}
			object result;
			try
			{
				result = base.ConvertTo(context, culture, value, destinationType);
			}
			catch
			{
				result = new ChartMargins();
			}
			return result;
		}

		// Token: 0x0600EEBC RID: 61116 RVA: 0x00365948 File Offset: 0x00363B48
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			Unit unit = propertyValues["Top"] as Unit;
			Unit unit2 = propertyValues["Right"] as Unit;
			Unit unit3 = propertyValues["Bottom"] as Unit;
			Unit left = propertyValues["Left"] as Unit;
			if (unit == null || unit2 == null || left == null || unit3 == null)
			{
				throw new ArgumentException("Invalid value");
			}
			return new ChartMargins(((Dimensions)context.Instance).containerObject, unit, unit2, unit3, left);
		}

		// Token: 0x0600EEBD RID: 61117 RVA: 0x003659EF File Offset: 0x00363BEF
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600EEBE RID: 61118 RVA: 0x003659F2 File Offset: 0x00363BF2
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600EEBF RID: 61119 RVA: 0x003659F8 File Offset: 0x00363BF8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ChartMargins), attributes);
			string[] names = new string[]
			{
				"Top",
				"Right",
				"Bottom",
				"Left"
			};
			return properties.Sort(names);
		}
	}
}
