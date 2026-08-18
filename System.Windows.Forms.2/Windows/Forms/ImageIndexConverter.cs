using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000292 RID: 658
	public class ImageIndexConverter : Int32Converter
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x00013062 File Offset: 0x00011262
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000BDDD7 File Offset: 0x000BBFD7
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x000BDDDF File Offset: 0x000BBFDF
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

		// Token: 0x060029BD RID: 10685 RVA: 0x000BDDE8 File Offset: 0x000BBFE8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null && string.Compare(text, SR.GetString("toStringNone"), true, culture) == 0)
			{
				return -1;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000BDE24 File Offset: 0x000BC024
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is int && (int)value == -1)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000BDE80 File Offset: 0x000BC080
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
							array[count] = -1;
						}
						else
						{
							array = new object[count];
						}
						for (int i = 0; i < count; i++)
						{
							array[i] = i;
						}
						return new TypeConverter.StandardValuesCollection(array);
					}
				}
			}
			if (this.IncludeNoneAsStandardValue)
			{
				return new TypeConverter.StandardValuesCollection(new object[]
				{
					-1
				});
			}
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040010F0 RID: 4336
		private string parentImageListProperty = "Parent";
	}
}
