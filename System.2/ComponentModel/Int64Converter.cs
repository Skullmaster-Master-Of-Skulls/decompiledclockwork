using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000570 RID: 1392
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000E42A5 File Offset: 0x000E24A5
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000E42B1 File Offset: 0x000E24B1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000E42BF File Offset: 0x000E24BF
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000E42CE File Offset: 0x000E24CE
		internal override object FromString(string value, CultureInfo culture)
		{
			return long.Parse(value, culture);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000E42DC File Offset: 0x000E24DC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
