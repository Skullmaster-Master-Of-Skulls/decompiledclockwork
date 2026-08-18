using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x02000043 RID: 67
	public class SizeFConverter : TypeConverter
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001B418 File Offset: 0x00019618
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
			float[] array2 = new float[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (float)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 2)
			{
				return new SizeF(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				text2,
				"Width,Height"
			}));
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001B4FC File Offset: 0x000196FC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is SizeF)
			{
				SizeF sizeF = (SizeF)value;
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
				string[] array = new string[2];
				int num = 0;
				array[num++] = converter.ConvertToString(context, culture, sizeF.Width);
				array[num++] = converter.ConvertToString(context, culture, sizeF.Height);
				return string.Join(separator, array);
			}
			if (destinationType == typeof(InstanceDescriptor) && value is SizeF)
			{
				SizeF sizeF2 = (SizeF)value;
				ConstructorInfo constructor = typeof(SizeF).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						sizeF2.Width,
						sizeF2.Height
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001B657 File Offset: 0x00019857
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new SizeF((float)propertyValues["Width"], (float)propertyValues["Height"]);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001B684 File Offset: 0x00019884
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(SizeF), attributes);
			return properties.Sort(new string[]
			{
				"Width",
				"Height"
			});
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
