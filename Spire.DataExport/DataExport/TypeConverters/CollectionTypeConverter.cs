using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Spire.DataExport.TypeConverters
{
	// Token: 0x020001A1 RID: 417
	public class CollectionTypeConverter : ExpandableObjectConverter
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x0007921C File Offset: 0x0007821C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			while (destinationType != typeof(InstanceDescriptor))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return base.CanConvertTo(context, destinationType);
				}
			}
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00079270 File Offset: 0x00078270
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			while (destinationType != typeof(InstanceDescriptor))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return base.ConvertTo(context, culture, value, destinationType);
				}
			}
			if (true)
			{
			}
			return new InstanceDescriptor(value.GetType().GetConstructor(Type.EmptyTypes), null, false);
		}
	}
}
