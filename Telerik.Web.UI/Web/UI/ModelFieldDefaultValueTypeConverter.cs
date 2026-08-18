using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000101 RID: 257
	internal class ModelFieldDefaultValueTypeConverter : TypeConverter
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00026AE0 File Offset: 0x00024CE0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00026B00 File Offset: 0x00024D00
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			ModelFieldDefaultValueWrapper modelFieldDefaultValueWrapper = value as ModelFieldDefaultValueWrapper;
			if (destinationType == typeof(InstanceDescriptor) && modelFieldDefaultValueWrapper != null)
			{
				ConstructorInfo constructor = typeof(ModelFieldDefaultValueWrapper).GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						modelFieldDefaultValueWrapper.DefaultValue
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00026B7B File Offset: 0x00024D7B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00026B9C File Offset: 0x00024D9C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				return new ModelFieldDefaultValueWrapper(text);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
