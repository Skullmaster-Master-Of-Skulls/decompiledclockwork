using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005BA RID: 1466
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000EFB1D File Offset: 0x000EDD1D
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000EFB29 File Offset: 0x000EDD29
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000EFB37 File Offset: 0x000EDD37
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000EFB46 File Offset: 0x000EDD46
		internal override object FromString(string value, CultureInfo culture)
		{
			return ulong.Parse(value, culture);
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000EFB54 File Offset: 0x000EDD54
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
