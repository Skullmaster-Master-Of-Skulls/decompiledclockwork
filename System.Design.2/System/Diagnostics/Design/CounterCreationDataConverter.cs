using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Diagnostics.Design
{
	// Token: 0x02000208 RID: 520
	internal class CounterCreationDataConverter : ExpandableObjectConverter
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x0006F520 File Offset: 0x0006D720
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006F540 File Offset: 0x0006D740
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is CounterCreationData)
			{
				CounterCreationData counterCreationData = (CounterCreationData)value;
				ConstructorInfo constructor = typeof(CounterCreationData).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(string),
					typeof(PerformanceCounterType)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						counterCreationData.CounterName,
						counterCreationData.CounterHelp,
						counterCreationData.CounterType
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
