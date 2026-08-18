using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x02000031 RID: 49
	public class SizeConverter : TypeConverter
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016F74 File Offset: 0x00015174
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
				return new Size(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				text2,
				"Width,Height"
			}));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017058 File Offset: 0x00015258
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Size)
			{
				if (destinationType == typeof(string))
				{
					Size size = (Size)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, size.Width);
					array[num++] = converter.ConvertToString(context, culture, size.Height);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Size size2 = (Size)value;
					ConstructorInfo constructor = typeof(Size).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							size2.Width,
							size2.Height
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000171AC File Offset: 0x000153AC
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			object obj = propertyValues["Width"];
			object obj2 = propertyValues["Height"];
			if (obj == null || obj2 == null || !(obj is int) || !(obj2 is int))
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"));
			}
			return new Size((int)obj, (int)obj2);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001721C File Offset: 0x0001541C
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Size), attributes);
			return properties.Sort(new string[]
			{
				"Width",
				"Height"
			});
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
