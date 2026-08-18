using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x0200002D RID: 45
	public class RectangleConverter : TypeConverter
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015C1C File Offset: 0x00013E1C
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
			int[] array2 = new int[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (int)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 4)
			{
				return new Rectangle(array2[0], array2[1], array2[2], array2[3]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				"text",
				text2,
				"x, y, width, height"
			}));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015D10 File Offset: 0x00013F10
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Rectangle)
			{
				if (destinationType == typeof(string))
				{
					Rectangle rectangle = (Rectangle)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[4];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, rectangle.X);
					array[num++] = converter.ConvertToString(context, culture, rectangle.Y);
					array[num++] = converter.ConvertToString(context, culture, rectangle.Width);
					array[num++] = converter.ConvertToString(context, culture, rectangle.Height);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Rectangle rectangle2 = (Rectangle)value;
					ConstructorInfo constructor = typeof(Rectangle).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							rectangle2.X,
							rectangle2.Y,
							rectangle2.Width,
							rectangle2.Height
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015ED8 File Offset: 0x000140D8
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			object obj = propertyValues["X"];
			object obj2 = propertyValues["Y"];
			object obj3 = propertyValues["Width"];
			object obj4 = propertyValues["Height"];
			if (obj == null || obj2 == null || obj3 == null || obj4 == null || !(obj is int) || !(obj2 is int) || !(obj3 is int) || !(obj4 is int))
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
			}
			return new Rectangle((int)obj, (int)obj2, (int)obj3, (int)obj4);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015F84 File Offset: 0x00014184
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Rectangle), attributes);
			return properties.Sort(new string[]
			{
				"X",
				"Y",
				"Width",
				"Height"
			});
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
