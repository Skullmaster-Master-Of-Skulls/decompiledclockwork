using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200020C RID: 524
	internal class DataGridViewRowConverter : ExpandableObjectConverter
	{
		// Token: 0x06002278 RID: 8824 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000A50CC File Offset: 0x000A32CC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewRow dataGridViewRow = value as DataGridViewRow;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewRow != null)
			{
				ConstructorInfo constructor = dataGridViewRow.GetType().GetConstructor(new Type[0]);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
