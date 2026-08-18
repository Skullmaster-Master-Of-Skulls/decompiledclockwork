using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000520 RID: 1312
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000E04D9 File Offset: 0x000DE6D9
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000E04E5 File Offset: 0x000DE6E5
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000E04F3 File Offset: 0x000DE6F3
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000E0502 File Offset: 0x000DE702
		internal override object FromString(string value, CultureInfo culture)
		{
			return byte.Parse(value, culture);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000E0510 File Offset: 0x000DE710
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
