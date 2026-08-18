using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000096 RID: 150
	public sealed class TimeSpanMinutesOrInfiniteConverter : TimeSpanMinutesConverter
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001CCBA File Offset: 0x0001AEBA
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(TimeSpan));
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(ctx, ci, value, type);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
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
