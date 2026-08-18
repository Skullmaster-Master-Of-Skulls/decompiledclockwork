using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000753 RID: 1875
	internal class StringToObjectConverter : TypeConverter
	{
		// Token: 0x0600425C RID: 16988 RVA: 0x000D03AB File Offset: 0x000CE5AB
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000D03CC File Offset: 0x000CE5CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				return new ObjectWrapper(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000D03F3 File Offset: 0x000CE5F3
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000D0414 File Offset: 0x000CE614
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			ObjectWrapper objectWrapper = value as ObjectWrapper;
			if (destinationType == typeof(InstanceDescriptor) && objectWrapper != null)
			{
				ConstructorInfo constructor = typeof(ObjectWrapper).GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						objectWrapper.Value.ToString()
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
