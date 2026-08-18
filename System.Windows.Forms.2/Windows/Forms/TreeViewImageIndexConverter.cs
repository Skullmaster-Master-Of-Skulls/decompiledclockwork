using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000421 RID: 1057
	public class TreeViewImageIndexConverter : ImageIndexConverter
	{
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override bool IncludeNoneAsStandardValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x00137038 File Offset: 0x00135238
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				if (string.Compare(text, SR.GetString("toStringDefault"), true, culture) == 0)
				{
					return -1;
				}
				if (string.Compare(text, SR.GetString("toStringNone"), true, culture) == 0)
				{
					return -2;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x00137090 File Offset: 0x00135290
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is int)
			{
				int num = (int)value;
				if (num == -1)
				{
					return SR.GetString("toStringDefault");
				}
				if (num == -2)
				{
					return SR.GetString("toStringNone");
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x00137100 File Offset: 0x00135300
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				object obj = context.Instance;
				PropertyDescriptor propertyDescriptor = ImageListUtils.GetImageListProperty(context.PropertyDescriptor, ref obj);
				while (obj != null && propertyDescriptor == null)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
					foreach (object obj2 in properties)
					{
						PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj2;
						if (typeof(ImageList).IsAssignableFrom(propertyDescriptor2.PropertyType))
						{
							propertyDescriptor = propertyDescriptor2;
							break;
						}
					}
					if (propertyDescriptor == null)
					{
						PropertyDescriptor propertyDescriptor3 = properties[base.ParentImageListProperty];
						if (propertyDescriptor3 != null)
						{
							obj = propertyDescriptor3.GetValue(obj);
						}
						else
						{
							obj = null;
						}
					}
				}
				if (propertyDescriptor != null)
				{
					ImageList imageList = (ImageList)propertyDescriptor.GetValue(obj);
					if (imageList != null)
					{
						int num = imageList.Images.Count + 2;
						object[] array = new object[num];
						array[num - 2] = -1;
						array[num - 1] = -2;
						for (int i = 0; i < num - 2; i++)
						{
							array[i] = i;
						}
						return new TypeConverter.StandardValuesCollection(array);
					}
				}
			}
			return new TypeConverter.StandardValuesCollection(new object[]
			{
				-1,
				-2
			});
		}
	}
}
