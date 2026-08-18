using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005AA RID: 1450
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000EC730 File Offset: 0x000EA930
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000EC73C File Offset: 0x000EA93C
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000EC74A File Offset: 0x000EA94A
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000EC759 File Offset: 0x000EA959
		internal override object FromString(string value, CultureInfo culture)
		{
			return sbyte.Parse(value, culture);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000EC768 File Offset: 0x000EA968
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
