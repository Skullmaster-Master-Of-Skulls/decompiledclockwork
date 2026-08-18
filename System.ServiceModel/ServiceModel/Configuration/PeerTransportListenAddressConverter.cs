using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066C RID: 1644
	internal class PeerTransportListenAddressConverter : TypeConverter
	{
		// Token: 0x06003F2E RID: 16174 RVA: 0x000F0083 File Offset: 0x000EE283
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000F00A1 File Offset: 0x000EE2A1
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(IPAddress) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000F00BF File Offset: 0x000EE2BF
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return IPAddress.Parse(value as string);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000F00DE File Offset: 0x000EE2DE
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is IPAddress)
			{
				return ((IPAddress)value).ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
