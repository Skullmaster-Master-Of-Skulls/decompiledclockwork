using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005B9 RID: 1465
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000EFABD File Offset: 0x000EDCBD
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000EFAC9 File Offset: 0x000EDCC9
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000EFAD7 File Offset: 0x000EDCD7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000EFAE6 File Offset: 0x000EDCE6
		internal override object FromString(string value, CultureInfo culture)
		{
			return uint.Parse(value, culture);
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000EFAF4 File Offset: 0x000EDCF4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
