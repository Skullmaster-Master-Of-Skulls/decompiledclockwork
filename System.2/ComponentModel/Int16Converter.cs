using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200056E RID: 1390
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000E41E4 File Offset: 0x000E23E4
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000E41F0 File Offset: 0x000E23F0
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000E41FE File Offset: 0x000E23FE
		internal override object FromString(string value, CultureInfo culture)
		{
			return short.Parse(value, culture);
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000E420C File Offset: 0x000E240C
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000E421C File Offset: 0x000E241C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
