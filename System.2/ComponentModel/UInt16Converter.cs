using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005B8 RID: 1464
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000EFA5B File Offset: 0x000EDC5B
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000EFA67 File Offset: 0x000EDC67
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000EFA75 File Offset: 0x000EDC75
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000EFA84 File Offset: 0x000EDC84
		internal override object FromString(string value, CultureInfo culture)
		{
			return ushort.Parse(value, culture);
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000EFA94 File Offset: 0x000EDC94
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
