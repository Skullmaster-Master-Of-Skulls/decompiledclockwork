using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000256 RID: 598
	internal class FlatButtonAppearanceConverter : ExpandableObjectConverter
	{
		// Token: 0x060025BA RID: 9658 RVA: 0x000AFB70 File Offset: 0x000ADD70
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return "";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000AFB98 File Offset: 0x000ADD98
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null && context.Instance is Button)
			{
				Attribute[] array = new Attribute[attributes.Length + 1];
				attributes.CopyTo(array, 0);
				array[attributes.Length] = new ApplicableToButtonAttribute();
				attributes = array;
			}
			return TypeDescriptor.GetProperties(value, attributes);
		}
	}
}
