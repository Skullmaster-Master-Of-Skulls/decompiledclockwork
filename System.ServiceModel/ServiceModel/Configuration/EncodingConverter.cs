using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000619 RID: 1561
	internal class EncodingConverter : TypeConverter
	{
		// Token: 0x06003C05 RID: 15365 RVA: 0x000E56D1 File Offset: 0x000E38D1
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000E56EF File Offset: 0x000E38EF
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000E5710 File Offset: 0x000E3910
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			Encoding encoding;
			if (string.Compare(text, "utf-8", StringComparison.OrdinalIgnoreCase) == 0)
			{
				encoding = TextEncoderDefaults.Encoding;
			}
			else
			{
				encoding = Encoding.GetEncoding(text);
			}
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ConfigInvalidEncodingValue", new object[]
				{
					text
				}));
			}
			return encoding;
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x000E577C File Offset: 0x000E397C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is Encoding)
			{
				Encoding encoding = (Encoding)value;
				return encoding.HeaderName;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
