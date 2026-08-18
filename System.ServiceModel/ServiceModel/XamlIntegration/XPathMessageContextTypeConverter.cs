using System;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel.Dispatcher;
using System.Windows.Markup;

namespace System.ServiceModel.XamlIntegration
{
	// Token: 0x0200045E RID: 1118
	public class XPathMessageContextTypeConverter : TypeConverter
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x000A96EC File Offset: 0x000A78EC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(MarkupExtension) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000A970A File Offset: 0x000A790A
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(MarkupExtension) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000A9728 File Offset: 0x000A7928
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is XPathMessageContextMarkupExtension)
			{
				return ((MarkupExtension)value).ProvideValue(null);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000A9748 File Offset: 0x000A7948
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			XPathMessageContext xpathMessageContext = value as XPathMessageContext;
			if (xpathMessageContext != null && typeof(MarkupExtension) == destinationType)
			{
				return new XPathMessageContextMarkupExtension(xpathMessageContext);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
