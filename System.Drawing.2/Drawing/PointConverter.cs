using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x0200002B RID: 43
	public class PointConverter : TypeConverter
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001525C File Offset: 0x0001345C
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
			if (array2.Length == 2)
			{
				return new Point(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				text2,
				"x, y"
			}));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015340 File Offset: 0x00013540
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Point)
			{
				if (destinationType == typeof(string))
				{
					Point point = (Point)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, point.X);
					array[num++] = converter.ConvertToString(context, culture, point.Y);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Point point2 = (Point)value;
					ConstructorInfo constructor = typeof(Point).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							point2.X,
							point2.Y
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00015494 File Offset: 0x00013694
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			object obj = propertyValues["X"];
			object obj2 = propertyValues["Y"];
			if (obj == null || obj2 == null || !(obj is int) || !(obj2 is int))
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
			}
			return new Point((int)obj, (int)obj2);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015504 File Offset: 0x00013704
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Point), attributes);
			return properties.Sort(new string[]
			{
				"X",
				"Y"
			});
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
