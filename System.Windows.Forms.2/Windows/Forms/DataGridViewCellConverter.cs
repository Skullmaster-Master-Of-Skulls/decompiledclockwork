using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001A8 RID: 424
	internal class DataGridViewCellConverter : ExpandableObjectConverter
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0008F03C File Offset: 0x0008D23C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewCell dataGridViewCell = value as DataGridViewCell;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewCell != null)
			{
				ConstructorInfo constructor = dataGridViewCell.GetType().GetConstructor(new Type[0]);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
