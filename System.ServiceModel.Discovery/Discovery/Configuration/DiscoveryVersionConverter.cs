using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B5 RID: 181
	public class DiscoveryVersionConverter : TypeConverter
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00012E70 File Offset: 0x00011070
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00012E8E File Offset: 0x0001108E
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00012EAC File Offset: 0x000110AC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return DiscoveryVersion.FromName((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00012ECB File Offset: 0x000110CB
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is DiscoveryVersion)
			{
				return ((DiscoveryVersion)value).Name;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
