using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x0200076D RID: 1901
	internal sealed class VersionConverter : ConfigurationConverterBase
	{
		// Token: 0x06005B9D RID: 23453 RVA: 0x0013D8B8 File Offset: 0x0013BAB8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return new Version((string)value);
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x0013D8C8 File Offset: 0x0013BAC8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Version version = (Version)value;
			return version.ToString();
		}
	}
}
