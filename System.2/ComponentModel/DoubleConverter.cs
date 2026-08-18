using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000548 RID: 1352
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000E2CE2 File Offset: 0x000E0EE2
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000E2CE5 File Offset: 0x000E0EE5
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000E2CF1 File Offset: 0x000E0EF1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000E2D03 File Offset: 0x000E0F03
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000E2D16 File Offset: 0x000E0F16
		internal override object FromString(string value, CultureInfo culture)
		{
			return double.Parse(value, culture);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000E2D24 File Offset: 0x000E0F24
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
