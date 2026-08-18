using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C2 RID: 1730
	internal class ReliableMessagingVersionConverter : TypeConverter
	{
		// Token: 0x06004319 RID: 17177 RVA: 0x000FD55D File Offset: 0x000FB75D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000FD57B File Offset: 0x000FB77B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000FD59C File Offset: 0x000FB79C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text == "Default")
			{
				return ReliableMessagingVersion.Default;
			}
			if (text == "WSReliableMessaging11")
			{
				return ReliableMessagingVersion.WSReliableMessaging11;
			}
			if (!(text == "WSReliableMessagingFebruary2005"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidReliableMessagingVersionValue", new object[]
				{
					text
				}));
			}
			return ReliableMessagingVersion.WSReliableMessagingFebruary2005;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000FD618 File Offset: 0x000FB818
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(typeof(string) == destinationType) || !(value is ReliableMessagingVersion))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			ReliableMessagingVersion reliableMessagingVersion = (ReliableMessagingVersion)value;
			if (reliableMessagingVersion == ReliableMessagingVersion.Default)
			{
				return "Default";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "WSReliableMessaging11";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "WSReliableMessagingFebruary2005";
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassInstanceValue", new object[]
			{
				typeof(ReliableMessagingVersion).FullName
			})));
		}
	}
}
