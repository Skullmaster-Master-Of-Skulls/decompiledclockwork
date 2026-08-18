using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Text;

namespace Telerik.Web
{
	// Token: 0x0200086F RID: 2159
	internal class EncodingConverter : TypeConverter
	{
		// Token: 0x06004FAD RID: 20397 RVA: 0x000F9C13 File Offset: 0x000F7E13
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x000F9C25 File Offset: 0x000F7E25
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType;
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x000F9C38 File Offset: 0x000F7E38
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				Encoding encoding = Encoding.GetEncoding(text);
				if (text != null)
				{
					return encoding;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x000F9C6C File Offset: 0x000F7E6C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Encoding encoding = value as Encoding;
			if (typeof(string) == destinationType && encoding != null)
			{
				return encoding.HeaderName;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
