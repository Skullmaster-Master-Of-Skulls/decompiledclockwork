using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001B6 RID: 438
	public class DataGridViewCellStyleConverter : TypeConverter
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00090A78 File Offset: 0x0008EC78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is DataGridViewCellStyle)
			{
				ConstructorInfo constructor = value.GetType().GetConstructor(new Type[0]);
				return new InstanceDescriptor(constructor, new object[0], false);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
