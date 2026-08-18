using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000098 RID: 152
	public sealed class TimeSpanSecondsOrInfiniteConverter : TimeSpanSecondsConverter
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(TimeSpan));
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(ctx, ci, value, type);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001CDE2 File Offset: 0x0001AFE2
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return TimeSpan.MaxValue;
			}
			return base.ConvertFrom(ctx, ci, data);
		}
	}
}
