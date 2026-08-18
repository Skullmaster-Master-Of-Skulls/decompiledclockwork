using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x020000A2 RID: 162
	public sealed class WhiteSpaceTrimStringConverter : ConfigurationConverterBase
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0001DB41 File Offset: 0x0001BD41
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(string));
			if (value == null)
			{
				return string.Empty;
			}
			return ((string)value).Trim();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).Trim();
		}
	}
}
