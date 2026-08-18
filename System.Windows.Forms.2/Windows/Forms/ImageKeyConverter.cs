using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000293 RID: 659
	public class ImageKeyConverter : StringConverter
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x00013062 File Offset: 0x00011262
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000BDFF3 File Offset: 0x000BC1F3
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000BDFFB File Offset: 0x000BC1FB
		internal string ParentImageListProperty
		{
			get
			{
				return this.parentImageListProperty;
			}
			set
			{
				this.parentImageListProperty = value;
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000BE004 File Offset: 0x000BC204
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000BE022 File Offset: 0x000BC222
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (value == null)
			{
				return "";
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000BE048 File Offset: 0x000BC248
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && value is string && ((string)value).Length == 0)
			{
				return SR.GetString("toStringNone");
			}
			if (destinationType == typeof(string) && value == null)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000BE0CC File Offset: 0x000BC2CC
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
						PropertyDescriptor propertyDescriptor3 = properties[this.ParentImageListProperty];
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
						int count = imageList.Images.Count;
						object[] array;
						if (this.IncludeNoneAsStandardValue)
						{
							array = new object[count + 1];
							array[count] = "";
						}
						else
						{
							array = new object[count];
						}
						StringCollection keys = imageList.Images.Keys;
						for (int i = 0; i < keys.Count; i++)
						{
							if (keys[i] != null && keys[i].Length != 0)
							{
								array[i] = keys[i];
							}
						}
						return new TypeConverter.StandardValuesCollection(array);
					}
				}
			}
			if (this.IncludeNoneAsStandardValue)
			{
				return new TypeConverter.StandardValuesCollection(new object[]
				{
					""
				});
			}
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040010F1 RID: 4337
		private string parentImageListProperty = "Parent";
	}
}
