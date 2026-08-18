using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017FD RID: 6141
	public class PaddingsConverter : TypeConverter
	{
		// Token: 0x0600EEC1 RID: 61121 RVA: 0x00365A4E File Offset: 0x00363C4E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EEC2 RID: 61122 RVA: 0x00365A6C File Offset: 0x00363C6C
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
				return new ChartPaddings(array2[0]);
			}
			if (array2.Length != 4)
			{
				throw new ArgumentException("Input value is invalid");
			}
			return new ChartPaddings(array2[0], array2[1], array2[2], array2[3]);
		}

		// Token: 0x0600EEC3 RID: 61123 RVA: 0x00365B50 File Offset: 0x00363D50
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			ChartPaddings chartPaddings = value as ChartPaddings;
			if (chartPaddings != null && destinationType == typeof(string))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Unit));
				return string.Join(separator, new string[]
				{
					converter.ConvertToString(context, culture, chartPaddings.Top),
					converter.ConvertToString(context, culture, chartPaddings.Right),
					converter.ConvertToString(context, culture, chartPaddings.Bottom),
					converter.ConvertToString(context, culture, chartPaddings.Left)
				});
			}
			object result;
			try
			{
				result = base.ConvertTo(context, culture, value, destinationType);
			}
			catch
			{
				result = new ChartPaddings();
			}
			return result;
		}

		// Token: 0x0600EEC4 RID: 61124 RVA: 0x00365C4C File Offset: 0x00363E4C
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
			return new ChartPaddings(unit, unit2, unit3, left);
		}

		// Token: 0x0600EEC5 RID: 61125 RVA: 0x00365CE3 File Offset: 0x00363EE3
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600EEC6 RID: 61126 RVA: 0x00365CE6 File Offset: 0x00363EE6
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600EEC7 RID: 61127 RVA: 0x00365CEC File Offset: 0x00363EEC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ChartPaddings), attributes);
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
