using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001767 RID: 5991
	public class CornersConverter : TypeConverter
	{
		// Token: 0x0600E9CE RID: 59854 RVA: 0x00352817 File Offset: 0x00350A17
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600E9CF RID: 59855 RVA: 0x00352838 File Offset: 0x00350A38
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
			if (array.Length != 5)
			{
				throw new ArgumentException("String cannot be converted to Corners");
			}
			CornerType[] array2 = new CornerType[array.Length - 1];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(CornerType));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (CornerType)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length != 4)
			{
				throw new ArgumentException("Failed convert to Corners");
			}
			int roundSize = int.Parse(array[4]);
			return new Corners(array2[0], array2[1], array2[2], array2[3], roundSize);
		}

		// Token: 0x0600E9D0 RID: 59856 RVA: 0x00352928 File Offset: 0x00350B28
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Corners corners = value as Corners;
			if (corners != null && destinationType == typeof(string))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(CornerType));
				return string.Join(separator, new string[]
				{
					converter.ConvertToString(context, culture, corners.TopLeft),
					converter.ConvertToString(context, culture, corners.TopRight),
					converter.ConvertToString(context, culture, corners.BottomLeft),
					converter.ConvertToString(context, culture, corners.BottomRight),
					corners.RoundSize.ToString()
				});
			}
			object result;
			try
			{
				result = base.ConvertTo(context, culture, value, destinationType);
			}
			catch
			{
				result = new Corners();
			}
			return result;
		}

		// Token: 0x0600E9D1 RID: 59857 RVA: 0x00352A44 File Offset: 0x00350C44
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600E9D2 RID: 59858 RVA: 0x00352A48 File Offset: 0x00350C48
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Corners), attributes);
			string[] names = new string[]
			{
				"TopLeft",
				"TopRight",
				"BottomLeft",
				"BottomRight",
				"RoundSize"
			};
			return properties.Sort(names);
		}

		// Token: 0x0600E9D3 RID: 59859 RVA: 0x00352A9E File Offset: 0x00350C9E
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600E9D4 RID: 59860 RVA: 0x00352AA4 File Offset: 0x00350CA4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			Corners corners = new Corners();
			if (context.Instance is StyleSeriesItemLabel)
			{
				corners.cornersContainerObject = ((StyleSeriesItemLabel)context.Instance).styleContainerObject;
			}
			if (context.Instance is StyleSeries)
			{
				corners.cornersContainerObject = ((StyleSeries)context.Instance).styleSeriesParent;
			}
			corners.TopLeft = (CornerType)propertyValues["TopLeft"];
			corners.TopRight = (CornerType)propertyValues["TopRight"];
			corners.BottomLeft = (CornerType)propertyValues["BottomLeft"];
			corners.BottomRight = (CornerType)propertyValues["BottomRight"];
			corners.RoundSize = (int)propertyValues["RoundSize"];
			return corners;
		}
	}
}
