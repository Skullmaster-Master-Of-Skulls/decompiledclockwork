using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005AC RID: 1452
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000EC7F5 File Offset: 0x000EA9F5
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000EC7F8 File Offset: 0x000EA9F8
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000EC804 File Offset: 0x000EAA04
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000EC816 File Offset: 0x000EAA16
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000EC829 File Offset: 0x000EAA29
		internal override object FromString(string value, CultureInfo culture)
		{
			return float.Parse(value, culture);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000EC838 File Offset: 0x000EAA38
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
