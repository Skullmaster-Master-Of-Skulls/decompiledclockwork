using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200056F RID: 1391
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000E4245 File Offset: 0x000E2445
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000E4251 File Offset: 0x000E2451
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000E425F File Offset: 0x000E245F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000E426E File Offset: 0x000E246E
		internal override object FromString(string value, CultureInfo culture)
		{
			return int.Parse(value, culture);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000E427C File Offset: 0x000E247C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
