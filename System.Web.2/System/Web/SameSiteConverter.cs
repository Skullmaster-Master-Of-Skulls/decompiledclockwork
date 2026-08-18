using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web
{
	// Token: 0x02000094 RID: 148
	internal class SameSiteConverter : EnumConverter
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001629F File Offset: 0x0001449F
		public SameSiteConverter() : base(typeof(SameSiteMode))
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000162B4 File Offset: 0x000144B4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null && text.Equals("Unspecified", StringComparison.InvariantCultureIgnoreCase))
			{
				return (SameSiteMode)(-1);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000162EC File Offset: 0x000144EC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is SameSiteMode && destinationType == typeof(string))
			{
				int num = (int)value;
				if (num < 0)
				{
					return "Unspecified";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
