using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001C1 RID: 449
	internal class DataGridViewColumnConverter : ExpandableObjectConverter
	{
		// Token: 0x06001FA4 RID: 8100 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00095A38 File Offset: 0x00093C38
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewColumn dataGridViewColumn = value as DataGridViewColumn;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewColumn != null)
			{
				ConstructorInfo constructor;
				if (dataGridViewColumn.CellType != null)
				{
					constructor = dataGridViewColumn.GetType().GetConstructor(new Type[]
					{
						typeof(Type)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							dataGridViewColumn.CellType
						}, false);
					}
				}
				constructor = dataGridViewColumn.GetType().GetConstructor(new Type[0]);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
