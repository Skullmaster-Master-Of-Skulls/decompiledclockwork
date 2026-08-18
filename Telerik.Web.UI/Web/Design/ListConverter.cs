using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.Design
{
	// Token: 0x0200102A RID: 4138
	internal class ListConverter : CollectionConverter
	{
		// Token: 0x0600A327 RID: 41767 RVA: 0x00244FF8 File Offset: 0x002431F8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600A328 RID: 41768 RVA: 0x00245018 File Offset: 0x00243218
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

		// Token: 0x0600A329 RID: 41769 RVA: 0x0024504C File Offset: 0x0024324C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600A32A RID: 41770 RVA: 0x0024506C File Offset: 0x0024326C
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
