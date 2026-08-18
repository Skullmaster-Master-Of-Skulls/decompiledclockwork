using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017FA RID: 6138
	internal class ListConverter : CollectionConverter
	{
		// Token: 0x0600EEB0 RID: 61104 RVA: 0x0036552C File Offset: 0x0036372C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EEB1 RID: 61105 RVA: 0x0036554C File Offset: 0x0036374C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object sourceObj)
		{
			string text = sourceObj as string;
			if (text != null)
			{
				return text.Split(new char[]
				{
					','
				});
			}
			return base.ConvertFrom(context, culture, sourceObj);
		}

		// Token: 0x0600EEB2 RID: 61106 RVA: 0x00365580 File Offset: 0x00363780
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600EEB3 RID: 61107 RVA: 0x003655A0 File Offset: 0x003637A0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object destinationObj, Type destinationType)
		{
			string[] array = destinationObj as string[];
			if (array != null)
			{
				string text = string.Join(",", array);
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(string).GetConstructor(new Type[]
					{
						typeof(string)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							text
						});
					}
				}
				else if (destinationType == typeof(string))
				{
					return text;
				}
			}
			return base.ConvertTo(context, culture, destinationObj, destinationType);
		}
	}
}
