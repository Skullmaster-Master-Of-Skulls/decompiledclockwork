using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x0200003F RID: 63
	public class ImageFormatConverter : TypeConverter
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001A824 File Offset: 0x00018A24
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string b = text.Trim();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					if (string.Equals(propertyInfo.Name, b, StringComparison.OrdinalIgnoreCase))
					{
						object[] index = null;
						return propertyInfo.GetValue(null, index);
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001A884 File Offset: 0x00018A84
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is ImageFormat)
			{
				PropertyInfo propertyInfo = null;
				PropertyInfo[] properties = this.GetProperties();
				foreach (PropertyInfo propertyInfo2 in properties)
				{
					if (propertyInfo2.GetValue(null, null).Equals(value))
					{
						propertyInfo = propertyInfo2;
						break;
					}
				}
				if (propertyInfo != null)
				{
					if (destinationType == typeof(string))
					{
						return propertyInfo.Name;
					}
					if (destinationType == typeof(InstanceDescriptor))
					{
						return new InstanceDescriptor(propertyInfo, null);
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001A92B File Offset: 0x00018B2B
		private PropertyInfo[] GetProperties()
		{
			return typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A940 File Offset: 0x00018B40
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					object[] index = null;
					arrayList.Add(propertyInfo.GetValue(null, index));
				}
				this.values = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this.values;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400056D RID: 1389
		private TypeConverter.StandardValuesCollection values;
	}
}
