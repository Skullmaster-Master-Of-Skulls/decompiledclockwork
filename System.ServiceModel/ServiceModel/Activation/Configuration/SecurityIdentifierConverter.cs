using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Security.Principal;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D6 RID: 1494
	internal class SecurityIdentifierConverter : TypeConverter
	{
		// Token: 0x06003A02 RID: 14850 RVA: 0x000DFD9B File Offset: 0x000DDF9B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000DFDB9 File Offset: 0x000DDFB9
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000DFDD7 File Offset: 0x000DDFD7
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return new SecurityIdentifier((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000DFDF8 File Offset: 0x000DDFF8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is SecurityIdentifier)
			{
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)value;
				return securityIdentifier.Value;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
