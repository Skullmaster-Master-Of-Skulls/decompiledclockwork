using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.Design
{
	// Token: 0x0200102B RID: 4139
	internal class PathsConverter : TypeConverter
	{
		// Token: 0x0600A32C RID: 41772 RVA: 0x0024510E File Offset: 0x0024330E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600A32D RID: 41773 RVA: 0x0024512C File Offset: 0x0024332C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				return new List<string>(text.Split(new char[]
				{
					','
				}));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600A32E RID: 41774 RVA: 0x00245165 File Offset: 0x00243365
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(List<string>) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600A32F RID: 41775 RVA: 0x00245198 File Offset: 0x00243398
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			List<string> list = value as List<string>;
			if (value != null && list == null)
			{
				throw new ArgumentException("Invalid Paths.", "value");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return string.Empty;
				}
				return string.Join(",", list.ToArray());
			}
			else
			{
				if (!(destinationType == typeof(InstanceDescriptor)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value == null)
				{
					return null;
				}
				object[] array = null;
				List<string> list2 = list;
				MemberInfo constructor;
				if (list2.Count == 0)
				{
					constructor = typeof(List<string>).GetConstructor(new Type[0]);
				}
				else
				{
					constructor = typeof(List<string>).GetConstructor(new Type[]
					{
						typeof(IEnumerable<string>)
					});
					array = list2.ToArray();
				}
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						array
					});
				}
				return null;
			}
		}
	}
}
