using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000095 RID: 149
	public class TimeSpanMinutesConverter : ConfigurationConverterBase
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0001CC54 File Offset: 0x0001AE54
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(TimeSpan));
			return ((long)((TimeSpan)value).TotalMinutes).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001CC90 File Offset: 0x0001AE90
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			long num = long.Parse((string)data, CultureInfo.InvariantCulture);
			return TimeSpan.FromMinutes((double)num);
		}
	}
}
