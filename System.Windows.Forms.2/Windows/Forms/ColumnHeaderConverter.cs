using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000158 RID: 344
	public class ColumnHeaderConverter : ExpandableObjectConverter
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is ColumnHeader))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			ColumnHeader columnHeader = (ColumnHeader)value;
			Type reflectionType = TypeDescriptor.GetReflectionType(value);
			InstanceDescriptor instanceDescriptor = null;
			ConstructorInfo constructor;
			if (columnHeader.ImageIndex != -1)
			{
				constructor = reflectionType.GetConstructor(new Type[]
				{
					typeof(int)
				});
				if (constructor != null)
				{
					instanceDescriptor = new InstanceDescriptor(constructor, new object[]
					{
						columnHeader.ImageIndex
					}, false);
				}
			}
			if (instanceDescriptor == null && !string.IsNullOrEmpty(columnHeader.ImageKey))
			{
				constructor = reflectionType.GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (constructor != null)
				{
					instanceDescriptor = new InstanceDescriptor(constructor, new object[]
					{
						columnHeader.ImageKey
					}, false);
				}
			}
			if (instanceDescriptor != null)
			{
				return instanceDescriptor;
			}
			constructor = reflectionType.GetConstructor(new Type[0]);
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, new object[0], false);
			}
			throw new ArgumentException(SR.GetString("NoDefaultConstructor", new object[]
			{
				reflectionType.FullName
			}));
		}
	}
}
